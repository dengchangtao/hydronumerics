using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace HydroNumerics.MikeSheTools.PFS.SheFile
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class CompControlParaModLMG: PFSMapper
  {


    internal CompControlParaModLMG(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public int Touched
    {
      get
      {
        return _pfsHandle.GetKeyword("Touched", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Touched", 1).GetParameter(1).Value = value;
      }
    }

    public int IsDataUsedInSetup
    {
      get
      {
        return _pfsHandle.GetKeyword("IsDataUsedInSetup", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("IsDataUsedInSetup", 1).GetParameter(1).Value = value;
      }
    }

    public int MXITER
    {
      get
      {
        return _pfsHandle.GetKeyword("MXITER", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("MXITER", 1).GetParameter(1).Value = value;
      }
    }

    public int MXCYC
    {
      get
      {
        return _pfsHandle.GetKeyword("MXCYC", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("MXCYC", 1).GetParameter(1).Value = value;
      }
    }

    public double BCLOSE
    {
      get
      {
        return _pfsHandle.GetKeyword("BCLOSE", 1).GetParameter(1).ToDouble();
      }
      set
      {
        _pfsHandle.GetKeyword("BCLOSE", 1).GetParameter(1).Value = value;
      }
    }

    public int DAMP
    {
      get
      {
        return _pfsHandle.GetKeyword("DAMP", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("DAMP", 1).GetParameter(1).Value = value;
      }
    }

    public int DUP
    {
      get
      {
        return _pfsHandle.GetKeyword("DUP", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("DUP", 1).GetParameter(1).Value = value;
      }
    }

    public double DLOW
    {
      get
      {
        return _pfsHandle.GetKeyword("DLOW", 1).GetParameter(1).ToDouble();
      }
      set
      {
        _pfsHandle.GetKeyword("DLOW", 1).GetParameter(1).Value = value;
      }
    }

    public int STOR1
    {
      get
      {
        return _pfsHandle.GetKeyword("STOR1", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("STOR1", 1).GetParameter(1).Value = value;
      }
    }

    public double STOR2
    {
      get
      {
        return _pfsHandle.GetKeyword("STOR2", 1).GetParameter(1).ToDouble();
      }
      set
      {
        _pfsHandle.GetKeyword("STOR2", 1).GetParameter(1).Value = value;
      }
    }

    public double STOR3
    {
      get
      {
        return _pfsHandle.GetKeyword("STOR3", 1).GetParameter(1).ToDouble();
      }
      set
      {
        _pfsHandle.GetKeyword("STOR3", 1).GetParameter(1).Value = value;
      }
    }

    public int ICG
    {
      get
      {
        return _pfsHandle.GetKeyword("ICG", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ICG", 1).GetParameter(1).Value = value;
      }
    }

    public int UseDefault
    {
      get
      {
        return _pfsHandle.GetKeyword("UseDefault", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("UseDefault", 1).GetParameter(1).Value = value;
      }
    }

    public int IOUTAMG
    {
      get
      {
        return _pfsHandle.GetKeyword("IOUTAMG", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("IOUTAMG", 1).GetParameter(1).Value = value;
      }
    }

  }
}
