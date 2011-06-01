﻿using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HydroNumerics.Core;
using HydroNumerics.Wells;
using HydroNumerics.Time.Core;

namespace HydroNumerics.JupiterTools
{
  public class Reader
  {

    private JupiterXL JXL;



    public Reader(string DataBaseFile)
    {
      JXL = new JupiterXL(DataBaseFile);
    }

    /// <summary>
    /// Disposes the dataset and closes the connection to the database.
    /// </summary>
    public void Dispose()
    {
      JXL.Dispose();
    }

    /// <summary>
    /// Read in water levels from a Jupiter access database. 
    /// Entries with blank dates of waterlevels are skipped.
    /// </summary>
    /// <param name="DataBaseFile"></param>
    /// <param name="CreateWells"></param>
    public void Waterlevels(IWellCollection Wells, bool OnlyRo)
    {
        JXL.ReadWaterLevels(OnlyRo);

        foreach (var WatLev in JXL.WATLEVEL)
        {
            //Find the well in the dictionary
            if (Wells.Contains(WatLev.BOREHOLENO))
            {
              IIntake I = Wells[WatLev.BOREHOLENO].Intakes.FirstOrDefault(var => var.IDNumber == WatLev.INTAKENO);
                if (I != null)
                    FillInWaterLevel(I, WatLev);
            }
        }
        JXL.WATLEVEL.Clear();

        foreach (Well W in Wells)
          foreach (Intake I in W.Intakes)
            I.HeadObservations.Sort();
    }
    

    /// <summary>
    /// Put the observation in the well
    /// </summary>
    /// <param name="CurrentWell"></param>
    /// <param name="WatLev"></param>
    private void FillInWaterLevel(IIntake CurrentIntake, JupiterXL.WATLEVELRow WatLev)
    {
      string Description;
      if (WatLev.IsSITUATIONNull())
        Description = "Unknown";
      else
      {
        if (WatLev.SITUATION == 0)
          Description = "Ro";
        else
          Description = "Drift";
      }

      if (!WatLev.IsTIMEOFMEASNull())
        if (!WatLev.IsWATLEVMSLNull())
          CurrentIntake.HeadObservations.Items.Add(new TimestampValue(WatLev.TIMEOFMEAS, WatLev.WATLEVMSL,Description));
        else if (!WatLev.IsWATLEVGRSUNull())
          CurrentIntake.HeadObservations.Items.Add(new TimestampValue(WatLev.TIMEOFMEAS, CurrentIntake.well.Terrain - WatLev.WATLEVGRSU,Description));

    }


    /// <summary>
    /// Read Extractions.
    /// The boolean set dates indicates whether the dates read from the DRWPLANTINTAKE table should be used as Pumpingstart
    /// and pumpingstop.
    /// </summary>
    /// <param name="Plants"></param>
    /// <param name="Wells"></param>
    public IPlantCollection ReadPlants(IWellCollection Wells)
    {
      List<Plant> Plants = new List<Plant>();
      IPlantCollection DPlants = new IPlantCollection();

      JXL.ReadExtractions();

      IIntake CurrentIntake = null;
      Plant CurrentPlant;

      List<Tuple<int, Plant>> SubPlants = new List<Tuple<int, Plant>>();


      foreach (var Anlaeg in JXL.DRWPLANT)
      {
        CurrentPlant = new Plant(Anlaeg.PLANTID);
        DPlants.Add(CurrentPlant);

        if (Anlaeg.IsPLANTNAMENull())
          CurrentPlant.Name = "<no name in database>";
        else
          CurrentPlant.Name = Anlaeg.PLANTNAME;

        CurrentPlant.Address = Anlaeg.PLANTADDRESS;

        CurrentPlant.PostalCode = Anlaeg.PLANTPOSTALCODE;

        if (!Anlaeg.IsPERMITDATENull())
          CurrentPlant.PermitDate = Anlaeg.PERMITDATE;

        if (!Anlaeg.IsPERMITEXPIREDATENull())
          CurrentPlant.PermitExpiryDate = Anlaeg.PERMITEXPIREDATE;

        CurrentPlant.Permit = Anlaeg.PERMITAMOUNT;

        if (!Anlaeg.IsSUPPLANTNull())
          SubPlants.Add(new Tuple<int, Plant>(Anlaeg.SUPPLANT, CurrentPlant));

        if (!Anlaeg.IsXUTMNull())
          CurrentPlant.X = Anlaeg.XUTM;

        if (!Anlaeg.IsYUTMNull())
          CurrentPlant.Y = Anlaeg.YUTM;



        //Loop the intakes. Only add intakes from wells already in table
        foreach (var IntakeData in Anlaeg.GetDRWPLANTINTAKERows())
        {
          if (Wells.Contains(IntakeData.BOREHOLENO))
          {
            JupiterWell jw = Wells[IntakeData.BOREHOLENO] as JupiterWell;
            CurrentIntake = jw.Intakes.FirstOrDefault(var => var.IDNumber == IntakeData.INTAKENO);
            if (CurrentIntake != null)
            {
              PumpingIntake CurrentPumpingIntake = new PumpingIntake(CurrentIntake, CurrentPlant);
              CurrentPlant.PumpingIntakes.Add(CurrentPumpingIntake);

              if (!IntakeData.IsSTARTDATENull())
                CurrentPumpingIntake.StartNullable = IntakeData.STARTDATE;
              else if (jw.StartDate.HasValue)
                CurrentPumpingIntake.StartNullable = jw.StartDate;
              else if (CurrentIntake.Screens.Where(var => var.StartDate.HasValue).Count() != 0)
                CurrentPumpingIntake.StartNullable = CurrentIntake.Screens.Where(var => var.StartDate.HasValue).Min(var => var.StartDate);

              if (!IntakeData.IsENDDATENull())
                CurrentPumpingIntake.EndNullable = IntakeData.ENDDATE;
              else if (jw.EndDate.HasValue)
                CurrentPumpingIntake.EndNullable = jw.EndDate;
              else if (CurrentIntake.Screens.Where(var => var.EndDate.HasValue).Count() != 0)
                CurrentPumpingIntake.EndNullable = CurrentIntake.Screens.Where(var => var.EndDate.HasValue).Max(var => var.EndDate);
            }
          }
        }
      }


      //Now attach the subplants
      foreach (Tuple<int, Plant> KVP in SubPlants)
      {
        Plant Upper;
        if (DPlants.TryGetValue(KVP.First, out Upper))
        {
          if (Upper == KVP.Second)
          {
            string l = "what";
          }
          else
          {
            Upper.SubPlants.Add(KVP.Second);
            foreach (PumpingIntake PI in KVP.Second.PumpingIntakes)
            {
              PumpingIntake d = Upper.PumpingIntakes.FirstOrDefault(var => var.Intake.well.ID == PI.Intake.well.ID);
              //Remove pumping intakes from upper plant if they are attached to lower plants.
              if (d != null)
                Upper.PumpingIntakes.Remove(d);
            }
          }
        }
      }

      return DPlants;
    }


