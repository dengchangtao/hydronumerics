using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace HydroNumerics.MikeSheTools.PFS.SheFile
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class CompControlParaSZ: PFSMapper
  {

    private CompControlParaSZPCGTrans _compControlParaSZPCGTrans;
    private CompControlParaSZPCGTrans _compControlParaSZPCGSteady;
    private CompControlParaSZSOR _compControlParaSZSOR;
    private CompControlParaRivEx _compControlParaRivEx;

    internal CompControlParaSZ(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "CompControlParaSZPCGTrans":
          _compControlParaSZPCGTrans = new CompControlParaSZPCGTrans(sub);
          break;
        case "CompControlParaSZPCGSteady":
          _compControlParaSZPCGSteady = new CompControlParaSZPCGTrans(sub);
          break;
        case "CompControlParaSZSOR":
          _compControlParaSZSOR = new CompControlParaSZSOR(sub);
          break;
        case "CompControlParaRivEx":
          _compControlParaRivEx = new CompControlParaRivEx(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public CompControlParaSZPCGTrans CompControlParaSZPCGTrans
    {
     get { return _compControlParaSZPCGTrans; }
    }

    public CompControlParaSZPCGTrans CompControlParaSZPCGSteady
    {
     get { return _compControlParaSZPCGSteady; }
    }

    public CompControlParaSZSOR CompControlParaSZSOR
    {
     get { return _compControlParaSZSOR; }
    }

    public CompControlParaRivEx CompControlParaRivEx
    {
     get { return _compControlParaRivEx; }
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

    public int SolverOption
    {
      get
      {
        return _pfsHandle.GetKeyword("SolverOption", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("SolverOption", 1).GetParameter(1).Value = value;
      }
    }

  }
}
