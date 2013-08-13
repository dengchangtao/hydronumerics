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
  public partial class PROPERTIES: PFSMapper
  {


    internal PROPERTIES(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "MIKEZero_Shape_Polygon_Object":
          MIKEZero_Shape_Polygon_Object = new MIKEZero_Shape_Polygon_Object(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }

    }

    public PROPERTIES(string pfsname)
    {
      _pfsHandle = new PFSSection(pfsname);

      _pfsHandle.AddKeyword(new PFSKeyword("ProjectionType", PFSParameterType.String, ""));

      _pfsHandle.AddKeyword(new PFSKeyword("OverlayDrawingOrder", PFSParameterType.Integer, 0));

      MIKEZero_Shape_Polygon_Object = new MIKEZero_Shape_Polygon_Object("MIKEZero_Shape_Polygon_Object" );
      _pfsHandle.AddSection(MIKEZero_Shape_Polygon_Object._pfsHandle);

    }

    public MIKEZero_Shape_Polygon_Object MIKEZero_Shape_Polygon_Object{get; private set;}

    public string ProjectionType
    {
      get
      {
        return _pfsHandle.GetKeyword("ProjectionType", 1).GetParameter(1).ToString();
      }
      set
      {
        _pfsHandle.GetKeyword("ProjectionType", 1).GetParameter(1).Value = value;
      }
    }

    public int OverlayDrawingOrder
    {
      get
      {
        return _pfsHandle.GetKeyword("OverlayDrawingOrder", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("OverlayDrawingOrder", 1).GetParameter(1).Value = value;
      }
    }

  }
}