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
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms.IEC60599
{
    public class LimitsRule : IRule
    {
        private bool HasCommunicatingOltc { get; set; }

        public LimitsRule(bool hasCommunicatingOltc)
        {
            HasCommunicatingOltc = hasCommunicatingOltc;
        }

        public void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            var hydCheck = CheckLimit(currentDga, Gas.Hydrogen, ref outputs);
            var methCheck = CheckLimit(currentDga, Gas.Methane, ref outputs);
            var ethCheck = CheckLimit(currentDga, Gas.Ethane, ref outputs);
            var ethyCheck = CheckLimit(currentDga, Gas.Ethylene, ref outputs);
            var acetCheck = CheckLimit(currentDga, Gas.Acetylene, ref outputs);
            var coCheck = CheckLimit(currentDga, Gas.CarbonMonoxide, ref outputs);
            var co2Check = CheckLimit(currentDga, Gas.CarbonDioxide, ref outputs);

            outputs.Add(new Output() { Name = "LimitExceeded", Description = (hydCheck || methCheck || ethyCheck || ethyCheck || acetCheck || coCheck || co2Check).ToString() });
        }

        public bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            return currentDga != null;
        }

        /// <summary>
        /// Checks whether a particular gas exceeds the typical values as per the table.
        /// </summary>
        /// <param name="currentDga">The current or latest DGA.</param>
        /// <param name="gas">The Gas for which the limit needs to be checked</param>
        /// <param name="outputs">The list of outputs of the algorithm.</param>
        /// <returns>True if the chosen gas of the last DGA exceeds the limit</returns>
        private bool CheckLimit(DissolvedGasAnalysis currentDga, Gas gas, ref List<IOutput> outputs)
        {
            var currentGas = (IMeasurement)currentDga.GetType().GetProperty(gas.ToString()).GetValue(currentDga);

            if (currentGas.Value > TypicalConcentrations[gas].Item2)
            {
                if (!outputs.Exists(o => o.Name == $"{gas.ToString()} Concentration"))
                {
                    outputs.Add(new Output() { Name = $"{gas.ToString()} Concentration", Description = $"The concentration of {gas.ToString()} is {currentGas.Value.ToString("0.00")} ul/l which is higher than the typical value of {TypicalConcentrations[gas].Item2.ToString("0.00")} ul/l." });
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Table A.2 - Ranges of 90% typical gas concentration values
        /// observed in various asset classes, in ul/l
        /// </summary>
        /// <returns>A Dictionary of Gas and limit values in a tuple</returns>
        public Dictionary<Gas, Tuple<double, double>> TypicalConcentrations
        {
            get
            {
                return new Dictionary<Gas, Tuple<double, double>>()
                {
                    { Gas.Hydrogen, Tuple.Create(50.0, 150.0) },
                    { Gas.Methane, Tuple.Create(30.0, 130.0) },
                    { Gas.Ethane, Tuple.Create(20.0, 90.0) },
                    { Gas.Ethylene, Tuple.Create(60.0, 280.0) },
                    { Gas.Acetylene, HasCommunicatingOltc ? Tuple.Create(2.0, 20.0) : Tuple.Create(60.0, 280.0) },
                    { Gas.CarbonMonoxide, Tuple.Create(400.0, 600.0) },
                    { Gas.CarbonDioxide, Tuple.Create(3800.0, 14000.0) }
                };
            }
        }
    }
}
