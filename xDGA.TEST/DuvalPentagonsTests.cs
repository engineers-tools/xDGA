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


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using xDGA.CORE.Algorithms;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;

namespace xDGA.TEST
{
    [TestClass]
    public class DuvalPentagonsTests
    {
        private DateTime currDate = new DateTime(2017, 05, 17);
        private DissolvedGasAnalysis emptyDga;
        private List<IOutput> Outputs;

        public DuvalPentagonsTests()
        {
            emptyDga = new DissolvedGasAnalysis();
            Outputs = new List<IOutput>();
        }

        [TestMethod]
        public void PentagoOneCorrectAreaIdentified()
        {
            var dga = new DissolvedGasAnalysis(currDate, 32.0, 0.1, 0.5, 0.1, 0.1, 0.0, 0.0, 0.0, 0.0);
            var pentagon1 = new DuvalPentagonOneRule();
            var outputs = Outputs;
            pentagon1.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.PD, pentagon1.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 32.0, 0.1, 10.0, 0.1, 0.1, 0.0, 0.0, 0.0, 0.0);
            pentagon1.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.S, pentagon1.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 0.1, 100.0, 100.0, 0.1, 0.1, 0.0, 0.0, 0.0, 0.0);
            pentagon1.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.T1, pentagon1.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 0.1, 100.0, 0.1, 10.0, 0.1, 0.0, 0.0, 0.0, 0.0);
            pentagon1.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.T2, pentagon1.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 0.1, 10.0, 0.1, 100.0, 0.1, 0.0, 0.0, 0.0, 0.0);
            pentagon1.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.T3, pentagon1.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 0.1, 0.1, 0.1, 100.0, 100.0, 0.0, 0.0, 0.0, 0.0);
            pentagon1.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.D2, pentagon1.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 100.0, 0.1, 0.1, 0.1, 100.0, 0.0, 0.0, 0.0, 0.0);
            pentagon1.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.D1, pentagon1.FailureCode);
        }

        [TestMethod]
        public void PentagoTwoCorrectAreaIdentified()
        {
            var dga = new DissolvedGasAnalysis(currDate, 32.0, 0.1, 0.5, 0.1, 0.1, 0.0, 0.0, 0.0, 0.0);
            var pentagon2 = new DuvalPentagonTwoRule();
            var outputs = Outputs;
            pentagon2.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.PD, pentagon2.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 32.0, 0.1, 10.0, 0.1, 0.1, 0.0, 0.0, 0.0, 0.0);
            pentagon2.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.S, pentagon2.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 0.1, 100.0, 100.0, 0.1, 0.1, 0.0, 0.0, 0.0, 0.0);
            pentagon2.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.O, pentagon2.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 0.1, 100.0, 0.1, 10.0, 0.1, 0.0, 0.0, 0.0, 0.0);
            pentagon2.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.C, pentagon2.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 0.1, 10.0, 0.1, 100.0, 0.1, 0.0, 0.0, 0.0, 0.0);
            pentagon2.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.T3_H, pentagon2.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 0.1, 0.1, 0.1, 100.0, 100.0, 0.0, 0.0, 0.0, 0.0);
            pentagon2.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.D2, pentagon2.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 100.0, 0.1, 0.1, 0.1, 100.0, 0.0, 0.0, 0.0, 0.0);
            pentagon2.Execute(ref dga, ref emptyDga, ref outputs);

            Assert.AreEqual(FailureType.Code.D1, pentagon2.FailureCode);
        }

        [TestMethod]
        public void PentagonsAlgorithmOuputsAreCorrect()
        {
            var dga = new DissolvedGasAnalysis(currDate, 32.0, 0.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
            var outputs = Outputs;
            var algo = new DuvalPentagonsAlgorithm(dga.ToSerialisedJson());

            algo.Execute();
            Assert.AreEqual(FailureType.Description[FailureType.Code.PD], algo.Outputs[1].Description);
            Assert.AreEqual(FailureType.Description[FailureType.Code.PD], algo.Outputs[2].Description);

            dga = new DissolvedGasAnalysis(currDate, 32.0, 0.0, 10.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
            algo = new DuvalPentagonsAlgorithm(dga.ToSerialisedJson());
            algo.Execute();
            Assert.AreEqual(FailureType.Description[FailureType.Code.S], algo.Outputs[1].Description);
            Assert.AreEqual(FailureType.Description[FailureType.Code.S], algo.Outputs[2].Description);

            dga = new DissolvedGasAnalysis(currDate, 0.0, 100.0, 100.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
            algo = new DuvalPentagonsAlgorithm(dga.ToSerialisedJson());
            algo.Execute();
            Assert.AreEqual(FailureType.Description[FailureType.Code.T1], algo.Outputs[1].Description);
            Assert.AreEqual(FailureType.Description[FailureType.Code.O], algo.Outputs[2].Description);

            dga = new DissolvedGasAnalysis(currDate, 0.0, 100.0, 0.0, 10.0, 0.0, 0.0, 0.0, 0.0, 0.0);
            algo = new DuvalPentagonsAlgorithm(dga.ToSerialisedJson());
            algo.Execute();
            Assert.AreEqual(FailureType.Description[FailureType.Code.T2], algo.Outputs[1].Description);
            Assert.AreEqual(FailureType.Description[FailureType.Code.C], algo.Outputs[2].Description);

            dga = new DissolvedGasAnalysis(currDate, 0.0, 10.0, 0.0, 100.0, 0.0, 0.0, 0.0, 0.0, 0.0);
            algo = new DuvalPentagonsAlgorithm(dga.ToSerialisedJson());
            algo.Execute();
            Assert.AreEqual(FailureType.Description[FailureType.Code.T3], algo.Outputs[1].Description);
            Assert.AreEqual(FailureType.Description[FailureType.Code.T3_H], algo.Outputs[2].Description);

            dga = new DissolvedGasAnalysis(currDate, 0.0, 0.0, 0.0, 100.0, 100.0, 0.0, 0.0, 0.0, 0.0);
            algo = new DuvalPentagonsAlgorithm(dga.ToSerialisedJson());
            algo.Execute();
            Assert.AreEqual(FailureType.Description[FailureType.Code.D2], algo.Outputs[1].Description);
            Assert.AreEqual(FailureType.Description[FailureType.Code.D2], algo.Outputs[2].Description);

            dga = new DissolvedGasAnalysis(currDate, 100.0, 0.0, 0.0, 0.0, 100.0, 0.0, 0.0, 0.0, 0.0);
            algo = new DuvalPentagonsAlgorithm(dga.ToSerialisedJson());
            algo.Execute();
            Assert.AreEqual(FailureType.Description[FailureType.Code.D1], algo.Outputs[1].Description);
            Assert.AreEqual(FailureType.Description[FailureType.Code.D1], algo.Outputs[2].Description);
        }
    }
}
