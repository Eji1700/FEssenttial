let items = 
    [   1, 0.25M
        5, 0.25M
        1, 2.25M
        1, 125M
        7, 10.9M
    ]

let sum items = 
    items
    |> List.sumBy (fun (q,p) -> decimal q * p)

[ 1 .. 10 ]
|> List.fold (fun acc v -> acc + v ) 0

let getTotal items =
    items 
    |> List.fold (fun acc (q, p) -> acc + decimal q * p) 0M

let total = getTotal items

let myList = [1;2;3;4;5;7;6;5;4;3]

let gbResult = myList |> List.groupBy id

let unique items =
    items
    |> List.groupBy id
    |> List.map (fun (i,_) -> i)

let unResult = unique myList

let uniqueSet items = 
    items |> Set.ofList

let setResult = uniqueSet myList

let nums = [1..10]

nums
|> List.filter (fun v -> v % 2 = 1)
|> List.map (fun v -> v * v)
|> List.sum

match nums with 
| [] -> 0
| items -> 
    items
    |> List.reduce( fun acc v -> acc + if v % 2 = 1 then (v * v) else 0)

nums
|> List.sumBy (fun v -> if v % 2 = 1 then (v * v) else 0)

