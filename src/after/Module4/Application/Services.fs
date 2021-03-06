﻿namespace Services

type ICustomerService =
    abstract UpgradeCustomer : int -> Types.Customer 
    abstract GetCustomerInfo : Types.Customer -> string
    
type CustomerService() =
    
    interface ICustomerService with    
        member this.UpgradeCustomer id =
            id
            |> Functions.getCustomer
            |> Functions.upgradeCustomer

        member this.GetCustomerInfo customer =
            let isAdult = Functions.isAdult customer
            let alert = Functions.getAlert customer
            sprintf "Id: %i, IsVip: %b, Credit: %.2f, IsAdult: %b, Alert: %s"
                customer.Id customer.IsVip customer.Credit isAdult alert
                
module Service =                 
    let customerService =
        { new ICustomerService with 
            member this.UpgradeCustomer id =
                id
                |> Functions.getCustomer
                |> Functions.upgradeCustomer

            member this.GetCustomerInfo customer =
                let isAdult = Functions.isAdult customer
                let alert = Functions.getAlert customer
                sprintf "Id: %i, IsVip: %b, Credit: %.2f, IsAdult: %b, Alert: %s"
                    customer.Id customer.IsVip customer.Credit isAdult alert  }         