    public int FillInExtractionWithCount(IPlantCollection Plants)
    {
      FillInExtraction(Plants);
      return JXL.INTAKECATCHMENT.Count + JXL.WRRCATCHMENT.Count;
    }


    public void FillInExtraction(IPlantCollection Plants)
    {

      JXL.ReadExtractions();

      Plant CurrentPlant;
      //Loop the extractions
      foreach (var Ext in JXL.WRRCATCHMENT)
      {
        if (Plants.TryGetValue(Ext.PLANTID, out CurrentPlant))
        {
          if (!Ext.IsAMOUNTNull())
            CurrentPlant.Extractions.AddSiValue(Ext.STARTDATE,Ext.ENDDATE, Ext.AMOUNT);
          if (!Ext.IsSURFACEWATERVOLUMENull())
            CurrentPlant.SurfaceWaterExtrations.AddSiValue(Ext.STARTDATE, Ext.ENDDATE, Ext.SURFACEWATERVOLUME);
        }
      }


      //In ribe amt extractions are in another table

      foreach (var IntExt in JXL.INTAKECATCHMENT)
      {
        if (Plants.TryGetValue(IntExt.DRWPLANTINTAKERow.PLANTID, out CurrentPlant))
        {
          //It would be possible to store this on the intake instead of the plant.
          //We are throwing away information!

          if (!IntExt.IsVOLUMENull())
          {
            CurrentPlant.Extractions.AddSiValue(IntExt.STARTDATE, IntExt.ENDDATE, IntExt.VOLUME);
            //if (IntExt.ENDDATE.Year != IntExt.STARTDATE.Year)
            //  throw new Exception("Volume cover period longer than 1 year)");

            //var E = CurrentPlant.Extractions.Items.FirstOrDefault(var => var.StartTime.Year == IntExt.ENDDATE.Year);
            //if (E == null)
            //  CurrentPlant.Extractions.AddSiValue (new TimeSeriesEntry(IntExt.ENDDATE, IntExt.VOLUME));
            //else
            //  E.Value += IntExt.VOLUME;
          }
        }
      }
    }


    /// <summary>
    /// Reads in all wells from a Jupiter database. 
    /// Only reads geographical information and location of Intakes and screen
    /// </summary>
    /// <param name="DataBaseFile"></param>
    public IWellCollection Wells()
    {
      IWellCollection Wells = new IWellCollection();
      //Construct the data set
      JXL.ReadWells(true, false);

      Well CurrentWell;
      IIntake CurrentIntake;

      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        CurrentWell = new Well(Boring.BOREHOLENO);
        Wells.Add(CurrentWell);

        if (!Boring.IsXUTMNull())
          CurrentWell.X = Boring.XUTM;
        if (!Boring.IsYUTMNull())
          CurrentWell.Y = Boring.YUTM;

        CurrentWell.Description = Boring.LOCATION;
        if (!Boring.IsELEVATIONNull())
          CurrentWell.Terrain = Boring.ELEVATION;

        //Loop the intakes
        foreach (var IntakeData in Boring.GetINTAKERows())
        {
          CurrentIntake = CurrentWell.Intakes.FirstOrDefault(var => var.IDNumber == IntakeData.INTAKENO);
          if (CurrentIntake == null)
            CurrentIntake = CurrentWell.AddNewIntake(IntakeData.INTAKENO);


          //Loop the screens. One intake can in special cases have multiple screens
          foreach (var ScreenData in IntakeData.GetSCREENRows())
          {
              Screen CurrentScreen = new Screen(CurrentIntake);
              CurrentScreen.DepthToTop = ScreenData.TOP;
              CurrentScreen.DepthToBottom = ScreenData.BOTTOM;
              CurrentScreen.Number = ScreenData.SCREENNO;
          }//Screen loop
        }//Intake loop
      }//Bore loop

