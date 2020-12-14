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
    public class CoordinatesTests
    {
        [TestMethod]
        public void PolygonalCoordinateCalculation()
        {
            var axis = new PolygonalAxis("Ethane", new Measurement() { Value = 18.0, Unit = new AngleUnits.Degrees() });
            var polygonalCoordinate = new PolygonalOrdinate(axis, 34);
            var calculatedCoordinate = polygonalCoordinate.GetCartesianCoordinate();
            var expectedCoordinate = new CartesianCoordinate(32.3, 10.5);

            Assert.AreEqual(expectedCoordinate.X, Math.Round(calculatedCoordinate.X, 1));
            Assert.AreEqual(expectedCoordinate.Y, Math.Round(calculatedCoordinate.Y, 1));
        }

        [TestMethod]
        public void CalculateCentroidOfArea()
        {
            var area = new Area();
            area.Coordinates.Add(new CartesianCoordinate(0.0, 8.1));
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(4.8, -6.5));
            area.Coordinates.Add(new CartesianCoordinate(-29.4, -40.5));
            area.Coordinates.Add(new CartesianCoordinate(-32.3, 10.5));

            var centroid = area.GetCentroid();

            Assert.AreEqual(-17.3, Math.Round(centroid.X, 1));
            Assert.AreEqual(-9.1, Math.Round(centroid.Y, 1));
        }

        [TestMethod]
        public void CalculateArea()
        {
            var area = new Area();
            area.Coordinates.Add(new CartesianCoordinate(0.0, 8.1));
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(4.8, -6.5));
            area.Coordinates.Add(new CartesianCoordinate(-29.4, -40.5));
            area.Coordinates.Add(new CartesianCoordinate(-32.3, 10.5));
            
            Assert.AreEqual(-1132, Math.Round(area.GetArea(), 1));
        }

        [TestMethod]
        public void CheckIfPointIsInArea()
        {
            var area = new Area();
            // 10 x 10 Square on the positive (x,y) quadrant
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(0.0, 10.0));
            area.Coordinates.Add(new CartesianCoordinate(10.0, 10.0));
            area.Coordinates.Add(new CartesianCoordinate(10.0, 0.0));

            var point = new CartesianCoordinate(5.0, 5.0);

            Assert.IsTrue(area.CheckIfCoordinateIsInArea(point));

            area = new Area();
            // 10 x 10 Square on the positive (x) & negative (y) quadrant
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(0.0, -10.0));
            area.Coordinates.Add(new CartesianCoordinate(10.0, -10.0));
            area.Coordinates.Add(new CartesianCoordinate(10.0, 0.0));

            point = new CartesianCoordinate(5.0, -5.0);

            Assert.IsTrue(area.CheckIfCoordinateIsInArea(point));

            area = new Area();
            // 10 x 10 Square on the negative (x) & negative (y) quadrant
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(0.0, -10.0));
            area.Coordinates.Add(new CartesianCoordinate(-10.0, -10.0));
            area.Coordinates.Add(new CartesianCoordinate(-10.0, 0.0));

            point = new CartesianCoordinate(-5.0, -5.0);

            Assert.IsTrue(area.CheckIfCoordinateIsInArea(point));

            area = new Area();
            // 10 x 10 Square on the negative (x) & positive (y) quadrant
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(0.0, 10.0));
            area.Coordinates.Add(new CartesianCoordinate(-10.0, 10.0));
            area.Coordinates.Add(new CartesianCoordinate(-10.0, 0.0));

            point = new CartesianCoordinate(-5.0, 5.0);

            Assert.IsTrue(area.CheckIfCoordinateIsInArea(point));

            // Irregular area
            area.Coordinates.Clear();
            area.Coordinates.Add(new CartesianCoordinate(0.0, 8.1));
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(4.8, -6.5));
            area.Coordinates.Add(new CartesianCoordinate(-29.4, -40.5));
            area.Coordinates.Add(new CartesianCoordinate(-32.3, 10.5));

            point = new CartesianCoordinate(-17.3, -9.1);

            Assert.IsTrue(area.CheckIfCoordinateIsInArea(point));
        }

        [TestMethod]
        public void CheckIfPointIsOusideArea()
        {
            var area = new Area();
            // 10 x 10 Square on the positive quadrant
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(0.0, 10.0));
            area.Coordinates.Add(new CartesianCoordinate(10.0, 10.0));
            area.Coordinates.Add(new CartesianCoordinate(10.0, 0.0));

            var point = new CartesianCoordinate(11.0, 5.0);

            Assert.IsFalse(area.CheckIfCoordinateIsInArea(point));

            // Irregular area
            area.Coordinates.Clear();
            area.Coordinates.Add(new CartesianCoordinate(0.0, 8.1));
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(4.8, -6.5));
            area.Coordinates.Add(new CartesianCoordinate(-29.4, -40.5));
            area.Coordinates.Add(new CartesianCoordinate(-32.3, 10.5));

            point = new CartesianCoordinate(17.3, -9.1);

            Assert.IsFalse(area.CheckIfCoordinateIsInArea(point));
        }

        [TestMethod]
        public void CheckIfPointIsInAreaEdge()
        {
            var area = new Area();
            // 10 x 10 Square on the positive quadrant
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(0.0, 10.0));
            area.Coordinates.Add(new CartesianCoordinate(10.0, 10.0));
            area.Coordinates.Add(new CartesianCoordinate(10.0, 0.0));

            var point = new CartesianCoordinate(10.0, 5.0);

            Assert.IsTrue(area.CheckIfCoordinateIsInArea(point));

            // Irregular area
            area.Coordinates.Clear();
            area.Coordinates.Add(new CartesianCoordinate(0.0, 8.1));
            area.Coordinates.Add(new CartesianCoordinate(0.0, 0.0));
            area.Coordinates.Add(new CartesianCoordinate(4.8, -6.5));
            area.Coordinates.Add(new CartesianCoordinate(-29.4, -40.5));
            area.Coordinates.Add(new CartesianCoordinate(-32.3, 10.5));

            point = new CartesianCoordinate(0.0, 5.0);

            Assert.IsTrue(area.CheckIfCoordinateIsInArea(point));
        }
    }
}
