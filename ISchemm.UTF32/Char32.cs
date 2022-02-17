using System;
using System.Text;

namespace ISchemm.UTF32
{
    public struct Char32 : IEquatable<Char32>, IComparable<Char32>
    {
        public static readonly UTF32Encoding Encoding = new UTF32Encoding(bigEndian: !BitConverter.IsLittleEndian, byteOrderMark: false);

        public int Value;

        public int CompareTo(Char32 other)
        {
            return Value.CompareTo(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Char32 @char && Equals(@char);
        }

        public bool Equals(Char32 other)
        {
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Char32 left, Char32 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Char32 left, Char32 right)
        {
            return !(left == right);
        }

        public static bool operator <(Char32 left, Char32 right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Char32 left, Char32 right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Char32 left, Char32 right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(Char32 left, Char32 right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static Char32 FromArray(byte[] array, int index)
        {
            if (index + sizeof(int) > array.Length)
                throw new FormatException("Index out of bounds when converting to Char32");

            return new Char32 { Value = BitConverter.ToInt32(array, index) };
        }

        public byte[] ToArray()
        {
            return BitConverter.GetBytes(Value);
        }

        public static Char32 FromString(string str)
        {
            byte[] array = Encoding.GetBytes(str);
            if (array.Length > 4)
                throw new FormatException("Cannot convert a string with more than one codepoint to Char32");

            return FromArray(array, 0);
        }

        public override string ToString()
        {
            return Encoding.GetString(ToArray());
        }
    }
}
