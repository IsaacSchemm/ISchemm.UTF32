namespace ISchemm.UTF32

open System
open System.Collections
open System.Collections.Generic
open System.Runtime.InteropServices
open System.Text
open Microsoft.FSharp.NativeInterop

[<Struct>]
type Char32 = {
    Value: int32
} with
    static member Encoding = new UTF32Encoding(not BitConverter.IsLittleEndian, false)

    override this.ToString () =
        this.Value
        |> BitConverter.GetBytes
        |> Char32.Encoding.GetString

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

[<Struct>]
type String32 = {
    List: Char32 list
} with
    static member FromList (list: Char32 list) = { List = list }

    static member FromEnumerable (src: seq<Char32>) =
        src
        |> List.ofSeq
        |> String32.FromList

    static member ToEnumerable (src: String32) =
        src :> seq<Char32>

    static member FromString (str: string) =
        str
        |> Char32Array.FromString
        |> String32.FromEnumerable

    static member ToString (src: String32) =
        src.List
        |> Array.ofList
        |> Char32Array.GetString

    static member Empty = { List = [] }

    member this.Length = List.length this.List
    member this.Item index = this.List.[index]

    member this.GetSlice (startIndex, endIndex) =
        this.List.GetSlice (startIndex, endIndex)
        |> String32.FromList

    member this.Substring (startIndex) = this.[startIndex..]
    member this.Substring (startIndex, length) = this.[startIndex..startIndex + length - 1]

    override this.ToString () = String32.ToString this

    interface IReadOnlyList<Char32> with
        member this.Count: int = this.Length
        member this.GetEnumerator(): IEnumerator = (this.List :> IEnumerable).GetEnumerator()
        member this.GetEnumerator(): IEnumerator<Char32> = (this.List :> seq<Char32>).GetEnumerator()
        member this.Item with get (index: int): Char32 = this.List.[index]

module String32Replacement =
    type ISegment =
        abstract member StartIndex: int
        abstract member EndIndex: int
        abstract member ReplacementValue: String32

    let private Omit a b = {
        new ISegment with
            member __.StartIndex = a
            member __.EndIndex = b
            member __.ReplacementValue = String32.Empty
    }

    let DisplayRange (displayStartIndex: int, displayEndIndex: int) = seq {
        Omit 0 displayStartIndex
        Omit displayEndIndex System.Int32.MaxValue
    }

    let rec private Apply (remaining: ISegment list) (str: String32) = seq {
        match remaining with
        | [] ->
            yield str
        | e::tail ->
            let startIndex = max 0 e.StartIndex
            let endIndex = min str.Length e.EndIndex

            yield str.[endIndex ..]
            yield e.ReplacementValue
            yield! Apply tail str.[.. startIndex - 1]
    }

    let Replace (segments: seq<ISegment>) (str: String32) =
        str
        |> Apply
            (segments
            |> Seq.rev
            |> Seq.sortByDescending (fun x -> x.EndIndex)
            |> Seq.toList)
        |> Seq.rev
        |> Seq.collect id
        |> String32.FromEnumerable
