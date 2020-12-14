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

namespace xDGA.CORE.Algorithms
{
    /// <summary>=
    /// Duval's Triangle 2 is applicable to On-Load Tap Changers (OLTC)
    /// </summary>=
    public class DuvalTriangleTwoRule : AbstractDuvalTriangleRule
    {
        public DuvalTriangleTwoRule() : base("Duval Triangle 2 (OLTC)", Gas.Methane, Gas.Ethylene, Gas.Acetylene) { }

        public override void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            base.Execute(ref currentDga, ref previousDga, ref outputs);

            outputs.Add(new Output() { Name = "Notes", Description = "This algorithm applies to conventional, compartment-type OLTCs where normal operation involves mostly arc breaking in oil. A few resistive OLTCs of this type (i.e. UZBs) may have their normal operation in the X3 zone. For OLTCs of the conventional, vacuum bottle-type with no sparking of the selector in the cooling oil use Duval Triangle 1. For OLTCs of the in-tank type (i.e. Reinhausen (MR)) where most or a significant portion of current is dissipated in transition resistors and heats up the resistors, the normal operating zone may be located in a different part of the Triangle (i.e. T2 or T3)." });
        }

        internal override FailureType.Code DetermineFaultZone()
        {
            if (FirstPercentage < 19.0 && FirstPercentage >= 2.0 && SecondPercentage < 23.0 && SecondPercentage >= 6.0) return FailureType.Code.OltcN;
            else if (FirstPercentage < 19.0 && SecondPercentage < 6.0) return FailureType.Code.OltcD1;
            else if (FirstPercentage < 2.0 && SecondPercentage < 23.0 && SecondPercentage >= 6.0) return FailureType.Code.OltcD1;
            else if (FirstPercentage >= 19.0 && SecondPercentage < 23.0) return FailureType.Code.OltcX1;
            else if (SecondPercentage >= 23.0 && ThirdPercentage >= 15.0) return FailureType.Code.OltcX3;
            else if (ThirdPercentage < 15.0 && SecondPercentage >= 23.0 && SecondPercentage < 50.0) return FailureType.Code.OltcT2;
            else if (ThirdPercentage < 15.0 && SecondPercentage >= 50.0) return FailureType.Code.OltcT3;
            else return FailureType.Code.NA;
        }
    }
}
