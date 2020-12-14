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
using xDGA.CORE.Algorithms;
using System.Collections.Generic;
using xDGA.CORE.Units;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Algorithms.IEC60599;

namespace xDGA.TEST
{
    [TestClass]
    public class IEC60599Tests
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

        private DissolvedGasAnalysis CurrentDga;

        private DateTime prevDate = new DateTime(2016, 05, 17);
        private double prevHydrogen = 100.0;
        private double prevMethane = 20.0;
        private double prevEthane = 7.5;
        private double prevEthylene = 5.0;
        private double prevAcetylene = 2.5;
        private double prevCarbonMonoxide = 150.0;
        private double prevCarbonDioxide = 1500.0;
        private double prevOxygen = 355.0;
        private double prevNitrogen = 3550.0;

        private DissolvedGasAnalysis PreviousDga;

        private DissolvedGasAnalysis ZeroesDga = new DissolvedGasAnalysis(DateTime.Now, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        private List<IOutput> Outputs;

        public IEC60599Tests()
        {
            CurrentDga = new DissolvedGasAnalysis(currDate, currHydrogen, currMethane, currEthane, currEthylene, currAcetylene, currCarbonMonoxide, currCarbonDioxide, currOxygen, currNitrogen);
            PreviousDga = new DissolvedGasAnalysis(prevDate, prevHydrogen, prevMethane, prevEthane, prevEthylene, prevAcetylene, prevCarbonMonoxide, prevCarbonDioxide, prevOxygen, prevNitrogen);
            Outputs = new List<IOutput>();
        }

        [TestMethod]
        public void AppliesDefaultDetectionLimits()
        {
            var zeroesDga = ZeroesDga;
            var outputs = Outputs;
            var rule = new ApplyDetectionLimitsRule();
            rule.Execute(ref zeroesDga, ref zeroesDga, ref outputs);

            Assert.AreEqual(zeroesDga.Hydrogen.Value, rule.DetectionLimits[Gas.Hydrogen]);
            Assert.AreEqual(zeroesDga.Methane.Value, rule.DetectionLimits[Gas.Methane]);
            Assert.AreEqual(zeroesDga.Ethane.Value, rule.DetectionLimits[Gas.Ethane]);
            Assert.AreEqual(zeroesDga.Ethylene.Value, rule.DetectionLimits[Gas.Ethylene]);
            Assert.AreEqual(zeroesDga.Acetylene.Value, rule.DetectionLimits[Gas.Acetylene]);
            Assert.AreEqual(zeroesDga.CarbonMonoxide.Value, rule.DetectionLimits[Gas.CarbonMonoxide]);
            Assert.AreEqual(zeroesDga.CarbonDioxide.Value, rule.DetectionLimits[Gas.CarbonDioxide]);
            Assert.AreEqual(zeroesDga.Oxygen.Value, rule.DetectionLimits[Gas.Oxygen]);
            Assert.AreEqual(zeroesDga.Nitrogen.Value, rule.DetectionLimits[Gas.Nitrogen]);
        }

        [TestMethod]
        public void AppliesExternallyDefinedDetectionLimits()
        {
            var zeroesDga = ZeroesDga;
            var outputs = Outputs;
            var rule = new ApplyDetectionLimitsRule();
            rule.Execute(ref zeroesDga, ref zeroesDga, ref outputs);

            rule.DetectionLimits[Gas.Hydrogen] = 100;
            rule.DetectionLimits[Gas.Methane] = 100;
            rule.DetectionLimits[Gas.Ethane] = 100;
            rule.DetectionLimits[Gas.Ethylene] = 100;
            rule.DetectionLimits[Gas.Acetylene] = 100;
            rule.DetectionLimits[Gas.CarbonMonoxide] = 100;
            rule.DetectionLimits[Gas.CarbonDioxide] = 100;
            rule.DetectionLimits[Gas.Oxygen] = 100;
            rule.DetectionLimits[Gas.Nitrogen] = 100;

            Assert.AreEqual(zeroesDga.Hydrogen.Value, rule.DetectionLimits[Gas.Hydrogen]);
            Assert.AreEqual(zeroesDga.Methane.Value, rule.DetectionLimits[Gas.Methane]);
            Assert.AreEqual(zeroesDga.Ethane.Value, rule.DetectionLimits[Gas.Ethane]);
            Assert.AreEqual(zeroesDga.Ethylene.Value, rule.DetectionLimits[Gas.Ethylene]);
            Assert.AreEqual(zeroesDga.Acetylene.Value, rule.DetectionLimits[Gas.Acetylene]);
            Assert.AreEqual(zeroesDga.CarbonMonoxide.Value, rule.DetectionLimits[Gas.CarbonMonoxide]);
            Assert.AreEqual(zeroesDga.CarbonDioxide.Value, rule.DetectionLimits[Gas.CarbonDioxide]);
            Assert.AreEqual(zeroesDga.Oxygen.Value, rule.DetectionLimits[Gas.Oxygen]);
            Assert.AreEqual(zeroesDga.Nitrogen.Value, rule.DetectionLimits[Gas.Nitrogen]);
        }

        [TestMethod]
        public void ThrowsErrorWhenCurrentDgaDoesNotExist()
        {
            DissolvedGasAnalysis currDga = null;
            var prevDga = PreviousDga;
            var outputs = Outputs;
            var rule = new CurrentDgaExistsRule();

            Assert.ThrowsException<MissingFieldException>(() => { rule.Execute(ref currDga, ref prevDga, ref outputs); });
        }

        [TestMethod]
        public void DetectsWhenRatesOfChangeAreExceeded()
        {
            var currDga = CurrentDga;
            var prevDga = PreviousDga;
            var outputs = Outputs;
            var rule = new RateOfChangeRule(false, null);

            rule.Execute(ref currDga, ref prevDga, ref outputs);

            var limitExceeded = outputs.Find(o => o.Name == "RateOfChangeExceeded").Description;

            Assert.AreEqual(true.ToString(), limitExceeded);            
        }

        [TestMethod]
        public void DetectsWhenGasConcentrationExceedsLimits()
        {
            var currDga = CurrentDga;
            var prevDga = PreviousDga;
            var outputs = Outputs;
            var rule = new LimitsRule(false);

            rule.Execute(ref currDga, ref prevDga, ref outputs);

            var limitExceeded = outputs.Find(o => o.Name == "LimitExceeded").Description;

            Assert.AreEqual(true.ToString(), limitExceeded);
        }

        
    }
}
