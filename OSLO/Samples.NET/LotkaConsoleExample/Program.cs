using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Research.Oslo;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Research.Science.Data;
using Microsoft.Research.Science.Data.Imperative;

namespace LotkaConsoleExample
{
    class Program
    {
        // This example relies on the use of Microsoft Research Dataset Viewer, which is available 
        // at http://research.microsoft.com/en-us/um/cambridge/groups/science/tools/datasetviewer/datasetviewer.htm
        static void Main(string[] args)
        {
            lotkaExample();
        }

        public static void lotkaExample()
        {
            var tfinal = 200000.0; // Final time for simulation
            // Species names map to UnaryRx |-> [y2]{t^*}, UnaryLxLL_1 |-> {x^*}[i]:[y1l t^]<y1 x^>:[y1l t^]<y1 x^>, UnaryLxLL |-> {t^*}[y1 x^]<i y1l t^ y1l t^>, SpeciesR |-> <y2 t^ y2r>, sp_1 |-> [y2 t^]<y2r>, sp_0 |-> <y2>, SpeciesL |-> <y1l t^ y1 x^>, sp_3 |-> <y1l>[t^ y1 x^], sp_2 |-> <y1 x^ i y1l t^ y1l t^>, sp_5 |-> <y1>[x^ i y1l t^ y1l t^], sp_4 |-> <i>, BinaryLRxRR_1 |-> {x^*}[y2 i.1]:<y2>[t^ y2r]:<y2>[t^ y2r], BinaryLRxRR |-> {t^*}[y1 x^ y2]<i.1 t^ y2r t^ y2r>{t^*}, sp_7 |-> {t^*}[y1 x^ y2]<i.1 t^ y2r t^ y2r>:<y2>[t^]<y2r>, sp_9 |-> <y1l>[t^ y1 x^]:[y2 t^]<y2r>, sp_8 |-> <y1 x^ y2 i.1 t^ y2r t^ y2r>, sp_11 |-> <y1>[x^ y2 i.1 t^ y2r t^ y2r], sp_10 |-> <y2 i.1>, sp_6 |-> <y1l>[t^ y1 x^]:<y1 x^>[y2]<i.1 t^ y2r t^ y2r>{t^*}
            var species = new string[] { "UnaryRx", "UnaryLxLL_1", "UnaryLxLL", "SpeciesR", "sp_1", "sp_0", "SpeciesL", "sp_3", "sp_2", "sp_5", "sp_4", "BinaryLRxRR_1", "BinaryLRxRR", "sp_7", "sp_9", "sp_8", "sp_11", "sp_10", "sp_6" }; //% The list of all species
            int n = species.Length;
            Vector x0 = Vector.Zeros(n);

            // Assign initial conditions
            var UnaryRx = x0[0] = 1000.0;
            var UnaryLxLL_1 = x0[1] = 100000.0;
            var UnaryLxLL = x0[2] = 1000.0;
            var SpeciesR = x0[3] = 1000.0;
            var SpeciesL = x0[6] = 1000.0;
            var BinaryLRxRR_1 = x0[11] = 3000000.0;
            var BinaryLRxRR = x0[12] = 30000.0;

            var maxstep = 1.0;
            var opts = new Options { MaxStep = maxstep };

            // Solve the ODEs
            Console.WriteLine("Time,SpeciesL,SpecieR");
            var dataset = DataSet.OpenShared("msds:memory2");   
            // define variables 
            dataset.Add<double[]>("t", "t");   
            dataset.Add<double[]>("SpecieL", "t");   
            dataset.Add<double[]>("SpecieR", "t");   
            dataset.SpawnViewer("SpecieR(Time) Style:Polyline; Thickness:1; Visible:-6565.19579745115,21.3352279262563,219999.15388962,1004.11943342034;;SpecieL(Time) Style:Polyline; Thickness:1; Visible:-6565.19579745115,21.3352279262563,219999.15388962,1004.11943342034");
          
           foreach (var sp in Ode.GearBDF(0.0, x0, f, opts).SolveTo(tfinal))
            {
               
                Console.WriteLine("Time={0}, SpeciesL={1}, SpecieR={2}", 
                    sp.T.ToString(CultureInfo.InvariantCulture),
                    sp.X[3].ToString(CultureInfo.InvariantCulture), 
                    sp.X[6].ToString(CultureInfo.InvariantCulture));
                dataset.Append("t", sp.T);
                dataset.Append("SpecieL", sp.X[3]);
                dataset.Append("SpecieR", sp.X[6]);
            }

           Console.WriteLine("Waiting for the DataSet Viewer...");   

        }

