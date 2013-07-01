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
  public partial class MSM_BITEM: PFSMapper
  {


    internal MSM_BITEM(PFSSection Section)
    {
      _pfsHandle = Section;

      BITEMs = new List<BITEM>();
      for (int i = 1; i <= Section.GetKeywordsNo("BITEM"); i++)
        BITEMs.Add(new BITEM(Section.GetKeyword("BITEM",i)));
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

      BITEMHeader = new BITEMHeader(_pfsHandle.GetKeyword("BITEMHeader", 1));
    }

    public MSM_BITEM()
    {
      _pfsHandle = new PFSSection("MSM_BITEM");

      _pfsHandle.AddKeyword(new PFSKeyword("SYNTAX_VERSION", PFSParameterType.Integer, 0));
      _pfsHandle.AddKeyword(new PFSKeyword("UNIT_TYPE", PFSParameterType.Integer, 0));
    }

    public BITEMHeader BITEMHeader{get; private set;}
    public List<BITEM> BITEMs {get; private set;}
    public int SYNTAX_VERSION1
    {
      get
      {
        return _pfsHandle.GetKeyword("SYNTAX_VERSION", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("SYNTAX_VERSION", 1).GetParameter(1).Value = value;
      }
    }

    public int UNIT_TYPE1
    {
      get
      {
        return _pfsHandle.GetKeyword("UNIT_TYPE", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("UNIT_TYPE", 1).GetParameter(1).Value = value;
      }
    }

  }
}
