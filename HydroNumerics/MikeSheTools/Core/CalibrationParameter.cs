﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace HydroNumerics.MikeSheTools.Core
{
  public class CalibrationParameter
  {
    private PropertyInfo propInfo;
    private List<object> obj;

    public CalibrationParameter TiedTo { get; set; }
    public ParameterType ParType { get; set; }
    public ParameterGroup Group { get; set; }

    public double MaxValue { get; set; }
    public double MinValue { get; set; }
    public double Scale { get; set; }
    public double Offset { get; set; }
    public double Dercom { get; set; }
    public string ParChgLim { get; set; }

    private double currentValue;

    public CalibrationParameter()
    { }

    public CalibrationParameter(string PropertyName, IEnumerable<object> os)
    {
      obj = new List<object>(os);
      propInfo = os.First().GetType().GetProperty(PropertyName);
    }


    public CalibrationParameter(string PropertyName, object o)
    {
      obj = new List<object>();
      obj.Add(o);
      propInfo = o.GetType().GetProperty(PropertyName);     
    }

    public string ShortName { get; set; }

    [DataMember]
    public string DisplayName { get; set; }
    public double CurrentValue
    {
      get
      {
        return GetValue();
      }
      set
      {
        SetValue(value);
      }
    }

    [OperationContract]
    public double GetValue()
    {
      if (propInfo == null)
        return currentValue;
      return (double)propInfo.GetValue(obj.First(), null);
    }

    [OperationContract]
    public void SetValue(double value)
    {
      if (propInfo == null)
        currentValue = value;
      else
      {
        foreach (var o in obj)
          propInfo.SetValue(o, value, null);
      }
    }
  }
}