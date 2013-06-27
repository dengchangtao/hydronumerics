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
  public partial class msm_ST: PFSMapper
  {

    private Metadata2 _metadata;
    private Rows2 _rows;

    internal msm_ST(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "Metadata":
          _metadata = new Metadata2(sub);
          break;
        case "Rows":
          _rows = new Rows2(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public Metadata2 Metadata
    {
     get { return _metadata; }
    }

    public Rows2 Rows
    {
     get { return _rows; }
    }

  }
}
