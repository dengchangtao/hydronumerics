﻿#pragma checksum "..\..\Window1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "56F671FB69030107F18AAA06EDD37C5E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HelixToolkit;
using SurfaceDemo;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SurfaceDemo {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SurfaceDemo.Window1 thisWindow;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu mainMenu;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ViewSource;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Fullscreen;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem surfacesMenu;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel SourcePanel;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slider1;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slider2;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slider3;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider slider4;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView errorList;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox source1;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HelixToolkit.HelixView3D view1;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SurfaceDemo.DynamicCodeSurface3D surface1;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HelixToolkit.StereoView3D view2;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\Window1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HelixToolkit.AnaglyphView3D view3;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SurfaceDemo;component/window1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.thisWindow = ((SurfaceDemo.Window1)(target));
            return;
            case 2:
            this.mainMenu = ((System.Windows.Controls.Menu)(target));
            return;
            case 3:
            
            #line 15 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Export_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 17 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ViewSource = ((System.Windows.Controls.MenuItem)(target));
            
            #line 20 "..\..\Window1.xaml"
            this.ViewSource.Click += new System.Windows.RoutedEventHandler(this.ViewSource_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 21 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ResetCamera_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Fullscreen = ((System.Windows.Controls.MenuItem)(target));
            
            #line 22 "..\..\Window1.xaml"
            this.Fullscreen.Click += new System.Windows.RoutedEventHandler(this.FullScreen_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 24 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Stereo_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 25 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Stereo_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 26 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Stereo_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 29 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.BrushHue_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 30 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.BrushHueStripes_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 31 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.BrushRainbow_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 32 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.BrushRainbowStripes_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 33 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.BrushPattern_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 34 "..\..\Window1.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.BrushImage_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.surfacesMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 18:
            this.SourcePanel = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 19:
            this.slider1 = ((System.Windows.Controls.Slider)(target));
            return;
            case 20:
            this.slider2 = ((System.Windows.Controls.Slider)(target));
            return;
            case 21:
            this.slider3 = ((System.Windows.Controls.Slider)(target));
            return;
            case 22:
            this.slider4 = ((System.Windows.Controls.Slider)(target));
            return;
            case 23:
            this.errorList = ((System.Windows.Controls.ListView)(target));
            return;
            case 24:
            this.source1 = ((System.Windows.Controls.TextBox)(target));
            
            #line 81 "..\..\Window1.xaml"
            this.source1.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.source1_TextChanged);
            
            #line default
            #line hidden
            return;
            case 25:
            this.view1 = ((HelixToolkit.HelixView3D)(target));
            return;
            case 26:
            this.surface1 = ((SurfaceDemo.DynamicCodeSurface3D)(target));
            return;
            case 27:
            this.view2 = ((HelixToolkit.StereoView3D)(target));
            return;
            case 28:
            this.view3 = ((HelixToolkit.AnaglyphView3D)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
