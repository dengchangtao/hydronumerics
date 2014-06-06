﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

using HydroNumerics.Core;
using HydroNumerics.Geometry;
using HydroNumerics.Geometry.Shapes;

namespace HydroNumerics.Nitrate.Model
{
  public class LakeSink : BaseModel, ISink
  {

    private Dictionary<int, int> MO5;

    public LakeSink()
    {

    }


    public override void ReadConfiguration(XElement Configuration)
    {
      base.ReadConfiguration(Configuration);

      if (Update)
      {
        Alpha = Configuration.SafeParseDouble("Alpha") ?? _Alpha;
        Beta = Configuration.SafeParseDouble("Beta") ?? _Beta;
        Par1 = Configuration.SafeParseDouble("Par1") ?? _Par1;
        ShapeFile = new SafeFile() { FileName = Configuration.SafeParseString("ShapeFileName") };
        ShapeFile.ColumnNames.Add(Configuration.SafeParseString("NameColumn") ?? "NAVN");
        ShapeFile.ColumnNames.Add(Configuration.SafeParseString("DepthColumn") ?? "Dybde");
        ShapeFile.ColumnNames.Add(Configuration.SafeParseString("InitNColumn") ?? "SoInitNmgL");
        ShapeFile.ColumnNames.Add(Configuration.SafeParseString("StartColumn") ?? "Aar_start");
        ShapeFile.ColumnNames.Add(Configuration.SafeParseString("EndColumn") ?? "Aar_slut");
      }
    }

    public override void Initialize(DateTime Start, DateTime End, IEnumerable<Catchment> Catchments)
    {
      MO5 = new Dictionary<int, int>();
      MO5.Add(1, 1);
      MO5.Add(2, 2);
      MO5.Add(3, 3);
      MO5.Add(4, 4);
      MO5.Add(5, 5);
      MO5.Add(6, 6);
      MO5.Add(7, 6);
      MO5.Add(8, 5);
      MO5.Add(9, 4);
      MO5.Add(10, 3);
      MO5.Add(11, 2);
      MO5.Add(12, 1);

      Dictionary<int, GeoRefData> LakeDepths = new Dictionary<int, GeoRefData>();

      using (ShapeReader s = new ShapeReader(ShapeFile.FileName))
      {
        foreach (var l in s.GeoData)
          LakeDepths.Add((int)l.Data[ShapeFile.ColumnNames[0]],l);
      }

      foreach (var c in Catchments.Where(ca => ca.BigLake != null))
      {
        GeoRefData lake;
        if (!LakeDepths.TryGetValue(c.BigLake.BigLakeID, out lake))
        {
          NewMessage(c.BigLake.Name + " removed! No entry found in " + ShapeFile.FileName);
          c.BigLake = null;
        }
        else if (c.M11Flow == null)
        {
          NewMessage(c.BigLake.Name + " removed! No Mike11 flow.");
          c.BigLake = null;
        }
        else if (!c.Geometry.Contains( (XYPoint)lake.Geometry))
        {
          c.BigLake = null;
        }
        else
        {
          c.BigLake.Volume = c.BigLake.Geometry.GetArea() * ((double)lake.Data[ShapeFile.ColumnNames[1] ]);
          c.BigLake.RetentionTime = c.BigLake.Volume / (c.M11Flow.GetTs(Time2.TimeStepUnit.Month).Average * 365.0 * 86400.0);
          c.BigLake.CurrentNMass = c.BigLake.Volume * ((double)lake.Data[ShapeFile.ColumnNames[2]]) / 1000.0;

          if ((int)lake.Data[ShapeFile.ColumnNames[3]] != 0)
            c.BigLake.Start = new DateTime((int)lake.Data[ShapeFile.ColumnNames[3]], 1, 1);
          else
            c.BigLake.Start = Start;

          if ((int)lake.Data[ShapeFile.ColumnNames[4]] != 0)
            c.BigLake.End = new DateTime((int)lake.Data[ShapeFile.ColumnNames[4]], 1, 1);
          else
            c.BigLake.End = End;
        }
      }
    }

