using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Research.Science;
using Microsoft.Research.Science.Data;
using sds = Microsoft.Research.Science.Data;
using Microsoft.Research.Science.Data.Imperative;
using Microsoft.Research.Oslo;



namespace DataSetSample
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // const double tmax=100, dt=0.0005, a=5, b=15, c=1;   
            //double x = 3.051522, y = 1.582542, z = 15.62388;   
            // create new memory dataset as shared  
            var dataset = DataSet.OpenShared("msds:memory2");   
            // define variables 
            dataset.Add<double[]>("t", "t");   
            dataset.Add<double[]>("x", "t");   
            dataset.Add<double[]>("y", "t");   
            dataset.Add<double[]>("z", "t");
            dataset.PutAttr(0, "VisualHints", "z(t) Style:Polyline; Stroke:#FF0000;;y(t) Style:Polyline; Stroke:#00FF00;;x(t) Style:Polyline;Stroke:#0000FF;; ");
            int No = 3;
            var tsim = new List<double>();
            var xsim = new List<double>[No];
            for (int i = 0; i < No; i++)
                xsim[i] = new List<double>();

            var ex = new OscillatingDNA();
            ex.solve(ref tsim, ref xsim);

            var dt = new List<double>();
            dt.Add(0.0);
            for (int i = 1; i < tsim.Count; i++)
                dt.Add(tsim[i] - tsim[i - 1]);

            foreach(var t in tsim)
                dataset.Append("t", t);
            foreach (var x1 in xsim[0])
                dataset.Append("x", x1);
            foreach (var x2 in xsim[1])
                dataset.Append("y", x2);
            foreach (var x3 in xsim[2])
                dataset.Append("z", x3);

            MySDSviewer.DataSet = dataset;
            Console.WriteLine("Waiting for the DataSet Viewer...");   
        
        }
    }
}
