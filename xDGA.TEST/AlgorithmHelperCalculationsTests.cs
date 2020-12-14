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
using xDGA.CORE.Algorithms;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;
using xDGA.CORE.Units;

namespace xDGA.TEST
{
    [TestClass]
    public class AlgorithmHelperCalculationsTests
    {
        private DateTime currDate = new DateTime(2017, 05, 17);
        private double currHydrogen = 200.0;
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

        public AlgorithmHelperCalculationsTests()
        {
            CurrentDga = new DissolvedGasAnalysis(currDate, currHydrogen, currMethane, currEthane, currEthylene, currAcetylene, currCarbonMonoxide, currCarbonDioxide, currOxygen, currNitrogen);
            PreviousDga = new DissolvedGasAnalysis(prevDate, prevHydrogen, prevMethane, prevEthane, prevEthylene, prevAcetylene, prevCarbonMonoxide, prevCarbonDioxide, prevOxygen, prevNitrogen);
        }

        [TestMethod]
        public void ValidGasRatioIsCalculated()
        {
            IMeasurement numGas = new GasMeasurement() { Value = 100, Unit = new ConcentrationUnits.PartsPerMillion() };
            IMeasurement denGas = new GasMeasurement() { Value = 100, Unit = new ConcentrationUnits.PartsPerMillion() };

            var ratio = AlgorithmHelperCalculations.GasRatio(numGas, denGas);

            Assert.AreEqual(1.0, ratio);
        }

        [TestMethod]
        public void NullGasRatioReturnedWhenDenominatorIsZero()
        {
            IMeasurement numGas = new GasMeasurement() { Value = 100, Unit = new ConcentrationUnits.PartsPerMillion() };
            IMeasurement denGas = new GasMeasurement() { Value = 0, Unit = new ConcentrationUnits.PartsPerMillion() };

            var ratio = AlgorithmHelperCalculations.GasRatio(numGas, denGas);

            Assert.AreEqual(null, ratio);
        }

        [TestMethod]
        public void ValidYearlyRateOfChangeIsCalculated()
        {
            var rate = AlgorithmHelperCalculations.RateOfChange(CurrentDga, PreviousDga, Gas.Hydrogen, new TimeUnits.Year());

            var expectedGasRateOfChange = (CurrentDga.Hydrogen.Value - PreviousDga.Hydrogen.Value) / (CurrentDga.SamplingDate.Year - PreviousDga.SamplingDate.Year);

            Assert.AreEqual(expectedGasRateOfChange, rate);
        }

        [TestMethod]
        public void ValidDailyRateOfChangeIsCalculated()
        {
            var rate = AlgorithmHelperCalculations.RateOfChange(CurrentDga, PreviousDga, Gas.Hydrogen, new TimeUnits.Day());

            var expectedGasRateOfChange = (CurrentDga.Hydrogen.Value - PreviousDga.Hydrogen.Value) / (365.0);

            Assert.AreEqual(expectedGasRateOfChange, rate);
        }

        [TestMethod]
        public void RateOfChangeReturnsNullWhenOneDgaIsMissing()
        {
            var rate1 = AlgorithmHelperCalculations.RateOfChange(null, PreviousDga, Gas.Hydrogen, new TimeUnits.Year());
            var rate2 = AlgorithmHelperCalculations.RateOfChange(CurrentDga, null, Gas.Hydrogen, new TimeUnits.Year());

            Assert.AreEqual(null, rate1);
            Assert.AreEqual(null, rate2);
        }
    }
}
