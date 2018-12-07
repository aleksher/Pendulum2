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

namespace TrigonometricSample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            //Try to solve ODE dx / dt = Sin(t), x(0) = 0
            //An example of RK547M method use
            var rk1 = Ode.RK547M(0, 20, new Microsoft.Research.Oslo.Vector(0),
                 (t, x) => { return new Vector(Math.Cos(t)); }// Right part
                 );
            //An example of Gear method use
            var rk2 = Ode.GearBDF(0, 20, new Microsoft.Research.Oslo.Vector(0),
               (t, x) => { return new Vector(Math.Cos(t)); }// Right part
               );

            SinT.Plot(rk2.Select(x => x.T).ToArray(), rk2.Select(x => Math.Sin(x.T)).ToArray());
            SinRK.Plot(rk1.Select(x => x.T).ToArray(), rk1.Select(x => x.X[0]).ToArray());
            SinGear.Plot(rk2.Select(x => x.T).ToArray(), rk2.Select(x => x.X[0]).ToArray());


        }
    }
}
