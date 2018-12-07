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

namespace PopulationDynamics
{
    public partial class MainPage : UserControl
    {
       public MainPage()
        {
            InitializeComponent();
            var rk = Ode.GearBDF(
                 0, // Initial time point
                 new Microsoft.Research.Oslo.Vector(0.4,  0.13), //  Initial X vector
                 Population); // Right part
    
            var eds = rk.AppendStep().SolveFromToStep(0,200, 0.1).ToArray();
            var x1 = eds.Select(p => new Point(p.T, p.X[0])).ToArray();
            var x2 = eds.Select(p => new Point(p.T, p.X[1])).ToArray();

            //Draw 1st population dynamics graph
            linegraph1.Plot(x1.Select(x=>x.X).ToArray(),x1.Select(x=>x.Y).ToArray());
            
            //Draw 2nd population dynamics graph
            linegraph2.Plot(x2.Select(x => x.X).ToArray(), x2.Select(x => x.Y).ToArray());
            
            //Draw phase portrait for system
            Portrait.Plot(x1.Select(x=>x.Y).ToArray(),x2.Select(x=>x.Y).ToArray());
        }


     

        public Microsoft.Research.Oslo.Vector Population(double t, Microsoft.Research.Oslo.Vector yv)
        {
            double[] y = yv;

             var a1=0.2d;var a2=0.15d;var b1=0.3d;var b2=0.2d;
         
             return new Microsoft.Research.Oslo.Vector(
            y[0]*(a1-b1*y[1]),y[1]*(b2*y[0]-a2)
                 );
        
        }

        public Microsoft.Research.Oslo.Vector PopulationWithControl(double t, Microsoft.Research.Oslo.Vector yv)
        {
            double[] y = yv;

            var a1 = 0.2d; var a2 = 0.15d; var beta1 = 0.3d; var beta2 = 0.2d;
            var b1 = 0.78d; var b2 = 1 - b1;

            var c1 = 0.19d; var c2 = 1 - c1;
            var val=-y[2]*b1*y[1]-y[3]*b2*y[1]-c2;
            var sgn = (double)val/(Math.Sqrt(0.1d+val*val));
            var u = sgn >= 0.0d ? 0.013*sgn : 0.0d;
            return new Microsoft.Research.Oslo.Vector(
            y[0] * (a1 - beta1 * y[1])-b1*u*y[1], y[1] * (beta2 * y[0] - a2)-b2*u*y[1],
           -y[2]*a1+y[2]*beta1*y[1]-y[3]*y[1]*beta2+c1,
            y[2]*y[0]*beta1+y[2]*b1*u-y[3]*beta2*y[0]+y[3]*a2+y[3]*b2*u);

        }

        
    
    }
}