    /// <summary>
    /// Returns the reduction in kg/s
    /// </summary>
    /// <param name="c"></param>
    /// <param name="CurrentMass"></param>
    /// <param name="CurrentTime"></param>
    /// <returns></returns>
    public double GetReduction(Catchment c, double CurrentMass, DateTime CurrentTime)
    {
      double red = 0;
      if (c.BigLake != null && c.BigLake.Start<=CurrentTime && c.BigLake.End>=CurrentTime)
      {
        double Reducer;
        if (c.BigLake.RetentionTime > 1)
        {
          Reducer = (Par1 * MO5[CurrentTime.Month] - c.Temperature.GetValue(CurrentTime, Time2.InterpolationMethods.DeleteValue))/100.0;
        }
        else
        {
          //Get the lake temperature
          double T = LakeTemperatureFromAir(c.Temperature.GetValue(CurrentTime, Time2.InterpolationMethods.DeleteValue), c.Temperature.GetValue(CurrentTime.AddMonths(-1),Time2.InterpolationMethods.DeleteValue), CurrentTime);
          c.BigLake.Temperature.Items.Add(new Time2.TimeStampValue(CurrentTime, T));
          
          Reducer = Alpha * Math.Pow(Beta, T - 20.0);
        }

        c.BigLake.CurrentNMass += CurrentMass;
        double removedN = Reducer * c.BigLake.CurrentNMass;
        //From m3/s to m3
        double mflow = c.M11Flow.GetTs(Time2.TimeStepUnit.Month).GetValue(CurrentTime) * DateTime.DaysInMonth(CurrentTime.Year, CurrentTime.Month) * 86400;

        if (mflow > 0)
        {
          double NOut = (c.BigLake.CurrentNMass - removedN) / (c.BigLake.Volume + mflow) * mflow;
          //        NOut = Math.Max(0,Math.Min(c.BigLake.CurrentNMass - removedN, NOut));

          c.BigLake.CurrentNMass = c.BigLake.CurrentNMass - removedN - NOut;

          //Store some results
          c.BigLake.NitrateReduction.Items.Add(new Time2.TimeStampValue(CurrentTime, Reducer));
          c.BigLake.NitrateConcentration.Items.Add(new Time2.TimeStampValue(CurrentTime, c.BigLake.CurrentNMass / c.BigLake.Volume));
          c.BigLake.FlushingRatio.Items.Add(new Time2.TimeStampValue(CurrentTime, c.BigLake.Volume / mflow));

          red = (CurrentMass - NOut) / (DateTime.DaysInMonth(CurrentTime.Year, CurrentTime.Month) * 86400.0);
        }
      }
      return red * MultiplicationPar + AdditionPar;
    }


    public double LakeTemperatureFromAir(double AirTemperature, double AirTemperaturePreviousMonth, DateTime CurrentTime)
    {
      return 1.517 + 0.3034 * AirTemperature + 0.1909 * AirTemperaturePreviousMonth + 0.6347 * AirTemperature * Math.Sin(Math.PI * CurrentTime.Month / 13.0);
    }


    #region Properties

   
    

    private double _Par1=6.117;
    public double Par1
    {
      get { return _Par1; }
      set
      {
        if (_Par1 != value)
        {
          _Par1 = value;
          NotifyPropertyChanged("Par1");
        }
      }
    }

    private double _Alpha=0.455;
    public double Alpha
    {
      get { return _Alpha; }
      set
      {
        if (_Alpha != value)
        {
          _Alpha = value;
          NotifyPropertyChanged("Alpha");
        }
      }
    }

    private double _Beta=1.087;
    public double Beta
    {
      get { return _Beta; }
      set
      {
        if (_Beta != value)
        {
          _Beta = value;
          NotifyPropertyChanged("Beta");
        }
      }
    }

    private SafeFile _ShapeFileName;
    public SafeFile ShapeFile
    {
      get { return _ShapeFileName; }
      set
      {
        if (_ShapeFileName != value)
        {
          _ShapeFileName = value;
          NotifyPropertyChanged("ShapeFileName");
        }
      }
    }
    
    



    #endregion

  }
}
