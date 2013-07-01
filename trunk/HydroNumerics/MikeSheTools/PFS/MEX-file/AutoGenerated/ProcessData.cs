using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;
using HydroNumerics.MikeSheTools.PFS.SheFile;

namespace HydroNumerics.MikeSheTools.PFS.MEX
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class ProcessData
  {

    private PFSKeyword _keyword;

    internal ProcessData(PFSKeyword keyword)
    {
       _keyword = keyword;
    }

    public ProcessData()
    {
       _keyword = new PFSKeyword("ProcessData");
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Double));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Double));
    }
    public int Par1
    {
      get { return _keyword.GetParameter(1).ToInt();}
      set { _keyword.GetParameter(1).Value = value;}
    }

    public double Par2
    {
      get { return _keyword.GetParameter(2).ToDouble();}
      set { _keyword.GetParameter(2).Value = value;}
    }

    public int Par3
    {
      get { return _keyword.GetParameter(3).ToInt();}
      set { _keyword.GetParameter(3).Value = value;}
    }

    public double Par4
    {
      get { return _keyword.GetParameter(4).ToDouble();}
      set { _keyword.GetParameter(4).Value = value;}
    }

  }
}
