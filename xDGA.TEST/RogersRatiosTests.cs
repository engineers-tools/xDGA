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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xDGA.CORE.Models;
using System.Collections.Generic;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Algorithms;

namespace xDGA.TEST
{
    [TestClass]
    public class RogersRatiosTests
    {
        private DateTime currDate = new DateTime(2017, 05, 17);
        private DissolvedGasAnalysis emptyDga;
        private List<IOutput> Outputs;

        public RogersRatiosTests()
        {
            emptyDga = new DissolvedGasAnalysis();
            Outputs = new List<IOutput>();
        }

        [TestMethod]
        public void RogersRatiosRuleProducesCorrectFailureCode()
        {
            var rule = new RogersRatiosRule();

            var dga = new DissolvedGasAnalysis(currDate, 100.0, 50.0, 2.0, 1.0, 0.09, 0.0, 0.0, 0.0, 0.0);
            rule.Execute(ref dga, ref emptyDga, ref Outputs);
            Assert.AreEqual(FailureType.Code.N, rule.FailureCode);

            dga = new DissolvedGasAnalysis(currDate, 100.0, 50.0, 0.1, 1.0, 0.1, 0.0, 0.0, 0.0, 0.0);
            Outputs.Clear();
            rule.Execute(ref dga, ref emptyDga, ref Outputs);
            Assert.AreEqual(FailureType.Code.D2, rule.FailureCode);
        }
    }
}
