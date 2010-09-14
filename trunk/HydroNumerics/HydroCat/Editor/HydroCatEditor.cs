﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HydroNumerics.HydroCat.Core;
using HydroNumerics.Time.Core;
using HydroNumerics.Time.Tools;
using HydroNumerics.Core;

namespace HydroNumerics.HydroCat.Editor
{
    public partial class HydroCatEditor : Form
    {
        HydroCatEngine hydroCatEngine;
      
        public HydroCatEditor()
        {
            InitializeComponent();
            hydroCatEngine = new HydroNumerics.HydroCat.Core.HydroCatEngine();


            //-- Default Initial values
            hydroCatEngine.InitialSnowStorage = 0;
            hydroCatEngine.InitialSurfaceStorage = 0;
            hydroCatEngine.InitialRootZoneStorage = 220;
            hydroCatEngine.InitialOverlandFlow = 0;
            hydroCatEngine.InitialInterFlow = 0;
            hydroCatEngine.InitialBaseFlow = 0.6;

            //-- Default Parameters
            hydroCatEngine.CatchmentArea = 160000000;
            hydroCatEngine.SnowmeltCoefficient = 2.0;
            hydroCatEngine.SurfaceStorageCapacity = 18;
            hydroCatEngine.RootZoneStorageCapacity = 250;
            hydroCatEngine.OverlandFlowCoefficient = 0.61;
            hydroCatEngine.InterflowCoefficient = 0.6; //??
            hydroCatEngine.OverlandFlowTreshold = 0.38;
            hydroCatEngine.InterflowTreshold = 0.08;
            hydroCatEngine.OverlandFlowTimeConstant = 0.3;
            hydroCatEngine.InterflowTimeConstant = 30;
            hydroCatEngine.BaseflowTimeConstant = 2800;

            // -- Default simulatio time ---
             hydroCatEngine.SimulationStartTime = new System.DateTime(1973, 1, 1, 12, 0, 0);
            hydroCatEngine.SimulationEndTime = new System.DateTime(1973, 12, 1, 12, 0, 0);

            // -- Default timeseries --
            Unit mmPrDayUnit = new HydroNumerics.Core.Unit("mm pr day", 1.0 / (1000 * 3600 * 24), 0);
            Unit centigradeUnit = new HydroNumerics.Core.Unit("Centigrade", 1.0, -273.15);
            int numberOfTimesteps = 365;
            hydroCatEngine.PrecipitationTs = new TimespanSeries("Precipitation", hydroCatEngine.SimulationStartTime, numberOfTimesteps, 1, TimestepUnit.Days, 0);
            hydroCatEngine.PrecipitationTs.Unit = mmPrDayUnit;
            hydroCatEngine.PotentialEvaporationTs = new TimespanSeries("PotentialEvaporation", hydroCatEngine.SimulationStartTime, numberOfTimesteps, 1, TimestepUnit.Days, 0);
            hydroCatEngine.PotentialEvaporationTs.Unit = mmPrDayUnit;
            hydroCatEngine.TemperatureTs = new TimespanSeries("Temperature", hydroCatEngine.SimulationStartTime, numberOfTimesteps, 1, TimestepUnit.Days, 0);
            hydroCatEngine.TemperatureTs.Unit = centigradeUnit;
            hydroCatEngine.ObservedRunoffTs = new TimestampSeries("ObservedRunoff", hydroCatEngine.SimulationStartTime, numberOfTimesteps, 1, TimestepUnit.Days, 0);
            hydroCatEngine.ObservedRunoffTs.Unit = new Unit("m3 pr sec", 1.0, 0.0);

            double[] precipitation = new double[365] { 0, 0, 2.3, 0.2, 0, 0.1, 0.1, 0, 0.3, 0.4, 0.1, 0.1, 1, 0, 0.5, 5.3, 1.9, 0.1, 0, 0.4, 1.1, 1.2, 2.6, 0, 1.5, 3.4, 6, 0.7, 4, 0.1, 0.6, 2, 0.4, 0.1, 0, 0, 0, 4.1, 0.2, 4, 0.3, 0.6, 3.1, 14.1, 3.9, 0.1, 3.7, 2.4, 4.3, 0, 0, 0, 0, 2.7, 1, 0.1, 0.3, 0.2, 0, 0.2, 8.7, 2, 1.8, 0.9, 0.1, 0.2, 0.1, 0.1, 0, 0, 0, 0, 0, 0, 0, 0.1, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.6, 1.8, 0, 1.1, 4.7, 2.4, 0, 0.1, 8.2, 4.6, 1, 0.1, 0, 0, 7.8, 2, 0.3, 0, 0.1, 0.1, 0, 0, 0.3, 0.1, 2.2, 2, 0, 0, 0, 0, 0, 0, 0, 0.1, 6.7, 0, 0, 0, 5.4, 2.2, 1, 10.8, 3.6, 3.1, 10.1, 0, 0.3, 2, 0, 0, 0, 0, 0, 0, 1.3, 0, 0, 7.6, 3.6, 0.1, 0, 0, 0, 0, 0.1, 4.5, 0, 12.8, 0, 4.4, 0, 0, 0, 0, 4.9, 0, 0.8, 0, 2.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7.1, 0.2, 0, 0, 0, 0, 0, 0, 0, 0, 4.4, 1.5, 4.6, 1.4, 0.8, 0, 0, 0, 0, 5.5, 4.6, 1.3, 0.1, 2.8, 2.6, 0, 0.1, 3.5, 1, 8.7, 0.5, 0, 0.4, 0, 0, 0, 0, 3.8, 4.9, 4.6, 2.5, 7.9, 0.1, 4.7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.2, 14.4, 2, 6.2, 0.5, 0.3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3.2, 8.1, 1.3, 20.2, 13.5, 1.1, 1.3, 0, 0, 2.1, 13.7, 3.9, 0.5, 0.1, 0, 0, 0.1, 0, 0, 0.2, 4.8, 7.2, 0.4, 0, 4.6, 0.2, 0, 0, 0.5, 4.8, 9.8, 0.6, 0, 0, 0, 0, 3.2, 0, 0, 0, 0, 0.4, 0, 0, 0, 0, 0, 0.5, 8.5, 0.5, 1.9, 1.2, 0, 3.2, 1.7, 12.3, 1.9, 0, 4.3, 3.6, 1.1, 6.7, 5.4, 0, 0, 0.9, 10, 7.8, 0.1, 1.3, 0.5, 0.1, 0.2, 0.5, 0, 7.2, 0, 0, 10.2, 9.7, 0.2, 0.1, 0.9, 3.5, 1.2, 4.9, 4.6, 3.5, 1.1, 5.8, 0.4, 0, 0, 0.6, 0.1, 1.2, 0.8, 9.4, 1.5, 0.1, 0.1, 0.2, 0.9, 4.8 };
            double[] potentialEvaporation = new double[365] { 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.18, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 0.65, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.23, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 2.10, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 3.90, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 4.19, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 3.23, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 1.63, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.77, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.27, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16, 0.16 };
            double[] temperature = new double[365] { -2.2, 0.1, 2.9, 2.6, 2.7, 6.1, 2.8, 1.4, -1.7, -1.7, -0.5, -2.4, -0.7, 1.2, 1.4, 1.6, 0.8, 0.5, 0.2, -0.3, 0.0, 1.8, 1.3, 0.9, 1.5, 3.7, 4.7, 2.6, 2.0, 3.8, 1.7, 2.8, 2.5, 3.8, 5.9, 6.5, 5.4, 6.0, 4.2, 3.3, 1.6, 1.0, 2.7, 3.0, 0.0, -0.8, 0.7, 0.3, 0.8, 0.6, 3.4, 7.3, 5.4, 2.5, -0.2, -1.2, -0.9, -0.6, 1.2, 4.2, 6.0, 3.4, 3.7, 4.8, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 10.0, 2.8, -0.9, 1.1, 1.7, 1.6, 4.5, 5.2, 1.8, 3.5, 7.6, 4.6, 2.5, -3.9, -5.4, -5.2, -5.7, -7.5, -8.3, -10.2, -2.5, 5.4, 3.2, 2.8, 2.3, -0.3, -3.2, -0.5, 4.4, 2.9, 2.7, 2.4, -0.7, -2.2, 1.4, -1.0, -2.1, -0.8, 0.5, 0.9, 0.8, 0.9, 0.9, 0.9, 3.9, 5.5, 4.0, 5.9, 3.5 };
            double[] observedRunoff = new double[365] { 1.197, 0.991, 1.047, 1.027, 1.123, 1.222, 1.213, 1.245, 1.130, 1.058, 1.012, 0.920, 0.901, 0.918, 0.984, 1.680, 1.805, 1.660, 1.407, 1.198, 1.114, 1.266, 1.637, 1.436, 1.489, 2.067, 2.997, 2.367, 2.330, 2.021, 1.818, 1.667, 1.466, 1.381, 1.298, 1.230, 1.267, 1.451, 1.262, 1.487, 1.470, 1.319, 1.520, 4.469, 3.972, 2.426, 2.037, 2.304, 3.869, 3.032, 2.324, 1.989, 1.632, 1.546, 1.517, 1.353, 1.197, 1.072, 1.071, 1.118, 1.985, 2.379, 2.120, 1.904, 1.597, 1.364, 1.170, 1.059, 1.058, 0.998, 0.928, 0.904, 0.877, 0.851, 0.835, 0.831, 0.816, 0.791, 0.776, 0.762, 0.747, 0.733, 0.730, 0.726, 0.712, 0.708, 0.695, 0.691, 0.688, 0.684, 0.710, 0.737, 0.696, 0.675, 0.854, 1.107, 1.223, 0.943, 0.803, 0.731, 0.809, 0.848, 0.764, 0.704, 0.682, 0.671, 0.660, 0.630, 0.621, 0.603, 0.640, 0.677, 0.640, 0.612, 0.612, 0.614, 0.615, 0.617, 0.618, 0.629, 0.750, 0.722, 0.694, 0.697, 0.805, 0.819, 0.857, 1.741, 1.513, 1.515, 2.098, 1.465, 1.294, 1.245, 1.062, 0.906, 0.817, 0.745, 0.697, 0.653, 0.621, 0.598, 0.570, 0.590, 0.570, 0.544, 0.520, 0.503, 0.495, 0.487, 0.479, 0.531, 0.507, 0.610, 0.526, 0.480, 0.443, 0.409, 0.387, 0.372, 0.372, 0.353, 0.327, 0.327, 0.320, 0.296, 0.281, 0.275, 0.269, 0.264, 0.251, 0.265, 0.264, 0.257, 0.256, 0.256, 0.262, 0.275, 0.303, 0.295, 0.287, 0.270, 0.266, 0.265, 0.265, 0.246, 0.246, 0.222, 0.219, 0.213, 0.230, 0.248, 0.231, 0.216, 0.209, 0.203, 0.200, 0.205, 0.213, 0.210, 0.208, 0.206, 0.204, 0.209, 0.265, 0.229, 0.223, 0.245, 0.223, 0.221, 0.215, 0.213, 0.207, 0.198, 0.200, 0.210, 0.209, 0.208, 0.210, 0.219, 0.211, 0.220, 0.205, 0.190, 0.182, 0.185, 0.180, 0.175, 0.170, 0.165, 0.163, 0.158, 0.153, 0.149, 0.149, 0.145, 0.143, 0.139, 0.140, 0.144, 0.144, 0.185, 0.203, 0.181, 0.187, 0.176, 0.172, 0.177, 0.155, 0.127, 0.122, 0.149, 0.161, 0.149, 0.145, 0.142, 0.140, 0.138, 0.136, 0.134, 0.132, 0.130, 0.299, 0.307, 0.617, 0.701, 0.507, 0.426, 0.391, 0.378, 0.612, 0.663, 0.559, 0.492, 0.449, 0.415, 0.416, 0.416, 0.410, 0.411, 0.411, 0.512, 0.656, 0.503, 0.456, 0.433, 0.417, 0.407, 0.398, 0.403, 0.476, 0.610, 0.567, 0.490, 0.445, 0.429, 0.420, 0.431, 0.433, 0.422, 0.411, 0.413, 0.416, 0.392, 0.382, 0.372, 0.368, 0.364, 0.385, 0.569, 0.466, 0.436, 0.431, 0.402, 0.436, 0.567, 1.033, 0.708, 0.622, 0.697, 0.692, 0.661, 1.021, 1.171, 0.826, 0.774, 0.788, 1.285, 1.390, 1.073, 0.899, 0.890, 0.885, 0.880, 0.875, 0.870, 2.079, 1.800, 1.191, 2.074, 3.291, 2.031, 1.310, 1.125, 1.448, 1.639, 2.028, 2.484, 1.743, 1.507, 2.017, 2.025, 1.371, 1.144, 1.137, 1.215, 1.384, 1.727, 3.071, 3.083, 2.936, 2.362, 1.984, 2.068, 2.522 };
            for (int i = 0; i < numberOfTimesteps; i++)
            {
                ((TimespanSeries)hydroCatEngine.PrecipitationTs).Items[i].Value = precipitation[i];
                ((TimespanSeries)hydroCatEngine.PotentialEvaporationTs).Items[i].Value = potentialEvaporation[i];
                ((TimespanSeries)hydroCatEngine.TemperatureTs).Items[i].Value = temperature[i];
                ((TimestampSeries)hydroCatEngine.ObservedRunoffTs).Items[i].Value = observedRunoff[i];
            }


            hydroCatEngine.Initialize();
            parametersPropertyGrid.SelectedObject = hydroCatEngine;

            timeSeriesPlot.Visible = true;
            Run();
          
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            Run();
            
        }

        private void Run()
        {

            hydroCatEngine.Initialize();
            try
            {
                hydroCatEngine.RunSimulation();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            
            timeSeriesPlot.TimeSeriesDataSet = hydroCatEngine.OutputTsg;
            timeSeriesPlot.Repaint();
            parametersPropertyGrid.Refresh();
           
        }

        private void timeseriesPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeSeriesGroupPropertiesDialog timeSeriesGroupPropertiesDialog = new TimeSeriesGroupPropertiesDialog(hydroCatEngine.OutputTsg);
            timeSeriesGroupPropertiesDialog.Show();
            
        }

        private void parametersPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //Run();
        }

        private void plotItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideOrShowCurves hideOrShowCurves = new HideOrShowCurves(timeSeriesPlot.TimeSeriesDataSet);
            hideOrShowCurves.Show();
        }
    }
}
