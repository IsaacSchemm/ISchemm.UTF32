namespace ISchemm.UTF32

open ISchemm.UTF32
open System.Collections
open System.Collections.Generic

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