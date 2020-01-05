#load "Types.fs"
#load "Functions.fs"

open System
open Types
open Functions

let customer = {
    Id = 1
    IsVip = false
    Credit = 0M
    // TODO
    // add missing fields 
        //    PersonalDetails 
        //    Notifications 
}

let purchases = (customer, 101M)
let vipCustomer = tryPromoteToVip purchases

let calculatedPurchases = getPurchases customer

let customerWithMoreCredit = customer |> increaseCredit (fun c -> c.IsVip)

let upgradedCustomer = upgradeCustomer customer

