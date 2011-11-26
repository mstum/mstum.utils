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
using System.Collections.Generic;
using System.Text;

namespace mstum.utils.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// A safe Substring method that does not throw an exception if startIndex or length is out of bounds of the string
        /// </summary>
        /// <remarks>
        /// http://www.stum.de/2009/02/27/a-safe-stringsubstring-extension-method/
        /// </remarks>
        /// <param name="input"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="suffix">An optional suffix to append if the string is truncated</param>
        /// <returns></returns>
        public static string SafeSubstring(this string input, int startIndex, int length, string suffix = "")
        {
            if (input.Length >= (startIndex + length))
            {
                return input.Substring(startIndex, length) + (suffix ?? string.Empty);
            }

            if (input.Length > startIndex)
            {
                return input.Substring(startIndex);
            }

            return string.Empty;
        }

        /// <summary>
        /// A shortcut for string.Format
        /// </summary>
        /// <remarks>
        /// http://www.stum.de/2009/08/20/turning-string-format-into-an-extension-method/
        /// </remarks>
        /// <param name="input"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Use(this string input, params object[] args)
        {
            if (input == null) return string.Empty;
            return string.Format(input, args);
        }

        /// <summary>
        /// If input is null or empty, return otherString
        /// </summary>
        /// <remarks>
        /// http://www.stum.de/2010/01/16/extension-method-return-another-string-if-string-is-null-or-empty/
        /// </remarks>
        /// <param name="input"></param>
        /// <param name="otherString"></param>
        /// <returns></returns>
        public static string IfEmpty(this string input, string otherString)
        {
            if (string.IsNullOrEmpty(input)) return otherString;
            return input;
        }

        /// <summary>
        /// Replace all occurences of a certain char with a string
        /// </summary>
        /// <remarks>
        /// http://www.stum.de/2010/02/16/an-extension-method-to-replace-multiple-chars-in-a-string/
        /// </remarks>
        /// <param name="input"></param>
        /// <param name="replacements">A dictionary where the key is a char to replace and the value is the string to replace it with. Value can be null.</param>
        /// <returns></returns>
        public static string ReplaceAll(this string input, IDictionary<char, string> replacements)
        {
            var sb = new StringBuilder(input.Length);
            foreach (char c in input)
            {
                if (!replacements.ContainsKey(c))
                {
                    sb.Append(c);
                }
                else
                {
                    var replacement = replacements[c];
                    if (!string.IsNullOrEmpty(replacement))
                    {
                        sb.Append(replacement);
                    }
                }
            }
            return sb.ToString();
        }
    }
}