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
using System.Collections.Generic;
using System.Linq;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;
using xDGA.CORE.Units;
using helper = xDGA.CORE.Algorithms.AlgorithmHelperCalculations;

namespace xDGA.CORE.Algorithms.IEC60599
{
    public class RateOfChangeRule : IRule
    {
        private bool HasCommunicatingOltc { get; set; }
        private IMeasurement OilVolume { get; set; }

        public RateOfChangeRule(bool hasCommunicatingOltc, IMeasurement oilVolume)
        {
            HasCommunicatingOltc = hasCommunicatingOltc;
            OilVolume = oilVolume;
        }

        public void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            var hydCheck = CheckRateOfChange(currentDga, previousDga, Gas.Hydrogen, ref outputs);
            var methCheck = CheckRateOfChange(currentDga, previousDga, Gas.Methane, ref outputs);
            var ethCheck = CheckRateOfChange(currentDga, previousDga, Gas.Ethane, ref outputs);
            var ethyCheck = CheckRateOfChange(currentDga, previousDga, Gas.Ethylene, ref outputs);
            var acetCheck = CheckRateOfChange(currentDga, previousDga, Gas.Acetylene, ref outputs);
            var coCheck = CheckRateOfChange(currentDga, previousDga, Gas.CarbonMonoxide, ref outputs);
            var co2Check = CheckRateOfChange(currentDga, previousDga, Gas.CarbonDioxide, ref outputs);

            outputs.Add(new Output() { Name = "RateOfChangeExceeded", Description = (hydCheck || methCheck || ethCheck || ethyCheck || acetCheck || coCheck || co2Check).ToString() });
        }

        public bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            return currentDga != null && previousDga != null;
        }

        /// <summary>
        /// Checks whether a particular gas exceeds
        /// the rate of change nominated in the 
        /// appropriate table.
        /// </summary>
        /// <param name="currentDga">The current or latest DGA.</param>
        /// <param name="previousDga">The previous DGA.</param>
        /// <param name="gas">The Gas for which the rate needs to be checked</param>
        /// <returns>True if the table value is exceeded</returns>
        private bool CheckRateOfChange(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, Gas gas, ref List<IOutput> outputs)
        {
            var rateOfChange = helper.RateOfChange(currentDga, previousDga, gas, new TimeUnits.Year());
            var lowerRate = TypicalRatesOfIncrease[gas].Item1;
            var upperRate = TypicalRatesOfIncrease[gas].Item2;
            var rateUnit = "ul/l/year";

            // If oil volume is available, make the appropriate conversion.
            if (OilVolume != null && OilVolume.Value != 0.0)
            {
                rateOfChange = rateOfChange * OilVolume.ConvertTo(new VolumeUnits.Litre());
                lowerRate = lowerRate * OilVolume.ConvertTo(new VolumeUnits.Litre());
                upperRate = upperRate * OilVolume.ConvertTo(new VolumeUnits.Litre());
                rateUnit = "ul/year";
            }

            if (rateOfChange != null && rateOfChange > upperRate)
            {
                if (!outputs.Exists(o => o.Name == $"{gas.ToString()} Rate of Change"))
                {
                    outputs.Add(new Output() { Name = $"{gas.ToString()} Rate of Change", Description = $"The rate of change of {gas.ToString()} is {rateOfChange.Value.ToString("0.00")} {rateUnit} which is higher than the typical value of {upperRate.ToString("0.00")} {rateUnit}." });
                }
                return true;
            }
            else if (rateOfChange != null && rateOfChange <= lowerRate)
            {
                if (!outputs.Exists(o => o.Name == $"{gas.ToString()} Rate of Change"))
                {
                    outputs.Add(new Output() { Name = $"{gas.ToString()} Rate of Change", Description = $"The rate of change of {gas.ToString()} is {rateOfChange.Value} {rateUnit} which is lower than the typical value of {lowerRate} {rateUnit}." });
                }
                return false;
            }

            return false;
        }

        /// <summary>
        /// Table A.3 - Ranges of 90% typical gas rates of increase values
        /// observed in power transformers, in ul/l/year
        /// </summary>
        /// <returns>A Dictionary of Gas and limit values in a tuple</returns>
        private Dictionary<Gas, Tuple<double, double>> TypicalRatesOfIncrease
        {
            get
            {
                return new Dictionary<Gas, Tuple<double, double>>()
                {
                    { Gas.Hydrogen, Tuple.Create(35.0, 132.0) },
                    { Gas.Methane, Tuple.Create(10.0, 120.0) },
                    { Gas.Ethane, Tuple.Create(5.0, 90.0) },
                    { Gas.Ethylene, Tuple.Create(32.0, 146.0) },
                    { Gas.Acetylene, HasCommunicatingOltc ? Tuple.Create(0.0, 4.0) : Tuple.Create(21.0, 37.0) },
                    { Gas.CarbonMonoxide, Tuple.Create(260.0, 1060.0) },
                    { Gas.CarbonDioxide, Tuple.Create(1700.0, 10000.0) }
                };
            }
        }
    }
}
