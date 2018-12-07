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

namespace OregonatorSample
{
    /// <summary>Solves and plots stiff Oregonator system using Gear BDF method</summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            var gear = Ode.GearBDF(
                 0, // Initial time point
                new Vector(0.001d, 0.004d, 0.0002d), //  Initial X vector
                Oregonator// Right part
                );

            var ed = gear.SolveFromToStep(0.0d,1000.0,0.1d).ToArray();

            // Draw numeric solution
            x1 = (LineGraph)plotter.FindName("x1");
            x1.Plot(ed.Select(sp => sp.T),ed.Select(sp => sp.X[0]));

            x2 = (LineGraph)plotter.FindName("x2");
            x2.Plot(ed.Select(sp => sp.T),ed.Select(sp => sp.X[1]));
            
            x3 = (LineGraph)plotter.FindName("x3");
            x3.Plot(ed.Select(sp => sp.T),ed.Select(sp => sp.X[2]));
        }

        public Vector Oregonator(double t, Vector yv)
        {            
            double[] y = yv; // For efficience purpose it is better to work directly with array that is inside vector

            return new Vector(
               77.27d*(y[1]+y[0]*(1-8.375d*1e-6d*y[0]-y[1])),
               1/77.27d*(y[2]-(1+y[0])*y[1]),
               0.161d*(y[0]-y[2])
             );
        }
    }
}