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

namespace MHCSample
{
    /// <summary>Solves and plots MHC sample</summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            var rk = Ode.GearBDF(
                0, // Initial time point
                Vector.Zeros(16), //  Initial X vector
                MHC,// Right part
                new Options // Optional options
                {
                    RelativeTolerance = 1e-3,AbsoluteTolerance=1e-6
                });

            // Solve to t = 2.0 and store results in array
            var ed = rk.SolveFromToStep(0,1000,0.05).ToArray(); 

            // Draw numeric solution
            x1 = (LineGraph)plotter.FindName("x1");
            x1.PlotY(ed.Select(sp => sp.X[0]));

            x2 = (LineGraph)plotter.FindName("x2");
            x2.PlotY(ed.Select(sp => sp.X[1]));

            x3 = (LineGraph)plotter.FindName("x3");
            x3.PlotY(ed.Select(sp => sp.X[2]));
            
            x4 = (LineGraph)plotter.FindName("x4");
            x4.PlotY(ed.Select(sp => sp.X[3]));

            x5 = (LineGraph)plotter.FindName("x5");
            x5.PlotY(ed.Select(sp => sp.X[4]));

            x6 = (LineGraph)plotter.FindName("x6");
            x6.PlotY(ed.Select(sp => sp.X[5]));
            
            x7 = (LineGraph)plotter.FindName("x7");
            x7.PlotY(ed.Select(sp =>  sp.X[6]));

            x8 = (LineGraph)plotter.FindName("x8");
            x8.PlotY(ed.Select(sp => sp.X[7]));

            x9 = (LineGraph)plotter.FindName("x9");
            x9.PlotY(ed.Select(sp => sp.X[8]));
            
            x10 = (LineGraph)plotter.FindName("x10");
            x10.PlotY(ed.Select(sp =>  sp.X[9]));

            x11 = (LineGraph)plotter.FindName("x11");
            x11.PlotY(ed.Select(sp =>  sp.X[10]));

            x12 = (LineGraph)plotter.FindName("x12");
            x12.PlotY(ed.Select(sp =>  sp.X[11]));
            
            x13 = (LineGraph)plotter.FindName("x13");
            x13.PlotY(ed.Select(sp =>  sp.X[12]));

            x14 = (LineGraph)plotter.FindName("x14");
            x14.PlotY(ed.Select(sp => sp.X[13]));

            x15 = (LineGraph)plotter.FindName("x15");
            x15.PlotY(ed.Select(sp => sp.X[14]));
            
            x16 = (LineGraph)plotter.FindName("x16");
            x16.PlotY(ed.Select(sp =>  sp.X[15]));
        }

        public Vector MHC(double t, Vector yv)
        {
            var gt = 1505;
            var dt = 0.0017;
            var gm = 150.5000;
            var dm = 7.9892e-005;
            var dme = 9.3293e-005;
            var b = 3.1773e-011;
            var dp = 0.1300;
            var bt = 1.6628e-009;
            var ut = 1.1846e-006;
            var e = 0.1142;
            var c = 8.3029e-008;
            var q = 2.1035e+004;
            var v = 936.3137;
            Vector u = new Vector(8.7641e-004, 5.6584e-006, 4.1766e-007);
            Vector gp =new Vector(2.0931e+004, 1.7593e+004, 1.0639e+004);

            //Number of peptide types
            var n = u.Length;    
            var dP = Vector.Zeros(n);
            var dMP = Vector.Zeros(n);
            var dTMP = Vector.Zeros(n);
            var dMeP = Vector.Zeros(n);


            var T=yv[0]; var M=yv[1]; var TM=yv[2]; 
 
            var  P=Vector.Zeros(3+n-4+1);
            for(int i=0;i<P.Length;i++)
            {
                P[i]=yv[3+i];
            }
            
            var  MP=Vector.Zeros(3+2*n-4-n+1);
            for(int i=0;i<MP.Length;i++)
            {
                MP[i]=yv[3+n+i];
            }
            
            var  TMP=Vector.Zeros(3+3*n-4-2*n+1);
            for(int i=0;i<P.Length;i++)
            {
                TMP[i]=yv[3+2*n+i];
            }
            
            var  MeP=Vector.Zeros(3+4*n-4-3*n+1);
            for(int i=0;i<P.Length;i++)
            {
                MeP[i]=yv[3+3*n+i];
            }
            var Me=yv[3+3*n];
       
            double[] y = yv; // For efficience purpose it is better to work directly with array that is inside vector

            // Assign derivatives
            var dT = ut*TM + ut*v*TMP.Sum + gt - T*(bt*M + dt);
            var dM = u*MP + ut*TM + gm - M*(b*P.Sum + bt*T + dm);
            var dTM = bt*M*T + q*(u*TMP) - TM*(ut + c*P.Sum);
            for(int i=0;i<n;i++)
            { dP[i] = u[i]*MP[i] + q*u[i]*TMP[i] + gp[i] - P[i]*(b*M + c*TM + dp);
                dMP[i] = b*M*P[i] + ut*v*TMP[i] - MP[i]*(u[i] + e);
                dTMP[i] = c*TM*P[i] - TMP[i]*(q*u[i] + ut*v);
                dMeP[i] = e*MP[i] - u[i]*MeP[i];
            }
            var dMe = u*MeP - dme*Me;

            Vector ans = Vector.Zeros(16);
            ans[0] = dT;
            ans[1] = dM;
            ans[2] = dTM;
            for (int i = 3; i < 3 + n; i++)
            {
                ans[i] = dP[i - 3];
            }
            for (int i = 3+n; i < 3 + 2*n; i++)
            {
                ans[i] = dMP[i - 3-n];
            }
            for (int i = 3+2*n; i < 3 + 3*n; i++)
            {
                ans[i] = dTMP[i - 3-2*n];
            }
            for (int i = 3+3*n; i < 3 + 4*n; i++)
            {
                ans[i] = dMeP[i - 3-3*n];
            }
            ans[3 + 4 * n] = dMe;
            return ans;
        }
    } 
}
