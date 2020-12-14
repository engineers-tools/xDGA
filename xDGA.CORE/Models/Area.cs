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
using System.Linq;
using System.Collections.Generic;

namespace xDGA.CORE.Models
{
    public class Area
    {
        public FailureType.Code FaultCode { get; set; }

        public List<CartesianCoordinate> Coordinates { get; set; } = new List<CartesianCoordinate>();

        public Area() { }

        public Area(FailureType.Code faultCode)
        {
            FaultCode = faultCode;
        }

        public CartesianCoordinate GetCentroid()
        {
            return new CartesianCoordinate(GetCentroidCoordinateX(), GetCentroidCoordinateY());
        }

        public double GetArea()
        {
            double area = 0.0;
            var coordCount = Coordinates.Count;

            for (int i = 0; i < coordCount; i++)
            {
                try
                {
                    area = area + ((Coordinates[i].X * Coordinates[i + 1].Y) - (Coordinates[i + 1].X * Coordinates[i].Y));
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentOutOfRangeException) area = area + ((Coordinates[i].X * Coordinates[0].Y) - (Coordinates[0].X * Coordinates[i].Y));
                }
            }

            return (0.5) * area;
        }

        public double GetCentroidCoordinateX()
        {
            double centroid = 0.0;
            var area = GetArea();
            var coordCount = Coordinates.Count;

            for (int i = 0; i < coordCount; i++)
            {
                try
                {
                    centroid = centroid + ((Coordinates[i].X + Coordinates[i + 1].X) * ((Coordinates[i].X * Coordinates[i + 1].Y) - (Coordinates[i + 1].X * Coordinates[i].Y)));
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentOutOfRangeException) centroid = centroid + ((Coordinates[i].X + Coordinates[0].X) * ((Coordinates[i].X * Coordinates[0].Y) - (Coordinates[0].X * Coordinates[i].Y)));
                }
            }

            return (1 / (6 * area)) * centroid;
        }

        public double GetCentroidCoordinateY()
        {
            double centroid = 0.0;
            var area = GetArea();
            var coordCount = Coordinates.Count;

            for (int i = 0; i < coordCount; i++)
            {
                try
                {
                    centroid = centroid + ((Coordinates[i].Y + Coordinates[i + 1].Y) * ((Coordinates[i].X * Coordinates[i + 1].Y) - (Coordinates[i + 1].X * Coordinates[i].Y)));
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentOutOfRangeException) centroid = centroid + ((Coordinates[i].Y + Coordinates[0].Y) * ((Coordinates[i].X * Coordinates[0].Y) - (Coordinates[0].X * Coordinates[i].Y)));
                }
            }

            return (1 / (6 * area)) * centroid;
        }

        public CartesianCoordinate GetCoordinate(double x, double y)
        {
            return Coordinates.Find(c => { return c.X == x && c.Y == y; });
        }

        /// <summary>
        /// Ensure coordinates are sorted in a counter-clockwise position
        /// </summary>
        private void SortCoordinates()
        {
            var angles = new Dictionary<double,CartesianCoordinate>();

            if (Coordinates.Count > 0)
            {
                foreach (var coordinate in Coordinates)
                {
                    angles.Add(coordinate.ToVector().Angle,coordinate);
                }

                angles.Values.ToList();

                var list = angles.Keys.ToList();
                list.Sort();

                Coordinates.Clear();

                foreach (var angle in list)
                {
                    Coordinates.Add(angles[angle]);
                }
            }
        }

        /// <summary>
        /// Determines whether a point is inside this polygonal area.
        /// If the point is at an edge or a vertex it is considered to be
        /// inside the area. It uses the algorithm documented by Paul Bourke.
        /// <see href="http://web.archive.org/web/20080812141848/http://local.wasp.uwa.edu.au/~pbourke/geometry/insidepoly/"/>
        /// </summary>
        /// <param name="coordinate">The <see cref="CartesianCoordinate"/> that will be checked.</param>
        /// <returns></returns>
        public bool CheckIfCoordinateIsInArea(CartesianCoordinate coordinate)
        {
            var isInside = false;
            int counter = 0;
            int i = 1;
            int n = Coordinates.Count;
            double xinters;
            CartesianCoordinate p1, p2;

            p1 = Coordinates[0];

            foreach (var point in Coordinates)
            {
                p2 = Coordinates[i % n];

                if (coordinate.Y > Math.Min(p1.Y,p2.Y))
                {
                    if(coordinate.Y <= Math.Max(p1.Y,p2.Y))
                    {
                        if(coordinate.X <= Math.Max(p1.X,p2.X))
                        {
                            if(p1.Y != p2.Y)
                            {
                                xinters = (coordinate.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;
                                if (p1.X == p2.X || coordinate.X <= xinters) counter++;
                            }
                        }
                    }
                }

                p1 = p2;

                i++;
            }

            if (counter % 2 != 0) isInside = true;

            return isInside;
        }
    }
}