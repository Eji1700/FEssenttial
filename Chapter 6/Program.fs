open System
open System.IO

type Customer = {
    CustomerId : string
    Email : string
    IsEligible : string
    IsRegistered : string
    DateRegistered : string
    Discount : string
}

let parse (data:string seq) =
    data
    |> Seq.skip 1
    |> Seq.map( fun line ->
        match line.Split('|') with 
        | [| customerId; email; eligible; registered; dateRegistered; discount |] ->
            Some {
                CustomerId = customerId
                Email = email
                IsEligible = eligible
                IsRegistered = registered
                DateRegistered = dateRegistered
                Discount = discount
            }
        | _ -> None
    )
    |> Seq.choose id

let readFile path =
    try
        File.ReadLines(path)
        |> Ok
    with
    | ex -> Error ex

let import path =
    match path |> readFile with
    | Ok data -> data |> parse |> Seq.iter(fun x -> printfn "%A" x)
    | Error ex -> printfn "Error: %A" ex.Message

[<EntryPoint>]
let main argv =
    Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")
    |> import
    0