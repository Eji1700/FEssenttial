open System

let tryParseDateTime (input:string) =
    match DateTime.TryParse input with 
    | true, result -> Some result
    | false, _ -> None

let isDate = tryParseDateTime "2019-08-01"
let isNotDate = tryParseDateTime "Hello"

let nullObj:string = null
let nullPri = Nullable<int>()
let fromNullObj = Option.ofObj nullObj
let fromNullPri = Option.ofNullable nullPri
let toNullObj = Option.toObj fromNullObj
let toNullPri = Option.toNullable fromNullPri

let resultFP = fromNullObj |> Option.defaultValue "-----"
let setUnknownAsDefault = Option.defaultValue "????"
let result = setUnknownAsDefault fromNullObj