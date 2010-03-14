using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace HydroNumerics.MikeSheTools.PFS.SheFile
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class CompControlParaModPCG4: PFSMapper
  {


    internal CompControlParaModPCG4(PFSSection Section)
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

    public int ITERI
    {
      get
      {
        return _pfsHandle.GetKeyword("ITERI", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ITERI", 1).GetParameter(1).Value = value;
      }
    }

    public double HCLOSE
    {
      get
      {
        return _pfsHandle.GetKeyword("HCLOSE", 1).GetParameter(1).ToDouble();
      }
      set
      {
        _pfsHandle.GetKeyword("HCLOSE", 1).GetParameter(1).Value = value;
      }
    }

    public double BFACT
    {
      get
      {
        return _pfsHandle.GetKeyword("BFACT", 1).GetParameter(1).ToDouble();
      }
      set
      {
        _pfsHandle.GetKeyword("BFACT", 1).GetParameter(1).Value = value;
      }
    }

    public int RESRED
    {
      get
      {
        return _pfsHandle.GetKeyword("RESRED", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("RESRED", 1).GetParameter(1).Value = value;
      }
    }

    public int MNORTH
    {
      get
      {
        return _pfsHandle.GetKeyword("MNORTH", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("MNORTH", 1).GetParameter(1).Value = value;
      }
    }

    public int ApplyDampening
    {
      get
      {
        return _pfsHandle.GetKeyword("ApplyDampening", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ApplyDampening", 1).GetParameter(1).Value = value;
      }
    }

    public int UseNewtonRaphson
    {
      get
      {
        return _pfsHandle.GetKeyword("UseNewtonRaphson", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("UseNewtonRaphson", 1).GetParameter(1).Value = value;
      }
    }

    public int INEWT
    {
      get
      {
        return _pfsHandle.GetKeyword("INEWT", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("INEWT", 1).GetParameter(1).Value = value;
      }
    }

    public int IDMPBOT
    {
      get
      {
        return _pfsHandle.GetKeyword("IDMPBOT", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("IDMPBOT", 1).GetParameter(1).Value = value;
      }
    }

    public int MUTPCG
    {
      get
      {
        return _pfsHandle.GetKeyword("MUTPCG", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("MUTPCG", 1).GetParameter(1).Value = value;
      }
    }

    public int OutputFromSolver
    {
      get
      {
        return _pfsHandle.GetKeyword("OutputFromSolver", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("OutputFromSolver", 1).GetParameter(1).Value = value;
      }
    }

  }
}
