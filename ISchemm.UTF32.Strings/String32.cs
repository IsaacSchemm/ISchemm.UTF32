using ISchemm.UTF32.Arrays;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ISchemm.UTF32.Strings
{
    public struct String32 : IEnumerable<Char32>, IEquatable<String32>
    {
        private readonly Char32[] _array;

        public String32(Char32[] array)
        {
            _array = array ?? throw new ArgumentNullException(nameof(array));
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (Char32 element in _array)
            {
                hash = hash * 31 + element.GetHashCode();
            }
            return hash;
        }

        public override bool Equals(object obj)
        {
            return obj is String32 str && Equals(str);
        }

        public bool Equals(String32 other)
        {
            return _array.SequenceEqual(other._array);
        }

        public String32 Substring(int startIndex)
        {
            return new String32(_array.Skip(startIndex).ToArray());
        }

        public String32 Substring(int startIndex, int length)
        {
            return new String32(_array.Skip(startIndex).Take(length).ToArray());
        }

        public static String32 FromEnumerable(IEnumerable<Char32> src)
        {
            return new String32(src.ToArray());
        }

        public static String32 FromString(string str)
        {
            return FromEnumerable(Char32Array.FromString(str));
        }

        public override string ToString()
        {
            return Char32Array.GetString(_array);
        }

        public IEnumerator<Char32> GetEnumerator()
        {
            return ((IEnumerable<Char32>)_array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _array.GetEnumerator();
        }
    }
}
