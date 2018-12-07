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


namespace FloralMateriaSample
{
    /// <summary>Solver Floral Material stiff system using Gear method</summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            var gear = Ode.GearBDF(
                     0, // Initial time point
                    new Vector(1,0,0,0,0,0,0,0.0057d), //  Initial X vector
                    FloralMaterial, // Right part
                    new Options() { RelativeTolerance = 1e-4, AbsoluteTolerance = 1e-6, MaxStep=0.7d}); 
           
            // Append time step as extra vector component and solve from t = 0 to 200 and output points with step 0.05
            var ed = gear.AppendStep().SolveFromToStep(0, 200,0.05).ToArray();
          

            // Draw numeric solution
            var x1data=ed.Select(p => new Point(p.T, p.X[0])).ToArray();
            x1.Plot(x1data.Select(x => x.X).ToArray(), x1data.Select(x => x.Y).ToArray());

            var x2data = ed.Select(p => new Point(p.T, p.X[1])).ToArray();
            x2.Plot(x2data.Select(x => x.X).ToArray(), x2data.Select(x => x.Y).ToArray());

            var x3data = ed.Select(p => new Point(p.T, p.X[2])).ToArray();
            x3.Plot(x3data.Select(x => x.X).ToArray(), x3data.Select(x => x.Y).ToArray());

            var x4data = ed.Select(p => new Point(p.T, p.X[3])).ToArray();
            x4.Plot(x4data.Select(x => x.X).ToArray(), x4data.Select(x => x.Y).ToArray());

            var x5data = ed.Select(p => new Point(p.T, p.X[4])).ToArray();
            x5.Plot(x5data.Select(x => x.X).ToArray(), x5data.Select(x => x.Y).ToArray());

            var x6data = ed.Select(p => new Point(p.T, p.X[5])).ToArray();
            x6.Plot(x1data.Select(x => x.X).ToArray(), x6data.Select(x => x.Y).ToArray());

            var x7data = ed.Select(p => new Point(p.T, p.X[6])).ToArray();
            x7.Plot(x7data.Select(x => x.X).ToArray(), x7data.Select(x => x.Y).ToArray());

            var x8data = ed.Select(p => new Point(p.T, p.X[7])).ToArray();
            x8.Plot(x8data.Select(x => x.X).ToArray(), x8data.Select(x => x.Y).ToArray());

        }

        public Vector FloralMaterial(double t, Vector yv)
        {
            double[] y = yv; // For efficience purpose it is better to work directly with array that is inside vector

            return new Vector(
                -1.71d*y[0]+0.43d*y[1]+8.23d*y[2]+0.0007d,
                1.71d*y[0]-8.75d*y[1],
                -10.03d*y[2]+0.43d*y[3]+0.035d*y[4],
                8.32d*y[1]+1.71d*y[2]-1.12d*y[3],
                -1.756d*y[4]+0.43d*y[5]+0.43d*y[6],
                -280d*y[5]*y[7]+0.69d*y[3]+1.71d*y[4]-0.43d*y[5]-0.69d*y[6],
                280d*y[5]*y[7]-1.87d*y[6],
                -280d*y[5]*y[7]+1.87d*y[6]
                );
        }
    }
}
