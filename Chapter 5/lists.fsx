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