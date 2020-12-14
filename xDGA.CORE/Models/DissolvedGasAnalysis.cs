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

using Newtonsoft.Json;
using System;
using xDGA.CORE.Interfaces;
using xDGA.CORE.Units;
using xDGA.CORE.Serialization;

namespace xDGA.CORE.Models
{
    public class DissolvedGasAnalysis
    {
        public DateTime SamplingDate { get; set; }
        [JsonConverter(typeof(GasMeasurementConverter))]
        public IMeasurement Hydrogen { get; set; }
        [JsonConverter(typeof(GasMeasurementConverter))]
        public IMeasurement Methane { get; set; }
        [JsonConverter(typeof(GasMeasurementConverter))]
        public IMeasurement Ethane { get; set; }
        [JsonConverter(typeof(GasMeasurementConverter))]
        public IMeasurement Ethylene { get; set; }
        [JsonConverter(typeof(GasMeasurementConverter))]
        public IMeasurement Acetylene { get; set; }
        [JsonConverter(typeof(GasMeasurementConverter))]
        public IMeasurement CarbonMonoxide { get; set; }
        [JsonConverter(typeof(GasMeasurementConverter))]
        public IMeasurement CarbonDioxide { get; set; }
        [JsonConverter(typeof(GasMeasurementConverter))]
        public IMeasurement Oxygen { get; set; }
        [JsonConverter(typeof(GasMeasurementConverter))]
        public IMeasurement Nitrogen { get; set; }

        public DissolvedGasAnalysis() { }

        /// <summary>
        /// Creates a new instance of DissolvedGasAnalysis.
        /// </summary>
        public DissolvedGasAnalysis(DateTime samplingDate, double hydrogen, double methane, double ethane,
            double ethylene, double acetylene, double carbonMonoxide, double carbonDioxide, double oxygen,
            double nitrogen)
        {
            SamplingDate = samplingDate;
            Hydrogen = new Measurement() { Value = hydrogen, Unit = new ConcentrationUnits.PartsPerMillion() };
            Methane = new Measurement() { Value = methane, Unit = new ConcentrationUnits.PartsPerMillion() };
            Ethane = new Measurement() { Value = ethane, Unit = new ConcentrationUnits.PartsPerMillion() };
            Ethylene = new Measurement() { Value = ethylene, Unit = new ConcentrationUnits.PartsPerMillion() };
            Acetylene = new Measurement() { Value = acetylene, Unit = new ConcentrationUnits.PartsPerMillion() };
            CarbonMonoxide = new Measurement() { Value = carbonMonoxide, Unit = new ConcentrationUnits.PartsPerMillion() };
            CarbonDioxide = new Measurement() { Value = carbonDioxide, Unit = new ConcentrationUnits.PartsPerMillion() };
            Oxygen = new Measurement() { Value = oxygen, Unit = new ConcentrationUnits.PartsPerMillion() };
            Nitrogen = new Measurement() { Value = nitrogen, Unit = new ConcentrationUnits.PartsPerMillion() };
        }

        /// <summary>
        /// Create a new DissolvedGasAnalysis instance.
        /// </summary>
        /// <param name="dgaData">Serialised JSON string with the DGA data.</param>
        public DissolvedGasAnalysis(string dgaData)
        {
            FromSerialisedJson(dgaData);
        }

        /// <summary>
        /// Returns a string with the JSON representation of this object.
        /// </summary>
        public string ToSerialisedJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Convert a serialised JSON string of an Dissolved Gas Analysis into
        /// a DissolvedGasAnalysis object.
        /// </summary>
        /// <param name="serializedDGA">String with JSON serialised DGA data.</param>
        /// <returns>This instance of DissolvedGasAnalysis with the appropriate data each field.</returns>
        public DissolvedGasAnalysis FromSerialisedJson(string serializedDGA)
        {
            var dga = JsonConvert.DeserializeObject<DissolvedGasAnalysis>(serializedDGA);

            SamplingDate = dga.SamplingDate;
            Hydrogen = dga.Hydrogen;
            Methane = dga.Methane;
            Ethane = dga.Ethane;
            Ethylene = dga.Ethylene;
            Acetylene = dga.Acetylene;
            CarbonMonoxide = dga.CarbonMonoxide;
            CarbonDioxide = dga.CarbonDioxide;
            Oxygen = dga.Oxygen;
            Nitrogen = dga.Nitrogen;

            return this;
        }
    }
}
