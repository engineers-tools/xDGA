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
using System.Collections.Generic;
using System.Linq;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms.IEEEC57104
{
    public class TableOneRule : IRule
    {

        private int? _TransformerAge { get; set; } = null;

        public TableOneRule(int? transformerAge)
        {
            _TransformerAge = transformerAge;
        }

        public void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            var isExceeded = _CheckIfTableOneValuesAreExceeded(currentDga, _TransformerAge);
        }

        public bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            return currentDga != null;
        }

        private bool _CheckIfTableOneValuesAreExceeded(DissolvedGasAnalysis dga, int? transformerAge)
        {
            DissolvedGasAnalysis limits = Tables.TableOneGasLimits(dga, transformerAge);

            if (dga.Hydrogen.Value <= limits.Hydrogen.Value &&
               dga.Methane.Value <= limits.Methane.Value &&
               dga.Ethane.Value <= limits.Ethane.Value &&
               dga.Ethylene.Value <= limits.Ethylene.Value &&
               dga.Acetylene.Value <= limits.Acetylene.Value &&
               dga.CarbonMonoxide.Value <= limits.CarbonMonoxide.Value &&
               dga.CarbonDioxide.Value <= limits.CarbonDioxide.Value)
                return true;

            return false;
        }
    }
}
