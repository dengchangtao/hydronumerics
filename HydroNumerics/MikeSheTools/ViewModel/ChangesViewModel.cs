﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Security.Principal;

using HydroNumerics.Wells;
using HydroNumerics.JupiterTools;
using HydroNumerics.JupiterTools.JupiterPlus;

namespace HydroNumerics.MikeSheTools.ViewModel
{
  public class ChangesViewModel:BaseViewModel
  {

    public ChangesViewModel()
    {
      Changes = new ObservableCollection<ChangeDescriptionViewModel>();
      Changes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Changes_CollectionChanged);
      ChangeController = new ChangeController();
      ChangeController.UserName = WindowsIdentity.GetCurrent().Name;
      ChangeController.ProjectName = "NoProjectName";
    }

    void SelectedProjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      NotifyPropertyChanged("SelectedChanges");
    }

    void Changes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
      NotifyPropertyChanged("DistinctUsers");
      NotifyPropertyChanged("DistinctProjects");
      NotifyPropertyChanged("SelectedChanges");
    }

    
    // The collection of changes
    private ObservableCollection<ChangeDescriptionViewModel> Changes = new ObservableCollection<ChangeDescriptionViewModel>();

    /// <summary>
    /// Adds a new change
    /// </summary>
    /// <param name="CDVM"></param>
    public void AddChange(ChangeDescriptionViewModel CDVM)
    {
      Changes.Add(CDVM);
      ChangeController.UserName = CDVM.User;
      ChangeController.ProjectName = CDVM.Project;
    }

    /// <summary>
    /// Gets the collection of selected changes
    /// </summary>
    public IEnumerable<ChangeDescriptionViewModel> SelectedChanges 
    {
      get
      {
        return Changes.Where(var=>SelectedUsers.Contains(var.User)).Where(var=>SelectedProjects.Contains(var.Project));
      }
    }

    public void SetDataBaseConnection(JupiterXLFastReader dbc)
    {
      ChangeController.DataBaseConnection = dbc;
      CheckChanges();
    }

    private void CheckChanges()
    {
      if (ChangeController.DataBaseConnection != null)
      {
        foreach (ChangeDescriptionViewModel cdv in Changes)
        {
          if (cdv.changeDescription.Action != TableAction.InsertRow)
          {
            DateTime? JupiterDate;
            cdv.IsFoundInJupiter = ChangeController.DataBaseConnection.TryGetLatestDate(cdv.changeDescription.Table, cdv.changeDescription.PrimaryKeys, out JupiterDate);

            if (JupiterDate.HasValue)
              cdv.IsDateOk = JupiterDate.Value.CompareTo(cdv.Date) < 1;
          }
        }
      }
    }

    
    public ChangeController ChangeController{get;private set;}

    public IPlantCollection Plants {get;set;}
    public IWellCollection Wells { get; set; }

    private ObservableCollection<string> selectedUsers;
    public ObservableCollection<string> SelectedUsers 
    { 
      get
      {
        if (selectedUsers == null)
        {
          selectedUsers = new ObservableCollection<string>(DistinctUsers);
          selectedUsers.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SelectedProjects_CollectionChanged);
        }
        return selectedUsers;
      }
    }

    public IEnumerable<string> DistinctUsers
    {
      get
      {
        return Changes.Select(var => var.User).Distinct();
      }
    }

    private ObservableCollection<string> selectedProjects;
    public ObservableCollection<string> SelectedProjects
    {
      get
      {
        if (selectedProjects == null)
        {
          selectedProjects = new ObservableCollection<string>(DistinctProjects);
          selectedProjects.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SelectedProjects_CollectionChanged);
        }
        return selectedProjects;
      }
    }


    public IEnumerable<string> DistinctProjects
    {
      get
      {
        return Changes.Select(var => var.Project).Distinct();
      }
    }

    #region Commands
    RelayCommand saveCommand;
    RelayCommand loadCommand;
    RelayCommand applyCommand;

    /// <summary>
    /// Gets the command that saves to an xml-file
    /// </summary>
    public ICommand SaveCommand
    {
      get
      {
        if (saveCommand == null)
        {
          saveCommand = new RelayCommand(param => Save(), param => CanSave);
        }
        return saveCommand;
      }
    }

    /// <summary>
    /// Gets the command that saves to an xml-file
    /// </summary>
    public ICommand LoadCommand
    {
      get
      {
        if (loadCommand == null)
        {
          loadCommand = new RelayCommand(param => Load(), param => CanLoad);
        }
        return loadCommand;
      }
    }

    /// <summary>
    /// Gets the command that saves to an xml-file
    /// </summary>
    public ICommand ApplyCommand
    {
      get
      {
        if (applyCommand == null)
        {
          applyCommand = new RelayCommand(param => Apply(), param => CanApply);
        }
        return applyCommand;
      }
    }

    private bool CanSave
    {
      get
      {
        return (Changes != null && Changes.Count > 0);
      }
    }

    private void Save()
    {
      Microsoft.Win32.SaveFileDialog savedialog = new Microsoft.Win32.SaveFileDialog();
      savedialog.Filter = "(*.xml)|*.xml";

      if (savedialog.ShowDialog().Value)
      {
        ChangeController.SaveToFile(SelectedChanges.Select(var => var.changeDescription), savedialog.FileName);
      }
    }

    private bool CanLoad
    {
      get
      {
        return true;
      }
    }

    private void Load()
    {
      Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
      openFileDialog.Filter = "Known file types (*.xml)|*.xml";
      openFileDialog.Title = "Select an XML file with changes in JupiterPlus format";

      if (openFileDialog.ShowDialog().Value)
      {
        foreach (var c in ChangeController.LoadFromFile(openFileDialog.FileName))
          Changes.Add(new ChangeDescriptionViewModel(c));
        CheckChanges();
      }
    }

    private bool CanApply
    {
      get
      {
        return (SelectedChanges.Any(var => var.IsApplied == false));
      }
    }

    private void Apply()
    {
      foreach (var v in SelectedChanges.Where(var=>var.IsApplied ==false))
      {
        v.IsApplied = ChangeController.ApplySingleChange(Plants, Wells, v.changeDescription);
      }
    }

    #endregion

  }
}
