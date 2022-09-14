open System
open System.IO

let readFile path =
    try
        File.ReadLines(path)
        |> Ok
    with
    | ex -> Error ex

let import path =
    match path |> readFile with
    | Ok data -> data |> Seq.iter(fun x -> printfn "%A" x)
    | Error ex -> printfn "Error: %A" ex.Message

[<EntryPoint>]
let main argv =
    Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")
    |> import
    0