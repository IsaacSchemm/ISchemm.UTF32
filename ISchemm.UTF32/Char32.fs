namespace ISchemm.UTF32

open System
open System.Text

[<Struct>]
type Char32 = {
    Value: int32
} with
    static member Encoding = new UTF32Encoding(not BitConverter.IsLittleEndian, false)

    static member FromArray (array: byte[], index: int) =
        if index + sizeof<int> > array.Length then
            raise (FormatException "Index out of bounds when converting to Char32")

        { Value = BitConverter.ToInt32 (array, index) }

    member this.ToArray () =
        BitConverter.GetBytes this.Value

    static member FromString (str: string) =
        let array = Char32.Encoding.GetBytes str
        if array.Length > 4 then
            raise (FormatException "Cannot convert a string with more than one codepoint to Char32")

        Char32.FromArray (array, 0)

    override this.ToString () =
        let array = this.ToArray ()
        Char32.Encoding.GetString array
