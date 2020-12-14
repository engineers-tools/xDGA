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


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xDGA.CORE.Models;

namespace xDGA.TEST
{
    [TestClass]
    public class SerializationTests
    {
        private DateTime currDate = new DateTime(2017,05,17);
        private double currHydrogen = 200.0;
        private double currMethane = 40.0;
        private double currEthane = 15.5;
        private double currEthylene = 10.0;
        private double currAcetylene = 4.5;
        private double currCarbonMonoxide = 300.0;
        private double currCarbonDioxide = 3000.0;
        private double currOxygen = 750.0;
        private double currNitrogen = 7500.0;

        private string currSampleSerialized = "{\"SamplingDate\":\"2017-05-17T00:00:00\"," +
            "\"Hydrogen\":{\"Value\":200.0,\"Unit\":{\"Name\":\"Parts per Million\",\"Symbol\":\"PPM\"," +
            "\"Base\":1.0,\"Family\":\"Concentration\"}},\"Methane\":{\"Value\":40.0,\"Unit\":{\"Name\":" +
            "\"Parts per Million\",\"Symbol\":\"PPM\",\"Base\":1.0,\"Family\":\"Concentration\"}}," +
            "\"Ethane\":{\"Value\":15.5,\"Unit\":{\"Name\":\"Parts per Million\",\"Symbol\":\"PPM\"," +
            "\"Base\":1.0,\"Family\":\"Concentration\"}},\"Ethylene\":{\"Value\":10.0,\"Unit\":{\"Name\":" +
            "\"Parts per Million\",\"Symbol\":\"PPM\",\"Base\":1.0,\"Family\":\"Concentration\"}},\"Acetylene\":" +
            "{\"Value\":4.5,\"Unit\":{\"Name\":\"Parts per Million\",\"Symbol\":\"PPM\",\"Base\":1.0,\"Family\":" +
            "\"Concentration\"}},\"CarbonMonoxide\":{\"Value\":300.0,\"Unit\":{\"Name\":\"Parts per Million\"," +
            "\"Symbol\":\"PPM\",\"Base\":1.0,\"Family\":\"Concentration\"}},\"CarbonDioxide\":{\"Value\":3000.0," +
            "\"Unit\":{\"Name\":\"Parts per Million\",\"Symbol\":\"PPM\",\"Base\":1.0,\"Family\":\"Concentration\"}}," +
            "\"Oxygen\":{\"Value\":750.0,\"Unit\":{\"Name\":\"Parts per Million\",\"Symbol\":\"PPM\",\"Base\":" +
            "1.0,\"Family\":\"Concentration\"}},\"Nitrogen\":{\"Value\":7500.0,\"Unit\":{\"Name\":\"Parts per Million\"," +
            "\"Symbol\":\"PPM\",\"Base\":1.0,\"Family\":\"Concentration\"}}}";

        private DateTime prevDate = new DateTime(2016, 05, 17);
        private double prevHydrogen = 100.0;
        private double prevMethane = 20.0;
        private double prevEthane = 7.5;
        private double prevEthylene = 5.0;
        private double prevAcetylene = 2.5;
        private double prevCarbonMonoxide = 150.0;
        private double prevCarbonDioxide = 1500.0;
        private double prevOxygen = 355.0;
        private double prevNitrogen = 3550.0;

        [TestMethod]
        public void SerializeSample()
        {
            var dga = new DissolvedGasAnalysis(currDate, currHydrogen, currMethane, currEthane, currEthylene, currAcetylene,
                currCarbonMonoxide, currCarbonDioxide, currOxygen, currNitrogen);

            Assert.AreEqual(currSampleSerialized,dga.ToSerialisedJson());
        }

        [TestMethod]
        public void DeserializeSample()
        {
            var dga = new DissolvedGasAnalysis(currDate, currHydrogen, currMethane, currEthane, currEthylene, currAcetylene,
                currCarbonMonoxide, currCarbonDioxide, currOxygen, currNitrogen);

            Assert.AreEqual(dga,dga.FromSerialisedJson(currSampleSerialized));
        }
    }
}
