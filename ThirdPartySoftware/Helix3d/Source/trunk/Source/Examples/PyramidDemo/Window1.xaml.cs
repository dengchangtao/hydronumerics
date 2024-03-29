﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit;

namespace PyramidDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        // testing performance on big model...

        // Great pyramid of giza 280:440:440 cubits,  total ~ 2 301 370 cubits

        // http://en.wikipedia.org/wiki/Great_Pyramid_of_Giza
        // http://en.wikipedia.org/wiki/Cubits

        // cheating: only drawing outside layer of cubits
        // cheating: divided each dimension by 10...

        private readonly CubeVisual3D cubit;
        private double a = 0.524;
        private double b = 0.95;
        private int nside = 150;
        private int nvert = 150;

        public Window1()
        {
            InitializeComponent();

            cubit = new CubeVisual3D { SideLength = a * b, Fill = Brushes.SandyBrown };
            Loaded += Window1_Loaded;
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            // cubit.Model.Freeze();
            // cubit.Material.Freeze();
            var cuboidGeometry = cubit.Model.Geometry as MeshGeometry3D;

            var vis = new ModelVisual3D();

            bool allInOneGeometry = true;

            MeshBuilder builder = null;

            if (allInOneGeometry)
                builder = new MeshBuilder();

            int countAll = 0;
            int countModelled = 0;
            for (int i = 0; i < nvert; i++)
            {
                double m = nside - i * (double)nside / nvert;
                var mx = (int)m;
                Debug.WriteLine(i + ": " + mx);
                for (int j = 0; j <= mx; j++)
                {
                    for (int k = 0; k <= mx; k++)
                    {
                        countAll++;

                        // only adding blocks on the outside...
                        if (j > 0 && j < mx - 1 && i > 0 && i < nvert - 1 && k > 0 && k < mx - 1)
                            continue;

                        countModelled++;

                        var center = new Point3D(a * (j - (double)mx / 2), a * (k - (double)mx / 2), (i + 0.5) * a);

                        if (allInOneGeometry)
                            builder.AddBox(center, a * b, a * b, a * b);
                        else
                        {
                            // add a new visual for each cubit, but reuse the geometry
                            var transform = new TranslateTransform3D(center.ToVector3D());
                            var c = new ModelVisual3D
                                        {
                                            Content =
                                                new GeometryModel3D
                                                    {
                                                        Geometry = cuboidGeometry,
                                                        Material = cubit.Material,
                                                        Transform = transform
                                                    }
                                        };
                            vis.Children.Add(c);
                        }
                    }
                }
            }

            if (allInOneGeometry)
            {
                var c = new ModelVisual3D
                            {
                                Content =
                                    new GeometryModel3D
                                        {
                                            Geometry = builder.ToMesh(),
                                            Material = cubit.Material
                                        }
                            };
                view1.Children.Add(c);
            }
            else
            {
                view1.Children.Add(vis);
            }

            var mg = cubit.Model.Geometry as MeshGeometry3D;
            int ntri = mg.TriangleIndices.Count / 3;
            int ntriTotal = ntri * countModelled;

            if (allInOneGeometry)
                ntriTotal = builder.TriangleIndices.Count / 3;

            Title = "Mini Pyramid, total " + countAll + " cubits, rendered with " + countModelled + " cubits / " + ntriTotal +
                    " triangles.";

            view1.LookAt(new Point3D(0, 0, a * nvert / 2), new Vector3D(-70, -60, -100), 0);
        }
    }
}