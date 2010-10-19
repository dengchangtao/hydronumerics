﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

using HydroNumerics.Core;
using HydroNumerics.Time.Core;

namespace HydroNumerics.HydroNet.Core
{
  [DataContract]
  [KnownType(typeof(Stream))]
  [KnownType(typeof(Lake))]
  public class Model
  {
    [DataMember]
    public List<IWaterBody> _waterBodies = new List<IWaterBody>();

    [DataMember]
    public DateTime CurrentTime { get; private set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public bool Initialized { get; private set; }


    /// <summary>
    /// Gets the maximum time the model can run to
    /// </summary>
    public DateTime EndTime
    {
      get
      {
        return _waterBodies.Min(var => var.EndTime);
      }
    }

    #region Constuctors

    public Model()
    {
        Initialized = false;
    }


    #endregion


    #region Simulations methods

    /// <summary>
    /// Moves the entire network in time from CurrentTime to End using the provided timestep.
    /// When using this method the entire network will be filled with water at the beginning
    /// </summary>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    /// <param name="TimeStep"></param>
    public void MoveInTime(DateTime End, TimeSpan TimeStep)
    {
      while (CurrentTime.Add(TimeStep) <= End)
      {
        Update(CurrentTime.Add(TimeStep));
      }

      if (CurrentTime > End)
      {
        Update(End);
        CurrentTime = End;
      }
    }

    /// <summary>
    /// Moves the entire network up to the new time
    /// </summary>
    /// <param name="TimeStep"></param>
    public void Update(DateTime NewTime)
    {
      if (!Initialized)
        Initialize();
      foreach (IWaterBody IW in _waterBodies)
        IW.Update(NewTime);
      CurrentTime = NewTime;
    }

    /// <summary>
    /// Store the current State
    /// </summary>
    /// <returns>State identifier.</returns>
    public string KeepCurrentState() 
    {
      foreach (IWaterBody IW in _waterBodies)
        IW.KeepCurrentState("StateName");
      return "StateName";
    }

    /// <summary>
    /// Restores the state identified by the parameter stateID. If the state identifier identified by
    /// stateID is not known an exception is be trown.
    /// </summary>
    /// <param name="stateID">State identifier.</param>
    public void RestoreState(string stateID)
    {
      foreach (IWaterBody IW in _waterBodies)
        IW.RestoreState(stateID);
      CurrentTime = _waterBodies.First().CurrentTime;
    }

    /// <summary>
    /// Sets a state and fills the network with water
    /// </summary>
    /// <param name="stateID"></param>
    /// <param name="CurrentTime"></param>
    public void SetState(string stateID, DateTime CurrentTime, IWaterPacket WaterTypeToFillWith)
    {
      this.CurrentTime = CurrentTime;

      foreach (IWaterBody IW in _waterBodies)
        IW.SetState(stateID, CurrentTime, WaterTypeToFillWith.DeepClone(IW.Volume));
    }


    /// <summary>
    /// Clears a state from memory. If the state identifier identified by
    /// stateID is not known by the linkable component an exception should be trown.
    /// </summary>
    /// <param name="stateID">State identifier.</param>
    void ClearState(string stateID)
    {
    }

    #endregion

    /// <summary>
    /// Saves the model to a file.
    /// </summary>
    /// <param name="FileName"></param>
    public void Save(string FileName)
    {
      ModelFactory.SaveModel(FileName, this);
    }


    #region Private Methods
    public void Initialize()
    {
        Initialized = true;

      //ToDo: sort network according to topology
      //Warn if there are WBs with no inflow.
    }

#endregion
  }
}
