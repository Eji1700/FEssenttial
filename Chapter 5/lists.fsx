// let items = [1 .. 5]
// let extendedItems = 6 :: items
// let readList items =
//     match items with 
//     | [] -> "Empty List"
//     | [head] -> $"Head: {head}"
//     | head::tail -> sprintf "Head: %A and Tail:%A" head tail

// // let emptyList = readList []
// let multipleList = readList [1;2;3;4;5]
// let singleItemList = readList [1]

let list1 = [1..5]
let list2 = [3..7]
let emptyList = []

let joined = list1 @ list2
let joinedEmpty = list1 @ emptyList
let emptyJoined = emptyList @ list1

let joinedLists = List.concat [list1; list2]

let myList = [1..9]

let getEvenss items =
    items 
    |> List.filter (fun x -> x % 2 = 0)

let evens = getEvenss myList

let sum items =
    items |> List.sum

let mySum = sum myList

let triple items =
    items
    |> List.map ( fun x -> x * 3)

let myTriples = triple [1..5]

let print items =
    items
    |> List.iter ( fun x -> (printfn "My value is %i" x))

print myList