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
  public partial class FRC_INFLOW1: PFSMapper
  {


    internal FRC_INFLOW1(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "OPERATOR":
          OPERATOR = new OPERATOR(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }

      Q_LIMIT = new Q_LIMIT(_pfsHandle.GetKeyword("Q_LIMIT", 1));
    }

    public FRC_INFLOW1(string pfsname)
    {
      _pfsHandle = new PFSSection(pfsname);

      Q_LIMIT = new Q_LIMIT("Q_LIMIT");
      _pfsHandle.AddKeyword(Q_LIMIT._keyword);
      OPERATOR = new OPERATOR("OPERATOR" );
      _pfsHandle.AddSection(OPERATOR._pfsHandle);

    }

    public OPERATOR OPERATOR{get; private set;}

    public Q_LIMIT Q_LIMIT{get; private set;}
  }
}