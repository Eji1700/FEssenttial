namespace MyProjectTests

open Xunit
open FsUnit
open MyProject.Customer

module ``When upgrading customer`` =
    let customerVIP = { Id = 1; IsVip = true; Credit = 0.0M }
    let customerSTD = { Id = 2; IsVip = false; Credit = 100.0M }

    [<Fact>]
    let ``should give VIP customer more credit`` () =
        let expected = { customerVIP with Credit = customerVIP.Credit + 100M }
        let upgraded = upgradeCustomer customerVIP
        upgraded |> should equal expected

    [<Fact>]
    let ``should convert eligible STD customer to VIP`` =
        let customer = { Id = 2; IsVip = false; Credit = 200M }
        let expected = { customer with IsVip = true; Credit = customer.Credit + 100M }

        let upgraded = upgradeCustomer customer
        upgraded |> should equal expected