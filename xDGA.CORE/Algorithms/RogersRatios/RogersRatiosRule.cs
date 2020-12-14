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
using Helper = xDGA.CORE.Algorithms.AlgorithmHelperCalculations;
using Ratio = xDGA.CORE.Models.GasRatios.Ratio;

namespace xDGA.CORE.Algorithms
{
    public class RogersRatiosRule : IRule
    {
        private Dictionary<Ratio, double> _Ratios { get; set; } = new Dictionary<Ratio, double>();
        public FailureType.Code FailureCode { get; set; } = FailureType.Code.NA;

        public void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            CalculateRatios(currentDga);
            FailureCode = CalculateFailureCode();
            outputs.Add(new Output() { Name = GasRatios.Description[Ratio.AcetyleneToEthylene], Description = _Ratios[Ratio.AcetyleneToEthylene].ToString("0.000") });
            outputs.Add(new Output() { Name = GasRatios.Description[Ratio.MethaneToHydrogen], Description = _Ratios[Ratio.MethaneToHydrogen].ToString("0.000") });
            outputs.Add(new Output() { Name = GasRatios.Description[Ratio.EthyleneToEthane], Description = _Ratios[Ratio.EthyleneToEthane].ToString("0.000") });
            outputs.Add(new Output() { Name = "Rogers Ratio Failure Code", Description = FailureType.Description[FailureCode] });
        }

        public bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            return currentDga != null && currentDga.Ethylene != null && currentDga.Hydrogen != null && currentDga.Methane != null && currentDga.Ethane != null && currentDga.Acetylene != null;
        }

        private void CalculateRatios(DissolvedGasAnalysis dga)
        {
            _Ratios.Clear();
            _Ratios.Add(Ratio.AcetyleneToEthylene, (double)Helper.GasRatio(dga.Acetylene, dga.Ethylene));
            _Ratios.Add(Ratio.MethaneToHydrogen, (double)Helper.GasRatio(dga.Methane, dga.Hydrogen));
            _Ratios.Add(Ratio.EthyleneToEthane, (double)Helper.GasRatio(dga.Ethylene, dga.Ethane));
        }

        private FailureType.Code CalculateFailureCode()
        {
            var code = FailureType.Code.NA;

            if (_Ratios[Ratio.AcetyleneToEthylene] < 0.1 && _Ratios[Ratio.MethaneToHydrogen] > 0.1 && _Ratios[Ratio.MethaneToHydrogen] < 1.0 && _Ratios[Ratio.EthyleneToEthane] < 1.0) code = FailureType.Code.N;

            if (_Ratios[Ratio.AcetyleneToEthylene] < 0.1 && _Ratios[Ratio.MethaneToHydrogen] < 0.1 && _Ratios[Ratio.EthyleneToEthane] < 1.0) code = FailureType.Code.PD;

            if (_Ratios[Ratio.AcetyleneToEthylene] >= 0.1 && _Ratios[Ratio.AcetyleneToEthylene] <= 3.0 && _Ratios[Ratio.MethaneToHydrogen] >= 0.1 && _Ratios[Ratio.MethaneToHydrogen] <= 1.0 && _Ratios[Ratio.EthyleneToEthane] > 3.0) code = FailureType.Code.D2;

            if (_Ratios[Ratio.AcetyleneToEthylene] < 0.1 && _Ratios[Ratio.MethaneToHydrogen] > 0.1 && _Ratios[Ratio.MethaneToHydrogen] < 1.0 && _Ratios[Ratio.EthyleneToEthane] >= 1.0 && _Ratios[Ratio.EthyleneToEthane] <= 3.0) code = FailureType.Code.T1;

            if (_Ratios[Ratio.AcetyleneToEthylene] < 0.1 && _Ratios[Ratio.MethaneToHydrogen] > 1.0 && _Ratios[Ratio.EthyleneToEthane] >= 1.0 && _Ratios[Ratio.EthyleneToEthane] <= 3.0) code = FailureType.Code.T2;

            if (_Ratios[Ratio.AcetyleneToEthylene] < 0.1 && _Ratios[Ratio.MethaneToHydrogen] > 1.0 && _Ratios[Ratio.EthyleneToEthane] >= 3.0) code = FailureType.Code.T3;

            return code;
        }
    }
}