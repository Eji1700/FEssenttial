﻿open System
open System.IO

let readFile path =
    try
        seq {
            use reader = new StreamReader(File.OpenRead(path))
            while not reader.EndOfStream do
                reader.ReadLine()
        } 
        |> Ok
    with
    | ex -> Error ex

let import path =
    match path |> readFile with
    | Ok data -> data |> Seq.iter(fun x -> printfn "%A" x)
    | Error ex -> printfn "Error: %A" ex.Message

[<EntryPoint>]
let main argv =
    let path = Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")
    |> import
    0