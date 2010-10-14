﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using HydroNumerics.HydroNet.Core;
using HydroNumerics.HydroNet.ViewModel;

namespace HydroNumerics.HydroNet.View
{
  /// <summary>
  /// Interaction logic for SingleWBModel.xaml
  /// </summary>
  public partial class SingleWBModel : Window
  {
    public SingleWBModel()
    {
      InitializeComponent();
      WaterView.DataContext = new WaterBodyViewModel((AbstractWaterBody)ModelFactory.GetModel(@"..\..\..\..\TestData\vedsted2.xml")._waterBodies.First());
    
    
    }
  }
}
