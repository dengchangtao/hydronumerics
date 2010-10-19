﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using HydroNumerics.Core;
using HydroNumerics.Time.Core;
using HydroNumerics.Geometry;

namespace HydroNumerics.HydroNet.Core
{
  [DataContract]
  public class GroundWaterBoundary:AbstractBoundary,IGroundwaterBoundary 
  {
    [DataMember]
    public IWaterPacket WaterSample { get; set; }

    /// <summary>
    /// Gets the accumulated water that has flown into this boundary. Is not reset
    /// </summary>
    [DataMember]
    public IWaterPacket ReceivedWater { get; private set; }

    [DataMember]
    public TimespanSeries WaterFlow { get; private set; }

    [DataMember]
    public IWaterBody Connection{get;set;}
    [DataMember]
    public double HydraulicConductivity { get; set; }
    [DataMember]
    public double Distance { get; set; }

    [DataMember]
    private GeoExchangeItem _head;

    [DataMember]
    private GeoExchangeItem leakageExchangeItem;

    /// <summary>
    /// Gets and sets the groundwater head
    /// </summary>
    public double GroundwaterHead
    {
      get
      {
        return _head.ExchangeValue;
      }
      set
      {
        _head.ExchangeValue = value;
      }
    }

    public GroundWaterBoundary() : base()
    {
      WaterFlow = new TimespanSeries();
      WaterFlow.Unit = UnitFactory.Instance.GetUnit(NamedUnits.cubicmeterpersecond);
      WaterFlow.Name = "Groundwater flow";
      Output.Items.Add(WaterFlow);
      BuildExchangeItems();
      WaterSample = new WaterPacket(1);

    }

    public GroundWaterBoundary(IWaterBody connection, double hydraulicConductivity, double distance, double groundwaterHead, XYPolygon ContactPolygon):this()
    {
      Connection = connection;
      HydraulicConductivity = hydraulicConductivity;
      Distance = distance;
      GroundwaterHead = groundwaterHead;
      ContactGeometry = ContactPolygon;
    }

    private void BuildExchangeItems()
    {
      _head = new GeoExchangeItem();
      _head.Description = "Ground water head for: " + Name;
      _head.Geometry = ContactGeometry;
      _head.IsInput = true;
      _head.IsOutput = false;
      //        GroundWaterHeadExchangeItem.Location = "Near " + Connection.Name;
      _head.Quantity = "Ground water head";
      _head.timeType = TimeType.TimeStamp;
      _head.Unit = UnitFactory.Instance.GetUnit(NamedUnits.meter);

      ExchangeItems.Add(_head);

      leakageExchangeItem = new GeoExchangeItem();
      //       leakageExchangeItem.Description = "leakage from: " + Connection.Name;
      leakageExchangeItem.Geometry = ContactGeometry;
      leakageExchangeItem.IsInput = false;
      leakageExchangeItem.IsOutput = true;
      //      leakageExchangeItem.Location = "Near " + Connection.Name;
      leakageExchangeItem.Quantity = "Leakage";
      leakageExchangeItem.timeType = TimeType.TimeSpan;
      leakageExchangeItem.Unit = UnitFactory.Instance.GetUnit(NamedUnits.cubicmeterpersecond);
      ExchangeItems.Add(leakageExchangeItem);      

    }


    #region IWaterSource Members

    public void Initialize()
    {
    }

    public IWaterPacket GetSourceWater(DateTime Start, TimeSpan TimeStep)
    {
      double WaterVolume = ((XYPolygon)ContactGeometry).GetArea() * HydraulicConductivity * (GroundwaterHead - Connection.WaterLevel) / Distance * TimeStep.TotalSeconds;

      leakageExchangeItem.ExchangeValue = WaterVolume / TimeStep.TotalSeconds;
      WaterFlow.AddSiValue(Start, Start.Add(TimeStep), leakageExchangeItem.ExchangeValue);

      return WaterSample.DeepClone(WaterVolume);
    }

    /// <summary>
    /// Positive
    /// </summary>
    /// <param name="Start"></param>
    /// <param name="TimeStep"></param>
    /// <returns></returns>
    public double GetSinkVolume(DateTime Start, TimeSpan TimeStep)
    {
      double WaterVolume = ((XYPolygon)ContactGeometry).GetArea() * HydraulicConductivity * (Connection.WaterLevel - GroundwaterHead) / Distance * TimeStep.TotalSeconds;
      return WaterVolume;
    }

    public void ReceiveSinkWater(DateTime Start, TimeSpan TimeStep, IWaterPacket Water)
    {
      leakageExchangeItem.ExchangeValue = -Water.Volume / TimeStep.TotalSeconds;

      WaterFlow.AddSiValue(Start, Start.Add(TimeStep), leakageExchangeItem.ExchangeValue);

      if (ReceivedWater == null)
        ReceivedWater = Water;
      else
        ReceivedWater.Add(Water);
    }
    /// <summary>
    /// Returns true if water is flowing into the stream.
    /// </summary>
    public bool IsSource(DateTime time)
    {
      return Connection.WaterLevel < GroundwaterHead;
    }


    public DateTime EndTime
    {
      get
      {
        return DateTime.MaxValue;
      }
    }

    public DateTime StartTime
    {
      get
      {
        return DateTime.MinValue;
      }
    }

    #endregion
  }
}
