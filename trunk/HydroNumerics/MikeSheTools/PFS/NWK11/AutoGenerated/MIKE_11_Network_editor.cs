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
  public partial class MIKE_11_Network_editor: PFSMapper
  {


    internal MIKE_11_Network_editor(PFSSection Section)
    {
      _pfsHandle = Section;

      FLOOD_QQs = new List<WEIR>();
      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "FORMAT_VERSION":
          FORMAT_VERSION = new FORMAT_VERSION(sub);
          break;
        case "DATA_AREA":
          DATA_AREA = new DATA_AREA(sub);
          break;
        case "POINTS":
          POINTS = new POINTS(sub);
          break;
        case "BRANCHES":
          BRANCHES = new BRANCHES(sub);
          break;
        case "STRUCTURE_MODULE":
          STRUCTURE_MODULE = new STRUCTURE_MODULE(sub);
          break;
        case "CATCHMENT":
          CATCHMENT = new WEIR(sub);
          break;
        case "COMPUTATIONAL_SETUP":
          COMPUTATIONAL_SETUP = new COMPUTATIONAL_SETUP(sub);
          break;
        case "MIKESHECOUPLING":
          MIKESHECOUPLING = new MIKESHECOUPLING1(sub);
          break;
        case "CHANNELROUTING":
          CHANNELROUTING = new WEIR(sub);
          break;
        case "DIVERSION":
          DIVERSION = new WEIR(sub);
          break;
        case "ALIGNMENTLINE":
          ALIGNMENTLINE = new WEIR(sub);
          break;
        case "JUNCTION":
          JUNCTION = new WEIR(sub);
          break;
        case "MIKE11_ROUTING":
          MIKE11_ROUTING = new MIKE11_ROUTING(sub);
          break;
        case "MODFLOW":
          MODFLOW = new MODFLOW(sub);
          break;
        case "LAYER_FILES":
          LAYER_FILES = new LAYER_FILES(sub);
          break;
          default:
            if (sub.Name.Substring(0,6).Equals("FLOOD_"))
            {
              FLOOD_QQs.Add(new WEIR(sub));
              break;
            }
            _unMappedSections.Add(sub.Name);
          break;
        }
      }

    }

    public MIKE_11_Network_editor(string pfsname)
    {
      _pfsHandle = new PFSSection(pfsname);

      FLOOD_QQs = new List<WEIR>();
      FORMAT_VERSION = new FORMAT_VERSION("FORMAT_VERSION" );
      _pfsHandle.AddSection(FORMAT_VERSION._pfsHandle);

      DATA_AREA = new DATA_AREA("DATA_AREA" );
      _pfsHandle.AddSection(DATA_AREA._pfsHandle);

      POINTS = new POINTS("POINTS" );
      _pfsHandle.AddSection(POINTS._pfsHandle);

      BRANCHES = new BRANCHES("BRANCHES" );
      _pfsHandle.AddSection(BRANCHES._pfsHandle);

      STRUCTURE_MODULE = new STRUCTURE_MODULE("STRUCTURE_MODULE" );
      _pfsHandle.AddSection(STRUCTURE_MODULE._pfsHandle);

      CATCHMENT = new WEIR("CATCHMENT" );
      _pfsHandle.AddSection(CATCHMENT._pfsHandle);

      COMPUTATIONAL_SETUP = new COMPUTATIONAL_SETUP("COMPUTATIONAL_SETUP" );
      _pfsHandle.AddSection(COMPUTATIONAL_SETUP._pfsHandle);

      MIKESHECOUPLING = new MIKESHECOUPLING1("MIKESHECOUPLING" );
      _pfsHandle.AddSection(MIKESHECOUPLING._pfsHandle);

      CHANNELROUTING = new WEIR("CHANNELROUTING" );
      _pfsHandle.AddSection(CHANNELROUTING._pfsHandle);

      DIVERSION = new WEIR("DIVERSION" );
      _pfsHandle.AddSection(DIVERSION._pfsHandle);

      ALIGNMENTLINE = new WEIR("ALIGNMENTLINE" );
      _pfsHandle.AddSection(ALIGNMENTLINE._pfsHandle);

      JUNCTION = new WEIR("JUNCTION" );
      _pfsHandle.AddSection(JUNCTION._pfsHandle);

      MIKE11_ROUTING = new MIKE11_ROUTING("MIKE11_ROUTING" );
      _pfsHandle.AddSection(MIKE11_ROUTING._pfsHandle);

      MODFLOW = new MODFLOW("MODFLOW" );
      _pfsHandle.AddSection(MODFLOW._pfsHandle);

      LAYER_FILES = new LAYER_FILES("LAYER_FILES" );
      _pfsHandle.AddSection(LAYER_FILES._pfsHandle);

    }

    public FORMAT_VERSION FORMAT_VERSION{get; private set;}

    public DATA_AREA DATA_AREA{get; private set;}

    public POINTS POINTS{get; private set;}

    public BRANCHES BRANCHES{get; private set;}

    public STRUCTURE_MODULE STRUCTURE_MODULE{get; private set;}

    public WEIR CATCHMENT{get; private set;}

    public COMPUTATIONAL_SETUP COMPUTATIONAL_SETUP{get; private set;}

    public MIKESHECOUPLING1 MIKESHECOUPLING{get; private set;}

    public WEIR CHANNELROUTING{get; private set;}

    public WEIR DIVERSION{get; private set;}

    public WEIR ALIGNMENTLINE{get; private set;}

    public WEIR JUNCTION{get; private set;}

    public MIKE11_ROUTING MIKE11_ROUTING{get; private set;}

    public MODFLOW MODFLOW{get; private set;}

    public LAYER_FILES LAYER_FILES{get; private set;}

    public List<WEIR> FLOOD_QQs {get; private set;}

  }
}
