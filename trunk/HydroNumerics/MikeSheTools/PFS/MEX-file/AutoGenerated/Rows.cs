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
  public partial class Rows: PFSMapper
  {


    internal Rows(PFSSection Section)
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

      rHeader = new rHeader(_pfsHandle.GetKeyword("rHeader", 1));
      r = new r(_pfsHandle.GetKeyword("r", 1));
    }

    public Rows(string pfsname)
    {
      _pfsHandle = new PFSSection(pfsname);

      rHeader = new rHeader("rHeader");
      _pfsHandle.AddKeyword(rHeader._keyword);
      r = new r("r");
      _pfsHandle.AddKeyword(r._keyword);
    }

    public rHeader rHeader{get; private set;}
    public r r{get; private set;}
  }
}
