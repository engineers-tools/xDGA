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
using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms
{
    public class RogersRatiosAlgorithm : AbstractAlgorithm
    {
        public override string Version => "Rogers Ratios as described in IEEE C57.104";

        /// <summary>
        /// The Dissolved Gas Analysis that will be used in the assessment.
        /// </summary>
        public DissolvedGasAnalysis DGA { get; internal set; }

        /// <summary>
        /// Creates a new RogersRatioAlgorithm instance.
        /// </summary>
        /// <param name="dga">The JSON serialsed DGA data.</param>
        public RogersRatiosAlgorithm(string dga)
        {
            DGA = new DissolvedGasAnalysis(dga);
        }

        public override void Execute()
        {
            var dga = DGA;
            DissolvedGasAnalysis prevDga = null;
            var outputs = Outputs;

            Rules.Add(new ApplyDetectionLimitsRule());
            Rules.Add(new RogersRatiosRule());

            // Create a Title output
            outputs.Add(new Output() { Name = "Title", Description = $"Interpretation of Dissolved Gas Analysis as per {Version}" });

            foreach (var rule in Rules)
            {
                rule.Execute(ref dga, ref prevDga, ref outputs);
            }

            Outputs = outputs;
        }
    }
}
