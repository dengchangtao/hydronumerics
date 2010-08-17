﻿using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HydroNumerics.Core;
using HydroNumerics.Time.Core;

namespace HydroNumerics.HydroNet.Core
{
  [DataContract]
  public class FlowBoundary:AbstractBoundary,IWaterSinkSource 
  {
    [DataMember]
    private BaseTimeSeries TS = null;

    public DateTime EndTime
    {
      get
      {
        if (TS == null)
          return DateTime.MaxValue;
        else
          return TS.EndTime;
      }
    }

    public void Initialize()
    {
        _exchangeItems = new List<GeoExchangeItem>();
        GeoExchangeItem flowExchangeItem = new GeoExchangeItem();
        flowExchangeItem.Description = "Flow (Boundary name: " + Name + ")";
        flowExchangeItem.Geometry = null;
        flowExchangeItem.ExchangeValue = Flow;
        if (TS != null)
        {
            flowExchangeItem.IsInput = false;
        }
        else
        {
            flowExchangeItem.IsInput = true;
        }
        flowExchangeItem.IsOutput = true;
        flowExchangeItem.Location = Name;
        flowExchangeItem.Quantity = "Flow";
        flowExchangeItem.timeType = TimeType.TimeSpan;
        flowExchangeItem.Unit = UnitFactory.Instance.GetUnit(NamedUnits.cubicmeterpersecond);
        this.ExchangeItems.Add(flowExchangeItem);
    }

    public FlowBoundary()
    {
      WaterSample = new WaterPacket(1);
      Area = 1;
    }


    public FlowBoundary(double FlowRate):this()
    {
        Flow = FlowRate;
    }

    public FlowBoundary(BaseTimeSeries ts) : this()
    {
      TS = ts;
    }

    public TimespanSeries TimeValues
    {
      get
      {
        return (TimespanSeries)TS;
      }
    }



    #region IWaterSource Members


    public IWaterPacket GetSourceWater(DateTime Start, TimeSpan TimeStep)
    {
      if (TS != null)
        Flow = TS.GetSiValue(Start, Start.Add(TimeStep));

      double _routedFlow = Area * Flow * TimeStep.TotalSeconds;
      return WaterSample.DeepClone(_routedFlow);
    }

    public double GetSinkVolume(DateTime Start, TimeSpan TimeStep)
    {
      if (TS != null)
        Flow = TS.GetSiValue(Start, Start.Add(TimeStep));

      return -Area * Flow * TimeStep.TotalSeconds;
    }

    public bool Source(DateTime time)
    {
      if (TS != null)
        Flow = TS.GetSiValue(time);
      return Flow >= 0;
    }

    #endregion
  }
}
