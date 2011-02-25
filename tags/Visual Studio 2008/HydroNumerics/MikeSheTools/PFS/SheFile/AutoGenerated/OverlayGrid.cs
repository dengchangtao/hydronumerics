using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace HydroNumerics.MikeSheTools.PFS.SheFile
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class OverlayGrid: PFSMapper
  {

    private DFS_2D_DATA_FILE _dFS_2D_DATA_FILE;

    internal OverlayGrid(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "DFS_2D_DATA_FILE":
          _dFS_2D_DATA_FILE = new DFS_2D_DATA_FILE(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public DFS_2D_DATA_FILE DFS_2D_DATA_FILE
    {
     get { return _dFS_2D_DATA_FILE; }
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

    public int Transparency
    {
      get
      {
        return _pfsHandle.GetKeyword("Transparency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Transparency", 1).GetParameter(1).Value = value;
      }
    }

    public int Display
    {
      get
      {
        return _pfsHandle.GetKeyword("Display", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Display", 1).GetParameter(1).Value = value;
      }
    }

  }
}
