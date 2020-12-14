// The MIT License (MIT)
//
// Copyright (c) 2017-2020 Carlos Gamez
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


using System;
using xDGA.CORE.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xDGA.CORE.Models;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;
using System.Collections.Generic;

namespace xDGA.TEST
{
    [TestClass]
    public class DuvalTrianglesTests
    {
        private DateTime currDate = new DateTime(2017, 05, 17);
        private double currHydrogen = 2000.0;
        private double currMethane = 40.0;
        private double currEthane = 15.5;
        private double currEthylene = 10.0;
        private double currAcetylene = 4.5;
        private double currCarbonMonoxide = 300.0;
        private double currCarbonDioxide = 3000.0;
        private double currOxygen = 750.0;
        private double currNitrogen = 7500.0;

        private DissolvedGasAnalysis DGA;
        private DissolvedGasAnalysis emptyDga;
        private List<IOutput> Outputs;

        public DuvalTrianglesTests()
        {
            DGA = new DissolvedGasAnalysis(currDate, currHydrogen, currMethane, currEthane, currEthylene, currAcetylene, currCarbonMonoxide, currCarbonDioxide, currOxygen, currNitrogen);
            emptyDga = new DissolvedGasAnalysis();
            Outputs = new List<IOutput>();
        }

        [TestMethod]
        public void TriangleOneCalculatesCorrectFaultZone()
        {
            var algo = new DuvalTriangleOneRule();
            var outputs = Outputs;

            var dga = new DissolvedGasAnalysis(DateTime.Today, 0, 100, 0, 0, 0, 0, 0, 0, 0);
            algo.Execute(ref dga, ref emptyDga, ref outputs);
            Assert.AreEqual(FailureType.Code.PD, algo.FailureCode);

            dga = new DissolvedGasAnalysis(DateTime.Today, 0, 100, 0, 10, 0, 0, 0, 0, 0);
            algo.Execute(ref dga, ref emptyDga, ref outputs);
            Assert.AreEqual(FailureType.Code.T1, algo.FailureCode);

            dga = new DissolvedGasAnalysis(DateTime.Today, 0, 100, 0, 50, 0, 0, 0, 0, 0);
            algo.Execute(ref dga, ref emptyDga, ref outputs);
            Assert.AreEqual(FailureType.Code.T2, algo.FailureCode);

            dga = new DissolvedGasAnalysis(DateTime.Today, 0, 100, 0, 200, 0, 0, 0, 0, 0);
            algo.Execute(ref dga, ref emptyDga, ref outputs);
            Assert.AreEqual(FailureType.Code.T3, algo.FailureCode);

            dga = new DissolvedGasAnalysis(DateTime.Today, 0, 100, 0, 10, 30, 0, 0, 0, 0);
            algo.Execute(ref dga, ref emptyDga, ref outputs);
            Assert.AreEqual(FailureType.Code.D1, algo.FailureCode);

            dga = new DissolvedGasAnalysis(DateTime.Today, 0, 100, 0, 70, 50, 0, 0, 0, 0);
            algo.Execute(ref dga, ref emptyDga, ref outputs);
            Assert.AreEqual(FailureType.Code.D2, algo.FailureCode);

            dga = new DissolvedGasAnalysis(DateTime.Today, 0, 100, 0, 10, 10, 0, 0, 0, 0);
            algo.Execute(ref dga, ref emptyDga, ref outputs);
            Assert.AreEqual(FailureType.Code.DT, algo.FailureCode);

            dga = new DissolvedGasAnalysis(DateTime.Today, 0, 100, 0, 200, 100, 0, 0, 0, 0);
            algo.Execute(ref dga, ref emptyDga, ref outputs);
            Assert.AreEqual(FailureType.Code.DT, algo.FailureCode);
        }

