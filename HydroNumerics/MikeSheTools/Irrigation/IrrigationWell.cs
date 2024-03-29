﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HydroNumerics.Wells;

namespace HydroNumerics.MikeSheTools.Irrigation
{
  public class IrrigationWell:Well
  {
    public int GridCode { get; set; }

    public double MaxRate { get; set; }

    public double MaxDepth { get; set; }

    public IrrigationWell(string ID)
      : base(ID)
    { }

  }
}
