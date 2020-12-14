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

using xDGA.CORE.Models;

namespace xDGA.CORE.Algorithms
{
    public class DuvalPentagonTwoRule : AbstractDuvalPentagonRule
    {
        public DuvalPentagonTwoRule() : base("Duval Pentagon 2", Gas.Hydrogen, Gas.Ethane, Gas.Methane, Gas.Ethylene, Gas.Acetylene)
        {
            Pentagon.Areas.Add(new Area(FailureType.Code.PD)
            {
                Coordinates =
                {
                    new CartesianCoordinate(0.0, 33.0),
                    new CartesianCoordinate(-1.0, 33.0),
                    new CartesianCoordinate(-1.0, 24.5),
                    new CartesianCoordinate(0.0, 24.5)
                }
            });

            Pentagon.Areas.Add(new Area(FailureType.Code.D1)
            {
                Coordinates =
                {
                    new CartesianCoordinate(0.0, 40.0),
                    new CartesianCoordinate(38.0, 12.4),
                    new CartesianCoordinate(32.0, -6.0),
                    new CartesianCoordinate(4.0, 16.0),
                    new CartesianCoordinate(0.0, 1.5)
                }
            });

            Pentagon.Areas.Add(new Area(FailureType.Code.D2)
            {
                Coordinates =
                {
                    new CartesianCoordinate(4.0, 16.0),
                    new CartesianCoordinate(32.0, -6.0),
                    new CartesianCoordinate(24.0, -30.0),
                    new CartesianCoordinate(-1.0, -2.0)
                }
            });

            Pentagon.Areas.Add(new Area(FailureType.Code.S)
            {
                Coordinates =
                {
                    new CartesianCoordinate(0.0, 1.5),
                    new CartesianCoordinate(0.0, 24.5),
                    new CartesianCoordinate(-1.0, 24.5),
                    new CartesianCoordinate(-1.0, 33.0),
                    new CartesianCoordinate(0.0, 33.0),
                    new CartesianCoordinate(0.0, 40.0),
                    new CartesianCoordinate(-38.0, 12.4),
                    new CartesianCoordinate(-35.0, 3.0)
                }
            });

            Pentagon.Areas.Add(new Area(FailureType.Code.T3_H)
            {
                Coordinates =
                {
                    new CartesianCoordinate(-1.0, -2.0),
                    new CartesianCoordinate(-3.5, -3.0),
                    new CartesianCoordinate(2.5, -32.4),
                    new CartesianCoordinate(23.3, -32.4),
                    new CartesianCoordinate(24.0, -30.0)
                }
            });

            Pentagon.Areas.Add(new Area(FailureType.Code.C)
            {
                Coordinates =
                {
                    new CartesianCoordinate(2.5, -32.4),
                    new CartesianCoordinate(-3.5, -3.0),
                    new CartesianCoordinate(-11.0, -8.0),
                    new CartesianCoordinate(-21.5, -32.4)
                }
            });

            Pentagon.Areas.Add(new Area(FailureType.Code.O)
            {
                Coordinates =
                {
                    new CartesianCoordinate(-21.5, -32.4),
                    new CartesianCoordinate(-11.0, -8.0),
                    new CartesianCoordinate(-3.5, -3.0),
                    new CartesianCoordinate(-1.0, -2.0),
                    new CartesianCoordinate(0.0, 1.5),
                    new CartesianCoordinate(-35.0, 3.0),
                    new CartesianCoordinate(-23.3, -32.4)
                }
            });
        }
    }
}
