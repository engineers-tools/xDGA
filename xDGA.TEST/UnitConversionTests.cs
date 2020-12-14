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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xDGA.CORE.Models;
using xDGA.CORE.Units;

namespace xDGA.TEST
{
    [TestClass]
    public class UnitConversionTests
    {
        [TestMethod]
        public void DegreesToRadians()
        {
            var degrees = new Measurement() { Value = 90.0, Unit = new AngleUnits.Degrees() };
            var radians = degrees.ConvertTo(new AngleUnits.Radians());
            var expectedRadians = Math.PI * (degrees.Value / 180.0);

            Assert.AreEqual(expectedRadians, radians);
        }

        [TestMethod]
        public void RadiansToDegrees()
        {
            var radians = new Measurement() { Value = Math.PI, Unit = new AngleUnits.Radians() };
            var degrees = radians.ConvertTo(new AngleUnits.Degrees());
            var expectedDegrees = 180.0;

            Assert.AreEqual(expectedDegrees, degrees);
        }
    }
}