      return Wells;
    }

    /// <summary>
    /// Fills the data row with entries common for Intake and Extractions.
    /// </summary>
    /// <param name="CurrentIntake"></param>
    private void AddCommonDataForNovana(JupiterIntake CurrentIntake)
    {
      JupiterWell CurrentWell;

      NovanaTables.IntakeCommonRow CurrentRow = (NovanaTables.IntakeCommonRow)CurrentIntake.Data;

      CurrentWell = (JupiterWell)CurrentIntake.well;

      CurrentRow.NOVANAID = CurrentWell.ID.Replace(" ", "") + "_" + CurrentIntake.IDNumber;

      CurrentRow.XUTM = CurrentWell.X;
      CurrentRow.YUTM = CurrentWell.Y;

      //Make sure all the necessary data have been read.
      if (JXL.ReducedRead| JXL.BOREHOLE.Count ==0)
        JXL.ReadWells(false, false);
      if (JXL.LITHSAMP.Count == 0)
        JXL.ReadInLithology();

      var BoringsData = JXL.BOREHOLE.FindByBOREHOLENO(CurrentWell.ID);
      var IntakeData = BoringsData.GetINTAKERows().First(var => var.INTAKENO == CurrentIntake.IDNumber);

      CurrentRow.JUPKOTE = BoringsData.ELEVATION;
      CurrentRow.BOREHOLENO = BoringsData.BOREHOLENO;
      CurrentRow.INTAKENO = CurrentIntake.IDNumber;
      CurrentRow.LOCATION = BoringsData.LOCATION;

      CurrentRow.ANTINT_B = CurrentWell.Intakes.Count();


      if (!BoringsData.IsDRILENDATENull())
        CurrentRow.DRILENDATE = BoringsData.DRILENDATE;

      if (!BoringsData.IsABANDONDATNull())
        CurrentRow.ABANDONDAT = BoringsData.ABANDONDAT;

      CurrentRow.ABANDCAUSE = BoringsData.ABANDCAUSE;
      CurrentRow.DRILLDEPTH = BoringsData.DRILLDEPTH;


      CurrentRow.CASIBOT = -999;

      //Assumes that the string no from the intake identifies the correct Casing
      foreach (var Casing in BoringsData.GetCASINGRows())
      {
        if (!IntakeData.IsSTRINGNONull() & !Casing.IsSTRINGNONull())
          if (IntakeData.STRINGNO == Casing.STRINGNO & !Casing.IsBOTTOMNull())
            CurrentRow.CASIBOT = Casing.BOTTOM;
      }

      CurrentRow.PURPOSE = BoringsData.PURPOSE;
      CurrentRow.USE = BoringsData.USE;
      CurrentRow.INTAKETOP = -999;
      CurrentRow.INTAKEBOT = -999;

      if (CurrentIntake.Screens.Count != 0)
      {
        if (CurrentIntake.Screens.Where(var1=>var1.DepthToTop.HasValue).Count()!=0)
          CurrentRow.INTAKETOP = CurrentIntake.Screens.Where(var1=>var1.DepthToTop.HasValue).Min(var => var.DepthToTop.Value);
  
        if (CurrentIntake.Screens.Where(var1=>var1.DepthToBottom.HasValue).Count()!=0)
          CurrentRow.INTAKEBOT = CurrentIntake.Screens.Where(var1 => var1.DepthToBottom.HasValue).Max(var => var.DepthToBottom.Value);
      }

      CurrentRow.INTAKTOPK = -999;
      CurrentRow.INTAKBOTK = -999;

      if (CurrentRow.JUPKOTE != -999)
      {
        if (CurrentRow.INTAKETOP != -999)
          CurrentRow.INTAKTOPK = CurrentRow.JUPKOTE - CurrentRow.INTAKETOP;
        if (CurrentRow.INTAKEBOT != -999)
          CurrentRow.INTAKBOTK = CurrentRow.JUPKOTE - CurrentRow.INTAKEBOT;
      }

      //Takes the minimum of all non-null dates
      IEnumerable<JupiterXL.SCREENRow> NonNullList = IntakeData.GetSCREENRows().Where(x => !x.IsSTARTDATENull());
      if (NonNullList.Count() > 0)
        CurrentRow.INTSTDATE2 = NonNullList.Min(x => x.STARTDATE);

      //Takes the maximum of all non-null dates
      NonNullList = IntakeData.GetSCREENRows().Where(x => !x.IsENDDATENull());
      if (NonNullList.Count() > 0)
        CurrentRow.INTENDATE2 = NonNullList.Max(x => x.ENDDATE);

      CurrentRow.RESROCK = IntakeData.RESERVOIRROCK;

      //Loop the lithology
      //This loop is entered foreach intake thus only read lithology once.
      if (CurrentWell.LithSamples.Count == 0)
      {
        foreach (var Lith in BoringsData.GetLITHSAMPRows())
        {
          Lithology L = new Lithology();
          L.Bottom = Lith.BOTTOM;
          L.Top = Lith.TOP;
          L.RockSymbol = Lith.ROCKSYMBOL;
          L.RockType = Lith.ROCKTYPE;
          L.TotalDescription = Lith.TOTALDESCR;
          CurrentWell.LithSamples.Add(L);
        }
      }

      CurrentRow.RESROCK = "-999";
      CurrentRow.SUMSAND = -999;
      CurrentRow.BOTROCK = "-999";


      if (CurrentWell.LithSamples.Count != 0 & CurrentIntake.Screens.Count!=0)
      {
        CurrentWell.LithSamples.Sort();
        CurrentRow.BOTROCK = CurrentWell.LithSamples[CurrentWell.LithSamples.Count - 1].RockSymbol;
        Dictionary<string, double> SoilLengths = new Dictionary<string, double>();

        double ScreenLength = 0;

        //Now build information about reservoir rock in front of screen
        //Loop all screens
        foreach (Screen SC in CurrentIntake.Screens)
        {
          //Do not use dummy values
          if (SC.DepthToBottom.HasValue & SC.DepthToTop.HasValue)
          {
            ScreenLength += SC.DepthToBottom.Value - SC.DepthToTop.Value;

            //Get the samples that are within the filter
            var sampleswithinFilter = CurrentWell.LithSamples.Where(var => var.Top < SC.DepthToBottom & var.Bottom > SC.DepthToTop);

            //Now calculate the percentages
            foreach (Lithology L in sampleswithinFilter)
            {
              double percent = (Math.Min(SC.DepthToBottom.Value, L.Bottom) - Math.Max(SC.DepthToTop.Value, L.Top));
              if (SoilLengths.ContainsKey(L.RockSymbol))
                SoilLengths[L.RockSymbol] += percent;
              else
                SoilLengths.Add(L.RockSymbol, percent);
            }
          }
        }

        if (SoilLengths.Count != 0)
        {
          double sumsand = 0;
          string[] magasiner = new string[] { "s", "k", "g" };
          //Build the resrock string
          StringBuilder resrock = new StringBuilder();
          foreach (KeyValuePair<string, double> KVP in SoilLengths)
          {
            double percent = KVP.Value / ScreenLength * 100;
            resrock.Append(KVP.Key + ": " + percent.ToString("###") + "% ");
            if (magasiner.Contains(KVP.Key.ToLower()))
              sumsand += percent;
            if (KVP.Key.Length >= 2 && magasiner.Contains(KVP.Key.Substring(1, 1).ToLower()))
              sumsand += percent;
          }
          CurrentRow.RESROCK = resrock.ToString();
          CurrentRow.SUMSAND = sumsand;
        }
      }
    }

