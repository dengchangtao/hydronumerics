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
  public partial class TIME_SERIES_FILE: PFSMapper
  {


    internal TIME_SERIES_FILE(PFSSection Section)
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

    public TIME_SERIES_FILE(string pfsname)
    {
      _pfsHandle = new PFSSection(pfsname);

      _pfsHandle.AddKeyword(new PFSKeyword("Touched", PFSParameterType.Integer, 0));

      _pfsHandle.AddKeyword(new PFSKeyword("FILE_NAME", PFSParameterType.FileName, ""));

      _pfsHandle.AddKeyword(new PFSKeyword("ITEM_COUNT", PFSParameterType.Integer, 0));

      _pfsHandle.AddKeyword(new PFSKeyword("ITEM_NUMBERS", PFSParameterType.Integer, 0));

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

    public string FILE_NAME
    {
      get
      {
        return _pfsHandle.GetKeyword("FILE_NAME", 1).GetParameter(1).ToString();
      }
      set
      {
        _pfsHandle.GetKeyword("FILE_NAME", 1).GetParameter(1).Value = value;
      }
    }

    public int ITEM_COUNT
    {
      get
      {
        return _pfsHandle.GetKeyword("ITEM_COUNT", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ITEM_COUNT", 1).GetParameter(1).Value = value;
      }
    }

    public int ITEM_NUMBERS
    {
      get
      {
        return _pfsHandle.GetKeyword("ITEM_NUMBERS", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ITEM_NUMBERS", 1).GetParameter(1).Value = value;
      }
    }

  }
}
