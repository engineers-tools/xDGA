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

using System.Text;
using System.Collections.Generic;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms.IEC60599
{
    public class AcetyleneToHydrogenRatioRule : IRule
    {
        private bool HasCommunicatingOltc;

        public AcetyleneToHydrogenRatioRule(bool hasCommunicatingOltc)
        {
            HasCommunicatingOltc = hasCommunicatingOltc;
        }

        public void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            var ratio = AlgorithmHelperCalculations.GasRatio(currentDga.Acetylene, currentDga.Hydrogen);

            var diagnosis = new StringBuilder();
            if (ratio > 2.0)
            {
                if (!HasCommunicatingOltc)
                {
                    diagnosis.AppendLine("Although this transformer has been identified as not having communication between the On-Load Tap Changer oil and the main tank oil, ");
                }
                else
                {
                    diagnosis.AppendLine("This transformer has been identified as having communication between the On-Load Tap Changer oil and the main tank oil.");
                }

                diagnosis.AppendLine($"Acetylene to Hydrogen ratios higher than 2 or 3 in the main tank ({((double)ratio).ToString("0.00")} in this case) are considered as an indication of OLTC contamination. This can be confirmed by comparing DGA results in the main tank, in the OLTC and in the conservators.");
            }

            if (diagnosis.Length > 0)
            {
                outputs.Add(new Output() { Name = "C2H2 / H2", Description = diagnosis.ToString() });
            }
        }

        public bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            return currentDga != null && currentDga.Acetylene != null && currentDga.Hydrogen != null;
        }
    }
}
