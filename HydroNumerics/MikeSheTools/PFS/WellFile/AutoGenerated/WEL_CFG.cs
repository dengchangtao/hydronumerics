using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;
using HydroNumerics.MikeSheTools.PFS.SheFile;

namespace HydroNumerics.MikeSheTools.PFS.Well
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class WEL_CFG: PFSMapper
  {


    internal WEL_CFG(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "WELLDATA":
          WELLDATA = new WELLDATA(sub);
          break;
        case "WELLFIELD":
          WELLFIELD = new WELLFIELD(sub);
          break;
        case "Foreground":
          Foreground = new Foreground(sub);
          break;
        case "Background":
          Background = new Foreground(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }

    }

    public WEL_CFG(string pfsname)
    {
      _pfsHandle = new PFSSection(pfsname);

      WELLDATA = new WELLDATA("WELLDATA" );
      _pfsHandle.AddSection(WELLDATA._pfsHandle);

      WELLFIELD = new WELLFIELD("WELLFIELD" );
      _pfsHandle.AddSection(WELLFIELD._pfsHandle);

      Foreground = new Foreground("Foreground" );
      _pfsHandle.AddSection(Foreground._pfsHandle);

      Background = new Foreground("Background" );
      _pfsHandle.AddSection(Background._pfsHandle);

    }

    public WELLDATA WELLDATA{get; private set;}

    public WELLFIELD WELLFIELD{get; private set;}

    public Foreground Foreground{get; private set;}

    public Foreground Background{get; private set;}

  }
}
