﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

using GalaSoft.MvvmLight.Command;

using HydroNumerics.Wells;
using HydroNumerics.Core;
using HydroNumerics.Core.WPF;

namespace HydroNumerics.MikeSheTools.ViewModel
{
  public class ScreenAdderViewModel : BaseViewModel, IDataErrorInfo
  {
    private WellViewModel well;
    private ScreenViewModel svm;

    private bool NeedToCancel { get;  set; }

    public ScreenAdderViewModel(WellViewModel well)
    {
      this.well = well;
      svm = well.AddScreen();
      NeedToCancel = true;
      CurrentChange = new ChangeDescriptionViewModel(svm.CVM.ChangeController.NewScreen(svm._screen));
      RaisePropertyChanged("CurrentChange");
      RaisePropertyChanged("Intakes");
    }


    public IEnumerable<IIntake> Intakes
    {
      get
      {
        return well.Intakes;
      }
    }

    public IIntake Intake
    {
      get
      {
        return svm.Intake;
      }
      set
      {
        if (Intake != value)
        {
          Intake.Screens.Remove(svm._screen);
          Intake = value;
          Intake.Screens.Add(svm._screen);
          svm.Intake = Intake;
        }
      }
    }

    public double? DepthToTop
    {
      get
      {
        return svm._screen.DepthToTop;
      }
      set
      {
        svm._screen.DepthToTop = value;
        RaisePropertyChanged("DepthToTop");
      }
    }

    public double? DepthToBottom
    {
      get
      {
        return svm._screen.DepthToBottom;
      }
      set
      {
        svm._screen.DepthToBottom = value;
        RaisePropertyChanged("DepthToBottom");

      }
    }

    #region OKCommand
    RelayCommand okCommand;

    /// <summary>
    /// Gets the command that loads the Mike she
    /// </summary>
    public ICommand OkCommand
    {
      get
      {
        if (okCommand == null)
        {
          okCommand = new RelayCommand(() => this.OK(), () => this.CanOK);
        }
        return okCommand;
      }
    }

    private bool CanOK 
    { 
      get
      {
        return svm._screen.DepthToBottom.HasValue & svm._screen.DepthToTop.HasValue;
      }
    }

    private void OK()
    {
      var cd = svm.CVM.ChangeController.NewScreen(svm._screen);
      CurrentChange.changeDescription.ChangeValues.Clear();
      CurrentChange.changeDescription.ChangeValues.AddRange(cd.ChangeValues);
      CurrentChange.IsApplied = true;
      svm.CVM.AddChange(CurrentChange, false);
      svm.FireEvents();
      NeedToCancel = false;

      if (RequestClose != null)
        RequestClose();
    }

    #endregion

    public void Cancel()
    {
      if (NeedToCancel)
      {
        well.RemoveScreen(svm);
        NeedToCancel = false;
      } 
    }
    
    public ChangeDescriptionViewModel CurrentChange { get; private set; }

    public event Action RequestClose;

    public string Error
    {
      get
      {
        return null;
      }
    }

    public string this[string name]
    {
      get
      {
        string result = null;

        if (name == "DepthToBottom" || name == "DepthToTop")
        {
          if (DepthToBottom < DepthToTop)
          {
            result = "The bottom of the screen must be below the top";
          }
          if (DepthToBottom > Intake.well.Depth)
          {
            result = "The bottom of the screen must be above the bottom of the well";
          }
        }
        return result;
      }
    }

  }
}
