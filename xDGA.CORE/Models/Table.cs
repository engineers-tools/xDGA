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
using System.Collections.Generic;
using System.Linq;

namespace xDGA.CORE.Models
{
    public class Table
    {
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Row> Rows { get; set; } = new List<Row>();
        public List<Cell> Cells { get; set; } = new List<Cell>();

        public Table() { }
        
        public Table(int rows, int columns)
        {
            for (int i = 1; i <= rows; i++)
            {
                Rows.Add(new Row() { Index = i, Name = string.Empty });
            }

            for (int i = 1; i <= columns; i++)
            {
                Columns.Add(new Column() { Index = i, Name = string.Empty });
            }
        }

        public object this[int row,int column]
        {
            get => _GetCell(row,column)?.Value;

            set
            {
                if(row > Rows.Count)
                {
                    for (int i = Rows.Count + 1; i <= row; i++)
                    {
                        Rows.Add(new Row() { Index = i, Name = string.Empty });
                    }
                }

                if (column > Columns.Count)
                {
                    for (int i = Columns.Count + 1; i <= column; i++)
                    {
                        Columns.Add(new Column() { Index = i, Name = string.Empty });
                    }
                }

                var cell = _GetCell(row, column);
                if(cell == null)
                {
                    cell = new Cell() { Row = row, Column = column, Value = value };
                    Cells.Add(cell);
                }
                else
                {
                    Cells[Cells.IndexOf(cell)].Value = value;
                }
            }
        }

        private Cell _GetCell(int row, int column)
        {
            return Cells.Where(c => c.Row == row && c.Column == column).FirstOrDefault();
        }
    }
}
