type IFizzBuzz =
    abstract member Calculate : int -> string

type FizzBuzz(mapping) =
    let calculate n =
        mapping
        |> List.map (fun (v, s) -> if n % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s = "" then string n else s
    
    interface IFizzBuzz with 
        member _.Calculate(value) =
            calculate value 

let defaultMapping = [(3, "Fizz"); (5,"Buzz")]

let fizzBuzz = FizzBuzz(defaultMapping) :> IFizzBuzz
let fifteen = fizzBuzz.Calculate(15)

let doFizzBuzz mapping range =
    let fizzBuzz = FizzBuzz(mapping) :> IFizzBuzz
    range
    |> List.map fizzBuzz.Calculate

let output = doFizzBuzz defaultMapping [1..15]
