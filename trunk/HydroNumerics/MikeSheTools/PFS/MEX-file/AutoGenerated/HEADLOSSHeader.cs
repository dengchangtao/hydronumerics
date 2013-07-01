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
  public partial class HEADLOSSHeader
  {

    private PFSKeyword _keyword;

    internal HEADLOSSHeader(PFSKeyword keyword)
    {
       _keyword = keyword;
    }

    public HEADLOSSHeader()
    {
       _keyword = new PFSKeyword("HEADLOSSHeader");
    }
    public int NumberOfParameters
    {
      get { return _keyword.GetParametersNo(); }
    }

    public string GetValue(int index)
    {
      return _keyword.GetParameter(index + 1).ToString();
    }

    public void SetValue(int index, string value)
    {
      _keyword.GetParameter(index + 1).Value = value;
    }

    public void AddValue(int value)
    {
      _keyword.AddParameter(new PFSParameter(PFSParameterType.String, value));
    }

  }
}
