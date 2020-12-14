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
using System.Text;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms.IEC60599
{
    public class CarbonDioxideToCarbonMonoxideRatioRule : IRule
    {
        public void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            var ratio = AlgorithmHelperCalculations.GasRatio(currentDga.CarbonDioxide, currentDga.CarbonMonoxide);

            var co2CoDiagnosis = new StringBuilder();
            if (ratio < 3.0)
            {
                co2CoDiagnosis.AppendLine($"The Carbon Dioxide to Carbon Monoxide Ratio (CO2/CO) is {((double)ratio).ToString("0.00")} which is less than 3. This is generally considered as an indication of probable paper involvement in a fault, with possible carbonization, in the presence of other fault gases.");
            }

            if (ratio > 10 && currentDga.CarbonDioxide.Value > 10000.0)
            {
                co2CoDiagnosis.AppendLine($"High values of CO2 ({currentDga.CarbonDioxide.Value.ToString("0.00")} > 10000) and high CO2/CO ratios ({1} > 10) can indicate mild (< 160 oC) overheating of paper or oil oxidation, especially in open transformers. The Carbon Dioxide to Carbon Monoxide Ratio (CO2/CO) is {((double)ratio).ToString("0.00")}");
            }

            if (co2CoDiagnosis.Length > 0)
            {
                outputs.Add(new Output() { Name = "CO2 / CO", Description = co2CoDiagnosis.ToString() });
            }
        }

        public bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            return currentDga != null && currentDga.CarbonDioxide != null && currentDga.CarbonMonoxide != null;
        }
    }
}
