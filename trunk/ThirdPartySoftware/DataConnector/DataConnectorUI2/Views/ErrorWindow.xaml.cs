﻿/* ******************************************************************************
 * 
 * Copyright 2010 Microsoft Corporation
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not 
 * use this file except in compliance with the License. You may obtain a copy of 
 * the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
 * KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED
 * WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
 * MERCHANTABLITY OR NON-INFRINGEMENT. 
 *  
 * See the Apache 2 License for the specific language governing permissions and
 * limitations under the License.
 * 
 ******************************************************************************* */


using System;
using System.Windows;
using System.Windows.Controls;

namespace DataConnectorUI2
{
    public partial class ErrorWindow : ChildWindow
    {
        public ErrorWindow(Exception e)
        {
            InitializeComponent();
            if (e != null)
            {
                ErrorTextBox.Text = e.Message + Environment.NewLine + Environment.NewLine + e.StackTrace;
            }
        }

        public ErrorWindow(Uri uri)
        {
            InitializeComponent();
            if (uri != null)
            {
                ErrorTextBox.Text = "Page not found: \"" + uri.ToString() + "\"";
            }
        }

        public ErrorWindow(string message, string details)
        {
            InitializeComponent();
            ErrorTextBox.Text = message + Environment.NewLine + Environment.NewLine + details;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}