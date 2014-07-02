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
  public partial class Foreground: PFSMapper
  {


    internal Foreground(PFSSection Section)
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

    public Foreground(string pfsname)
    {
      _pfsHandle = new PFSSection(pfsname);

      _pfsHandle.AddKeyword(new PFSKeyword("Touched", PFSParameterType.Integer, 0));

      _pfsHandle.AddKeyword(new PFSKeyword("NumberOfLayers", PFSParameterType.Integer, 0));

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

    public int NumberOfLayers
    {
      get
      {
        return _pfsHandle.GetKeyword("NumberOfLayers", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("NumberOfLayers", 1).GetParameter(1).Value = value;
      }
    }

  }
}
