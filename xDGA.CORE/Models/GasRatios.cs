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
    public static class GasRatios
    {
        public enum Ratio
        {
            HydrogenToMethane,
            HydrogenToEthane,
            HydrogenToEthylene,
            HydrogenToAcetylene,
            MethaneToHydrogen,
            MethaneToEthane,
            MethaneToEthylene,
            MethaneToAcetylene,
            EthaneToHydrogen,
            EthaneToMethane,
            EthaneToEthylene,
            EthaneToAcetylene,
            EthyleneToHydrogen,
            EthyleneToMethane,
            EthyleneToEthane,
            EthyleneToAcetylene,
            AcetyleneToHydrogen,
            AcetyleneToMethane,
            AcetyleneToEthane,
            AcetyleneToEthylene,
            CarbonMonoxideToCarbonDioxide,
            CarbonDioxideToCarbonMonoxide,
            NitrogenToOxygen,
            OxygenToNitrogen
        }

        public static Dictionary<Ratio, string> Description
        {
            get
            {
                return new Dictionary<Ratio, string>()
                {
                    { Ratio.HydrogenToMethane, "H2/CH4" },
                    { Ratio.HydrogenToEthane, "H2/C2H6" },
                    { Ratio.HydrogenToEthylene, "H2/C2H4" },
                    { Ratio.HydrogenToAcetylene, "H2/C2H2" },
                    { Ratio.MethaneToHydrogen, "CH4/H2" },
                    { Ratio.MethaneToEthane,  "CH4/C2H6" },
                    { Ratio.MethaneToEthylene, "CH4/C2H4" },
                    { Ratio.MethaneToAcetylene, "CH4/C2H2" },
                    { Ratio.EthaneToHydrogen, "C2H6/H2" },
                    { Ratio.EthaneToMethane, "C2H6/CH4" },
                    { Ratio.EthaneToEthylene, "C2H6/C2H4" },
                    { Ratio.EthaneToAcetylene, "C2H6/C2H2" },
                    { Ratio.EthyleneToHydrogen, "C2H4/H2" },
                    { Ratio.EthyleneToMethane, "C2H4/CH4" },
                    { Ratio.EthyleneToEthane, "C2H4/C2H6" },
                    { Ratio.EthyleneToAcetylene, "C2H4/C2H2" },
                    { Ratio.AcetyleneToHydrogen, "C2H2/H2" },
                    { Ratio.AcetyleneToMethane, "C2H2/CH4" },
                    { Ratio.AcetyleneToEthane, "C2H2/C2H6" },
                    { Ratio.AcetyleneToEthylene, "C2H2/C2H4" },
                    { Ratio.CarbonMonoxideToCarbonDioxide, "CO/CO2" },
                    { Ratio.CarbonDioxideToCarbonMonoxide, "CO2/CO" },
                    { Ratio.NitrogenToOxygen, "N2/O2" },
                    { Ratio.OxygenToNitrogen, "O2/N2" }
                };
            }
        }
    }
}
