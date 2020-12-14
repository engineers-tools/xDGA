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

using xDGA.CORE.Algorithms.IEEEC57104;
using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms
{
    public class IEEEC57104Algorithm : AbstractAlgorithm
    {
        public override string Version => "IEEE DRAFT PC57.104/D4.1, October 2017";

        /// <summary>
        /// The latest Dissolved Gas Analysis that will be used in the assessment.
        /// </summary>
        public DissolvedGasAnalysis CurrentDGA { get; internal set; }

        /// <summary>
        /// The previous Dissolved Gas Analysis that will be used in the assessment.
        /// </summary>
        public DissolvedGasAnalysis PreviousDGA { get; internal set; }

        /// <summary>
        /// The age of the transformer in years.
        /// </summary>
        public int? TransformerAge { get; internal set; }

        public IEEEC57104Algorithm(string currDGA, string prevDGA, int? transformerAge)
        {
            CurrentDGA = string.IsNullOrEmpty(currDGA) ? null : new DissolvedGasAnalysis(currDGA);
            PreviousDGA = string.IsNullOrEmpty(prevDGA) ? null : new DissolvedGasAnalysis(prevDGA);
            TransformerAge = transformerAge;
        }

        public override void Execute()
        {
            var currDga = CurrentDGA;
            var prevDga = PreviousDGA;
            var outputs = Outputs;

            Rules.Add(new CurrentDgaExistsRule());
            Rules.Add(new ApplyDetectionLimitsRule());
            Rules.Add(new TableOneRule(TransformerAge));

            // Create a Title output
            outputs.Add(new Output() { Name = "Title", Description = $"Interpretation of Dissolved Gas Analysis as per {Version}" });

            foreach (var rule in Rules)
            {
                if (rule.IsApplicable(currDga, prevDga, outputs))
                {
                    rule.Execute(ref currDga, ref prevDga, ref outputs);
                }
            }

            Outputs = outputs;
        }
    }
}
