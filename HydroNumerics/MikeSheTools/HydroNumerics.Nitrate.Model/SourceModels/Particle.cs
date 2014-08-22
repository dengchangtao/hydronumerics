﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HydroNumerics.Geometry;

namespace HydroNumerics.Nitrate.Model
{
  public class Particle:IXYPoint
  {
    public int ID { get; set; }
    public double TravelTime { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double XStart { get; set; }
    public double YStart { get; set; }
    public int Registration { get; set; }
    public string SinkType { get; set; }

    public override bool Equals(object obj)
    {
      return ID.Equals(((Particle)obj).ID);
    }

    public override int GetHashCode()
    {
      return ID.GetHashCode();
    }

  }
}