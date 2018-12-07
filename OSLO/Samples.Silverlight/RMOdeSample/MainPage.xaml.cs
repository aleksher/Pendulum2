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

namespace RMOdeSample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            var rk = Ode.RK547M(
                0, // Initial time point
                new Vector(0.2,0.4), //  Initial X vector
                RM); // Right part

            // Append time step as extra vector component and solve from t = 0 to 500 and output points with step 0.25
            var ed = rk.AppendStep().SolveFromToStep(0,500,0.25).ToArray();  

            // Draw numeric solution
            x1 = (LineGraph)plotter.FindName("x1");
            x1.PlotY(ed.Select(sp => sp.X[0]));

            x2 = (LineGraph)plotter.FindName("x2");
            x2.PlotY(ed.Select(sp => sp.X[1]));

            step = (LineGraph)plotter.FindName("step");
            step.PlotY(ed.Select(sp => sp.X[2]));

        }

        public Vector RM(double t, Vector yv)
        {
            double alpha = 0.2;
            double kappa = 0.14;
            double sigma = 0.15;

            double[] y = yv; // For efficience purpose it is better to work directly with array that is inside vector

            return new Vector(
                y[0] * (1 - y[0]) - y[0] * y[1] / (kappa + y[0]),
                alpha * y[0] * y[1] / (kappa + y[0]) - sigma * y[1]);
        }
    }
}
