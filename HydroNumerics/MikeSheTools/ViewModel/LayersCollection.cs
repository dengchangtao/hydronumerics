﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using HydroNumerics.MikeSheTools.Core;
using HydroNumerics.Wells;

namespace HydroNumerics.MikeSheTools.ViewModel
{
  public class LayersCollection:INotifyPropertyChanged
  {
    private StringBuilder _logstring;
    
    public Model MShe{get;private set;}
    public ObservableCollection<IWell> WellsOutSideModelDomain { get; private set; }
    public ObservableCollection<Screen> ScreensAboveTerrain { get; private set; }
    public ObservableCollection<Screen> ScreensBelowBottom { get; private set; }
    public ObservableCollection<Screen> ScreensWithMissingDepths { get; private set; }

    public IEnumerable<IWell> Wells { get; set; }
    public ObservableCollection<Layer> Layers { get; private set; }
    
    public LayersCollection()
    {
      Wells = new ObservableCollection<IWell>();
      Layers = new ObservableCollection<Layer>();
      WellsOutSideModelDomain = new ObservableCollection<IWell>();
      ScreensAboveTerrain = new ObservableCollection<Screen>();
      ScreensBelowBottom = new ObservableCollection<Screen>();
      ScreensWithMissingDepths = new ObservableCollection<Screen>();
      _logstring = new StringBuilder();
    }

    private void Load(string FileName)
    {
      if (MShe != null)
        MShe.Dispose();

      Layers.Clear();
      WellsOutSideModelDomain.Clear();
      MShe = new Model(FileName);

      //Create the layers
      for (int i = 0; i < MShe.GridInfo.NumberOfLayers; i++)
      {
        Layers.Add(new Layer(MShe.GridInfo.NumberOfLayers-i));

        //Bind layers together
        if (i > 0)
        {
          Layers[i]._below = Layers[i - 1];
          Layers[i - 1]._above = Layers[i];
        }
      }

      Layers[0].MoveUp = true;
    }

    public void DistributeIntakesOnLayers()
    {
      //Reset Layers
      foreach (Layer L in Layers)
      {
        L.Intakes.Clear();
        L.OriginalIntakes.Clear();
        L.IntakesMoved.Clear();
        L.IntakesAllowed = true;
      }
      

      //Distribute the intakes
      foreach (IWell W in Wells)
      {
        int col;
        int row;

        if(!MShe.GridInfo.TryGetIndex(W.X,W.Y, out col, out row))
          WellsOutSideModelDomain.Add(W);
        else
        {
          //Well has no terrain. Use model topography
          if (W.Terrain < 0)
            W.Terrain = MShe.GridInfo.SurfaceTopography.Data[row, col];
          
          foreach (IIntake I in W.Intakes)
            foreach (Screen S in I.Screens)
            {
              //Missing screen info
              if (S.TopAsKote < -990 || S.BottomAsKote < -990 || S.DepthToBottom < -990 || S.DepthToTop <-990)
                ScreensWithMissingDepths.Add(S);
              else
              {
                int TopLayer = MShe.GridInfo.GetLayer(col, row, S.TopAsKote.Value);
                int BottomLayer = MShe.GridInfo.GetLayer(col, row, S.BottomAsKote.Value);

                //Above terrain
                if (BottomLayer == -1)
                  ScreensAboveTerrain.Add(S);
                //Below bottom
                else if (TopLayer == -2)
                  ScreensBelowBottom.Add(S);
                else
                {
                  BottomLayer = Math.Max(0, BottomLayer);
                  if (TopLayer == -1)
                    TopLayer = MShe.GridInfo.NumberOfLayers - 1;

                  for (int i = BottomLayer; i <= TopLayer; i++)
                  {
                    //Prevent adding the same intake twice if has two screens in the same layer
                    if (I.Screens.Count > 1)
                    {
                      if (!Layers[i].Intakes.Contains(I))
                      {
                        Layers[i].Intakes.Add(I);
                        Layers[i].OriginalIntakes.Add(I);
                      }

                    }
                    else
                    {
                      Layers[i].Intakes.Add(I);
                      Layers[i].OriginalIntakes.Add(I);
                    }
                  }
                }
              }
            }
        }
      }
    }

    /// <summary>
    /// Gets a string with a log of moved intakes
    /// </summary>
    public string LogString
    {
      get { return _logstring.ToString(); }
    }


    public void BindAddedWells()
    {
      _logstring.Append("IntakeId\tOriginal screen top\tOriginal screen bottom\tNew layer number\t New Screen top\tNew screen bottom\n");
      int i = 0;
      foreach (Layer L in Layers)
      {
        for (int j = L.OriginalIntakes.Count; j < L.Intakes.Count; j++)
        {
          IIntake I = L.Intakes[j];
          _logstring.Append(I.ToString() + "\t" + I.Screens.First().TopAsKote + "\t" + I.Screens.First().BottomAsKote + "\t" + L.LayerNumber);

          I.Screens.Clear();
          Screen sc = new Screen(I);
          int col;
          int row;
          MShe.GridInfo.TryGetIndex(I.well.X, I.well.Y, out col, out row);

          sc.TopAsKote = MShe.GridInfo.UpperLevelOfComputationalLayers.Data[i][row, col] - 0.01;
          sc.BottomAsKote = MShe.GridInfo.LowerLevelOfComputationalLayers.Data[i][row, col] - 0.01;
          _logstring.Append("\t" + I.Screens.First().TopAsKote + "\t" + I.Screens.First().BottomAsKote + "\n");
        }

        i++;
      }
      DistributeIntakesOnLayers();
    }


    /// <summary>
    /// Gets a boolean to indicate if a model has been read
    /// </summary>
    public bool ModelRead
    {
      get { return MikeSheFileName != ""; }
    }

 
    /// <summary>
    /// Gets and sets the MikeSheFileName
    /// </summary>
    public string MikeSheFileName
    {
      get
      {
        if (MShe == null)
          return "";
        return MShe.Files.SheFile;
      }
      set
      {
        Load(value);
        DistributeIntakesOnLayers();
        NotifyPropertyChanged("MikeSheFileName");
        NotifyPropertyChanged("ModelRead");
      }
    }


    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged(String propertyName)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
      }
    }


    #endregion
  }
}
