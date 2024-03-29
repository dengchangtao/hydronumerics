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
  public partial class r
  {

    internal PFSKeyword _keyword;

    internal r(PFSKeyword keyword)
    {
       _keyword = keyword;
    }

    public r(string keywordname)
    {
       _keyword = new PFSKeyword(keywordname);
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Double, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Double, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
       _keyword.AddParameter(new PFSParameter(PFSParameterType.Integer, 0));
    }
    public int Par1
    {
      get { return _keyword.GetParameter(1).ToInt();}
      set { _keyword.GetParameter(1).Value = value;}
    }

    public int Par2
    {
      get { return _keyword.GetParameter(2).ToInt();}
      set { _keyword.GetParameter(2).Value = value;}
    }

    public int Par3
    {
      get { return _keyword.GetParameter(3).ToInt();}
      set { _keyword.GetParameter(3).Value = value;}
    }

    public int Par4
    {
      get { return _keyword.GetParameter(4).ToInt();}
      set { _keyword.GetParameter(4).Value = value;}
    }

    public double Par5
    {
      get { return _keyword.GetParameter(5).ToDouble();}
      set { _keyword.GetParameter(5).Value = value;}
    }

    public int Par6
    {
      get { return _keyword.GetParameter(6).ToInt();}
      set { _keyword.GetParameter(6).Value = value;}
    }

    public int Par7
    {
      get { return _keyword.GetParameter(7).ToInt();}
      set { _keyword.GetParameter(7).Value = value;}
    }

    public double Par8
    {
      get { return _keyword.GetParameter(8).ToDouble();}
      set { _keyword.GetParameter(8).Value = value;}
    }

    public int Par9
    {
      get { return _keyword.GetParameter(9).ToInt();}
      set { _keyword.GetParameter(9).Value = value;}
    }

    public int Par10
    {
      get { return _keyword.GetParameter(10).ToInt();}
      set { _keyword.GetParameter(10).Value = value;}
    }

    public int Par11
    {
      get { return _keyword.GetParameter(11).ToInt();}
      set { _keyword.GetParameter(11).Value = value;}
    }

    public int Par12
    {
      get { return _keyword.GetParameter(12).ToInt();}
      set { _keyword.GetParameter(12).Value = value;}
    }

    public int Par13
    {
      get { return _keyword.GetParameter(13).ToInt();}
      set { _keyword.GetParameter(13).Value = value;}
    }

    public int Par14
    {
      get { return _keyword.GetParameter(14).ToInt();}
      set { _keyword.GetParameter(14).Value = value;}
    }

    public int Par15
    {
      get { return _keyword.GetParameter(15).ToInt();}
      set { _keyword.GetParameter(15).Value = value;}
    }

  }
}
