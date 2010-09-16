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

using HydroNumerics.MikeSheTools.ViewModel;
using HydroNumerics.MikeSheTools.Mike11;

namespace HydroNumerics.MikeSheTools.View
{
  /// <summary>
  /// Interaction logic for M11View.xaml
  /// </summary>
  public partial class M11View : Window
  {
    M11ViewModel m11 = new M11ViewModel();

    public M11View()
    {
      InitializeComponent();
      BranchList.SelectionChanged += new SelectionChangedEventHandler(BranchList_SelectionChanged);
    }

    void BranchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      foreach (var b in e.RemovedItems)
        foreach (var CSC in ((M11Branch)b).CrossSections)
          m11.SelectedCrossSections.Remove((CSC));
      foreach (var b in e.AddedItems)
        foreach (var CSC in ((M11Branch)b).CrossSections)
          m11.SelectedCrossSections.Add(CSC);
      
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      m11.Sim11FileName = @"C:\users\Jacob\Work\HydroNumerics\MikeSheTools\TestData\Mike11\Novomr6_release2009.sim11";
      DataContext = m11;
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      m11.WriteToShape("");
    }

    private void AdjustDatums_Click(object sender, RoutedEventArgs e)
    {
      foreach (var CSC in CscList.SelectedItems)
      {
        CrossSection cs = CSC as CrossSection;
        if (cs.DEMHeight.HasValue)
          cs.HeigthAtMidstream = cs.DEMHeight.Value;
      }
    }
  }
}
