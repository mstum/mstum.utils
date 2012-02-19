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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mstum.utils.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the nth element of a sequence
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Thrown if the sequence contains fewer than the desired element index</exception>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Nth<T>(this IEnumerable<T> input, int index)
        {
            try
            {
                return input.Skip(index - 1).First();
            }
            catch (InvalidOperationException)
            {
                throw new IndexOutOfRangeException("Sequence contains fewer than " + index + " elements.");
            }
        }

        /// <summary>
        /// Returns the nth element of a sequence, or the default for the sequence element
        /// </summary>
        /// <remarks>
        /// This does not throw an exception if the index is bigger than the element count, it returns the default value instead.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T NthOrDefault<T>(this IEnumerable<T> input, int index)
        {
            return input.Skip(index-1).FirstOrDefault();
        }
    }
}
