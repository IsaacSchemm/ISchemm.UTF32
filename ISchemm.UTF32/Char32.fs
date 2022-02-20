namespace ISchemm.UTF32

open System
open System.Text

[<Struct>]
type Char32 = {
    Value: int32
} with
    static member Encoding = new UTF32Encoding(not BitConverter.IsLittleEndian, false)

    override this.ToString () =
        this.Value
        |> BitConverter.GetBytes
        |> Char32.Encoding.GetString
