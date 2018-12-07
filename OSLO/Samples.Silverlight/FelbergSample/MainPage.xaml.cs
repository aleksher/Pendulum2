using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Research.Oslo;
using Microsoft.Research.DynamicDataDisplay;

namespace FelbergSample
{
    /// <summary>
    /// Solver and plots Felberg system using R-K method (both numeric and analytic methods)
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            var rk = Ode.GearBDF(
                0, // Initial time point
                new Vector(1, Math.E), // Initial X vector
                Felberg, // Right part
                new Options { RelativeTolerance = 1e-5 }); // Set relative tolerance

            // Append time step as extra vector component and solve from t = 0 to 5 
            //and output points with step 0.01
            var ed = rk.AppendStep().SolveFromToStep(0, 5, 0.01).ToArray();

            // Draw numeric solution
            x1 = (LineGraph)plotter.FindName("x1");
            x1.Plot(ed.Select(sp => sp.T),ed.Select(sp => sp.X[0]));

            x2 = (LineGraph)plotter.FindName("x2");
            x2.Plot(ed.Select(sp => sp.T),ed.Select(sp => sp.X[1]));

            x1_1 = (LineGraph)plotter.FindName("x1_1");
            x1_1.Plot(ed.Select(sp => sp.T),ed.Select(sp => Math.Exp(Math.Sin(sp.T*sp.T))));

            x2_1 = (LineGraph)plotter.FindName("x2_1");
            x2_1.Plot(ed.Select(sp => sp.T),ed.Select(sp => Math.Exp(Math.Cos(sp.T * sp.T))));
        }

        public Vector Felberg(double t, Vector yv)
        {
            double[] y = yv; // For efficience purpose it is better to work directly with array that is inside vector

            return new Vector(
                2 * t * y[0] * Math.Log(Math.Max(1e-3, y[1])),
                -2 * t * y[1] * Math.Log(Math.Max(1e-3, y[0])));
        }
    }
}
