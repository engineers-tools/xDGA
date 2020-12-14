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
    public class OxygenToNitrogenRatioRule : IRule
    {
        public void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            var ratio = AlgorithmHelperCalculations.GasRatio(currentDga.Oxygen, currentDga.Nitrogen);
            var diagnosis = new StringBuilder();
            if (ratio < 0.3)
            {
                diagnosis.AppendLine("Dissolved O2 and N2 are found in oil as a result of contact with atmospheric air in the conservator of air-breathing equipment, or through leaks in sealed equipment. At equilibrium with air, the concentrations of O2 and N2 are approximately 32000 and 64000 ppm respectively and the O2/N2 ratio is approx. 0.5.");

                diagnosis.AppendLine($"In service, this ratio may decrease as a result of oil oxidation and/or paper ageing, if O2 is consumed more rapidrly than it is repalce by difussion. Factors such as the load and preservation system used may also affect the ratio, but with the exception of closed systems, ratios less than 0.3 ({((double)ratio).ToString("0.00")} in this case) are generally considered to indicate excessive consumption of oxygen.");
            }

            if (diagnosis.Length > 0)
            {
                outputs.Add(new Output() { Name = "O2 / N2", Description = diagnosis.ToString() });
            }
        }

        public bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            return currentDga != null && currentDga.Oxygen != null && currentDga.Nitrogen != null;
        }
    }
}
