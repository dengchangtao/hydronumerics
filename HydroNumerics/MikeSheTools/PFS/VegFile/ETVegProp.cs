using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;
using HydroNumerics.MikeSheTools.PFS.SheFile;

namespace HydroNumerics.MikeSheTools.PFS.VegetationFile
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class ETVegProp: PFSMapper
  {

    private VEGSETUP _vEGSETUP;

    internal ETVegProp(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "VEGSETUP":
          _vEGSETUP = new VEGSETUP(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public VEGSETUP VEGSETUP
    {
     get { return _vEGSETUP; }
    }

    public int FileVersion
    {
      get
      {
        return _pfsHandle.GetKeyword("FileVersion", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("FileVersion", 1).GetParameter(1).Value = value;
      }
    }

  }
}
