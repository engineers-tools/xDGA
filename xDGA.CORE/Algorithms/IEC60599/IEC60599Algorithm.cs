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

using xDGA.CORE.Algorithms.IEC60599;
using xDGA.CORE.Models;
using xDGA.CORE.Units;

namespace xDGA.CORE.Algorithms
{
    public class IEC60599Algorithm : AbstractAlgorithm
    {
        public override string Version => "IEC60599 Edition 3.0, 2015-09";

        /// <summary>
        /// The latest Dissolved Gas Analysis that will be used in the assessment.
        /// </summary>
        public DissolvedGasAnalysis CurrentDGA { get; internal set; }
        
        /// <summary>
        /// The previous Dissolved Gas Analysis that will be used in the assessment.
        /// </summary>
        public DissolvedGasAnalysis PreviousDGA { get; internal set; }

        /// <summary>
        /// The Oil Volume of the transformer.
        /// The dafault value is 0 l of oil.
        /// </summary>
        public Measurement OilVolume { get; set; } = new Measurement() { Value = 0, Unit = new VolumeUnits.Litre() };

        /// <summary>
        /// Flag that indicates whether the transformer has an On-Load Tap Changer (OLTC) that shares oil with the main tank or not.
        /// The dafaul value assumes the oil does not communicate.
        /// </summary>
        public bool HasCommunicatingOltc { get; set; } = false;

        /// <summary>
        /// Create a new instance of the IEC 60599 analysis algorithm
        /// </summary>
        /// <param name="currDGA">A JSON serialized string with the Current DGA data.</param>
        /// <param name="prevDGA">A JSON serialized string with the Previous DGA data.</param>
        /// <param name="oilVolume">The oil volume of the main tank in litres.</param>
        /// <param name="hasCommunicatingOltc">A flag that indicates whether the transformer's main tank and the OLTC diverter have a way to exchange oil.</param>
        public IEC60599Algorithm(string currDGA, string prevDGA, double oilVolume, bool hasCommunicatingOltc)
        {
            CurrentDGA = string.IsNullOrEmpty(currDGA) ? null : new DissolvedGasAnalysis(currDGA);
            PreviousDGA = string.IsNullOrEmpty(prevDGA) ? null : new DissolvedGasAnalysis(prevDGA);
            OilVolume = new Measurement() { Value = oilVolume, Unit = new VolumeUnits.Litre() };
            HasCommunicatingOltc = hasCommunicatingOltc;
        }

        public override void Execute()
        {
            var currDga = CurrentDGA;
            var prevDga = PreviousDGA;
            var outputs = Outputs;

            Rules.Add(new CurrentDgaExistsRule());
            Rules.Add(new ApplyDetectionLimitsRule());
            Rules.Add(new CarbonDioxideToCarbonMonoxideRatioRule());
            Rules.Add(new OxygenToNitrogenRatioRule());
            Rules.Add(new AcetyleneToHydrogenRatioRule(HasCommunicatingOltc));
            Rules.Add(new RateOfChangeRule(HasCommunicatingOltc, OilVolume));
            Rules.Add(new LimitsRule(HasCommunicatingOltc));
            Rules.Add(new FinalDiagnosisRule());

            // Create a Title output
            outputs.Add(new Output() { Name = "Title", Description = $"Interpretation of Dissolved Gas Analysis as per {Version}" });

            foreach (var rule in Rules)
            {
                if(rule.IsApplicable(currDga, prevDga, outputs))
                {
                    rule.Execute(ref currDga, ref prevDga, ref outputs);
                }
            }

            Outputs = outputs;
        }
    }
}