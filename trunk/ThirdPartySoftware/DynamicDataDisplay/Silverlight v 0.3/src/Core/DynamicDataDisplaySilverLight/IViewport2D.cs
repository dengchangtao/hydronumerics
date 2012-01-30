﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.Research.DynamicDataDisplay
{

    public interface IViewport2D
    {
        Rect Visible { get; set; }
        Rect Output { get; }
        CoordinateTransform Transform { get; set; }
        void FitToView();
    }
}
