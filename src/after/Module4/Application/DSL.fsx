


type Calc =
    | Val of int
    | Mul of Calc * Calc
    | Add of Calc * Calc
   
let rec eval calc =
    match calc with
    | Val n -> n
    | Mul (m1, m2) ->( eval m1)  * (eval m2)
    | Add (m1, m2) ->( eval m1)  + (eval m2)
    
let op = Add(Val 7, Mul(Val 10, Val 5))
eval op
let v n = Val n
let add a b = Add(a, b)
v 40 |> add (v 2) |> eval

let (+) a b = add a b 
v 2 + v 4 |> eval 



















// =====
(*
    Now that we have the building blocks to represent ideas 
    in F#, we have all the power we need to represent a real world problem in the language of mathematics.

    In this simple example we were able to represent and evaluate a four-function mathematical expression using only a discriminated union and a pattern match. You would be hard 
    pressed to write the equivalent C# in as few lines of code because you would need to add additional scaffolding to represent these concepts.
*)
// This Discriminated Union is sufficient to express any four-function
// mathematical expression.
type Expr =
    | Num      of int
    | Add      of Expr * Expr
    | Subtract of Expr * Expr
    | Multiply of Expr * Expr
    | Divide   of Expr * Expr
    
// This simple pattern match is all we need to evaluate those
// expressions. 
let rec evaluate expr =
    match expr with
    | Num(x)             -> x
    | Add(lhs, rhs)      -> (evaluate lhs) + (evaluate rhs)
    | Subtract(lhs, rhs) -> (evaluate lhs) - (evaluate rhs)
    | Multiply(lhs, rhs) -> (evaluate lhs) * (evaluate rhs)
    | Divide(lhs, rhs)   -> (evaluate lhs) / (evaluate rhs)

// 10 + 5
let ``10 + 5`` = 0

// 10 * 10 - 25 / 5
let sampleExpr = 
    Subtract(
        Multiply(
            Num(10), 
            Num(10)),
        Divide(
            Num(25), 
            Num(5)))
        
let result = evaluate sampleExpr

let n a = Num a
let add a b = Add(a,b)
let mul a b = Multiply(a,b)

let exp1 = n 4 |> add (n 7) |> mul (n 8)
let exp2 = add (n 1) (mul (n 2) (n 7))


let result' = evaluate exp1
let result'' = evaluate exp2


// It appears that building an internal LOGO-like DSL is surprisingly easy, and requires almost no code! What you need is just to define the basic types to describe your actions:

type Expression =
    | X
    | Constant of float
    | Add of Expression * Expression
    | Mul of Expression * Expression

let rec interpret (ex:Expression) =
    match ex with
    | X -> fun (x:float) -> x
    | Constant(value) -> fun (x:float) -> value
    | Add(leftExpression,rightExpression) ->
        let left = interpret leftExpression
        let right = interpret rightExpression
        fun (x:float) -> left x + right x
    | Mul(leftExpression,rightExpression) ->
        let left = interpret leftExpression
        let right = interpret rightExpression
        fun (x:float) -> left x * right x

let run (x:float,expression:Expression) =
        let f = interpret expression
        let result = f x
        printfn "Result: %.2f" result


let expression = Add(Constant(1.0),Mul(Constant(2.0),X))
run(10.0,expression)

let expression2 = Mul(X,Constant(10.0))
run(10.0,expression2)

let add' a b = Add(a, b)
let mul' a b = Mul(a, b)
let c v =  Constant v
let x = X

let expression3 = add' (c 1.0) (mul' (c 2.0) x)
run(10.0,expression3)



(*
>
Result: 21.00
Result: 100.00
*)

module Starbucks =

    (*
    Every morning on your way to the office, you pull your car up to your favorite coffee shop 
    for a Grande Skinny Cinnamon Dolce Latte with whip. 
    The barista always serves you exactly what you order. 
    She can do this because you placed your order using precise language that she understands. Y
    ou don’t have to explain the meaning of every term that you utter, 
    though to others what you say might be incomprehensible
    *)

    type size = Tall | Grande | Venti

    type drink = Latte | Cappuccino | Mocha | Americano

    type extra = Shot | Syrup

    type Cup = { Size:size; Drink:drink; Extras:extra list } with
        static member (+) (cup:Cup,extra:extra) =
            { cup with Extras = extra :: cup.Extras }
        static member Of size drink =
            { Size=size; Drink=drink; Extras=[] }
        
    let Price (cup:Cup) =
        let tall, grande, venti = 
            match cup.Drink with
            | Latte      -> 2.69, 3.19, 3.49
            | Cappuccino -> 2.69, 3.19, 3.49
            | Mocha      -> 2.99, 3.49, 3.79
            | Americano  -> 1.89, 2.19, 2.59
        let basePrice =
            match cup.Size with
            | Tall -> tall 
            | Grande -> grande
            | Venti -> venti
        let extras =
            cup.Extras |> List.sumBy (function
                | Shot -> 0.59
                | Syrup -> 0.39
            )
        basePrice + extras


    let myCoffe = Cup.Of Grande Mocha + Shot
    
    let price = myCoffe |> Price 