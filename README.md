ISchemm.UTF32
=============

This is a small library for handling UTF-32 characters, character arrays, and
strings in .NET, implemented in less than 100 lines of F# code:

* `Char32` - a struct type, implemented by a single 32-bit integer in native byte order
* `Char32Array` - a set of functions to convert between `Char32` arrays and UTF-32 byte arrays, and between `Char32` arrays and native UTF-16 strings
* `String32` - an immutable type, implemented by an F# list of `Char32` structs; implements `IReadOnlyList<Char32>` and supports F# indexing and slicing

Use `String32.FromString` and `String32.ToString` to convert to and from the .NET string type.

See COPYING.txt for license details.
