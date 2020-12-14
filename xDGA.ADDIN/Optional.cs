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

namespace xDGA.ADDIN
{
    /// <summary>
    /// This is a helper class that is used to deal with missing or optional
    /// Excel function parameters.
    /// </summary>
    public static class Optional
    {
        internal static string Check(object arg, string defaultValue)
        {
            if (arg is string)
                return (string)arg;
            else if (arg is ExcelMissing || arg is ExcelEmpty || arg is ExcelError)
                return defaultValue;
            else
                return arg.ToString();
        }

        internal static DateTime Check(object arg, DateTime defaultDate)
        {
            if (arg is double)
                return DateTime.FromOADate((double)arg);
            else if (arg is string)
                return DateTime.Parse((string)arg);
            else if (arg is ExcelMissing || arg is ExcelEmpty)
                return defaultDate;
            else
                throw new ArgumentException();
        }
    }
}
