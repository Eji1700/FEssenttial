namespace MyProject.Orders

type Item = {
    ProductId: int
    Quantity: int
}

type Order = {
    Id: int
    Items: Item list
}

module Domain =
    let addItem item order =
        let items = 
            item::order.Items
            |> List.groupBy (fun i -> i.ProductId)
            |> List.map(fun (id, items) -> { ProductId = id; Quantity = items |> List.sumBy ( fun i -> i.Quantity)})
            |> List.sortBy (fun i -> i.ProductId)
        { order with Items = items}

    let order = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 } ] }
    let newItemExistingProduct = { ProductId = 1; Quantity = 1 }
    let newItemNewProduct = { ProductId = 2; Quantity = 2 }