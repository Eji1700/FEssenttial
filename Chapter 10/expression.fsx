open System

type ILogger =
    abstract member Info : string -> unit
    abstract member Error : string -> unit

let logger = {
    new ILogger with 
        member _.Info(msg) = printfn "Info %s" msg
        member _.Error(msg) = printfn "Error: %s" msg
}

type MyClass(logger:ILogger) =
    let mutable count = 0 

    member _.DoSomething input = 
        logger.Info $"Processing {input} at {DateTime.UtcNow.ToString()}"
        count <- count + 1
        ()

    member _.Count = count

let myClass = MyClass(logger)
[1..10] |> List.iter myClass.DoSomething
printfn "%i" myClass.Count

let doSomethingElse (logger:ILogger) input = 
    logger.Info $"Processing {input} at {DateTime.UtcNow.ToString()}"
    ()

doSomethingElse logger "MyData"

type IRecentlyUsedList =
    abstract member IsEmpty : bool
    abstract member Size : int
    abstract member Capacity : int
    abstract member Clear : unit -> unit
    abstract member Add : string -> unit
    abstract member TryGet : int -> string option

type RecentlyUsedList(capactiy:int) =
    let items = ResizeArray<string>(capactiy)

    let add item =
        items.Remove item |> ignore
        if items.Count = items.Capacity then items.RemoveAt 0
        items.Add item

    let get index =
        if index >= 0 && index < items.Count
        then Some items[items.Count - index - 1]
        else None
    interface IRecentlyUsedList with 
        member _.IsEmpty = items.Count = 0
        member _.Size = items.Count
        member _.Capacity = items.Capacity
        member _.Clear() = items.Clear()
        member _.Add(item) = add item
        member _.TryGet(index) = get index

let mrul = RecentlyUsedList(5) :> IRecentlyUsedList

mrul.Capacity
mrul.Add "Test"
mrul.Size
mrul.Capacity
mrul.IsEmpty = false 

mrul.Add "Test2"
mrul.Add "Test3"
mrul.Add "Test4"
mrul.Add "Test"
mrul.Add "Test6"
mrul.Add "Test7"
mrul.Add "Test"

mrul.Size
mrul.Capacity
mrul.TryGet(0)
mrul.TryGet(4)

mrul.TryGet(0) = Some "Test"