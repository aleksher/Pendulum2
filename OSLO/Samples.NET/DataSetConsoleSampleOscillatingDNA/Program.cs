using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Research.Science.Data;
using Microsoft.Research.Science.Data.Imperative;

namespace DataSetConsoleSampleOscillatingDNA
{
    class Program
    {
        // This example relies on the use of Microsoft Research Dataset Viewer, which is available 
        // at http://research.microsoft.com/en-us/um/cambridge/groups/science/tools/datasetviewer/datasetviewer.htm
        static void Main(string[] args)
        {
            var dataset = DataSet.OpenShared("msds:memory2");
            // define variables 
            dataset.Add<double[]>("t", "t");
            dataset.Add<double[]>("x", "t");
            dataset.Add<double[]>("y", "t");
            dataset.Add<double[]>("z", "t");
            dataset.SpawnViewer("z(t) Style:Polyline; Stroke:#FF0000;;y(t) Style:Polyline; Stroke:#00FF00;;x(t) Style:Polyline; Stroke:#0000FF;;");
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

            foreach (var t in tsim)
                dataset.Append("t", t);
            foreach (var x1 in xsim[0])
                dataset.Append("x", x1);
            foreach (var x2 in xsim[1])
                dataset.Append("y", x2);
            foreach (var x3 in xsim[2])
                dataset.Append("z", x3);
            Console.WriteLine("Waiting for the DataSet Viewer...");   
        }
    }
}
