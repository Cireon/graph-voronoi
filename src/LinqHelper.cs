using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphVoronoi
{
    static class LinqHelper
    {
        static readonly Random random = new Random();

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> source)
            where T : class
        {
            return source.Where(arg => arg != null);
        }

        public static T MinBy<T, S>(this IEnumerable<T> source, Func<T, S> selector)
            where S : IComparable
        {
            return !source.Any() ? default(T) : source.Aggregate((a, b) => selector(a).CompareTo(selector(b)) < 0 ? a : b);
        }

        public static T MaxBy<T, S>(this IEnumerable<T> source, Func<T, S> selector)
            where S : IComparable
        {
            return !source.Any() ? default(T) : source.Aggregate((a, b) => selector(a).CompareTo(selector(b)) > 0 ? a : b);
        }

        public static T MinOrDefault<T>(this IEnumerable<T> source, T def = default(T))
        {
            return !source.Any() ? def : source.Min();
        }

        public static S MinOrDefault<T, S>(this IEnumerable<T> source, Func<T, S> selector, S def = default(S))
            where S : IComparable
        {
            return !source.Any() ? def : source.Min(selector);
        }

        public static T MaxOrDefault<T>(this IEnumerable<T> source, T def = default(T))
        {
            return !source.Any() ? def : source.Max();
        }

        public static S MaxOrDefault<T, S>(this IEnumerable<T> source, Func<T, S> selector, S def = default(S))
            where S : IComparable
        {
            return !source.Any() ? def : source.Max(selector);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> target, T item)
        {
            foreach (T t in target) yield return t;
            yield return item;
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> me,
            IEnumerable<KeyValuePair<TKey, TValue>> other)
        {
            foreach (var pair in other)
            {
                me.Add(pair);
            }
        }

        public static IEnumerable<T> Yield<T>(this T obj)
        {
            yield return obj;
        }


        public static T RandomElementOrDefault<T>(this IEnumerable<T> input)
        {
            return input.Any() ? input.RandomElement() : default(T);
        }

        /// <summary>
        /// Selects a random element from a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="input">The sequance to choose a random element from.</param>
        /// <returns>A random element from the input.</returns>
        public static T RandomElement<T>(this IEnumerable<T> input)
        {
            // optimisation for lists
            var asList = input as IList<T>;
            if (asList != null)
            {
                if (asList.Count == 0)
                    throw new InvalidOperationException("Sequence was empty");
                return asList[random.Next(asList.Count)];
            }

            T current = default(T);
            int count = 0;
            foreach (T element in input)
            {
                count++;
                if (random.Next(count) == 0)
                {
                    current = element;
                }
            }
            if (count == 0)
            {
                throw new InvalidOperationException("Sequence was empty");
            }
            return current;
        }

        /// <summary>
        /// Efficiently (O(n) with n the size of the input) selects a random number of elements from an enumerable.
        /// Each element has an equal chance to be contained in the result. The order of the output is arbitrary.
        /// </summary>
        /// <typeparam name="T">Type of the elements.</typeparam>
        /// <param name="input">The enumerable that random elements are selected from.</param>
        /// <param name="count">The number of random elements to return. If this is smaller than the inputs size, the entire input is returned.</param>
        /// <returns>Random elements from the input.</returns>
        public static IEnumerable<T> SelectRandom<T>(this IEnumerable<T> input, int count)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (count <= 0)
                return Enumerable.Empty<T>();

            // optimisation for collections
            var asCollection = input as ICollection<T>;
            if (asCollection != null && count >= asCollection.Count)
                // if we take as much or more than we have, return input
                return input;

            // we could use a List<T> as well, but an array should
            // be slightly faster, for as long as count <= input.Count()
            var set = new T[count];

            int i = 0;
            foreach (var item in input)
            {
                // copy first 'count' elements
                if (i < count)
                    set[i] = item;
                else
                {
                    // add others with decreasing likelyhood
                    int index = random.Next(i + 1);
                    if (index < count)
                        set[index] = item;
                }
                i++;
            }

            // if the input was smaller than 'count', resize output
            if (i < count)
                Array.Resize(ref set, i);

            return set;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int c = list.Count;
            for (int i = 0; i < c; i++)
            {
                int j = random.Next(c);

                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}
