namespace OrderTests
open MyProject.Orders
open MyProject.Orders.Domain
open Xunit
open FsUnit

module ``Add item to order`` =
    [<Fact>]
    let ``when product does not exist in empty order`` () =
        let myEmptyOrder = { Id = 1; Items = [] }
        let expected = { Id = 1; Items = [ { ProductId =1; Quantity = 3} ] }
        let actual = myEmptyOrder |> addItem { ProductId = 1; Quantity = 3 }
        actual |> should equal expected