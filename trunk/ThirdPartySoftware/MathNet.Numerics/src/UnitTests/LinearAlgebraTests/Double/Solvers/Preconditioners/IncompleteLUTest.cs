// <copyright file="IncompleteLUTest.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
// Copyright (c) 2009-2010 Math.NET
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace MathNet.Numerics.UnitTests.LinearAlgebraTests.Double.Solvers.Preconditioners
{
    using System;
    using System.Reflection;
    using LinearAlgebra.Double;
    using LinearAlgebra.Double.Solvers.Preconditioners;
    using NUnit.Framework;

    /// <summary>
    /// Incomplete LU preconditioner test.
    /// </summary>
    [TestFixture]
    public sealed class IncompleteLUFactorizationTest : PreconditionerTest
    {
        /// <summary>
        /// Invoke method from Ilutp class.
        /// </summary>
        /// <typeparam name="T">Type of the return value.</typeparam>
        /// <param name="ilu"><c>IncompleteLU</c> instance.</param>
        /// <param name="methodName">Method name.</param>
        /// <returns>Result of the method invocation.</returns>
        private static T GetMethod<T>(IncompleteLU ilu, string methodName)
        {
            var type = ilu.GetType();
            var methodInfo = type.GetMethod(
                methodName, 
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, 
                null, 
                CallingConventions.Standard, 
                new Type[0], 
                null);
            var obj = methodInfo.Invoke(ilu, null);
            return (T)obj;
        }

        /// <summary>
        /// Get upper triangle.
        /// </summary>
        /// <param name="ilu"><c>IncompleteLU</c> instance.</param>
        /// <returns>Upper triangle.</returns>
        private static Matrix GetUpperTriangle(IncompleteLU ilu)
        {
            return GetMethod<Matrix>(ilu, "UpperTriangle");
        }

        /// <summary>
        /// Get lower triangle.
        /// </summary>
        /// <param name="ilu"><c>IncompleteLU</c> instance.</param>
        /// <returns>Lower triangle.</returns>
        private static Matrix GetLowerTriangle(IncompleteLU ilu)
        {
            return GetMethod<Matrix>(ilu, "LowerTriangle");
        }

        /// <summary>
        /// Create preconditioner.
        /// </summary>
        /// <returns>New preconditioner instance.</returns>
        internal override IPreConditioner CreatePreconditioner()
        {
            return new IncompleteLU();
        }

        /// <summary>
        /// Check the result.
        /// </summary>
        /// <param name="preconditioner">Specific preconditioner.</param>
        /// <param name="matrix">Source matrix.</param>
        /// <param name="vector">Initial vector.</param>
        /// <param name="result">Result vector.</param>
        protected override void CheckResult(IPreConditioner preconditioner, SparseMatrix matrix, Vector vector, Vector result)
        {
            Assert.AreEqual(typeof(IncompleteLU), preconditioner.GetType(), "#01");

            // Compute M * result = product
            // compare vector and product. Should be equal
            Vector product = new DenseVector(result.Count);
            matrix.Multiply(result, product);

            for (var i = 0; i < product.Count; i++)
            {
                Assert.IsTrue(vector[i].AlmostEqual(product[i], -Epsilon.Magnitude()), "#02-" + i);
            }
        }

        /// <summary>
        /// Compare with original sparse matrix.
        /// </summary>
        [Test]
        public void CompareWithOriginalSparseMatrix()
        {
            var sparseMatrix = new SparseMatrix(3);
            sparseMatrix[0, 0] = -1;
            sparseMatrix[0, 1] = 5;
            sparseMatrix[0, 2] = 6;
            sparseMatrix[1, 0] = 3;
            sparseMatrix[1, 1] = -6;
            sparseMatrix[1, 2] = 1;
            sparseMatrix[2, 0] = 6;
            sparseMatrix[2, 1] = 8;
            sparseMatrix[2, 2] = 9;
            var ilu = new IncompleteLU();
            ilu.Initialize(sparseMatrix);
            var original = GetLowerTriangle(ilu).Multiply(GetUpperTriangle(ilu));
            for (var i = 0; i < sparseMatrix.RowCount; i++)
            {
                for (var j = 0; j < sparseMatrix.ColumnCount; j++)
                {
                    Assert.IsTrue(sparseMatrix[i, j].AlmostEqual(original[i, j], -Epsilon.Magnitude()), "#01-" + i + "-" + j);
                }
            }
        }
    }
}