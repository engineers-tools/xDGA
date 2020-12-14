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
using xDGA.CORE.Interfaces;

namespace xDGA.CORE.Algorithms
{
    public abstract class AbstractAlgorithm : IAlgorithm
    {
        public abstract string Version { get; }

        private List<IOutput> _Outputs;
        public List<IOutput> Outputs
        {
            get
            {
                if (_Outputs == null) _Outputs = new List<IOutput>();
                return _Outputs;
            }

            set
            {
                _Outputs = value;
            }
        }

        private List<IRule> _Rules;
        public List<IRule> Rules
        {
            get
            {
                if (_Rules == null) _Rules = new List<IRule>();
                return _Rules;
            }
        }

        public abstract void Execute();
    }
}
