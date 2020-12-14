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
using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms.IEEEC57104
{
    public static class Tables
    {
        /// <summary>
        /// 6.1.3 DGA status norms
        ///     Table 1 - 90th percentile gas concentrations as a
        ///     function of O2/N2 ratio and age in in ul/l
        /// </summary>
        private class TableOneRow
        {
            public Gas Gas { get; set; }
            public string OxygenToNitrogenRatio { get; set; }
            public int? MinimumAge { get; set; }
            public int? MaximumAge { get; set; }
            public int MaximumConcentration { get; set; }

            public TableOneRow(Gas gas, string oxygentToNitrogenRatio, int? minimumAge, int? maximumAge, int maximumConcentration)
            {
                Gas = gas;
                OxygenToNitrogenRatio = oxygentToNitrogenRatio;
                MinimumAge = minimumAge;
                MaximumAge = maximumAge;
                MaximumConcentration = maximumConcentration;
            }
        }

        private static List<TableOneRow> TableOne
        {
            get
            {
                var t = new List<TableOneRow>()
            {
                // Hydrogen (H2) row
                new TableOneRow(Gas.Hydrogen, "<=0.2", null, null, 80),
                new TableOneRow(Gas.Hydrogen, "<=0.2", 1, 10, 75),
                new TableOneRow(Gas.Hydrogen, "<=0.2", 10, 30, 75),
                new TableOneRow(Gas.Hydrogen, "<=0.2", 30, int.MaxValue, 100),
                new TableOneRow(Gas.Hydrogen, ">0.2", null, null, 40),
                new TableOneRow(Gas.Hydrogen, ">0.2", 1, 10, 40),
                new TableOneRow(Gas.Hydrogen, ">0.2", 10, 30, 40),
                new TableOneRow(Gas.Hydrogen, ">0.2", 30, int.MaxValue, 40),
                // Methane (CH4) row
                new TableOneRow(Gas.Methane, "<=0.2", null, null, 90),
                new TableOneRow(Gas.Methane, "<=0.2", 1, 10, 45),
                new TableOneRow(Gas.Methane, "<=0.2", 10, 30, 90),
                new TableOneRow(Gas.Methane, "<=0.2", 30, int.MaxValue, 110),
                new TableOneRow(Gas.Methane, ">0.2", null, null, 20),
                new TableOneRow(Gas.Methane, ">0.2", 1, 10, 20),
                new TableOneRow(Gas.Methane, ">0.2", 10, 30, 20),
                new TableOneRow(Gas.Methane, ">0.2", 30, int.MaxValue, 20),
                // Ethane (C2H6) row
                new TableOneRow(Gas.Ethane, "<=0.2", null, null, 90),
                new TableOneRow(Gas.Ethane, "<=0.2", 1, 10, 30),
                new TableOneRow(Gas.Ethane, "<=0.2", 10, 30, 90),
                new TableOneRow(Gas.Ethane, "<=0.2", 30, int.MaxValue, 150),
                new TableOneRow(Gas.Ethane, ">0.2", null, null, 15),
                new TableOneRow(Gas.Ethane, ">0.2", 1, 10, 15),
                new TableOneRow(Gas.Ethane, ">0.2", 10, 30, 15),
                new TableOneRow(Gas.Ethane, ">0.2", 30, int.MaxValue, 15),
                // Ethylene (C2H4) row
                new TableOneRow(Gas.Ethylene, "<=0.2", null, null, 50),
                new TableOneRow(Gas.Ethylene, "<=0.2", 1, 10, 20),
                new TableOneRow(Gas.Ethylene, "<=0.2", 10, 30, 50),
                new TableOneRow(Gas.Ethylene, "<=0.2", 30, int.MaxValue, 90),
                new TableOneRow(Gas.Ethylene, ">0.2", null, null, 50),
                new TableOneRow(Gas.Ethylene, ">0.2", 1, 10, 25),
                new TableOneRow(Gas.Ethylene, ">0.2", 10, 30, 60),
                new TableOneRow(Gas.Ethylene, ">0.2", 30, int.MaxValue, 60),
                // Acetylene (C2H4) row
                new TableOneRow(Gas.Acetylene, "<=0.2", null, null, 1),
                new TableOneRow(Gas.Acetylene, "<=0.2", 1, 10, 1),
                new TableOneRow(Gas.Acetylene, "<=0.2", 10, 30, 1),
                new TableOneRow(Gas.Acetylene, "<=0.2", 30, int.MaxValue, 1),
                new TableOneRow(Gas.Acetylene, ">0.2", null, null, 2),
                new TableOneRow(Gas.Acetylene, ">0.2", 1, 10, 2),
                new TableOneRow(Gas.Acetylene, ">0.2", 10, 30, 2),
                new TableOneRow(Gas.Acetylene, ">0.2", 30, int.MaxValue, 2),
                // Carbon Monoxide (CO) row
                new TableOneRow(Gas.CarbonMonoxide, "<=0.2", null, null, 900),
                new TableOneRow(Gas.CarbonMonoxide, "<=0.2", 1, 10, 900),
                new TableOneRow(Gas.CarbonMonoxide, "<=0.2", 10, 30, 900),
                new TableOneRow(Gas.CarbonMonoxide, "<=0.2", 30, int.MaxValue, 900),
                new TableOneRow(Gas.CarbonMonoxide, ">0.2", null, null, 500),
                new TableOneRow(Gas.CarbonMonoxide, ">0.2", 1, 10, 500),
                new TableOneRow(Gas.CarbonMonoxide, ">0.2", 10, 30, 500),
                new TableOneRow(Gas.CarbonMonoxide, ">0.2", 30, int.MaxValue, 500),
                // Carbon Dioxide (CO2) row
                new TableOneRow(Gas.CarbonDioxide, "<=0.2", null, null, 9000),
                new TableOneRow(Gas.CarbonDioxide, "<=0.2", 1, 10, 20),
                new TableOneRow(Gas.CarbonDioxide, "<=0.2", 10, 30, 50),
                new TableOneRow(Gas.CarbonDioxide, "<=0.2", 30, int.MaxValue, 90),
                new TableOneRow(Gas.CarbonDioxide, ">0.2", null, null, 50),
                new TableOneRow(Gas.CarbonDioxide, ">0.2", 1, 10, 25),
                new TableOneRow(Gas.CarbonDioxide, ">0.2", 10, 30, 60),
                new TableOneRow(Gas.CarbonDioxide, ">0.2", 30, int.MaxValue, 60),
            };

                return t;
            }
        }

        private static List<TableOneRow> GetRowsForGas(List<TableOneRow> inputTable,Gas gas)
        {
            return inputTable.Select(row => row).Where(row => row.Gas == gas).ToList<TableOneRow>();
        }

        private static List<TableOneRow> GetRowsForOxygenNitrogenRatio(List<TableOneRow> inputTable, string ratio)
        {
            return inputTable.Select(row => row).Where(row => row.OxygenToNitrogenRatio == ratio).ToList<TableOneRow>();
        }

        private static List<TableOneRow> GetRowsForTransformerAge(List<TableOneRow> inputTable, int? age)
        {
            if (age == null)
                return inputTable.Select(row => row).Where(row => row.MinimumAge == null && row.MaximumAge == null).ToList<TableOneRow>();
            else
                return inputTable.Select(row => row).Where(row => age >= row.MinimumAge && age <= row.MaximumAge).ToList<TableOneRow>();
        }

        public static DissolvedGasAnalysis TableOneGasLimits(DissolvedGasAnalysis dga, int? transformerAge)
        {
            DissolvedGasAnalysis dgaLimits = new DissolvedGasAnalysis();

            string ONRatio = string.Empty;

            if (dga.Nitrogen == null || dga.Nitrogen.Value == 0)
                ONRatio = ">0.2";
            else
                ONRatio = (dga.Oxygen.Value / dga.Nitrogen.Value) <= 0.2 ? "<=0.2" : ">0.2";



            return dgaLimits;
        }
    }
}