        [TestMethod]
        public void TriangleOneDoesNotRunWhenGasesAreNotSpecified()
        {
            var algo = new DuvalTriangleOneRule();
            var outputs = Outputs;
            var applicable = algo.IsApplicable(emptyDga, emptyDga, outputs);

            Assert.AreEqual(false, applicable);
        }

        [TestMethod]
        public void TriangleTwoCalculatesCorrectFaultZone()
        {
            var algo = new DuvalTriangleTwoRule();
            var outputs = Outputs;

            algo.Execute(ref DGA, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.OltcX1, algo.FailureCode);
        }

        [TestMethod]
        public void TriangleTwoDoesNotRunWhenGasesAreNotSpecified()
        {
            var algo = new DuvalTriangleTwoRule();
            var outputs = Outputs;
            var applicable = algo.IsApplicable(emptyDga, emptyDga, outputs);

            Assert.AreEqual(false, applicable);
        }

        [TestMethod]
        public void TriangleFourCalculatesCorrectFaultZone()
        {
            var algo = new DuvalTriangleFourRule(FailureType.Code.T2);
            var outputs = Outputs;
            algo.Execute(ref DGA, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.S, algo.FailureCode);
        }

        [TestMethod]
        public void TriangleFourDoesNotRunWhenGasesAreNotSpecified()
        {
            var algo = new DuvalTriangleFourRule(FailureType.Code.T2);
            var outputs = Outputs;
            var applicable = algo.IsApplicable(emptyDga, emptyDga, outputs);

            Assert.AreEqual(false, applicable);
        }

        [TestMethod]
        public void TriangleFiveCalculatesCorrectFaultZone()
        {
            var algo = new DuvalTriangleFiveRule(FailureType.Code.T2);
            var outputs = Outputs;
            algo.Execute(ref DGA, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.C, algo.FailureCode);
        }

        [TestMethod]
        public void TriangleFiveDoesNotRunWhenGasesAreNotSpecified()
        {
            var algo = new DuvalTriangleFiveRule(FailureType.Code.T2);
            var outputs = Outputs;
            var applicable = algo.IsApplicable(emptyDga, emptyDga, outputs);

            Assert.AreEqual(false, applicable);
        }

        [TestMethod]
        public void TriangleFourOnlyRunsWithCorrectCodeFromTriangleOne()
        {
            var triangleOneCode = FailureType.Code.PD;
            var algo = new DuvalTriangleFourRule(triangleOneCode);

            Assert.AreEqual(true, algo.IsApplicable(DGA, emptyDga, Outputs));

            triangleOneCode = FailureType.Code.T1;
            algo = new DuvalTriangleFourRule(triangleOneCode);

            Assert.AreEqual(true, algo.IsApplicable(DGA, emptyDga, Outputs));

            triangleOneCode = FailureType.Code.T2;
            algo = new DuvalTriangleFourRule(triangleOneCode);

            Assert.AreEqual(true, algo.IsApplicable(DGA, emptyDga, Outputs));

            triangleOneCode = FailureType.Code.D1;
            algo = new DuvalTriangleFourRule(triangleOneCode);

            Assert.AreEqual(false, algo.IsApplicable(DGA, emptyDga, Outputs));
        }

        [TestMethod]
        public void TriangleFiveOnlyRunsWithCorrectCodeFromTriangleOne()
        {
            var triangleOneCode = FailureType.Code.T2;
            var algo = new DuvalTriangleFiveRule(triangleOneCode);

            Assert.AreEqual(true, algo.IsApplicable(DGA, emptyDga, Outputs));

            triangleOneCode = FailureType.Code.T3;
            algo = new DuvalTriangleFiveRule(triangleOneCode);

            Assert.AreEqual(true, algo.IsApplicable(DGA, emptyDga, Outputs));

            triangleOneCode = FailureType.Code.D1;
            algo = new DuvalTriangleFiveRule(triangleOneCode);

            Assert.AreEqual(false, algo.IsApplicable(DGA, emptyDga, Outputs));
        }
    }
}
