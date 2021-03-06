﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Research.Oslo;
using System.Diagnostics;

namespace Oslo.NET.Tests
{
    [TestClass]
    public class SparseMatrixTests
    {
        const double Eps = 1e-10;


        [TestMethod]
        public void plusTest()
        {
            const int N = 50;
            SparseMatrix A = SparseMatrix.Identity(N, N);
            for (int i = 0; i < N; i++)
                A[i, i] = i % 5 == 0 ? 1.0 : 0.0;
            var A1 = A.Copy();
            SparseMatrix Zeros = SparseMatrix.Identity(N, N); 
            for (int i = 0; i < N; i++)
                Zeros[i, i] = 0.0;

            SparseMatrix B = SparseMatrix.Identity(N, N);
            for (int i = 0; i < N; i++)
                B[i, i] = i % 5 == 0 ? -1.0 : 0.0;

            var C = A.plus(B);
            AssertMatrixEqualsEps(C, Zeros);
            C = A1 + B;
            AssertMatrixEqualsEps(C, Zeros);
        }
        
        [TestMethod]
        public void minusTest()
        {
            const int N = 50;
            SparseMatrix A = SparseMatrix.Identity(N, N);
            for (int i = 0; i < N; i++)
                A[i, i] = i % 5 == 0 ? 1.0 : 0.0;
            var A1 = A.Copy();
            SparseMatrix Zeros = SparseMatrix.Identity(N, N);
            for (int i = 0; i < N; i++)
                Zeros[i, i] = 0.0;

            SparseMatrix B = SparseMatrix.Identity(N, N);
            for (int i = 0; i < N; i++)
                B[i, i] = i % 5 == 0 ? 1.0 : 0.0;

            var C = A.minus(B);
            AssertMatrixEqualsEps(C, Zeros);
            C = A - B;
            AssertMatrixEqualsEps(C, Zeros);
        }

        [TestMethod]
        public void isLowerTriangularTest()
        {
            const int N = 50;
            SparseMatrix A = SparseMatrix.Identity(N, N);
            for (int i = 0; i < N; i++)
                A[i, i] = i % 5 == 0 ? 1.0 : 0.0;

            Assert.AreEqual(A.IsLowerTriangular(), true);
            A[45, 40] = 1.0;
            Assert.AreEqual(A.IsLowerTriangular(), true);
            A[40, 45] = 1.0;
            Assert.AreEqual(A.IsLowerTriangular(), false);
        }

        [TestMethod]
        public void TimesEqualsTest()
        {
            const int N = 50;
            SparseMatrix A = SparseMatrix.Identity(N, N);
            for (int i = 0; i < N; i++)
                A[i, i] = i % 5 == 0 ? 1.0 : 0.0;
              
            SparseMatrix AInit = A.Copy();
            SparseMatrix B = A.Copy();
            for (int i = 0; i < N; i++)
                B[i, i] = i % 5 == 0 ? 2.0 : 0.0;
            var C = A.Mul(2.0);
            AssertMatrixEqualsEps(B, C);
            var D = AInit * 2.0;
            AssertMatrixEqualsEps(B, D);
        }

        [TestMethod]public void TimesTest()
        {
            const int N = 50;
            const int M = 30;
            SparseMatrix A = SparseMatrix.Identity(M, N);
            for (int i = 0; i < M; i++)
                if (i < N)
                    A[i, i] = i % 5 == 0 ? 1.0 : 0.0;

            Vector b = Vector.Zeros(N);
            for (int i = 0; i < N; i++)
                b[i] = 2.0;

            Vector B = Vector.Zeros(M);
            for (int i = 0; i < M; i++)
                B[i] = i % 5 == 0 ? 2.0 : 0.0;

            var C = A.times(b);
            AssertVectorEqualsEps(B, C);
        }

        [TestMethod]
        public void SolveGETest()
        {
            const int N = 50;
            SparseMatrix a = new SparseMatrix(N, N);
            for (int i = 0; i < N; i++)
                a[i, i] = 1;
            // Apply random rotations around each pair of axes. This will keep det(A) ~ 1
            Random rand = new Random();
            for (int i = 0; i < N; i++)
                for (int j = i + 1; j < N; j++)
                {
                    double angle = rand.NextDouble() * 2 * Math.PI;
                    SparseMatrix r = new SparseMatrix(N, N);
                    for (int k = 0; k < N; k++)
                        r[k, k] = 1;
                    r[i, i] = r[j, j] = Math.Cos(angle);
                    r[i, j] = Math.Sin(angle);
                    r[j, i] = -Math.Sin(angle);
                    a = a * r;
                }

            var ainit = a.Copy();
            // Generate random vector
            Vector b = Vector.Zeros(N);
            for (int i = 0; i < N; i++)
                b[i] = rand.NextDouble();

            var binit = b.Clone();
            // Solve system
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Vector x = a.SolveGE(b);
            sw.Stop();
            Trace.WriteLine("Gaussian elimination took: " + sw.ElapsedTicks);
            // Put solution into system
            Vector b2 = ainit * x;

            // Verify result is the same
            Assert.IsTrue(Vector.GetLInfinityNorm(binit, b2) < 1e-6);
        }

        private void AssertVectorEqualsEps(Vector A, Vector B)
        {
            double sum = 0.0;
            for (int i = 0; i < A.Length; i++)
            {
                sum += A[i] - B[i];
            }

            Assert.AreEqual(sum, 0.0, 1e-10);
        }

        private void AssertEqualsEps(double a, double b)
        {
            Assert.IsTrue(Math.Abs(a - b) < 1e-10);
        }

        private void AssertMatrixEqualsEps(SparseMatrix A, SparseMatrix B)
        {
            double sum = 0.0;
            for (int i = 0; i < A.RowDimension; i++)
                for (int j = 0; j < A.ColumnDimension; j++)
                    sum += A[i, j] - B[i, j];

            AssertEqualsEps(sum, 0.0);
        }
    }
}
