﻿open System
open System.IO
open System.Text.RegularExpressions
open FsToolkit.ErrorHandling.ValidationCE

type Customer = {
    CustomerId : string
    Email : string
    IsEligible : string
    IsRegistered : string
    DateRegistered : string
    Discount : string
}

type ValidatedCustomer = {
    CustomerId: string
    Email: string option
    IsEligible: bool
    IsRegistered: bool
    DateRegistered: DateTime option
    Discount: decimal option
}

let create customerId email isEligible isRegistered dateRegistered discount =
    {
        CustomerId = customerId
        Email = email
        IsEligible = isEligible
        IsRegistered = isRegistered
        DateRegistered = dateRegistered
        Discount = discount }

type ValidationError =
    | MissingData of name: string 
    | InvalidData of name: string * value: string

let (|ParseRegex|_|) regex str =
    let m = Regex(regex).Match(str)
    if m.Success then Some (List.tail [ for x in m.Groups -> x.Value ])
    else None

let (|IsValidEmail|_|) input =
    match input with 
    | ParseRegex ".*?@(.*)" [ _ ] -> Some input
    | _ -> None

let (|IsEmptyString|_|) (input:string) =
    if input.Trim() = "" then Some () else None

let (|IsDecimal|_|)(input:string) =
    let success, value = Decimal.TryParse input
    if success then Some value else None

let (|IsBoolean|_|) (input:string) =
    match input with 
    | "1" -> Some true
    | "0" -> Some false
    | _ -> None

let (|IsValidDate|_|)(input:string) =
    let (success, value) = input |> DateTime.TryParse
    if success then Some value else None

let validateCustomerId customerId =
    if customerId <> "" then Ok customerId
    else Error (MissingData "CustomerId")

let validateEmail email =
    if email <> "" then
        match email with 
        | IsValidEmail _ -> Ok (Some email)
        | _ -> Error (InvalidData("Email", email))
    else 
        Ok None

let validateIsEligible (isEligible:string)=
    match isEligible with 
    | IsBoolean b -> Ok b
    | _ -> Error (InvalidData ("IsEligible", isEligible))

let validateIsRegistered (isRegistered:string)=
    match isRegistered with
    | IsBoolean b -> Ok b
    | _ -> Error (InvalidData ("IsRegistered", isRegistered))

let validateDateRegistered (dateRegistered:string)=
    match dateRegistered with 
    | IsEmptyString -> Ok None
    | IsValidDate dt -> Ok (Some dt)
    | _ -> Error (InvalidData ("DateRegistered", dateRegistered))

let validateDiscount discount =
    match discount with 
    | IsEmptyString -> Ok None
    | IsDecimal value -> Ok (Some value)
    | _ -> Error (InvalidData ("Discount", discount))

let getError input =
    match input with
    | Ok _ -> []
    | Error ex -> [ ex ]

let getValue input =
    match input with 
    | Ok v -> v 
    | _ -> failwith "Oops, you shouldn't have got here!"

let validate (input:Customer) : Result<ValidatedCustomer, ValidationError list> =
    validation {
        let! customerId = 
            input.CustomerId 
            |> validateCustomerId
            |> Result.mapError List.singleton
        and! email = 
            input.Email 
            |> validateEmail
            |> Result.mapError List.singleton
        and! isEligible = 
            input.IsEligible 
            |> validateIsEligible
            |> Result.mapError List.singleton
        and! isRegistered = 
            input.IsRegistered 
            |> validateIsRegistered
            |> Result.mapError List.singleton
        and! dateRegistered = 
            input.DateRegistered 
            |> validateDateRegistered
            |> Result.mapError List.singleton
        and! discount = 
            input.Discount 
            |> validateDiscount
            |> Result.mapError List.singleton
        return create customerId email isEligible isRegistered dateRegistered discount    
    }
type DataReader = string -> Result<string seq,exn>
let readFile : DataReader =
    fun path ->
    try
        seq {
            use reader = new StreamReader(File.OpenRead(path))
            while not reader.EndOfStream do
            reader.ReadLine()
        }
        |> Ok
    with
    | ex -> Error ex

let parseLine (line:string) : Customer option =
    match line.Split('|') with
    |   [|  customerId
            email
            eligible
            registered
            dateRegistered
            discount 
        |] ->
        Some {
            CustomerId = customerId
            Email = email
            IsEligible = eligible
            IsRegistered = registered
            DateRegistered = dateRegistered
            Discount = discount
        }
    | _ -> None

let parse (data:string seq) =
    data
    |> Seq.skip 1
    |> Seq.map parseLine
    |> Seq.choose id
    |> Seq.map validate

let output data =
    data
    |> Seq.iter (fun x -> printfn "%A" x)

let import (dataReader:DataReader) path =
    match path |> dataReader with
    | Ok data -> data |> parse |> output
    | Error ex -> printfn "Error: %A" ex.Message

[<EntryPoint>]
let main argv =
    Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")
    |> import readFile
    0