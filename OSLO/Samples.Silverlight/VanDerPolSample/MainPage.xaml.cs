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

namespace VanDerPolSample
{
    /// <summary>Solver Van der Pol with A = 200 using both Gear and RK method</summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            var gear = Ode.GearBDF(
                0, // Initial time point
                new Vector(2.0, 0.0), //  Initial X vector
                VanDerPol, new Options { MinStep = 1e-6, MaxStep = 1e-1 }); // Right part

            var rk = Ode.RK547M(
                0, // Initial time point
                new Vector(2.0, 0.0), //  Initial X vector
                VanDerPol, new Options { MinStep = 1e-6, MaxStep = 1e-1 }); // Right part
           
            // Append time step as extra vector component and solve from t = 0 to 20 and output points with step 0.01
            var edg = gear.AppendStep().SolveFromTo(0.0, 20.0).ToArray();
            var edrk = rk.AppendStep().SolveFromTo(0.0, 20.0).ToArray();

            // Draw numeric solution
            x1 = (LineGraph)plotter.FindName("x1");
            x1.PlotY(edrk.Select(sp =>sp.X[0]));

            x2 = (LineGraph)plotter.FindName("x2");
            x2.PlotY(edrk.Select(sp => sp.X[1]));

            x1_1 = (LineGraph)plotter.FindName("x1_1");
            x1_1.PlotY(edg.Select(sp => sp.X[0]));

            x2_1 = (LineGraph)plotter.FindName("x2_1");
            x2_1.PlotY(edg.Select(sp => sp.X[1]));

        }

        public Vector VanDerPol(double t, Vector yv)
        {
            double a = 2000.0; // Stiff Van der Pol
            
            double[] y = yv; // For efficience purpose it is better to work directly with array that is inside vector

            return new Vector(
                y[1],
                -a * (y[1] * (y[0] * y[0] - 1) + y[0])
                );
        }
    }
}