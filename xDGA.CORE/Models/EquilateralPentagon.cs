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
    public class EquilateralPentagon
    {
        public List<PolygonalAxis> Axes { get; set; } = new List<PolygonalAxis>();
        public List<Area> Areas { get; set; } = new List<Area>();
        public List<PolygonalCoordinate> DataPoints { get; set; } = new List<PolygonalCoordinate>();

        public EquilateralPentagon(PolygonalAxis axisOne, PolygonalAxis axisTwo, PolygonalAxis axisThree, PolygonalAxis axisFour, PolygonalAxis axisFive, List<Area> areas)
        {
            Axes.Add(axisOne);
            Axes.Add(axisTwo);
            Axes.Add(axisThree);
            Axes.Add(axisFour);
            Axes.Add(axisFive);
            Areas = areas;
        }

        public EquilateralPentagon(PolygonalAxis axisOne, PolygonalAxis axisTwo, PolygonalAxis axisThree, PolygonalAxis axisFour, PolygonalAxis axisFive)
        {
            Axes.Add(axisOne);
            Axes.Add(axisTwo);
            Axes.Add(axisThree);
            Axes.Add(axisFour);
            Axes.Add(axisFive);
        }

        public FailureType.Code GetFaultCodeForDataPoint(PolygonalCoordinate dataPoint)
        {
            FailureType.Code faultCode = FailureType.Code.NA;

            CartesianCoordinate centroid = dataPoint.GetArea().GetCentroid();

            foreach (var area in Areas)
            {
                if (area.CheckIfCoordinateIsInArea(centroid)) faultCode = area.FaultCode;
            }

            return faultCode;
        }

        public PolygonalCoordinate AddDataPoint(double axisOne, double axisTwo, double axisThree, double axisFour, double axisFive)
        {
            var coordinate = new PolygonalCoordinate();

            coordinate.Ordinates.Add(new PolygonalOrdinate(Axes[0], axisOne));
            coordinate.Ordinates.Add(new PolygonalOrdinate(Axes[1], axisTwo));
            coordinate.Ordinates.Add(new PolygonalOrdinate(Axes[2], axisThree));
            coordinate.Ordinates.Add(new PolygonalOrdinate(Axes[3], axisFour));
            coordinate.Ordinates.Add(new PolygonalOrdinate(Axes[4], axisFive));

            DataPoints.Add(coordinate);

            return coordinate;
        }
    }
}
