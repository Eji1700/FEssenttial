open ComuptationExpression.AsyncResultDemoTests
open System.IO

[<EntryPoint>]
let main argv =
    // Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")
    // |> getFileInformation
    // |> Async.RunSynchronously
    // |> printfn "%A"

    printfn "Success: %b" success
    printfn "BadPassword: %b" badPassword
    printfn "InvalidUser: %b" invalidUser
    printfn "IsSuspended: %b" isSuspended
    printfn "IsBanned: %b" isBanned
    printfn "HasBadLuck: %b" hasBadLuck
    0
    pg 164