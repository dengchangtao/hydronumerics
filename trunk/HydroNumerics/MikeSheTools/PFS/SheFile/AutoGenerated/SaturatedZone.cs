using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace HydroNumerics.MikeSheTools.PFS.SheFile
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class SaturatedZone: PFSMapper
  {

    private GeoUnitsSZProperties _geoUnitsSZProperties;
    private GeoLayersSZ _geoLayersSZ;
    private GeoLensesSZ _geoLensesSZ;
    private SatWQLayers _satWQLayers;
    private CompLayersSZ _compLayersSZ;
    private CompLayersGeoSZ _compLayersGeoSZ;
    private Drainage _drainage;
    private Background _drainage_Modflow;
    private RiverMF _well;
    private Topography _headElevationUsedForAirFlow;

    internal SaturatedZone(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "GeoUnitsSZProperties":
          _geoUnitsSZProperties = new GeoUnitsSZProperties(sub);
          break;
        case "GeoLayersSZ":
          _geoLayersSZ = new GeoLayersSZ(sub);
          break;
        case "GeoLensesSZ":
          _geoLensesSZ = new GeoLensesSZ(sub);
          break;
        case "SatWQLayers":
          _satWQLayers = new SatWQLayers(sub);
          break;
        case "CompLayersSZ":
          _compLayersSZ = new CompLayersSZ(sub);
          break;
        case "CompLayersGeoSZ":
          _compLayersGeoSZ = new CompLayersGeoSZ(sub);
          break;
        case "Drainage":
          _drainage = new Drainage(sub);
          break;
        case "Drainage_Modflow":
          _drainage_Modflow = new Background(sub);
          break;
        case "Well":
          _well = new RiverMF(sub);
          break;
        case "HeadElevationUsedForAirFlow":
          _headElevationUsedForAirFlow = new Topography(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public GeoUnitsSZProperties GeoUnitsSZProperties
    {
     get { return _geoUnitsSZProperties; }
    }

    public GeoLayersSZ GeoLayersSZ
    {
     get { return _geoLayersSZ; }
    }

    public GeoLensesSZ GeoLensesSZ
    {
     get { return _geoLensesSZ; }
    }

    public SatWQLayers SatWQLayers
    {
     get { return _satWQLayers; }
    }

    public CompLayersSZ CompLayersSZ
    {
     get { return _compLayersSZ; }
    }

    public CompLayersGeoSZ CompLayersGeoSZ
    {
     get { return _compLayersGeoSZ; }
    }

    public Drainage Drainage
    {
     get { return _drainage; }
    }

    public Background Drainage_Modflow
    {
     get { return _drainage_Modflow; }
    }

    public RiverMF Well
    {
     get { return _well; }
    }

    public Topography HeadElevationUsedForAirFlow
    {
     get { return _headElevationUsedForAirFlow; }
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

    public int IsDataUsedInSetup
    {
      get
      {
        return _pfsHandle.GetKeyword("IsDataUsedInSetup", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("IsDataUsedInSetup", 1).GetParameter(1).Value = value;
      }
    }

    public int DispersionOption
    {
      get
      {
        return _pfsHandle.GetKeyword("DispersionOption", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("DispersionOption", 1).GetParameter(1).Value = value;
      }
    }

    public int Drainage1
    {
      get
      {
        return _pfsHandle.GetKeyword("Drainage", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Drainage", 1).GetParameter(1).Value = value;
      }
    }

    public int TypeOfGeoParaDistribution
    {
      get
      {
        return _pfsHandle.GetKeyword("TypeOfGeoParaDistribution", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("TypeOfGeoParaDistribution", 1).GetParameter(1).Value = value;
      }
    }

    public int TypeOfVerDiscr
    {
      get
      {
        return _pfsHandle.GetKeyword("TypeOfVerDiscr", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("TypeOfVerDiscr", 1).GetParameter(1).Value = value;
      }
    }

    public int Wells
    {
      get
      {
        return _pfsHandle.GetKeyword("Wells", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Wells", 1).GetParameter(1).Value = value;
      }
    }

    public int FractureWells
    {
      get
      {
        return _pfsHandle.GetKeyword("FractureWells", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("FractureWells", 1).GetParameter(1).Value = value;
      }
    }

    public double MimimumLayerThickness
    {
      get
      {
        return _pfsHandle.GetKeyword("MimimumLayerThickness", 1).GetParameter(1).ToDouble();
      }
      set
      {
        _pfsHandle.GetKeyword("MimimumLayerThickness", 1).GetParameter(1).Value = value;
      }
    }

    public int id
    {
      get
      {
        return _pfsHandle.GetKeyword("id", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("id", 1).GetParameter(1).Value = value;
      }
    }

    public int TypeAmbientWaterHeadInput
    {
      get
      {
        return _pfsHandle.GetKeyword("TypeAmbientWaterHeadInput", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("TypeAmbientWaterHeadInput", 1).GetParameter(1).Value = value;
      }
    }

    public int TypeOfRelPerm
    {
      get
      {
        return _pfsHandle.GetKeyword("TypeOfRelPerm", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("TypeOfRelPerm", 1).GetParameter(1).Value = value;
      }
    }

    public int Threshold
    {
      get
      {
        return _pfsHandle.GetKeyword("Threshold", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Threshold", 1).GetParameter(1).Value = value;
      }
    }

    public int MassTransferToImmobileWater
    {
      get
      {
        return _pfsHandle.GetKeyword("MassTransferToImmobileWater", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("MassTransferToImmobileWater", 1).GetParameter(1).Value = value;
      }
    }

  }
}
