namespace ISchemm.UTF32

open ISchemm.UTF32
open System.Collections
open System.Collections.Generic

[<Struct>]
type String32 = {
    List: Char32 list
} with
    member this.Substring (startIndex: int) =
        { List = this.List.[startIndex..] }

    member this.Substring (startIndex: int, length: int) =
        { List = this.List.[startIndex..startIndex+length-1] }

    static member FromEnumerable (src: seq<Char32>) =
        { List = List.ofSeq src }

    static member FromString (str: string) =
        str
        |> Char32Array.FromString
        |> String32.FromEnumerable

    override this.ToString () =
        Array.ofList this.List
        |> Char32Array.GetString

    interface seq<Char32> with
        member this.GetEnumerator(): IEnumerator =
            (this.List :> System.Collections.IEnumerable).GetEnumerator()
        member this.GetEnumerator(): IEnumerator<Char32> = 
            (this.List :> seq<Char32>).GetEnumerator()
