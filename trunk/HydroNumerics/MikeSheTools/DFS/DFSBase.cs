﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using DHI.Generic.MikeZero.DFS;

namespace HydroNumerics.MikeSheTools.DFS
{

  /// <summary>
  /// Abstract class that handles all direct access to .dfs-files. Uses static methods from DFSWrapper in 
  /// DHI.Generic.MikeZero.DFS.dll as well as direct calls into the ufs.dll
  /// </summary>
  public abstract class DFSBase
  {
    #region Calls directly into ufs.dll because the wrapped call does not work on vista due to something with strings.
    const string UFSDll = "ufs.dll";  // Name of dll. Should be in path

    /// <summary>
    /// Call directly into ufs.dll because the wrapped call does not work on vista due to something with strings.
    /// </summary>
    /// <param name="ItemPointer"></param>
    /// <param name="ItemType"></param>
    /// <param name="Name"></param>
    /// <param name="Unit"></param>
    /// <param name="DataType"></param>
    /// <returns></returns>
    [DllImport(UFSDll, CharSet = CharSet.None, CallingConvention = CallingConvention.StdCall)]
    private extern static int dfsGetItemInfo_(IntPtr ItemPointer, ref int ItemType, ref IntPtr Name, ref IntPtr Unit, ref int DataType);

    /// <summary>
    /// Call directly into ufs.dll because the wrapped call does not work on vista due to something with strings.
    /// </summary>
    /// <param name="HeaderPointer"></param>
    /// <param name="Projection"></param>
    /// <param name="longitude"></param>
    /// <param name="Latitude"></param>
    /// <param name="Orientation"></param>
    /// <returns></returns>
    [DllImport(UFSDll, CharSet = CharSet.None, CallingConvention = CallingConvention.StdCall)]
    private extern static int dfsGetGeoInfoUTMProj(IntPtr HeaderPointer, ref IntPtr Projection, ref double longitude, ref double Latitude, ref double Orientation);

    #endregion

    private int _currentTimeStep = -1;
    private int _currentItem = -1;
    private IntPtr _fileWriter = IntPtr.Zero;
    private IntPtr _headerWriter = IntPtr.Zero;
    private bool _initializedForWriting = false;
    private string _filename;
    private DateTime _firstTimeStep;
    private TimeSpan _timeStep = TimeSpan.Zero;

    protected string AbsoluteFileName;

    protected float[] dfsdata; //Buffer used to fill data into

    protected int _numberOfLayers = 1;
    protected int _numberOfColumns = 1;
    protected int _numberOfRows = 1;

    protected double _xOrigin;
    protected double _yOrigin;
    protected double _gridSize;
    public Item[] Items { get; private set; }

    private int _status;


    public int Status
    {
      get { return _status; }
      set
      {
        _status = value;
        if (_status != 0)
        {
          string error = "fjel";
        }
      }
    }


    #region Constructors

    public DFSBase(string FileName, string Title, int NumberOfItems, int NumberOfRows)
    {
      _filename = FileName;
      AbsoluteFileName = Path.GetFullPath(FileName);

      Status = DFSWrapper.dfsHeaderCreate(1, Title, "HydroNumerics", 1, NumberOfItems, 1, ref _headerWriter);

      Status = DFSWrapper.dfsSetGeoInfoUTMProj(_headerWriter, "NON-UTM", 89, 98, 0);

      Status = DFSWrapper.dfsSetEqCalendarAxis(_headerWriter, "2002-01-01", "12:00:00", 1400, 1, 86400, 0);

      IntPtr[] IPointers = new IntPtr[NumberOfItems];

      //Gets the pointers to the items
      for (int i = 0; i < NumberOfItems; i++)
      {
        IPointers[i] = (DFSWrapper.dfsItemD(_headerWriter, i + 1));
        Status = DFSWrapper.dfsSetItemInfo_(_headerWriter, IPointers[i], 100000, "name", "meter", 1);

        Status = DFSWrapper.dfsSetItemAxisEqD2(IPointers[i], 1000, 10, 10, 0, 0, 15, 15);
      }

      Status = DFSWrapper.dfsFileCreate(FileName, _headerWriter, ref _fileWriter);
      _initializedForWriting = true;

    }

