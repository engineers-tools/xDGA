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

using ExcelDna.Integration;
using System;
using xDGA.CORE.Models;
using xDGA.CORE.Algorithms;
using System.Text;

namespace xDGA.ADDIN
{
    public class AlgorithmFunctions
    {
        [ExcelFunction(Description = "Returns a string with the JSON serialised version of the Dissolved Gas Analysis data.")]
        public static object SERIALIZEDGA(object samplingDate, double hydrogen, double methane, double ethane,
            double ethylene, double acetylene, double carbonMonoxide, double carbonDioxide, double oxygen,
            double nitrogen)
        {
            DateTime date = Optional.Check(samplingDate, DateTime.MinValue);

            if (date == DateTime.MinValue) return ExcelError.ExcelErrorNull;

            var dga = new DissolvedGasAnalysis(date, hydrogen, methane, ethane, ethylene, acetylene, carbonMonoxide, carbonDioxide, oxygen, nitrogen);

            return dga.ToSerialisedJson();
        }

        [ExcelFunction(Description = "Executes the assessment algorithms as recommended by the IEC 60599 guidelines.")]
        public static object IEC_60599(
            [ExcelArgument(Description = "The JSON serialised string representing the latest Dissolved Gas Analysis data.")]
            string currentDga,
            [ExcelArgument(Description = "The JSON serialised string representing the previous Dissolved Gas Analysis data.")]
            object previousDga,
            [ExcelArgument(Description = "The oil volume of the transformer in litres.")]
            double oilVolume,
            [ExcelArgument(Description = "A boolean flag that indicates whether the transformer has an On-Load Tap Changer that communicates with the main tank or not.")]
            bool hasCommunicatingOltc = false)
        {
            try
            {
                string prevDga = Optional.Check(previousDga, string.Empty);

                var algo = new IEC60599Algorithm(currentDga, prevDga, oilVolume, hasCommunicatingOltc);
                algo.Execute();

                var output = new StringBuilder();

                foreach (var item in algo.Outputs)
                {
                    output.AppendLine($"[ {item.Name} => {item.Description} ]");
                }

                return output.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [ExcelFunction(Description = "Executes the assessment algorithms as recommended by the IEC C57.104 guidelines.")]
        public static object IEEE_C57104(
            [ExcelArgument(Description = "The JSON serialised string representing the latest Dissolved Gas Analysis data.")]
            string currentDga,
            [ExcelArgument(Description = "The JSON serialised string representing the previous Dissolved Gas Analysis data.")]
            object previousDga,
            [ExcelArgument(Description = "The age of the transformer in years (integer number).")]
            int? transformerAge = null)
        {
            try
            {
                string prevDga = Optional.Check(previousDga, string.Empty);

                var algo = new IEEEC57104Algorithm(currentDga, prevDga, transformerAge);
                algo.Execute();

                var output = new StringBuilder();

                foreach (var item in algo.Outputs)
                {
                    output.AppendLine($"[ {item.Name} => {item.Description} ]");
                }

                return output.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [ExcelFunction(Description = "Using the supplied DGA data it calculates the outputs (zones) of the various applicable Duval Triangles.")]
        public static object DUVALTRIANGLES(
            [ExcelArgument(Description = "The Dissolved Gas Analysis that will be assessed.")]
            string dga)
        {
            try
            {
                var algo = new DuvalTrianglesAlgorithm(dga);
                algo.Execute();

                var output = new StringBuilder();

                foreach (var item in algo.Outputs)
                {
                    output.AppendLine($"[ {item.Name} => {item.Description} ]");
                }

                return output.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [ExcelFunction(Description = "Using the supplied DGA data it calculates the outputs (zones) of the various applicable Duval Triangles for On-Load Tap changers (OLTC).")]
        public static object DUVALTRIANGLES_OLTC(
            [ExcelArgument(Description = "The Dissolved Gas Analysis that will be assessed.")]
            string dga)
        {
            try
            {
                var algo = new DuvalTrianglesOltcAlgorithm(dga);
                algo.Execute();

                var output = new StringBuilder();

                foreach (var item in algo.Outputs)
                {
                    output.AppendLine($"[ {item.Name} => {item.Description} ]");
                }

                return output.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [ExcelFunction(Description = "Using the supplied DGA data it calculates the outputs (zones) of the two Duval Pentagons.")]
        public static object DUVALPENTAGONS(
            [ExcelArgument(Description = "The Dissolved Gas Analysis that will be assessed.")]
            string dga)
        {
            try
            {
                var algo = new DuvalPentagonsAlgorithm(dga);
                algo.Execute();

                var output = new StringBuilder();

                foreach (var item in algo.Outputs)
                {
                    output.AppendLine($"[ {item.Name} => {item.Description} ]");
                }

                return output.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [ExcelFunction(Description = "Using the supplied DGA data it calculates the Rogers Ratios.")]
        public static object ROGERSRATIOS(
            [ExcelArgument(Description = "The Dissolved Gas Analysis that will be assessed.")]
            string dga)
        {
            try
            {
                var algo = new RogersRatiosAlgorithm(dga);
                algo.Execute();

                var output = new StringBuilder();

                foreach (var item in algo.Outputs)
                {
                    output.AppendLine($"[ {item.Name} => {item.Description} ]");
                }

                return output.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
