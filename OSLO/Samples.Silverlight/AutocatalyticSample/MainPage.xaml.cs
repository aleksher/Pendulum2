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

namespace AutocatalyticSample
{
    public partial class MainPage : UserControl
    {
        /// <summary>
        /// Sample: solving a system of ordinary differential equations (ODE) using the Oslo library,
        /// and visualizing results using D3 for Silverlight library (http://dynamicdatadispaly.codeplex.com).
        /// 
        /// Basically, the system is 
        ///
        /// X_t = k * B * X, 
        /// B_t = -k * B * X,
        /// P_t= k * B* X.
        ///
        // It is easily seen that there is a positive feedback with X, that leads to an autocatalytic behaviour. 
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            int No = 4;

            var t = new List<double>[No];
            var x = new List<double>[No];
            var sps=new List<double>[No];
            var spt = new List<double>[No];

            for (int j = 0; j < No; j++)
            {
                t[j] = new List<double>();
                x[j] = new List<double>();
                sps[j] = new List<double>();
                spt[j] = new List<double>();
            }

            int imax = 0;
            
            for (int j = 0; j < No; j++)
            {
                solveExternalExample(j, ref t[j], ref x[j]);
                if (x[j].Count > imax)
                    imax = x[j].Count;
            }

        
            for (int i = 0; i < imax; i++)
            {
                for (int j = 0; j < No; j++)
                {
                    if (x[j].Count > i)
                    {
                        sps[j].Add(x[j][i]);
                        spt[j].Add(t[j][i]/3600);
                    }                  
                }
            }              


            // Draw numeric solution
            x1 = (LineGraph)plotter.FindName("x1");
            x1.Plot(spt[0],sps[0]);

            x2 = (LineGraph)plotter.FindName("x2");
            x2.Plot(spt[1],sps[1]);

            x3 = (LineGraph)plotter.FindName("x3");
            x3.Plot(spt[2],sps[2]);

            x4 = (LineGraph)plotter.FindName("x4");
            x4.Plot(spt[3],sps[3]);
        }


        private static void solveExternalExample(int option,  ref List<double> tsim, ref List<double> xsim)
        {
            // Ode options and initial conditions, then solve
            double onex = 50.0;
            var treat = new double[] { 0.0, 0.1, 0.3, 1.0 };
            var ks = new double[] { 4.91e-6, 2.22e-6, 1.72e-6 };
            var ics = new double[] { 0.12, 0.25 };
            var bad = 0.1;

            var opts = new Options { MaxStep = 3600.0, RelativeTolerance = 1e-3 };
          
            var x0 = initialCondition(ics, onex);

            x0[0] += treat[option] * onex * (1 - bad);

            
            var sol = Ode.GearBDF(0.0, 20 * 3600.0, x0, (t, x) => derivs(t, x, ks));

            // Write to variables
            foreach (var sp in sol)
            {
                tsim.Add(sp.T);
                xsim.Add(sp.X[3]);
            }
        }

        private static Vector initialCondition(double[] pars, double onex)
        {
            // Read in model parameters
            var leak_BX2X = pars[0];
            var badB = pars[1];
            
            // Assign values to x0
            int n = 4;
            var x0 = Vector.Zeros(n);
            x0[0] = leak_BX2X * onex;
            x0[2] = onex * (1 - badB);

            return (x0);
        }

        private static Vector derivs(double t, Vector x, double[] pars)
        {
            // Declare variables for concentrations
            double X, Y, B, PX;

            // Declare variables for derivatives
            double dX, dY, dB, dPX;
            double[] dxdt;

            // Assign states
            X = x[0];
            Y = x[1];
            B = x[2];
            PX = x[3];

            double r_1;
            r_1 = pars[1] * B * X;

            // Assign derivatives
            dX = r_1;
            dY = 0.0;
            dB = -r_1;
            dPX = r_1;
            dxdt = new double[] { dX, dY, dB, dPX };

            return (new Vector(dxdt));
        }
    
    }
}
