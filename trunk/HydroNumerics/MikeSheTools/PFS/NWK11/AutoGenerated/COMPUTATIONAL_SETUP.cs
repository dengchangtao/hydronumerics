using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;
using HydroNumerics.MikeSheTools.PFS.SheFile;

namespace HydroNumerics.MikeSheTools.PFS.NWK11
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class COMPUTATIONAL_SETUP: PFSMapper
  {


    internal COMPUTATIONAL_SETUP(PFSSection Section)
    {
      _pfsHandle = Section;

      branchs = new List<branch1>();
      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
          default:
            if (sub.Name.Substring(0,6).Equals("branch"))
            {
              branchs.Add(new branch1(sub));
              break;
            }
            _unMappedSections.Add(sub.Name);
          break;
        }
      }

    }

    public COMPUTATIONAL_SETUP()
    {
      _pfsHandle = new PFSSection("COMPUTATIONAL_SETUP");

      _pfsHandle.AddKeyword(new PFSKeyword("SaveAllGridPoints", PFSParameterType.Boolean, true));
    }

    public List<branch1> branchs {get; private set;}

    public bool SaveAllGridPoints1
    {
      get
      {
        return _pfsHandle.GetKeyword("SaveAllGridPoints", 1).GetParameter(1).ToBoolean();
      }
      set
      {
        _pfsHandle.GetKeyword("SaveAllGridPoints", 1).GetParameter(1).Value = value;
      }
    }

  }
}
