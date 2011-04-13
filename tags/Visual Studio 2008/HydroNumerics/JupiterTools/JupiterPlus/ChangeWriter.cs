﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using HydroNumerics.JupiterTools;


namespace HydroNumerics.JupiterTools.JupiterPlus
{
  public class ChangeWriter
  {
    XDocument _changes;
    XElement _currentChanges;

    public ChangeWriter()
    {
      _changes = new XDocument();
      _changes.Add(new XElement("ChangeItems"));
    }

    public void AddChangeItem(string User, string Project, DateTime TimeOfChange)
    {
      _currentChanges = new XElement("Changes");
      _changes.Element("ChangeItems").Add(new XElement("ChangeItem",
        new XElement("User", User),
        new XElement("Project", Project),
        new XElement("Time", TimeOfChange.ToShortDateString()),
        _currentChanges));
    }

    public void Save(string FileName)
    {
      _changes.Save(FileName);
    }

    public void AddDeleteIntakeFromPlant(int PlantID, string WellId, int IntakeNo)
    {
      XElement X = new XElement("Change", new XElement("Table", "DRWPLANTINTAKE"), new XAttribute("Action", "DeleteRow"),
          new XElement("PrimaryKeys", new XElement("Key", PlantID, new XAttribute("Name", "PLANTID")),
            new XElement("Key", WellId, new XAttribute("Name", "BOREHOLENO")),
            new XElement("Key", IntakeNo, new XAttribute("Name", "INTAKENO"))),
          new XElement("Columns"));
      _currentChanges.Add(X);

    }


    public void AddWellX(string WellID, double NewValue)
    {
      GetBoreHoleColumn(WellID).Add(new XElement("Name", "XUTM"), new XElement("OldValue", null), new XElement("NewValue", NewValue.ToString()));
    }

    public void AddWellY(string WellID, double NewValue)
    {
      GetBoreHoleColumn(WellID).Add(new XElement("Name", "YUTM"), new XElement("OldValue", null), new XElement("NewValue", NewValue.ToString()));
    }

    public void AddWellTerrain(string WellID, double NewValue)
    {
      GetBoreHoleColumn(WellID).Add(new XElement("Name", "ELEVATION"), new XElement("OldValue", null), new XElement("NewValue", NewValue.ToString()));
    }

    private XElement GetBoreHoleColumn(string WellID)
    {
      XElement X = _currentChanges.Elements().FirstOrDefault(var => var.Element("PrimaryKeys").Element("Key").Value == WellID);
      if (X == null)
      {
        X = new XElement("Change", new XAttribute("Action", "AlterRow"), 
          new XElement("Table", "BOREHOLE"), 
          new XElement("Comment", "Kommentar"),
          new XElement("PrimaryKeys", new XElement("Key", WellID, new XAttribute("Name", "BOREHOLEID"))),
          new XElement("Columns"));
        _currentChanges.Add(X);
      }
      XElement newColumn = new XElement("Column");
      X.Element("Columns").Add(newColumn );
      return newColumn;
      
    }

    public override string ToString()
    {
      return _changes.ToString();
    }


  }
}