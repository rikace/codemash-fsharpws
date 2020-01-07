module Tests

open System
open Xunit
open Swensen.Unquote
open Types
open Functions

//let customer = {
//    Id = 1
//    IsVip = false
//    Credit = 80M
//    PersonalDetails = Some {
//        FirstName = "John"
//        LastName = "Doe"
//        DateOfBirth = DateTime(1970, 11, 23) }
//    Notifications = ReceiveNotifications(receiveDeals = true,
//                                         receiveAlerts = true)
//}
//
//[<Fact>]
//let ``3-1 Create customer``() =
//    test <@ customer.GetType() = typeof<Customer> @>
//
//
//[<Fact>]
//let ``3-2 Adult customer``() =
//    test <@ customer |> isAdult @>
//
//[<Fact>]
//let ``3-3 Non-adult customer``() =
//    let nonadult = { customer with PersonalDetails = Some { customer.PersonalDetails.Value with DateOfBirth = DateTime.Now.AddYears(-1) } }
//    test <@ not (nonadult |> isAdult) @>
//
//[<Fact>]
//let ``3-4 Customer without personal details``() =
//    let nonadult = { customer with PersonalDetails = None }
//    test <@ not (nonadult |> isAdult) @>
//
//[<Fact>]
//let ``3-5 Get alert when nofications are enabled``() =
//    let alert = customer |> getAlert
//    test <@ alert = "Alert for customer 1" @>
//
//[<Fact>]
//let ``3-6 Do not get alert when nofications are disabled``() =
//    let alert = { customer with Notifications = NoNotifications } |> getAlert
//    test <@ alert = "" @>