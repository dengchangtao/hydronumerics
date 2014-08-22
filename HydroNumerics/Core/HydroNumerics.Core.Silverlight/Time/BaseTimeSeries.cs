﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

using HydroNumerics.Core;

namespace HydroNumerics.Core.Time
{
  [DataContract]
  public class BaseTimeSeries<T>:BaseViewModel
  {

    /// <summary>
    /// The function that picks the value out of the type.
    /// </summary>
    protected Func<T, double> ValueGetter;


    public BaseTimeSeries()
    {
      TimeStepSize = TimeStepUnit.None;
      Items = new ObservableCollection<T>();
      Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Items_CollectionChanged);
    }

    public BaseTimeSeries(IEnumerable<T> Values)
    {
      Items = new ObservableCollection<T>(Values);
      Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Items_CollectionChanged);
    }

    private string _Unit;
    public string Unit
    {
      get { return _Unit; }
      set
      {
        if (_Unit != value)
        {
          _Unit = value;
          RaisePropertyChanged("Unit");
        }
      }
    }
    


    void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      //This should check if the values are actually changed before notifying
      RaisePropertyChanged("StartTime");
      RaisePropertyChanged("EndTime");
      RaisePropertyChanged("Sum");
      RaisePropertyChanged("Average");
      RaisePropertyChanged("Min");
      RaisePropertyChanged("Max");
      RaisePropertyChanged("Count");
    }

    /// <summary>
    /// Adds a range of data to the Items collection. 
    /// Actually creates a new ObservableCollection. Not sure if this actually works, so be careful
    /// </summary>
    /// <param name="Values"></param>
    public void AddRange(IEnumerable<T> Values)
    {
      List<T> temp = new List<T>(Items);
      temp.AddRange(Values);
      Items = new ObservableCollection<T>(temp);
      
      Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Items_CollectionChanged);
    }

    /// <summary>
    /// Gets the list of items
    /// </summary>
    [DataMember]
    public ObservableCollection<T> Items { get;  protected set; }

    /// <summary>
    /// Iterates the values
    /// </summary>
    public IEnumerable<double> Values
    {
      get
      {
        foreach(var v in Items)
          yield return ValueGetter(v);
      }
    }


    /// <summary>
    /// Gets and sets the unit of the time step size
    /// </summary>
    [DataMember]
    public TimeStepUnit TimeStepSize { get;  set; }


    private double _DeleteValue = 1e-035;
    /// <summary>
    /// Gets and set the delete value
    /// </summary>
    [DataMember]
    public double DeleteValue
    {
      get { return _DeleteValue; }
      set
      {
        if (_DeleteValue != value)
        {
          _DeleteValue = value;
          RaisePropertyChanged("DeleteValue");
        }
      }
    }

    /// <summary>
    /// Gets the number of values. Also counts delete values
    /// </summary>
    public int Count
    {
      get { return Items.Count; }
    }


    /// <summary>
    /// Gets the sum og the values
    /// </summary>
    public double Sum
    {
      get
      {
        return Items.Select(ValueGetter).Where(v=>v!=DeleteValue).Sum();
      }
    }

    /// <summary>
    /// Gets the average of the values
    /// </summary>
    public double Average
    {
      get
      {
        return Items.Select(ValueGetter).Where(v => v != DeleteValue).Average();
      }
    }

    /// <summary>
    /// Gets the maximum of the values
    /// </summary>
    public double Max
    {
      get
      {
        return Items.Select(ValueGetter).Where(v => v != DeleteValue).Max();
      }
    }

    /// <summary>
    /// Gets the minimum of the values
    /// </summary>
    public double Min
    {
      get
      {
        return Items.Select(ValueGetter).Where(v => v != DeleteValue).Min();
      }
    }

    private T _FirstMinValue;
    /// <summary>
    /// Gets the first occurence of the minimumvalue
    /// </summary>
    public T FirstMinValue
    {
      get
      {
        if (_FirstMinValue == null)
        {
          foreach (var v in Items)
            if (ValueGetter(v) == Min)
            {
              _FirstMinValue = v;
              break;
            }
        }
        return _FirstMinValue;
      }
    }


    private T _FirstMaxValue;
    /// <summary>
    /// Gets the first occurrence of the maximum value
    /// </summary>
    public T FirstMaxValue
    {
      get
      {
        if (_FirstMaxValue == null)
        {
          foreach (var v in Items)
            if (ValueGetter(v) == Max)
            {
              _FirstMaxValue = v;
              break;
            }
        }
        return _FirstMaxValue;
      }
    }


    
  }
}