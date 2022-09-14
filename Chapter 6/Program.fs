open System
open System.IO

let readFile path =
    seq {
        use reader = new StreamReader(File.OpenRead(path))
        while not reader.EndOfStream do
            reader.ReadLine()
    }

[<EntryPoint>]
let main argv =
    let path = Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")
    let data = readFile path
    data |> Seq.iter (fun x -> printfn "%s" x)
    0