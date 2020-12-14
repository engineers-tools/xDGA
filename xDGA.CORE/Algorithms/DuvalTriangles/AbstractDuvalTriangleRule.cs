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
    public abstract class AbstractDuvalTriangleRule : IRule
    {
        internal string TriangleName;

        internal Gas FirstGasEnum;
        internal Gas ThirdGasEnum;
        internal Gas SecondGasEnum;

        internal IMeasurement FirstGas;
        internal IMeasurement SecondGas;
        internal IMeasurement ThirdGas;

        internal double TotalGases = 0.0;
        internal double FirstPercentage = 0.0;
        internal double SecondPercentage = 0.0;
        internal double ThirdPercentage = 0.0;

        public FailureType.Code FailureCode { get; set; } = FailureType.Code.NA;

        public AbstractDuvalTriangleRule(string triangleName, Gas firstGas, Gas secondGas, Gas thirdGas)
        {
            TriangleName = triangleName;
            FirstGasEnum = firstGas;
            SecondGasEnum = secondGas;
            ThirdGasEnum = thirdGas;
        }

        public virtual void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            FindGases(currentDga);
            CalculatePercentages(FirstGas, SecondGas, ThirdGas);
            FailureCode = DetermineFaultZone();

            outputs.Add(new Output() { Name = TriangleName, Description = FailureType.Description[FailureCode] });
        }

        public virtual bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            FindGases(currentDga);
            return FirstGas != null && SecondGas != null & ThirdGas != null;
        }

        internal void FindGases(DissolvedGasAnalysis dga)
        {
            FirstGas = (IMeasurement)dga.GetType().GetProperty(FirstGasEnum.ToString()).GetValue(dga);
            SecondGas = (IMeasurement)dga.GetType().GetProperty(SecondGasEnum.ToString()).GetValue(dga);
            ThirdGas = (IMeasurement)dga.GetType().GetProperty(ThirdGasEnum.ToString()).GetValue(dga);
        }

        internal void CalculatePercentages(IMeasurement firstGas, IMeasurement secondGas, IMeasurement thirdGas)
        {
            TotalGases = firstGas.Value + secondGas.Value + thirdGas.Value;
            FirstPercentage = (firstGas.Value * 100.0) / TotalGases;
            SecondPercentage = (secondGas.Value * 100.0) / TotalGases;
            ThirdPercentage = (thirdGas.Value * 100.0) / TotalGases;
        }

        internal abstract FailureType.Code DetermineFaultZone();
    }
}
