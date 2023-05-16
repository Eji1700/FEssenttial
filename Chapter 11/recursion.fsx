let fact n acc = 
    let rec loop n acc = 
        match n with 
        | 1 -> acc 
        | _ -> loop (n-1) (acc * n)
    loop n 1

let rec badFib (n:int64) = 
    match n with 
    | 0L -> 0L
    | 1L -> 1L
    | s -> badFib (s-1L) + badFib (s-2L)

let fib (n:int64) =
    let rec loop n (a,b) =  
        match n with 
        | 0L -> a 
        | 1L -> b 
        | n -> loop (n-1L) (b, a+b)
    loop n (0L, 1L)

// fib 50L 

let mapping = 
    [   (3, "Fizz")
        (5, "Buzz")
        (7, "Bazz") ]

let fizzBuzz initialMapping n =
    let rec loop mapping acc =
        match mapping with 
        | [] -> if acc = "" then string n else acc 
        | head :: tail ->
            let value = 
                head 
                |> (fun (div, msg) -> if n % div = 0 then msg else "")
            loop tail (acc + value)
    loop initialMapping ""

[1 .. 105]
|> List.map(fizzBuzz mapping)
|> List.iter (printfn "%s")

let rec qsort input =
    match input with 
    | [] -> []
    | head :: tail ->
        let smaller, larger = List.partition(fun n -> head >= n) tail
        List.concat [qsort smaller; [head]; qsort larger]

[5;9;5;2;7;9;1;1;3;5] |> qsort |> printfn "%A"