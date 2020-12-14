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
using System.Linq;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;
using xDGA.CORE.Units;

namespace xDGA.CORE.Algorithms
{
    public abstract class AbstractDuvalPentagonRule : IRule
    {
        internal string PentagonName { get; set; }
        internal EquilateralPentagon Pentagon { get; set; }
        internal Dictionary<Gas, IMeasurement> GasMeasurements { get; set; } = new Dictionary<Gas, IMeasurement>();
        internal Dictionary<Gas, double> GasPercentages { get; set; } = new Dictionary<Gas, double>();

        public FailureType.Code FailureCode { get; set; } = FailureType.Code.NA;

        /// <summary>
        /// Creates a new instance of a Duval Pentagon.
        /// Specify gases in counter clock-wise order starting at 12 o'oclock position.
        /// </summary>
        public AbstractDuvalPentagonRule(string name, Gas hydrogen, Gas ethane, Gas methane, Gas ethylene, Gas acetylene)
        {
            PentagonName = name;

            Pentagon = new EquilateralPentagon(
                new PolygonalAxis(hydrogen.ToString(), new Measurement() { Value = 90.0, Unit = new AngleUnits.Degrees() }),
                new PolygonalAxis(ethane.ToString(), new Measurement() { Value = 162.0, Unit = new AngleUnits.Degrees() }),
                new PolygonalAxis(methane.ToString(), new Measurement() { Value = 234.0, Unit = new AngleUnits.Degrees() }),
                new PolygonalAxis(ethylene.ToString(), new Measurement() { Value = 306.0, Unit = new AngleUnits.Degrees() }),
                new PolygonalAxis(acetylene.ToString(), new Measurement() { Value = 18.0, Unit = new AngleUnits.Degrees() }));

            GasMeasurements.Add(hydrogen, null);
            GasMeasurements.Add(ethane, null);
            GasMeasurements.Add(methane, null);
            GasMeasurements.Add(ethylene, null);
            GasMeasurements.Add(acetylene, null);

            GasPercentages.Add(hydrogen, 0.0);
            GasPercentages.Add(ethane, 0.0);
            GasPercentages.Add(methane, 0.0);
            GasPercentages.Add(ethylene, 0.0);
            GasPercentages.Add(acetylene, 0.0);
        }

        public virtual void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            FindGases(currentDga);

            CalculatePercentages(currentDga);

            var coordinate = Pentagon.AddDataPoint(GasPercentages[Gas.Hydrogen], GasPercentages[Gas.Ethane], GasPercentages[Gas.Methane], GasPercentages[Gas.Ethylene], GasPercentages[Gas.Acetylene]);

            FailureCode = Pentagon.GetFaultCodeForDataPoint(coordinate);

            outputs.Add(new Output() { Name = PentagonName, Description = FailureType.Description[FailureCode] });
        }

        public virtual bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            FindGases(currentDga);
            return GasMeasurements.All(g => g.Value != null);
        }

        public double TotalGases()
        {
            return GasMeasurements.Sum(g => { return g.Value.Value; });
        }

        internal void FindGases(DissolvedGasAnalysis dga)
        {
            foreach (var gas in GasPercentages)
            {
                GasMeasurements[gas.Key] = (IMeasurement)dga.GetType().GetProperty(gas.Key.ToString()).GetValue(dga);
            }
        }

        internal void CalculatePercentages(DissolvedGasAnalysis dga)
        {
            double total = TotalGases();
            double gasPercentage = 0.0;

            foreach (var gas in GasMeasurements)
            {
                gasPercentage = (GasMeasurements[gas.Key].Value * 100.0) / total;
                GasPercentages[gas.Key] = gasPercentage;
            }
        }
    }
}