    public IWellCollection ReadWellsInSteps()
    {
      string[] NotExtractionPurpose = new string[] { "A", "G", "I", "J", "L", "R", "U", "M", "P"};
      string[] ExtractionUse = new string[]{"C","V","VA","VD","VH","VI","VM","VP","VV"};
      string[] NotExtractionUse = new string[] { "A", "G", "I", "J", "L", "R", "U", "M", "P"};

      IWellCollection Wells = new IWellCollection();
      JupiterWell CurrentWell;
      JupiterIntake CurrentIntake;

      #region Borehole
      JXL.ReadWellsOnly();
      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        CurrentWell = new JupiterWell(Boring.BOREHOLENO);
        Wells.Add(CurrentWell);

        if (!Boring.IsXUTMNull())
            CurrentWell.X = Boring.XUTM;
          else //If no x set x to 0!
            CurrentWell.X = 0;

          if (!Boring.IsYUTMNull())
            CurrentWell.Y = Boring.YUTM;
          else
            CurrentWell.Y = 0;

          CurrentWell.Description = Boring.LOCATION;
          CurrentWell.Terrain = Boring.ELEVATION;

        if (!Boring.IsDRILLDEPTHNull())
          CurrentWell.Depth = Boring.DRILLDEPTH;

          CurrentWell.UsedForExtraction = true;

        //Hvis USE er noget andet end indvinding
          if (NotExtractionUse.Contains(Boring.USE.ToUpper()))
            CurrentWell.UsedForExtraction = false;

        //Hvis den er oprettet med et andet formål og USE ikke er sat til indvinding er det ikke en indvindingsboring
          if (NotExtractionPurpose.Contains(Boring.PURPOSE.ToUpper()) & !ExtractionUse.Contains(Boring.USE.ToUpper()))
            CurrentWell.UsedForExtraction = false;

          if (!Boring.IsDRILENDATENull())
            CurrentWell.StartDate = Boring.DRILENDATE;
          if (!Boring.IsABANDONDATNull())
            CurrentWell.EndDate = Boring.ABANDONDAT;

      }
      JXL.BOREHOLE.Clear();
      #endregion

