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
    public class ApplyDetectionLimitsRule : IRule
    {
        public void Execute(ref DissolvedGasAnalysis currentDga, ref DissolvedGasAnalysis previousDga, ref List<IOutput> outputs)
        {
            if(currentDga != null) currentDga = _ApplyDetectionLimits(currentDga);
            if (previousDga != null) previousDga = _ApplyDetectionLimits(previousDga);
        }

        public bool IsApplicable(DissolvedGasAnalysis currentDga, DissolvedGasAnalysis previousDga, List<IOutput> outputs)
        {
            return true;
        }

        private Dictionary<Gas, double> _DetectionLimits = new Dictionary<Gas, double>();
        /// <summary>
        /// Recommended gas detection limits as per
        /// IEC 60567 Edition 3.0
        /// </summary>
        /// <returns>Dictionary with Gas and Detection Limit</returns>
        public Dictionary<Gas, double> DetectionLimits
        {
            get
            {
                if (_DetectionLimits.Count == 0)
                {
                    // Use default values if no other have been specified
                    return new Dictionary<Gas, double>()
                    {
                        { Gas.Hydrogen, 2.0 },
                        { Gas.Methane, 0.1 },
                        { Gas.Ethane, 0.1 },
                        { Gas.Ethylene, 0.1 },
                        { Gas.Acetylene, 0.1 },
                        { Gas.CarbonMonoxide, 5.0 },
                        { Gas.CarbonDioxide, 10.0 },
                        { Gas.Oxygen, 500.0 },
                        { Gas.Nitrogen, 2000.0 }
                    };
                }
                else
                {
                    return _DetectionLimits;
                }
            }

            set { _DetectionLimits = value; }
        }

        /// <summary>
        /// Section 6.1 -
        /// Check values with 0s (zeroes) and
        /// replace them with the detection
        /// limits S for each gas. Refer to
        /// IEC 60567 for recommended S values.
        /// </summary>
        private DissolvedGasAnalysis _ApplyDetectionLimits(DissolvedGasAnalysis dga)
        {
            dga.Hydrogen.Value = dga.Hydrogen.Value < DetectionLimits[Gas.Hydrogen] ? DetectionLimits[Gas.Hydrogen] : dga.Hydrogen.Value;
            dga.Methane.Value = dga.Methane.Value < DetectionLimits[Gas.Methane] ? DetectionLimits[Gas.Methane] : dga.Methane.Value;
            dga.Ethane.Value = dga.Ethane.Value < DetectionLimits[Gas.Ethane] ? DetectionLimits[Gas.Ethane] : dga.Ethane.Value;
            dga.Ethylene.Value = dga.Ethylene.Value < DetectionLimits[Gas.Ethylene] ? DetectionLimits[Gas.Ethylene] : dga.Ethylene.Value;
            dga.Acetylene.Value = dga.Acetylene.Value < DetectionLimits[Gas.Acetylene] ? DetectionLimits[Gas.Acetylene] : dga.Acetylene.Value;
            dga.CarbonMonoxide.Value = dga.CarbonMonoxide.Value < DetectionLimits[Gas.CarbonMonoxide] ? DetectionLimits[Gas.CarbonMonoxide] : dga.CarbonMonoxide.Value;
            dga.CarbonDioxide.Value = dga.CarbonDioxide.Value < DetectionLimits[Gas.CarbonDioxide] ? DetectionLimits[Gas.CarbonDioxide] : dga.CarbonDioxide.Value;
            dga.Oxygen.Value = dga.Oxygen.Value < DetectionLimits[Gas.Oxygen] ? DetectionLimits[Gas.Oxygen] : dga.Oxygen.Value;
            dga.Nitrogen.Value = dga.Nitrogen.Value < DetectionLimits[Gas.Nitrogen] ? DetectionLimits[Gas.Nitrogen] : dga.Nitrogen.Value;

            return dga;
        }
    }
}
