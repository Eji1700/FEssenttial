namespace OrderTests
open MyProject.Orders
open MyProject.Orders.Domain
open Xunit
open FsUnit

module ``Add item to order`` =
    [<Fact>]
    let ``when product does not exist in empty order`` () =
        let myEmptyOrder = 
            { Id = 1; Items = [] }
        
        let expected = 
            { Id = 1; Items = 
                [ { ProductId =1; Quantity = 3} ] }
        
        let actual = 
            myEmptyOrder 
            |> addItem { ProductId = 1; Quantity = 3 }

        actual |> should equal expected

    [<Fact>]
    let ``when product does not exist in non-empty order``() =
        let myOrder = 
            { Id = 1; Items = 
                [ { ProductId = 1; Quantity = 1} ] }

        let expected = 
            { Id = 1; Items = 
                [   { ProductId = 1; Quantity = 1}
                    { ProductId = 2; Quantity = 5} ] } 

        let actual = 
            myOrder
            |> addItem { ProductId = 2; Quantity = 5 }

        actual |> should equal expected

    [<Fact>]
    let ``when product exists in non-empty order`` () =
        let myOrder = 
            { Id = 1; Items = 
                [ { ProductId = 1; Quantity = 1} ] }
        
        let expected = 
            { Id = 1; Items = 
                [   ProductId = 1
                    Quantity = 4 ] }

        let actual = 
            myOrder 
            |> addItem { ProductId = 1; Quantity = 3 }
        
        actual |> should equal expected
