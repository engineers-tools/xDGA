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
    public class DuvalTrianglesAlgorithm : AbstractAlgorithm
    {
        public override string Version => "Duval Triangles for Oil Filled Transformers, Reactors and Cables";

        /// <summary>
        /// The Dissolved Gas Analysis that will be used in the assessment.
        /// </summary>
        public DissolvedGasAnalysis DGA { get; internal set; }

        /// <summary>
        /// Create a new instance of the Duval Triangles analysis algorithm
        /// </summary>
        /// <param name="dga">A JSON serialized string with the DGA data.</param>
        public DuvalTrianglesAlgorithm(string dga)
        {
            DGA = new DissolvedGasAnalysis(dga);
        }

        public override void Execute()
        {
            var dga = DGA;
            DissolvedGasAnalysis prevDga = null;
            var outputs = Outputs;

            // Create a Title output
            outputs.Add(new Output() { Name = "Title", Description = $"Interpretation of Dissolved Gas Analysis as per {Version}" });

            var applyDetectionLimitsRule = new ApplyDetectionLimitsRule();
            applyDetectionLimitsRule.Execute(ref dga, ref prevDga, ref outputs);

            var triangleOneRule = new DuvalTriangleOneRule();
            if (triangleOneRule.IsApplicable(dga, prevDga, outputs)) triangleOneRule.Execute(ref dga, ref prevDga, ref outputs);

            Rules.Add(new DuvalTriangleFourRule(triangleOneRule.FailureCode));
            Rules.Add(new DuvalTriangleFiveRule(triangleOneRule.FailureCode));

            foreach (var rule in Rules)
            {
                if (rule.IsApplicable(dga, prevDga, outputs))
                {
                    rule.Execute(ref dga, ref prevDga, ref outputs);
                }
            }

            Outputs = outputs;
        }
    }
}
 