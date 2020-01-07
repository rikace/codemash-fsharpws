module Functions

open Types
open System

let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 100M then { customer with IsVip = true }
    else customer

let getPurchases customer =
    if customer.Id % 2 = 0 then (customer, 120M)
    else (customer, 80M)

let checkAndUpdateVIPstatus condition customer =
    if condition customer then { customer with IsVip = true }
    else { customer with IsVip = false }

let increaseCreditUsingVip = checkAndUpdateVIPstatus (fun c -> c.IsVip |> not)

let upgradeCustomer = getPurchases >> tryPromoteToVip >> increaseCreditUsingVip

let isAdult customer =
    match customer.PersonalDetails with
    | None -> false
    | Some d -> d.DateOfBirth.AddYears 18 <= DateTime.Now.Date

let getAlert customer =
    match customer.Notifications with
    | ReceiveNotifications(receiveAlerts = true) ->
        sprintf "Alert for customer %i" customer.Id
    | _ -> ""