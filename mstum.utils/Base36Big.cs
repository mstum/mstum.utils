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