      #region Intakes
      //Intakes
      JXL.ReadIntakes();
      foreach (var Intake in JXL.INTAKE)
      {
        if (Wells.Contains(Intake.BOREHOLENO))
        {
          JupiterIntake I = Wells[Intake.BOREHOLENO].AddNewIntake(Intake.INTAKENO) as JupiterIntake;
          if (I != null)
          {
            if (!Intake.IsSTRINGNONull())
              I.StringNo = Intake.STRINGNO;
          }
        }
      }

      foreach( var Casing in JXL.CASING)
      {
        if (Wells.Contains(Casing.BOREHOLENO))
        {
          IIntake I = Wells[Casing.BOREHOLENO].Intakes.SingleOrDefault(var => ((JupiterIntake)var).StringNo == Casing.STRINGNO);
          if (I != null)
            if (!Casing.IsBOTTOMNull())
              I.Depth = Casing.BOTTOM;
        }
      }
      JXL.INTAKE.Clear();
      JXL.CASING.Clear();
#endregion

      #region Screens
      //Screens
      JXL.ReadScreens();
      foreach (var screen in JXL.SCREEN)
      {
        if (Wells.Contains(screen.BOREHOLENO))
        {
          CurrentIntake = Wells[screen.BOREHOLENO].Intakes.FirstOrDefault(var => var.IDNumber == screen.INTAKENO) as JupiterIntake;
          if (CurrentIntake != null)
          {
            Screen CurrentScreen = new Screen(CurrentIntake);
            if (!screen.IsTOPNull())
              CurrentScreen.DepthToTop = screen.TOP;
            if (!screen.IsBOTTOMNull())
              CurrentScreen.DepthToBottom = screen.BOTTOM;
            CurrentScreen.Number = screen.SCREENNO;

            if (!screen.IsSTARTDATENull())
              CurrentScreen.StartDate = screen.STARTDATE;

            if (!screen.IsENDDATENull())
              CurrentScreen.EndDate = screen.ENDDATE;
          }
        }
      }
      JXL.SCREEN.Clear();
      #endregion

