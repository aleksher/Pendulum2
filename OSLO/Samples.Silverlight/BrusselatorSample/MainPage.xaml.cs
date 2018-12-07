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
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.Oslo;

namespace Brusselator
{
    /// <summary>Solves Brusselator system using RK method</summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            var rk = Ode.RK547M(
                0, // Initial time point
                new Vector(1.01, 3), //  Initial X vector
                Brusselator); // Right part

            // Append time step as extra vector component and solve from t = 0 to 20 and output points with step 0.01
            var ed = rk.AppendStep().SolveFromToStep(0, 20, 0.01).ToArray();

            // Draw numeric solution
            x1 = (LineGraph)plotter.FindName("x1");
            x1.PlotY(ed.Select(sp => sp.X[0]));
            
            x2 = (LineGraph)plotter.FindName("x2");
            x2.PlotY(ed.Select(sp => sp.X[1]));

        }

        public Vector Brusselator(double t, Vector yv)
        {           
            double[] y = yv; // For efficience purpose it is better to work directly with array that is inside vector

            return new Vector(
              1+y[0]*y[0]*y[1]-4*y[0],
              3*y[0]-y[0]*y[0]*y[1]);
        }
    }
}
