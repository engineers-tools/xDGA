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

namespace xDGA.CORE.Models
{
    /// <summary>
    /// Types of faults as described in Section 5.3 page 13 of
    /// the IEC 60599 guidelines.
    /// </summary>
    public static class FailureType
    {
        /// <summary>
        /// Code for each type of fault.
        /// </summary>
        public enum Code
        {
            N,
            PD,
            D1,
            D2,
            DT,
            T1,
            T2,
            T3,
            T3_H,
            S,
            O,
            C,
            NA,
            ND,

            OltcD1,
            OltcN,
            OltcT2,
            OltcT3,
            OltcX1,
            OltcX3
        }

        /// <summary>
        /// A table that provides a string description of each
        /// of the applicable failure codes.
        /// </summary>
        public static Dictionary<Code, string> Description
        {
            get
            {
                return new Dictionary<Code, string>()
                {
                    { Code.N, "N => Normal Operation" },
                    { Code.PD, "PD => Partial Discharges" },
                    { Code.D1, "D1 => Discharges of low energy" },
                    { Code.D2, "D2 => Discharges of high energy" },
                    { Code.DT, "DT => Mixtures of electrical and thermal faults." },
                    { Code.T1, "T1 => Thermal fault t < 300 oC" },
                    { Code.T2, "T2 => Thermal fault 300 oC < t < 700 oC" },
                    { Code.T3, "T3 => Thermal fault t > 700 oC" },
                    { Code.T3_H, "T3-H => Thermal Faults in Oil Only" },
                    { Code.S, "S => Stray gassing of Oil (< 200 oC)" },
                    { Code.C, "C => Possible carbonization of paper" },
                    { Code.O, "O => Overheating (< 250 oC)" },
                    { Code.NA, "NA => Not Available" },
                    { Code.ND, "ND => Not Determined" },

                    { Code.OltcD1, "N => Normal Operation" },
                    { Code.OltcN, "D1 => Abnormal Arcing" },
                    { Code.OltcT2, "T2 => Severe thermal fault (300 < T < 700 oC), heavy coking" },
                    { Code.OltcT3, "T3 => Severe thermal fault (T > 700 oC), heavy coking" },
                    { Code.OltcX1, "X1 => Abnormal arcing or thermal fault in progress" },
                    { Code.OltcX3, "X3 => Fault T2 or T3 in progress, or abnormal severe arcing D1" }
                };
            }
        }
    }
}
