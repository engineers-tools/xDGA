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
using System.Text;
using System.Threading.Tasks;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms
{
    public class DuvalTriangleFourRule : AbstractDuvalTriangleRule
    {
        private FailureType.Code TriangleOneFailureCode;

        public DuvalTriangleFourRule(FailureType.Code triangleOneFailureCode) : base("Duval Triangle 4", Gas.Hydrogen, Gas.Methane, Gas.Ethane)
        {
            TriangleOneFailureCode = triangleOneFailureCode;
        }

        internal override FailureType.Code DetermineFaultZone()
        {
            if (ThirdPercentage < 1.0 && SecondPercentage >= 2.0 && SecondPercentage < 15.0) return FailureType.Code.PD;
            else if (ThirdPercentage < 46.0 && FirstPercentage >= 9.0 && ThirdPercentage >= 30.0) return FailureType.Code.S;
            else if (ThirdPercentage < 24.0 && SecondPercentage < 36.0 && ThirdPercentage >= 1.0) return FailureType.Code.S;
            else if (ThirdPercentage < 1.0 && SecondPercentage < 2.0) return FailureType.Code.S;
            else if (ThirdPercentage < 1.0 && SecondPercentage >= 15.0 && SecondPercentage < 36.0) return FailureType.Code.S;
            else if (ThirdPercentage < 30.0 && FirstPercentage >= 15.0 && ThirdPercentage >= 24.0) return FailureType.Code.S;
            else if (ThirdPercentage < 24.0 && FirstPercentage >= 15.0 && SecondPercentage >= 36.0) return FailureType.Code.C;
            else if (ThirdPercentage < 30.0 && FirstPercentage < 15.0) return FailureType.Code.C;
            else if (ThirdPercentage >= 30.0 && FirstPercentage < 9.0) return FailureType.Code.O;
            else if (ThirdPercentage >= 46.0 && FirstPercentage >= 9.0) return FailureType.Code.ND;
            else return FailureType.Code.NA;
        }

        public override bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            var triangleOneResult = (
                TriangleOneFailureCode == FailureType.Code.PD || 
                TriangleOneFailureCode == FailureType.Code.T1 || 
                TriangleOneFailureCode == FailureType.Code.T2);

            return triangleOneResult && base.IsApplicable(currentDga, previousDga, outputs);
        }
    }
}
