/*
Copyright (c) 2017 Maicon Feldhaus

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do 
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial 
portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES 
OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN 
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using UnityEngine;
using System.Collections.Generic;

namespace Lune.Utils
{
    public static class CollectionsExtensions
    {
        /// <summary>
        /// Shuffles the element order of the specified IList.
        /// </summary>
        public static void Shuffle<T>(this IList<T> source)
        {
            int count = source.Count;
            int last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                int r = Random.Range(i, count);
                T tmp = source[i];
                source[i] = source[r];
                source[r] = tmp;
            }
        }

        /// <summary>
        /// Creates a list containing arithmetic progressions.
        /// </summary>
        /// <param name="start">Starting number of the sequence.</param>
        /// <param name="stop">Generate numbers up to, but not including this number.</param>
        /// <param name="step">Difference between each number in the sequence.</param>
        /// <returns></returns>
        public static IList<int> IntRange(int start, int stop, int step = 1)
        {
            int len = Mathf.CeilToInt((stop - start) / (float)step);
            int[] array = new int[len];
            for (int i = 0; i < len; i++)
            {
                array[i] = start + i * step;
            }
            return array;
        }

        /// <summary>
        /// Creates a list containing arithmetic progressions.
        /// </summary>
        /// <param name="stop">Number of integers (whole numbers) to generate, starting from zero.</param>
        /// <returns></returns>
        public static IList<int> IntRange(int stop)
        {
            return IntRange(0, stop);
        }

        /// <summary>
        /// Slice a list and returns a new list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source list</param>
        /// <param name="start">Starting number for slice.</param>
        /// <param name="stop">Slice to, but not including this number.</param>
        /// <returns></returns>
        public static T[] Slice<T>(this IList<T> source, int start, int stop)
        {
            T[] result = new T[stop];
            for (int i = 0; i < stop; i++)
            {
                result[i] = source[start + i];
            }
            return result;
        }

        /// <summary>
        /// Returns a random value from the source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source list</param>
        /// <returns></returns>
        public static T GetRandomItem<T>(this IList<T> source)
        {
            return source[Random.Range(0, source.Count)];
        }
    }
}