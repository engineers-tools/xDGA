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

using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;
using xDGA.CORE.Units;

namespace xDGA.CORE.Algorithms
{
    public static class AlgorithmHelperCalculations
    {
        /// <summary>
        /// Calculate the ratio of two gases. If the denominator of the ratio
        /// is zero and the ratio is not computable, the function will return
        /// null.
        /// </summary>
        /// <param name="gasNumerator"></param>
        /// <param name="gasDenominator"></param>
        public static double? GasRatio(IMeasurement gasNumerator, IMeasurement gasDenominator)
        {
            if (gasDenominator.Value == 0) return null;

            return gasNumerator.Value / gasDenominator.Value;
        }

        /// <summary>
        /// Calculate the rate of change of the given gas using the DGA data
        /// currently loaded in the CurrentDGA and PreviousDGA properties.
        /// </summary>
        /// <param name="currentDga">Current or latest DGA.</param>
        /// <param name="previousDga">Previous DGA.</param>
        /// <param name="gas">Gas for which the ratio is required.</param>
        /// <param name="timeUnit">The time unit over which the ratio will be calculated.</param>
        /// <returns>The calculated rate of change or null if the data is not sufficient.</returns>
        public static double? RateOfChange(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, Gas gas, IUnit timeUnit)
        {
            // Both DGAs are required to calcualte the rate of change
            if (currentDga == null || previousDga == null) return null;

            var currentDate = currentDga.SamplingDate;
            var previousDate = previousDga.SamplingDate;
            var yearsTimeUnit = new TimeUnits.Year();

            var dateDifference = (currentDate.Year - previousDate.Year) / (timeUnit.Base / yearsTimeUnit.Base);

            var currentGas = (IMeasurement)currentDga.GetType().GetProperty(gas.ToString()).GetValue(currentDga);
            var previousGas = (IMeasurement)previousDga.GetType().GetProperty(gas.ToString()).GetValue(previousDga);

            return (currentGas.Value - previousGas.Value) / dateDifference;
        }
    }
}
