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

using System.Collections.Generic;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;
using helper = xDGA.CORE.Algorithms.AlgorithmHelperCalculations;

namespace xDGA.CORE.Algorithms.IEC60599
{
    public class FinalDiagnosisRule : IRule
    {
        private bool ExceedsConcentrations { get; set; }
        private bool ExceedsRatesOfChange { get; set; }

        public void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            var limitExceeded = outputs.Find(o => o.Name == "LimitExceeded");
            var rateOfChangeExceeded = outputs.Find(o => o.Name == "RateOfChangeExceeded");
            ExceedsConcentrations = limitExceeded == null ? false : bool.Parse(limitExceeded.Description);
            ExceedsRatesOfChange = rateOfChangeExceeded == null ? false : bool.Parse(rateOfChangeExceeded.Description);

            // At least one gas above typical values and rates
            // of increase
            if (ExceedsConcentrations || ExceedsRatesOfChange)
            {
                var acetyleneHydrogen = helper.GasRatio(currentDga.Acetylene, currentDga.Hydrogen);
                var methaneHydrogen = helper.GasRatio(currentDga.Methane, currentDga.Hydrogen);
                var ethyleneEthane = helper.GasRatio(currentDga.Ethylene, currentDga.Ethane);

                if (acetyleneHydrogen != null && methaneHydrogen != null && ethyleneEthane != null)
                {
                    var code = GetFaultCode((double)acetyleneHydrogen, (double)methaneHydrogen, (double)ethyleneEthane);

                    if ((ExceedsConcentrations && ExceedsRatesOfChange)
                        || code == FailureType.Code.D2)
                    {
                        outputs.Add(new Output() { Name = "Final Diagnosis", Description = "Alarm" });
                    }
                    else
                    {
                        outputs.Add(new Output() { Name = "Final Diagnosis", Description = "Alert" });
                    }
                }
            }
            else if (!ExceedsConcentrations && !ExceedsRatesOfChange)
            {
                // All gasses below typical values of
                // gas concentrations and rates of gas increase
                outputs.Add(new Output() { Name = "Final Diagnosis", Description = "Normal" });
            }
            else
            {
                if (ExceedsConcentrations)
                {
                    outputs.Add(new Output() { Name = "Final Diagnosis", Description = "Alert" });
                }
                else
                {
                    outputs.Add(new Output() { Name = "Final Diagnosis", Description = "Normal" });
                }
            }
        }

        public bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            return outputs.Exists(o => o.Name == "LimitExceeded") || outputs.Exists(o => o.Name == "RateOfChangeExceeded");
        }

        /// <summary>
        /// Returns the FailureCode as per Table 1 - Page 14
        /// </summary>
        /// <param name="actyleneEthylene">The ratio of Acetylene to Ethylene</param>
        /// <param name="methaneHydrogen">The ratio of Methane to Hydrogen</param>
        /// <param name="ethyleneEthane">The ratio of Ethylene to Ethane</param>
        /// <returns>The FailureType.Code enum value</returns>
        private FailureType.Code GetFaultCode(double actyleneEthylene, double methaneHydrogen, double ethyleneEthane)
        {
            if (methaneHydrogen < 0.1
                && ethyleneEthane < 0.2)
                return FailureType.Code.PD;

            if (actyleneEthylene > 1.0
                && (methaneHydrogen >= 0.1 && methaneHydrogen <= 0.5)
                && ethyleneEthane > 1.0)
                return FailureType.Code.D1;

            if ((actyleneEthylene >= 0.6 && actyleneEthylene <= 2.5)
                && (methaneHydrogen >= 0.1 && methaneHydrogen <= 1.0)
                && ethyleneEthane > 2.0)
                return FailureType.Code.D2;

            if (ethyleneEthane < 1.0)
                return FailureType.Code.T1;

            if (actyleneEthylene < 0.1
                && methaneHydrogen > 1.0
                && (ethyleneEthane >= 1.0 && ethyleneEthane <= 4.0))
                return FailureType.Code.T2;

            if (actyleneEthylene < 0.2
                && methaneHydrogen > 1.0
                && ethyleneEthane > 4.0)
                return FailureType.Code.T3;

            return FailureType.Code.NA;
        }
    }
}