    public DFSBase(string DFSFileName)
    {
      _filename = DFSFileName;
      AbsoluteFileName = Path.GetFullPath(DFSFileName);

      Status = DFSWrapper.dfsFileRead(DFSFileName, ref _headerWriter, ref _fileWriter);
      if (Status != 0)
        return; //Not a valid file. 

      int filetype = DFSWrapper.dfsGetFileType(_headerWriter);
      int stattype = DFSWrapper.dfsGetItemStatsType(_headerWriter);
      string appTitle = DFSWrapper.dfsGetAppTitle(_headerWriter);
      int appVersion = DFSWrapper.dfsGetAppVersionNo(_headerWriter);

      int nitems = DFSWrapper.dfsGetNoOfItems(_headerWriter);
      Items = new Item[nitems];

      //Gets the pointers and create the items items
      for (int i = 1; i <= nitems; i++)
      {
        Items[i - 1] = new Item(DFSWrapper.dfsItemD(_headerWriter, i));
      }

      string eum_unit = "";
      int unit = 0;

      int data_type = 0;
      int item_type = 0;

      float x = 0;
      float y = 0;
      float z = 0;

      float dx = 0;
      float dy = 0;
      float dz = 0;

      IntPtr name = new IntPtr();
      double lon = 0;
      double lat = 0;
      double or = 0;

      //Reads the projection
      Status = dfsGetGeoInfoUTMProj(_headerWriter, ref name, ref lon, ref lat, ref or);
      _xOrigin = lon;
      _yOrigin = lat;

      int axistype = DFSWrapper.dfsGetItemAxisType(FirstItem.ItemPointer);

      if (axistype == 3)
      {
        IntPtr coords = new IntPtr();
        Status = DFSWrapper.dfsGetItemAxisNeqD1(FirstItem.ItemPointer, ref unit, ref eum_unit, ref data_type, ref coords);
      }
      //DFS2 from MikeShe
      else if (axistype == 5)
      {
        Status = DFSWrapper.dfsGetItemAxisEqD2(FirstItem.ItemPointer, ref item_type, ref eum_unit, ref _numberOfColumns, ref _numberOfRows, ref x, ref y, ref dx, ref dy);
      }
      //DFS3 from MikeShe
      else if (axistype == 8)
      {
        Status = DFSWrapper.dfsGetItemAxisEqD3(FirstItem.ItemPointer, ref item_type, ref eum_unit, ref _numberOfColumns, ref _numberOfRows, ref _numberOfLayers, ref x, ref y, ref z, ref dx, ref dy, ref dz);
      }
      _gridSize = dx;


      //Prepares an array of floats to recieve the data
      dfsdata = new float[_numberOfColumns * _numberOfRows * _numberOfLayers];

      //Now look at time axis
      int timeAxisType = DFSWrapper.dfsGetTimeAxisType(_headerWriter);
      string startdate = "";
      string starttime = "";
      double tstart = 0;
      double tstep = 0;
      int nt = 0;
      int tindex = 0;

      if (timeAxisType != 4)
      {
        Status = DFSWrapper.dfsGetEqCalendarAxis(_headerWriter, ref startdate, ref starttime, ref unit, ref eum_unit, ref tstart, ref tstep, ref nt, ref tindex);

        if (unit == 1400)
          _timeStep = TimeSpan.FromSeconds(tstep);
        else if (unit == 1402)
          _timeStep = TimeSpan.FromHours(tstep);

      }
      else if (timeAxisType == 4)
      {
        Status = DFSWrapper.dfsGetNeqCalendarAxis(_headerWriter, ref startdate, ref starttime, ref unit, ref eum_unit, ref tstart, ref tstep, ref nt, ref tindex);
      }

      NumberOfTimeSteps = nt;
      TimeSteps = new DateTime[NumberOfTimeSteps];

      if (startdate != null & starttime != null)
      {
        _firstTimeStep = DateTime.Parse(startdate).Add(TimeSpan.Parse(starttime));
        TimeSteps[0] = _firstTimeStep;
      }

      for (int i = 1; i < nt; i++)
      {
        if (timeAxisType == 4)
        {
          if (unit == 1400)
            TimeSteps[i] = _firstTimeStep.AddSeconds(ReadItemTimeStep(i, 1));
          else if (unit == 1402)
            TimeSteps[i] = _firstTimeStep.AddHours(ReadItemTimeStep(i, 1));
        }
        else
          TimeSteps[i] = TimeSteps[i - 1].Add(_timeStep);
      }
    }

    #endregion

    #region Read methods

    /// <summary>
    /// Returns the zero-based index of the TimeStep closest to the TimeStamp. If the timestamp falls exactly between two timestep the smallest is returned.
    /// If the TimeStamp is before the first timestep 0 is returned.
    /// If the TimeStamp is after the last timestep the index of the last timestep is returned
    /// </summary>
    /// <param name="TimeStamp"></param>
    /// <returns></returns>
    public int GetTimeStep(DateTime TimeStamp)
    {
      if (TimeStamp < _firstTimeStep || NumberOfTimeSteps == 1)
        return 0;
      int TimeStep;
      //fixed timestep
      if (_timeStep != TimeSpan.Zero)
        TimeStep = (int)Math.Round(TimeStamp.Subtract(_firstTimeStep).TotalSeconds / _timeStep.TotalSeconds, 0);
      //Variabale timestep
      else
      {
        //Last timestep is known
        if (TimeStamp >= TimeSteps[TimeSteps.Length - 1])
          return TimeSteps.Length - 1;

        int i = 1;
        //Loop the timesteps
        while (TimeStamp > TimeSteps[i])
        {
          i++;
        }
        //Check if last one was actually closer
        if (TimeSteps[i].Subtract(TimeStamp) < TimeStamp.Subtract(TimeSteps[i - 1]))
          return i;
        else
          return i - 1;
      }
      return Math.Min(NumberOfTimeSteps, TimeStep);
    }