        public static Vector f(double t, Vector x)
        {
            // Assign states
            var UnaryRx = x[0];
            var UnaryLxLL_1 = x[1];
            var UnaryLxLL = x[2];
            var SpeciesR = x[3];
            var sp_1 = x[4];
            var sp_0 = x[5];
            var SpeciesL = x[6];
            var sp_3 = x[7];
            var sp_2 = x[8];
            var sp_5 = x[9];
            var sp_4 = x[10];
            var BinaryLRxRR_1 = x[11];
            var BinaryLRxRR = x[12];
            var sp_7 = x[13];
            var sp_9 = x[14];
            var sp_8 = x[15];
            var sp_11 = x[16];
            var sp_10 = x[17];
            var sp_6 = x[18];

            var dx = Vector.Zeros(x.Length);

            // Assign derivatives
            dx[0] = 0.0;
            dx[1] = 0.0;
            dx[2] = 0.0;
            dx[3] = -1E-05 * UnaryRx * SpeciesR - 1E-05 * BinaryLRxRR * SpeciesR + 0.1 * sp_7 + 2 * 1E-05 * BinaryLRxRR_1 * sp_8 - 1E-05 * sp_6 * SpeciesR;
            dx[4] = 1E-05 * UnaryRx * SpeciesR;
            dx[5] = 1E-05 * UnaryRx * SpeciesR;
            dx[6] = -1E-05 * UnaryLxLL * SpeciesL + 2 * 1E-05 * UnaryLxLL_1 * sp_2 - 1E-05 * BinaryLRxRR * SpeciesL - 1E-05 * sp_7 * SpeciesL + 0.1 * sp_6;
            dx[7] = 1E-05 * UnaryLxLL * SpeciesL;
            dx[8] = 1E-05 * UnaryLxLL * SpeciesL - 1E-05 * UnaryLxLL_1 * sp_2;
            dx[9] = 1E-05 * UnaryLxLL_1 * sp_2;
            dx[10] = 1E-05 * UnaryLxLL_1 * sp_2;
            dx[11] = 0.0;
            dx[12] = 0.0;
            dx[13] = 1E-05 * BinaryLRxRR * SpeciesR - 0.1 * sp_7 - 1E-05 * sp_7 * SpeciesL;
            dx[14] = 1E-05 * sp_7 * SpeciesL + 1E-05 * sp_6 * SpeciesR;
            dx[15] = 1E-05 * sp_7 * SpeciesL - 1E-05 * BinaryLRxRR_1 * sp_8 + 1E-05 * sp_6 * SpeciesR;
            dx[16] = 1E-05 * BinaryLRxRR_1 * sp_8;
            dx[17] = 1E-05 * BinaryLRxRR_1 * sp_8;
            dx[18] = 1E-05 * BinaryLRxRR * SpeciesL - 0.1 * sp_6 - 1E-05 * sp_6 * SpeciesR;

            return dx;
        }

        public static SparseMatrix createRandomSparseMatrix(int n)
        {
            Random r = new Random(1);
            var sm = new SparseMatrix(n, n);
            for (int i = 0; i < 3 * n; i++)
                sm[r.Next(n), r.Next(n)] = Math.Pow(-1, r.Next(1)) * r.NextDouble();
            var b = Vector.Zeros(n);
            for (int i = 0; i < n; i++)
            {
                sm[i, i] = -3 * r.NextDouble();
                b[i] = r.NextDouble();
            }

            var dm = sm.DenseMatrix();

            return sm;
        }
    }
}