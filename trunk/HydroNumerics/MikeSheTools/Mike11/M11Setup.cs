﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HydroNumerics.MikeSheTools.DFS;

using DHI.Generic;
using DHI.Mike1D.CrossSections;

namespace HydroNumerics.MikeSheTools.Mike11
{
  public class M11Setup
  {
    public Network network { get; private set; }
    private List<CrossSection> _crossSections = new List<CrossSection>();

    /// <summary>
    /// Reads the network from a NWK11-file
    /// </summary>
    /// <param name="Nwk11FileName"></param>
    public void ReadNetwork(string Nwk11FileName)
    {
      network = new Network(Nwk11FileName);
    }

    /// <summary>
    /// Reads the cross sections from an .xns-file
    /// </summary>
    /// <param name="xnsFile"></param>
    public void ReadCrossSections(string xnsFile)
    {
      DFS0 d = new DFS0(xnsFile);

      CrossSectionCollection csc = new CrossSectionCollection();
      csc.Connection.FilePath = xnsFile;
      csc.Connection.Bridge = csc.Connection.AvailableBridges[0];
      csc.Connection.Open(false);

      //Now loop the cross sections
      foreach (var cs in csc.CrossSections)
      {
        //Create a HydroNumerics.MikeSheTools.Mike11.CrossSection from the M11-CrossSection
        CrossSection MyCs = new CrossSection(cs);
        _crossSections.Add(MyCs);
        CombineNetworkAndCrossSections(MyCs);
      }

      d.Dispose();
    }

    public void WriteToShape(string FilePrefix)
    {
      network.WriteToShape(Path.GetFileNameWithoutExtension(FilePrefix));
    }


    /// <summary>
    /// Adds the cross section to the correct branch
    /// </summary>
    /// <param name="MyCs"></param>
    private void CombineNetworkAndCrossSections(CrossSection MyCs)
    {
      if (network != null)
      {
        //Finds the branch with the correct name, topoid, and chainage interval
        M11Branch mb = network.Branches.FirstOrDefault(var => var.Name == MyCs.BranchName & var.TopoID == MyCs.TopoID & var.ChainageStart < MyCs.Chainage & var.ChainageEnd > MyCs.Chainage);
        if (mb != null)
          mb.AddCrossection(MyCs);
      } 
    }

  }
}
