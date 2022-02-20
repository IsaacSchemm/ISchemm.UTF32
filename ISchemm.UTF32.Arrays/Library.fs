namespace ISchemm.UTF32.Arrays

open System
open ISchemm.UTF32

module Char32Array =
    let FromByteArray (array: byte[]): Char32[] =
        [|
            if array.Length % sizeof<Char32> <> 0 then
                raise (FormatException $"Length of array must be divisible by {sizeof<Char32>}")

            for i in 0 .. array.Length - 1 do
                if i % 4 = 0 then
                    yield { Value = BitConverter.ToInt32 (array, i) }
        |]

    let FromString (str: string): Char32[] =
        str
        |> Char32.Encoding.GetBytes
        |> FromByteArray

    let GetByteArray (array: Char32[]): byte[] =
        [|
            for x in array do
                yield! BitConverter.GetBytes x.Value
        |]

    let GetString (array: Char32[]): string =
        array
        |> GetByteArray
        |> Char32.Encoding.GetString