    /// <summary>
    /// Reads data for the TimeStep and Item if necessary and fills them into the buffer.
    /// Time steps counts from 0 and Item from 1.
    /// In case of nonequidistant time (only dfs0) it returns the timestep as double
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    protected double ReadItemTimeStep(int TimeStep, int Item)
    {
      double time = 0;

      if (TimeStep != _currentTimeStep || Item != _currentItem)
      {
        _currentTimeStep = TimeStep;
        _currentItem = Item;
        //Spools to the correct Item and TimeStep
        Status = DFSWrapper.dfsFindItemDynamic(_headerWriter, _fileWriter, TimeStep, Item);
        if (Status != 0)
          throw new Exception("Could not find TimeStep number: " + TimeStep + " and Item number: " + Item);

        //Reads the data
        Status = DFSWrapper.dfsReadItemTimeStep(_headerWriter, _fileWriter, ref time, dfsdata);
        if (Status != 0)
          throw new Exception("Error in file: " + _filename + " reading timestep number: " + this._currentTimeStep);
      }
      return time;
    }

    #endregion

    #region Write methods

    /// <summary>
    /// Writes data for the TimeStep and Item
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    protected void WriteItemTimeStep(int TimeStep, int Item, float[] data)
    {
      if (!_initializedForWriting)
        InitializeForWriting();

      //Spools to the correct Item and TimeStep
      Status = DFSWrapper.dfsFindItemDynamic(_headerWriter, _fileWriter, TimeStep, Item);
      //      if (ok != 0)
      //          throw new Exception("Could not find TimeStep number: " + TimeStep + " and Item number: " + Item);

      double time = 50;

      //Writes the data
      Status = DFSWrapper.dfsWriteItemTimeStep(_headerWriter, _fileWriter, time, data);
      if (Status != 0)
        throw new Exception("Error writing timestep number: " + _currentTimeStep);
    }

    /// <summary>
    /// Opens the file for writing. First closes the file since it has already been opened for reading
    /// </summary>
    private void InitializeForWriting()
    {
      Dispose(false);
      Status = DFSWrapper.dfsFileEdit(_filename, ref _headerWriter, ref _fileWriter);
      if (Status != 0)
        throw new Exception("Error in initializing file : " + _filename + " for writing");
      _initializedForWriting = true;
    }

    /// <summary>
    /// Writes timestep and starttime
    /// Because it is called twice
    /// </summary>
    private void WriteTime()
    {
      if (!_initializedForWriting)
        InitializeForWriting();
      Status = DFSWrapper.dfsSetEqCalendarAxis(_headerWriter, _firstTimeStep.ToString("yyyy-MM-dd"), _firstTimeStep.ToString("hh:mm:ss"), 1400, 0, _timeStep.TotalSeconds, 0);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the first item. There should always be at least one item
    /// </summary>
    public Item FirstItem
    {
      get
      {
        return Items[0];
      }
    }


    /// <summary>
    /// Gets an array with the timesteps.
    /// </summary>
    public DateTime[] TimeSteps { get; private set; }

    /// <summary>
    /// Gets and sets the date and time of the first time step.
    /// </summary>
    public DateTime TimeOfFirstTimestep
    {
      get
      {
        return _firstTimeStep;
      }
      set
      {
        _firstTimeStep = value;
        WriteTime();
      }
    }

    /// <summary>
    /// Gets and sets the size of a time step
    /// </summary>
    public TimeSpan TimeStep
    {
      get
      {
        return _timeStep;
      }
      set
      {
        if (_timeStep == TimeSpan.Zero)
          throw new Exception("Cannot set the time step when the dfs-file is non-equidistant");
        _timeStep = value;
        WriteTime();
      }
    }


    /// <summary>
    /// Gets the DeleteValue from the DFS-file
    /// </summary>
    public double DeleteValue
    {
      get
      {
        return DFSWrapper.dfsGetDeleteValFloat(_headerWriter);
      }
    }


    /// <summary>
    /// Gets the FileName
    /// </summary>
    public string FileName
    {
      get
      {
        return _filename;
      }
    }


    /// <summary>
    /// Gets the number of timesteps
    /// </summary>
    public int NumberOfTimeSteps { get; protected set; }


    #endregion

    #region Dispose methods

    /// <summary>
    /// Override of the Dispose method in DFSFileInfo which probably does not account for finalization
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        dfsdata = null;
      }
      Status = DFSWrapper.dfsFileClose(_headerWriter, ref _fileWriter);
    }

    /// <summary>
    /// Destructor called when the object is garbage collected.
    /// </summary>
    ~DFSBase()
    {
      // Simply call Dispose(false).
      Dispose(false);
    }

    #endregion

  }
}
