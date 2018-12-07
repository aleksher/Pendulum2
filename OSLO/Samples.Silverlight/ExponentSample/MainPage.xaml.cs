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
namespace ExponentSample
{
    public partial class MainPage : UserControl
    { 
        public MainPage()
        {
            InitializeComponent();

            //An example of Gear method use
            var rk1 = Ode.GearBDF(
                 0, // Initial time point
                 new Microsoft.Research.Oslo.Vector(1000.0), //  Initial X vector
                 (t, x) => { return new Vector(-0.01 * x[0]); }, // Right part
                 new Options {MaxStep=5.0d,NumberOfIterations=4}); 
            
            var eds1 = rk1.AppendStep().SolveFromToStep(0, 2000,0.1).ToArray();
            var x11 = eds1.Select(p => new Point(p.T, p.X[0])).ToArray();
            
            linegraph1.Plot(x11.Select(x => x.X).ToArray(), x11.Select(x => x.Y).ToArray());

            //An example of RK method use
            var rk2 = Ode.RK547M(
                0, // Initial time point
                new Microsoft.Research.Oslo.Vector(1000.0), //  Initial X vector
                (t, x) => { return new Vector(-0.01 * x[0]); }); // Right part

            var eds2 = rk2.AppendStep().SolveFromToStep(0, 2000,0.1).ToArray();
            var x12 = eds2.Select(p => new Point(p.T, p.X[0])).ToArray();
            linegraph2.Plot(x12.Select(x => x.X).ToArray(), 
                            x12.Select(x => 1000*Math.Exp(-0.01d*x.X)).ToArray());

            var err=x11.Max(x=>Math.Abs(x.Y-1000*Math.Exp(-0.01d*x.X)));

        }
    }
}
