#region License
/* The MIT License (MIT)
 * Copyright (c) 2019 Michael Stum, http://www.Stum.de <opensource@stum.de>
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

namespace mstum.utils
{
    /// <summary>
    /// The Minimum and Maximum value of something
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Extent<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }
        public Extent()
        {
        }

        public Extent(T min, T max)
        {
            Min = min;
            Max = max;
        }
    }

    public static class ExtentExtensionMethods
    {
        public static Extent<TResult> Extent<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<TSource> Extent<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var comparer = Comparer<TSource>.Default;
            var minValue = default(TSource);
            var maxValue = default(TSource);
            if (minValue == null)
            {
                foreach (var x in source)
                {
                    if (x != null && (maxValue == null || comparer.Compare(x, maxValue) > 0))
                    {
                        maxValue = x;
                    }

                    if (x != null && (minValue == null || comparer.Compare(x, minValue) < 0))
                    {
                        minValue = x;
                    }

                }
                return new Extent<TSource>(minValue, maxValue);
            }
            else
            {
                bool hasMinValue = false;
                bool hasMaxValue = false;
                foreach (var x in source)
                {
                    if (hasMinValue)
                    {
                        if (comparer.Compare(x, minValue) < 0)
                        {
                            minValue = x;
                        }
                    }
                    else
                    {
                        minValue = x;
                        hasMinValue = true;
                    }

                    if (hasMaxValue)
                    {
                        if (comparer.Compare(x, maxValue) > 0)
                        {
                            maxValue = x;
                        }
                    }
                    else
                    {
                        maxValue = x;
                        hasMaxValue = true;
                    }
                }
                if (hasMinValue && hasMaxValue) return new Extent<TSource>(minValue, maxValue);
                throw new InvalidOperationException("Sequence contains no elements");
            }
        }

        // Non-Boxing Methods
        public static Extent<int> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<int?> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<long> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<long?> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<float> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<float?> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<double> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<double?> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<decimal> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<decimal?> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<DateTime> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, DateTime> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<DateTime?> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, DateTime?> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<TimeSpan> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<TimeSpan?> Extent<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan?> selector)
        {
            return Extent(source.Select(selector));
        }

        public static Extent<int> Extent(this IEnumerable<int> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            int minValue = 0;
            int maxValue = 0;
            bool hasMinValue = false;
            bool hasMaxValue = false;
            foreach (var x in source)
            {
                if (hasMinValue)
                {
                    if (x < minValue) minValue = x;
                }
                else
                {
                    minValue = x;
                    hasMinValue = true;
                }

                if (hasMaxValue)
                {
                    if (x > maxValue) maxValue = x;
                }
                else
                {
                    maxValue = x;
                    hasMaxValue = true;
                }
            }
            if (hasMinValue) return new Extent<int>(minValue, maxValue);
            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static Extent<int?> Extent(this IEnumerable<int?> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            int? minValue = null;
            int? maxValue = null;
            foreach (var x in source)
            {
                if (minValue == null || x < minValue)
                {
                    minValue = x;
                }

                if (maxValue == null || x > maxValue)
                {
                    maxValue = x;
                }
            }
            return new Extent<int?>(minValue, maxValue);
        }

        public static Extent<long> Extent(this IEnumerable<long> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            long minValue = 0;
            long maxValue = 0;
            bool hasMinValue = false;
            bool hasMaxValue = false;
            foreach (var x in source)
            {
                if (hasMinValue)
                {
                    if (x < minValue) minValue = x;
                }
                else
                {
                    minValue = x;
                    hasMinValue = true;
                }

                if (hasMaxValue)
                {
                    if (x > maxValue) maxValue = x;
                }
                else
                {
                    maxValue = x;
                    hasMaxValue = true;
                }
            }
            if (hasMinValue) return new Extent<long>(minValue, maxValue);
            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static Extent<long?> Extent(this IEnumerable<long?> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            long? minValue = null;
            long? maxValue = null;
            foreach (var x in source)
            {
                if (minValue == null || x < minValue)
                {
                    minValue = x;
                }

                if (maxValue == null || x > maxValue)
                {
                    maxValue = x;
                }
            }
            return new Extent<long?>(minValue, maxValue);
        }

        public static Extent<float> Extent(this IEnumerable<float> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            float minValue = 0;
            float maxValue = 0;
            bool hasMinValue = false;
            bool hasMaxValue = false;
            foreach (var x in source)
            {
                // Normally NaN < anything is false, as is anything < NaN
                // However, this leads to some irksome outcomes in Min and Max.
                // If we use those semantics then > Min(NaN, 5.0) is NaN, but
                // > Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                // ordering where NaN is smaller and bigger than every value,
                // including negative/positive infinity.

                if (hasMinValue)
                {
                    if (x < minValue || float.IsNaN(x)) minValue = x;
                }
                else
                {
                    minValue = x;
                    hasMinValue = true;
                }

                if (hasMaxValue)
                {
                    if (x > maxValue || float.IsNaN(x)) maxValue = x;
                }
                else
                {
                    maxValue = x;
                    hasMaxValue = true;
                }
            }
            if (hasMinValue) return new Extent<float>(minValue, maxValue);
            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static Extent<float?> Extent(this IEnumerable<float?> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            float? minValue = 0;
            float? maxValue = 0;
            foreach (var x in source)
            {
                if (x == null) continue;
                if (minValue == null || x < minValue || float.IsNaN(x.Value))
                {
                    minValue = x;
                }
                if (maxValue == null || x > maxValue || float.IsNaN(x.Value))
                {
                    maxValue = x;
                }
            }
            return new Extent<float?>(minValue, maxValue);
        }

        public static Extent<double> Extent(this IEnumerable<double> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            double minValue = 0;
            double maxValue = 0;
            bool hasMinValue = false;
            bool hasMaxValue = false;
            foreach (var x in source)
            {
                if (hasMinValue)
                {
                    if (x < minValue || double.IsNaN(x)) minValue = x;
                }
                else
                {
                    minValue = x;
                    hasMinValue = true;
                }

                if (hasMaxValue)
                {
                    if (x > maxValue || double.IsNaN(x)) maxValue = x;
                }
                else
                {
                    maxValue = x;
                    hasMaxValue = true;
                }
            }
            if (hasMinValue) return new Extent<double>(minValue, maxValue);
            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static Extent<double?> Extent(this IEnumerable<double?> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            double? minValue = 0;
            double? maxValue = 0;
            foreach (var x in source)
            {
                if (x == null) continue;
                if (minValue == null || x < minValue || double.IsNaN(x.Value))
                {
                    minValue = x;
                }
                if (maxValue == null || x > maxValue || double.IsNaN(x.Value))
                {
                    maxValue = x;
                }
            }
            return new Extent<double?>(minValue, maxValue);
        }

        public static Extent<decimal> Extent(this IEnumerable<decimal> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            decimal minValue = 0;
            decimal maxValue = 0;
            bool hasMinValue = false;
            bool hasMaxValue = false;
            foreach (var x in source)
            {
                if (hasMinValue)
                {
                    if (x < minValue) minValue = x;
                }
                else
                {
                    minValue = x;
                    hasMinValue = true;
                }

                if (hasMaxValue)
                {
                    if (x > maxValue) maxValue = x;
                }
                else
                {
                    maxValue = x;
                    hasMaxValue = true;
                }
            }
            if (hasMinValue) return new Extent<decimal>(minValue, maxValue);
            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static Extent<decimal?> Extent(this IEnumerable<decimal?> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            decimal? minValue = null;
            decimal? maxValue = null;
            foreach (var x in source)
            {
                if (minValue == null || x < minValue)
                {
                    minValue = x;
                }

                if (maxValue == null || x > maxValue)
                {
                    maxValue = x;
                }
            }
            return new Extent<decimal?>(minValue, maxValue);
        }

        public static Extent<DateTime> Extent(this IEnumerable<DateTime> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            DateTime minValue = DateTime.MinValue;
            DateTime maxValue = DateTime.MinValue;
            bool hasMinValue = false;
            bool hasMaxValue = false;
            foreach (var x in source)
            {
                if (hasMinValue)
                {
                    if (x < minValue) minValue = x;
                }
                else
                {
                    minValue = x;
                    hasMinValue = true;
                }

                if (hasMaxValue)
                {
                    if (x > maxValue) maxValue = x;
                }
                else
                {
                    maxValue = x;
                    hasMaxValue = true;
                }
            }
            if (hasMinValue) return new Extent<DateTime>(minValue, maxValue);
            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static Extent<DateTime?> Extent(this IEnumerable<DateTime?> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            DateTime? minValue = null;
            DateTime? maxValue = null;
            foreach (var x in source)
            {
                if (minValue == null || x < minValue)
                {
                    minValue = x;
                }

                if (maxValue == null || x > maxValue)
                {
                    maxValue = x;
                }
            }
            return new Extent<DateTime?>(minValue, maxValue);
        }
        public static Extent<TimeSpan> Extent(this IEnumerable<TimeSpan> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            TimeSpan minValue = TimeSpan.Zero;
            TimeSpan maxValue = TimeSpan.Zero;
            bool hasMinValue = false;
            bool hasMaxValue = false;
            foreach (var x in source)
            {
                if (hasMinValue)
                {
                    if (x < minValue) minValue = x;
                }
                else
                {
                    minValue = x;
                    hasMinValue = true;
                }

                if (hasMaxValue)
                {
                    if (x > maxValue) maxValue = x;
                }
                else
                {
                    maxValue = x;
                    hasMaxValue = true;
                }
            }
            if (hasMinValue) return new Extent<TimeSpan>(minValue, maxValue);
            throw new InvalidOperationException("Sequence contains no elements");
        }

        public static Extent<TimeSpan?> Extent(this IEnumerable<TimeSpan?> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            TimeSpan? minValue = null;
            TimeSpan? maxValue = null;
            foreach (var x in source)
            {
                if (minValue == null || x < minValue)
                {
                    minValue = x;
                }

                if (maxValue == null || x > maxValue)
                {
                    maxValue = x;
                }
            }
            return new Extent<TimeSpan?>(minValue, maxValue);
        }
    }
}