      return Wells;
    }

    /// <summary>
    /// Reads the lithology and assign to all the JupiterWells in the collection
    /// </summary>
    /// <param name="Wells"></param>
    public void ReadLithology(IWellCollection Wells)
    {
      JupiterWell CurrentWell;
      JXL.ReadInLithology();
      CurrentWell = Wells.FirstOrDefault() as JupiterWell;

      //Loop the lithology
      foreach (var Lith in JXL.LITHSAMP)
      {
        if (CurrentWell.ID == Lith.BOREHOLENO)
        {
          Lithology L = new Lithology();
          L.Bottom = Lith.BOTTOM;
          L.Top = Lith.TOP;
          L.RockSymbol = Lith.ROCKSYMBOL;
          L.RockType = Lith.ROCKTYPE;
          L.TotalDescription = Lith.TOTALDESCR;
          CurrentWell.LithSamples.Add(L);
        }
        else
        {
          if (Wells.Contains(Lith.BOREHOLENO))
          {
            CurrentWell = Wells[Lith.BOREHOLENO] as JupiterWell;
            Lithology L = new Lithology();
            L.Bottom = Lith.BOTTOM;
            L.Top = Lith.TOP;
            L.RockSymbol = Lith.ROCKSYMBOL;
            L.RockType = Lith.ROCKTYPE;
            L.TotalDescription = Lith.TOTALDESCR;
            CurrentWell.LithSamples.Add(L);
          }
        }
      }
      JXL.LITHSAMP.Clear();
    }

    /// <summary>
    /// Reads all water levels and assign them to the wells in the collection
    /// </summary>
    /// <param name="Wells"></param>
    public void ReadWaterLevels(IWellCollection Wells)
    {
      IWell CurrentWell = Wells.FirstOrDefault();
      IIntake CurrentIntake;

      JXL.ReadWaterLevels(false);

      foreach (var WatLev in JXL.WATLEVEL)
      {
        if (CurrentWell.ID == WatLev.BOREHOLENO)
        {
          CurrentIntake = CurrentWell.Intakes.FirstOrDefault(var => var.IDNumber == WatLev.INTAKENO) as JupiterIntake;
          if (CurrentIntake is JupiterIntake)
            ((JupiterIntake)CurrentIntake).RefPoint = WatLev.REFPOINT;
          FillInWaterLevel(CurrentIntake, WatLev);
        }
        else
        {
          if (Wells.Contains(WatLev.BOREHOLENO))
          {
            CurrentIntake = Wells[WatLev.BOREHOLENO].Intakes.FirstOrDefault(var => var.IDNumber == WatLev.INTAKENO) as JupiterIntake;
            if (CurrentIntake is JupiterIntake)
              ((JupiterIntake)CurrentIntake).RefPoint = WatLev.REFPOINT;
            FillInWaterLevel(CurrentIntake, WatLev);
          }
        }
      }
      JXL.WATLEVEL.Clear();
    }

    public IEnumerable<JupiterIntake> AddDataForNovanaExtraction(IEnumerable<Plant> Plants, DateTime StartDate, DateTime EndDate)
    {
      NovanaTables.IntakeCommonDataTable DT2 = new NovanaTables.IntakeCommonDataTable();
      NovanaTables.IndvindingerDataTable DT1 = new NovanaTables.IndvindingerDataTable();
      NovanaTables.IndvindingerRow CurrentRow;

      List<JupiterIntake> _intakes = new List<JupiterIntake>();

      //Make sure all the necessary data have been read.
      //if (JXL.ReducedRead)
        JXL.ReadWells(false, false);
        JXL.ReadExtractions();


      //Loop the plants
      foreach (Plant P in Plants)
      {
        var anlaeg = JXL.DRWPLANT.FindByPLANTID(P.IDNumber);


        //Loop the wells
        foreach (IWell IW in P.PumpingWells)
        {
          var wellData=JXL.BOREHOLE.FindByBOREHOLENO(IW.ID);
          //Construct a JupiterWell
          JupiterWell Jw = new JupiterWell(IW);
          //Loop the intakes
          foreach (IIntake I in Jw.Intakes)
          {
            //If the plant does not use all intakes in a well we should not print it
            if (null!=P.PumpingIntakes.FirstOrDefault(var=> var.Intake.IDNumber.Equals(I.IDNumber) & var.Intake.well.ID.Equals(Jw.ID)))
            {
              var intakedata = wellData.GetINTAKERows().FirstOrDefault(var => var.INTAKENO == I.IDNumber);
              JupiterIntake CurrentIntake = I as JupiterIntake;
              CurrentIntake.Data = DT2.NewIntakeCommonRow();
              //Read generic data
              AddCommonDataForNovana(CurrentIntake);
              DT2.Rows.Add(CurrentIntake.Data);
              CurrentRow = DT1.NewIndvindingerRow();

              //Construct novana id
              string NovanaID = P.IDNumber + "_" + CurrentIntake.well.ID.Replace(" ", "") + "_" + CurrentIntake.IDNumber;

              CurrentRow.NOVANAID = NovanaID;
              CurrentIntake.Data["NOVANAID"] = NovanaID;
              CurrentRow.JUPDTMK = -999;

              FillPlantDataIntoDataRow(CurrentRow, anlaeg, P, StartDate, EndDate);

              //Aktiv periode
              var plantintake = anlaeg.GetDRWPLANTINTAKERows().FirstOrDefault(var => var.BOREHOLENO == Jw.ID & var.INTAKENO == I.IDNumber);
              NovanaTables.IntakeCommonRow TIC = CurrentIntake.Data as NovanaTables.IntakeCommonRow;

              CurrentRow.FRAAAR = 1000;
              int nextyear;
              if (!plantintake.IsSTARTDATENull())
              {
                CurrentRow.INTSTDATE = plantintake.STARTDATE;
                CurrentRow.FRAAAR = Math.Max(CurrentRow.FRAAAR, GetFraAar(plantintake.STARTDATE));
              }
              if (!wellData.IsDRILENDATENull())
                CurrentRow.FRAAAR = Math.Max(CurrentRow.FRAAAR, GetFraAar(wellData.DRILENDATE));

              if (!TIC.IsINTSTDATE2Null())
                CurrentRow.FRAAAR = Math.Max(CurrentRow.FRAAAR, GetFraAar(TIC.INTSTDATE2));

              CurrentRow.TILAAR = 9999;
              if (!plantintake.IsENDDATENull())
              {
                CurrentRow.INTENDDATE = plantintake.ENDDATE;
                CurrentRow.TILAAR = Math.Min(CurrentRow.TILAAR, GetTilAar(plantintake.ENDDATE));
              }
              if (!wellData.IsABANDONDATNull())
                CurrentRow.TILAAR = Math.Min(CurrentRow.TILAAR, GetTilAar(wellData.ABANDONDAT));
              if (!TIC.IsINTENDATE2Null())
                CurrentRow.TILAAR = Math.Min(CurrentRow.TILAAR, GetTilAar(TIC.INTENDATE2));

              //Do not include the intake if it is not active within the given period.
              if (CurrentRow.FRAAAR > EndDate.Year || CurrentRow.TILAAR < StartDate.Year)
                DT2.Rows.Remove(CurrentIntake.Data);
              else
              {
                DT1.Rows.Add(CurrentRow);
                _intakes.Add(CurrentIntake);
              }
            }
          }
        }
      }


      //Add a blank string to ensure length of column
      DT2.Rows[0]["Comment"] = "                                                   ";
      DT2.Rows[0]["OK_KOMMENT"] = "                                                   ";
      DT2.Merge(DT1);

      return _intakes;
    }

    private int GetFraAar(DateTime Date)
    {
      if (Date.DayOfYear > 182)
        return Date.Year + 1;
      else
        return Date.Year;
    }

    private int GetTilAar(DateTime Date)
    {
      if (Date.DayOfYear < 182)
        return Date.Year - 1;
      else
        return Date.Year;
    }


      public NovanaTables.IndvindingerDataTable FillPlantData(IEnumerable<Plant> plants, DateTime StartDate, DateTime EndDate)
      {
          NovanaTables.IndvindingerDataTable DT = new NovanaTables.IndvindingerDataTable();
          NovanaTables.IndvindingerRow CurrentRow;
          JupiterXL.DRWPLANTRow anlaeg;

          foreach (Plant P in plants)
          {
              anlaeg = JXL.DRWPLANT.FindByPLANTID(P.IDNumber);
              CurrentRow = DT.NewIndvindingerRow();
              FillPlantDataIntoDataRow(CurrentRow, anlaeg, P, StartDate, EndDate);
              CurrentRow.NOVANAID = P.IDNumber.ToString();
              DT.AddIndvindingerRow(CurrentRow);
          }
              return DT;
      }

    private void FillPlantDataIntoDataRow(NovanaTables.IndvindingerRow CurrentRow, JupiterXL.DRWPLANTRow anlaeg, Plant P, DateTime StartDate, DateTime EndDate)
    {
      CurrentRow.PLANTID = anlaeg.PLANTID;
      CurrentRow.PLANTNAME = anlaeg.PLANTNAME;

      //Get additional data about the plant from the dataset
      CurrentRow.NYKOMNR = anlaeg.MUNICIPALITYNO2007;
      CurrentRow.KOMNR = anlaeg.MUNICIPALITYNO;
      CurrentRow.ATYP = anlaeg.PLANTTYPE;
      CurrentRow.ANR = anlaeg.SERIALNO;
      CurrentRow.UNR = anlaeg.SUBNO;
      CurrentRow.ANTUNDERA = P.SubPlants.Count;

      if (anlaeg.IsXUTMNull())
        CurrentRow.ANLUTMX = 0;
      else
        CurrentRow.ANLUTMX = anlaeg.XUTM;

      if (anlaeg.IsYUTMNull())
        CurrentRow.ANLUTMY = 0;
      else
        CurrentRow.ANLUTMY = anlaeg.YUTM;

      CurrentRow.VIRKTYP = anlaeg.COMPANYTYPE;
      CurrentRow.ACTIVE = anlaeg.ACTIVE;

      if (!anlaeg.IsSUPPLANTNull())
        CurrentRow.OVERANL = anlaeg.SUPPLANT;


      if (P.Extractions.Items.Count > 0)
      {
        var SelectecExtrations = P.Extractions.Items.Where(var => var.StartTime >= StartDate && var.StartTime <= EndDate);
        var ActualValue = SelectecExtrations.FirstOrDefault(var => var.StartTime.Year == EndDate.Year);

        if (SelectecExtrations.Count() > 0)
        {
          CurrentRow.MEANINDV = SelectecExtrations.Average(var => var.Value);
          if (ActualValue != null)
            CurrentRow.AKTUELIND = ActualValue.Value;
          else
            CurrentRow.AKTUELIND = 0;
        }
      }
      CurrentRow.ANTINT_A = P.PumpingIntakes.Count;
      CurrentRow.ANTBOR_A = P.PumpingWells.Count;


    }

    public void AddDataForNovanaPejl(IEnumerable<JupiterIntake> Intakes)
    {
      NovanaTables.PejlingerDataTable DT1 = new NovanaTables.PejlingerDataTable();
      NovanaTables.PejlingerRow CurrentRow;

      NovanaTables.IntakeCommonDataTable DT2 = new NovanaTables.IntakeCommonDataTable();

      foreach (JupiterIntake CurrentIntake in Intakes)
      {
        CurrentIntake.Data = DT2.NewIntakeCommonRow();
        AddCommonDataForNovana(CurrentIntake);
        DT2.Rows.Add(CurrentIntake.Data);
        CurrentRow = DT1.NewPejlingerRow();
        CurrentRow.NOVANAID = CurrentIntake.Data["NOVANAID"].ToString();

        DT1.Rows.Add(CurrentRow);

        //Create statistics on water levels
        CurrentRow.ANTPEJ = CurrentIntake.HeadObservations.Items.Count;
        if (CurrentRow.ANTPEJ > 0)
        {
          CurrentRow.REFPOINT = CurrentIntake.RefPoint;
          CurrentRow.MINDATO = CurrentIntake.HeadObservations.StartTime;
          CurrentRow.MAXDATO = CurrentIntake.HeadObservations.EndTime;
          CurrentRow.AKTAAR = CurrentRow.MAXDATO.Year - CurrentRow.MINDATO.Year + 1;
          CurrentRow.AKTDAGE = CurrentRow.MAXDATO.Subtract(CurrentRow.MINDATO).Days + 1;
          CurrentRow.PEJPRAAR = CurrentRow.ANTPEJ / CurrentRow.AKTAAR;
          CurrentRow.MAXPEJ = CurrentIntake.HeadObservations.Items.Max(num => num.Value);
          CurrentRow.MINPEJ = CurrentIntake.HeadObservations.Items.Min(num => num.Value);
          CurrentRow.MEANPEJ = CurrentIntake.HeadObservations.Items.Average(num => num.Value);
        }
      }
      //Add a blank string to ensure length of column
      DT2.Rows[0]["Comment"] = "                                                   ";
      DT2.Rows[0]["OK_KOMMENT"] = "                                                   ";

      DT2.Merge(DT1);
    }


    public IWellCollection WellsForNovana(bool Lithology, bool WaterLevel, bool Chemistry, bool OnlyRo)
    {
      string[] NotExtractionPurpose = new string[] { "A", "G", "I", "J", "L", "R", "U", "M", "P"};

      string[] ExtractionUse = new string[]{"C","V","VA","VD","VH","VI","VM","VP","VV"};
      string[] NotExtractionUse = new string[] { "A", "G", "I", "J", "L", "R", "U", "M", "P"};

      IWellCollection Wells = new IWellCollection();
      //Construct the data set
      if (WaterLevel)
          JXL.ReadWaterLevels(OnlyRo);

      if (Lithology)
        JXL.ReadInLithology();
      if (Chemistry)
        JXL.ReadInChemistrySamples();

        JXL.ReadWells(false, WaterLevel);

      JupiterWell CurrentWell;
      IIntake CurrentIntake;

      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        CurrentWell = new JupiterWell(Boring.BOREHOLENO);
        Wells.Add(CurrentWell);

        if (!Boring.IsXUTMNull())
            CurrentWell.X = Boring.XUTM;
          else //If no x set x to 0!
            CurrentWell.X = 0;

          if (!Boring.IsYUTMNull())
            CurrentWell.Y = Boring.YUTM;
          else
            CurrentWell.Y = 0;

          CurrentWell.Description = Boring.LOCATION;
          CurrentWell.Terrain = Boring.ELEVATION;


          CurrentWell.UsedForExtraction = true;

        //Hvis USE er noget andet end indvinding
          if (NotExtractionUse.Contains(Boring.USE.ToUpper()))
            CurrentWell.UsedForExtraction = false;

        //Hvis den er oprettet med et andet formål og USE ikke er sat til indvinding er det ikke en indvindingsboring
          if (NotExtractionPurpose.Contains(Boring.PURPOSE.ToUpper()) & !ExtractionUse.Contains(Boring.USE.ToUpper()))
            CurrentWell.UsedForExtraction = false;

          //Loop the lithology
          foreach (var Lith in Boring.GetLITHSAMPRows())
          {
            Lithology L = new Lithology();
            L.Bottom = Lith.BOTTOM;
            L.Top = Lith.TOP;
            L.RockSymbol = Lith.ROCKSYMBOL;
            L.RockType = Lith.ROCKTYPE;
            L.TotalDescription = Lith.TOTALDESCR;
            CurrentWell.LithSamples.Add(L);
          }

          //Reads in chemistry
          foreach (var Chem in Boring.GetGRWCHEMSAMPLERows())
          {
            foreach (var analysis in Chem.GetGRWCHEMANALYSISRows())
            {
              ChemistrySample C = new ChemistrySample();
              C.SampleDate = Chem.SAMPLEDATE;
              C.CompoundNo = analysis.COMPOUNDNO;
              C.Amount = analysis.AMOUNT;
              C.Unit = analysis.UNIT;
              C.CompoundName = JXL.COMPOUNDLIST.FindByCOMPOUNDNO(C.CompoundNo).LONG_TEXT;
              CurrentWell.ChemSamples.Add(C);
            }
          }

        //Loop the intakes
        foreach (var Intake in Boring.GetINTAKERows())
        {
          CurrentIntake = CurrentWell.AddNewIntake(Intake.INTAKENO);

          //Loop the screens. One intake can in special cases have multiple screens
          foreach (var ScreenData in Intake.GetSCREENRows())
          {
            Screen CurrentScreen = new Screen(CurrentIntake);
            if (!ScreenData.IsTOPNull()) 
              CurrentScreen.DepthToTop = ScreenData.TOP;
            if (!ScreenData.IsBOTTOMNull())
              CurrentScreen.DepthToBottom = ScreenData.BOTTOM;
            CurrentScreen.Number = ScreenData.SCREENNO;
          }//Screen loop       

          //Read in the water levels
          foreach (var WatLev in Intake.GetWATLEVELRows())
          {
            ((JupiterIntake)CurrentIntake).RefPoint = WatLev.REFPOINT;
            FillInWaterLevel(CurrentIntake, WatLev);
          }         

        }//Intake loop

      }//Bore loop
      JXL.WATLEVEL.Clear();
      foreach (Well W in Wells)
        foreach (Intake I in W.Intakes)
          I.HeadObservations.Sort();

      return Wells;
    }

  }
}
