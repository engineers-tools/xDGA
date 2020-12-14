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

namespace xDGA.TEST
{
    [TestClass]
    public class TableTests
    {
        [TestMethod]
        public void CanCreateTableWithCorrectNumberOfRowsAndColumns()
        {
            var t = new Table(4,5);

            Assert.AreEqual(4, t.Rows.Count);
            Assert.AreEqual(5, t.Columns.Count);
        }

        [TestMethod]
        public void CanAddArbitraryRowsAndColumnsToTable()
        {
            var t = new Table();
            t[3, 5] = "some value";
            t[2, 4] = 14;

            Assert.AreEqual(3, t.Rows.Count);
            Assert.AreEqual(5, t.Columns.Count);
        }

        [TestMethod]
        public void CorrectValuesAreExtractedFromTableCells()
        {
            var v1 = "some value";
            var v2 = 14;

            var t = new Table();
            t[3, 5] = v1;
            t[2, 4] = v2;

            Assert.AreEqual(v1, t[3,5]);
            Assert.AreEqual(v2, t[2,4]);
        }
    }
}
