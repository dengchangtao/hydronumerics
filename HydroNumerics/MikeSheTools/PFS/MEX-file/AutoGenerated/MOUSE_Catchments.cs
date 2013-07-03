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
  public partial class MOUSE_Catchments: PFSMapper
  {


    internal MOUSE_Catchments(PFSSection Section)
    {
      _pfsHandle = Section;

      Lines = new List<Line>();
      for (int i = 1; i <= Section.GetKeywordsNo("Line"); i++)
        Lines.Add(new Line(Section.GetKeyword("Line",i)));
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

      LineHeader = new LineHeader(_pfsHandle.GetKeyword("LineHeader", 1));
    }

    public MOUSE_Catchments(string pfsname)
    {
      _pfsHandle = new PFSSection(pfsname);

      Lines = new List<Line>();
      _pfsHandle.AddKeyword(new PFSKeyword("SYNTAX_VERSION", PFSParameterType.Integer, 0));

      _pfsHandle.AddKeyword(new PFSKeyword("UNIT_TYPE", PFSParameterType.Integer, 0));

      LineHeader = new LineHeader("LineHeader");
      _pfsHandle.AddKeyword(LineHeader._keyword);
    }

    public LineHeader LineHeader{get; private set;}
    public List<Line> Lines {get; private set;}
    public int SYNTAX_VERSION
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

    public int UNIT_TYPE
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
