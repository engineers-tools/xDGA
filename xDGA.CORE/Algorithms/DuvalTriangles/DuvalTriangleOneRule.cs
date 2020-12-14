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
using xDGA.CORE.Interfaces;
using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms
{
    /// <summary>=
    /// Implements the calculations that determine in
    /// which zone of Duval's Triangle 1 the sample is.
    /// </summary>=
    public class DuvalTriangleOneRule : AbstractDuvalTriangleRule
    {
        public DuvalTriangleOneRule() : base("Duval Triangle 1", Gas.Methane, Gas.Ethylene, Gas.Acetylene) { }

        internal override FailureType.Code DetermineFaultZone()
        {
            if(FirstPercentage >= 98.0) return FailureType.Code.PD;
            else if(FirstPercentage < 98.0 && SecondPercentage < 20.0 && ThirdPercentage < 4.0) return FailureType.Code.T1;
            else if(SecondPercentage >= 20.0 && SecondPercentage < 50.0 && ThirdPercentage < 4.0) return FailureType.Code.T2;
            else if(SecondPercentage >= 50.0 && ThirdPercentage < 15.0) return FailureType.Code.T3;
            else if(SecondPercentage < 23.0 && ThirdPercentage >= 13.0) return FailureType.Code.D1;
            else if(SecondPercentage >= 23.0 && SecondPercentage < 40.0 && ThirdPercentage >= 13.0 && ThirdPercentage < 29.0) return FailureType.Code.D2;
            else if(SecondPercentage >= 23.0 && ThirdPercentage >= 29.0) return FailureType.Code.D2;
            else if(SecondPercentage < 50.0 && ThirdPercentage < 13.0 && ThirdPercentage >= 4.0) return FailureType.Code.DT;
            else if(SecondPercentage >= 40.0 && ThirdPercentage >= 15.0 && ThirdPercentage < 29.0) return FailureType.Code.DT;
            else if(SecondPercentage >= 40.0 && SecondPercentage < 50.0 && ThirdPercentage >= 13.0 && ThirdPercentage < 15.0) return FailureType.Code.DT;
            else return FailureType.Code.NA;
        }
    }
}
