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
  public partial class MOUSE_Computation: PFSMapper
  {


    internal MOUSE_Computation(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "MOUSE_Files":
          MOUSE_Files = new MOUSE_Files(sub);
          break;
        case "MOUSE_HD_parameters":
          MOUSE_HD_parameters = new MOUSE_HD_parameters(sub);
          break;
        case "MOUSE_RUNOFF_parameters":
          MOUSE_RUNOFF_parameters = new MOUSE_RUNOFF_parameters(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }

    }

    public MOUSE_Computation(string pfsname)
    {
      _pfsHandle = new PFSSection(pfsname);

      _pfsHandle.AddKeyword(new PFSKeyword("Computation_Type", PFSParameterType.Integer, 0));

      _pfsHandle.AddKeyword(new PFSKeyword("Language_Type", PFSParameterType.Integer, 0));

      _pfsHandle.AddKeyword(new PFSKeyword("Unit_Type", PFSParameterType.Integer, 0));

      MOUSE_Files = new MOUSE_Files("MOUSE_Files" );
      _pfsHandle.AddSection(MOUSE_Files._pfsHandle);

      MOUSE_HD_parameters = new MOUSE_HD_parameters("MOUSE_HD_parameters" );
      _pfsHandle.AddSection(MOUSE_HD_parameters._pfsHandle);

      MOUSE_RUNOFF_parameters = new MOUSE_RUNOFF_parameters("MOUSE_RUNOFF_parameters" );
      _pfsHandle.AddSection(MOUSE_RUNOFF_parameters._pfsHandle);

    }

    public MOUSE_Files MOUSE_Files{get; private set;}

    public MOUSE_HD_parameters MOUSE_HD_parameters{get; private set;}

    public MOUSE_RUNOFF_parameters MOUSE_RUNOFF_parameters{get; private set;}

    public int Computation_Type
    {
      get
      {
        return _pfsHandle.GetKeyword("Computation_Type", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Computation_Type", 1).GetParameter(1).Value = value;
      }
    }

    public int Language_Type
    {
      get
      {
        return _pfsHandle.GetKeyword("Language_Type", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Language_Type", 1).GetParameter(1).Value = value;
      }
    }

    public int Unit_Type
    {
      get
      {
        return _pfsHandle.GetKeyword("Unit_Type", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Unit_Type", 1).GetParameter(1).Value = value;
      }
    }

  }
}
