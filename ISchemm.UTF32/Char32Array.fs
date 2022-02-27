#nowarn "9"

namespace ISchemm.UTF32

open System
open System.Runtime.InteropServices
open Microsoft.FSharp.NativeInterop

module Char32Array =
    let FromByteArray (bArr: byte[]): Char32[] =
        if bArr.Length % sizeof<Char32> <> 0 then
            raise (FormatException $"Length of array must be divisible by {sizeof<Char32>}")

        if bArr.Length = 0 then
            [||]
        else
            let cArr = Array.zeroCreate<Char32> (bArr.Length / sizeof<Char32>)
            use cPtr = fixed cArr in Marshal.Copy (bArr, 0, NativePtr.toNativeInt cPtr, bArr.Length)
            cArr

    let FromString (str: string): Char32[] =
        str
        |> Char32.Encoding.GetBytes
        |> FromByteArray

    let GetByteArray (cArr: Char32[]): byte[] =
        if cArr.Length = 0 then
            [||]
        else
            let bArr = Array.zeroCreate<byte> (cArr.Length * sizeof<Char32>)
            use cPtr = fixed cArr in Marshal.Copy (NativePtr.toNativeInt cPtr, bArr, 0, bArr.Length)
            bArr

    let GetString (array: Char32[]): string =
        array
        |> GetByteArray
        |> Char32.Encoding.GetString
