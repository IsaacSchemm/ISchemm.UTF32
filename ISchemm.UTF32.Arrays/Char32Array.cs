using System;
using System.Runtime.InteropServices;

namespace ISchemm.UTF32.Arrays
{
    public static class Char32Array
    {
        public unsafe static Char32[] FromByteArray(byte[] array)
        {
            if (array.Length % sizeof(Char32) != 0)
                throw new FormatException($"Length of array must be divisible by {sizeof(Char32)}");

            Char32[] destArray = new Char32[array.Length / sizeof(Char32)];
            fixed (Char32* dest = destArray) {
                Marshal.Copy(array, 0, (IntPtr)dest, array.Length);
            }
            return destArray;
        }

        public static Char32[] FromString(string str)
        {
            return FromByteArray(Char32.Encoding.GetBytes(str));
        }

        public unsafe static byte[] GetByteArray(this Char32[] array)
        {
            byte[] destArray = new byte[array.Length * sizeof(Char32)];
            fixed (Char32* src = array)
            {
                Marshal.Copy(new IntPtr(src), destArray, 0, destArray.Length);
            }
            return destArray;
        }

        public static string GetString(this Char32[] array)
        {
            return Char32.Encoding.GetString(GetByteArray(array));
        }
    }
}
