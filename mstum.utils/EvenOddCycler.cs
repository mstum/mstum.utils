#region License
/* The MIT License (MIT)
 * Copyright (c) 2011 Michael Stum, http://www.Stum.de <opensource@stum.de>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion
namespace mstum.utils
{
    /// <summary>
    /// A simple Even/Odd Class cycler
    /// </summary>
    /// <remarks>
    /// http://www.stum.de/2010/01/22/a-simple-evenodd-cycler-for-net/
    /// </remarks>
    public class EvenOddCycler
    {
        private readonly string _oddClassName;
        private readonly string _evenClassName;
        private int _numCycles;

        public EvenOddCycler() : this("even", "odd") { }

        public EvenOddCycler(string evenClassName, string oddClassName)
        {
            _evenClassName = evenClassName;
            _oddClassName = oddClassName;
            _numCycles = 0;
        }

        /// <summary>
        /// Toggle the class and return the class name
        /// </summary>
        /// <returns></returns>
        public string Cycle()
        {
            _numCycles++;
            return (_numCycles % 2 == 0) ? _evenClassName : _oddClassName;
        }

        /// <summary>
        /// Reset the Cycler
        /// </summary>
        /// <remarks>
        /// To reuse the Cycler on multiple tables, reset it so that Cycle returns the odd class for it's first iteration again
        /// </remarks>
        public void Reset()
        {
            _numCycles = 0;
        }
    }
}