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
  public partial class MOUSE_RTC_SENSORS: PFSMapper
  {


    internal MOUSE_RTC_SENSORS(PFSSection Section)
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
