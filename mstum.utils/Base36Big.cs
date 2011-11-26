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
using System.Numerics;

namespace mstum.utils
{
    /// <summary>
    /// A Base36 De- and Encoder for big numbers
    /// </summary>
    public static class Base36Big
    {
        private const string CharList = "0123456789abcdefghijklmnopqrstuvwxyz";
        private static char[] CharArray = CharList.ToCharArray();

        /// <summary>
        /// Decode the Base36 Encoded string into a number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static BigInteger Decode(string input)
        {
            var reversed = input.ToLower().Reverse();
            BigInteger result = BigInteger.Zero;
            int pos = 0;
            foreach (char c in reversed)
            {
                result = BigInteger.Add(result, BigInteger.Multiply(CharList.IndexOf(c), BigInteger.Pow(36, pos)));
                pos++;
            }
            return result;
        }

        /// <summary>
        /// Encode the given number into a Base36 string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String Encode(BigInteger input)
        {
            if (input.Sign < 0) throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");

            var result = new Stack<char>();
            while (!input.IsZero)
            {
                var index = (int)(input % 36);
                result.Push(CharList[index]);
                input = BigInteger.Divide(input, 36);
            }
            return new string(result.ToArray());
        }
    }
}
