open System

// single line comments use a double slash
(* multi line comments use (* . . . *) pair -end of multi line comment- *)

// The let statement
// let <name> <parms> = <expr> 


// ======== "Variables" (but not really) ==========
// The "let" keyword defines an (immutable) value
let myInt = 5
let myFloat = 3.14
let myString = "hello functional programming"   //note that no types needed
let a = 1
let b = 2
let sum x y = x + y
let res = sum a b
let myFunction = fun number -> number * number


// Create mutable types – mutable and ref
let mutable myNumber = 42
myNumber  <-  51

let myRefVar = ref 42
myRefVar := 53
printfn "%d" !myRefVar


// ======== Lists ============
let twoToFive = [2;3;4;5]        // Square brackets create a list with
                                 // semicolon delimiters.
let oneToFive = 1 :: twoToFive   // :: creates list with new 1st element
// The result is [1;2;3;4;5]
let zeroToFive = [0;1] @ twoToFive   // @ concats two lists


let numbers = [1..100]
let numbersWithZero = 0 :: numbers
let evenNumbers = numbersWithZero |> List.filter (fun x -> x % 2 = 0)


// IMPORTANT: commas are never used as delimiters, only semicolons!

// ======== Functions ========
// The "let" keyword also defines a named function.
let square x = x * x          // Note that no parens are used.
square 3                      // Now run the function. Again, no parens.

// inline 
let add x y = x + y           // don't use add (x,y)! It means something
                              // completely different.
add 2 3                       // Now run the function.


let plusOne x = x + 1
let isEven x = x % 2 = 0

// Composition - Pipe and Composition operators
let inline (|>) x f = f x
let inline (>>) f g x = g(f x)

let squarePlusOne x =  x |> square |> plusOne
let plusOneIsEven = plusOne >> isEven


let square' x = x * x
let negate x = x * -1
let print x = printfn "The number is %d" x

// let ``square then negate then print`` 



// functions as values
let squareclone = square
let result = [1..10] |> List.map squareclone |> List.sum

// functions taking other functions as parameters
let execFunction aFunc aParam = aFunc aParam
let result2 = execFunction square 12

let divide x y =
    match y with
    | 0 -> None
    | _ -> Some(x/y)

let result' = divide 4 2
let result'' = divide 4 0


// You can use parens to clarify precedence. In this example,
// do "map" first, with two args, then do "sum" on the result.
// Without the parens, "List.map" would be passed as an arg to List.sum
let sumOfSquaresTo100 =
   List.sum ( List.map square [1..100] )

// You can pipe the output of one operation to the next using "|>"
// Here is the same sumOfSquares function written using pipes
let sumOfSquaresTo100piped =
   [1..100] |> List.map square |> List.sum  // "square" was defined earlier

// you can define lambdas (anonymous functions) using the "fun" keyword
let sumOfSquaresTo100withFun =
   [1..100] |> List.map (fun x->x*x) |> List.sum

// In F# returns are implicit -- no "return" needed. A function always
// returns the value of the last expression used.



// to define a multiline function, just use indents. No semicolons needed.
let evens list =
   let isEven x = x%2 = 0     // Define "isEven" as an inner ("nested") function
   List.filter isEven list    // List.filter is a library function
                              // with two parameters: a boolean function
                              // and a list to work on

evens oneToFive               // Now run the function





// ======== Pattern Matching ========
// Match..with.. is a supercharged case/switch statement.
let simplePatternMatch =
   let x = "a"
   match x with
    | "a" -> printfn "x is a"
    | "b" -> printfn "x is b"
    | _ -> printfn "x is something else"   // underscore matches anything

// Some(..) and None are roughly analogous to Nullable wrappers
let validValue = Some(99)
let invalidValue = None

// In this example, match..with matches the "Some" and the "None",
// and also unpacks the value in the "Some" at the same time.
let optionPatternMatch input =
   match input with
    | Some i -> printfn "input is an int=%d" i
    | None -> printfn "input is missing"

optionPatternMatch validValue
optionPatternMatch invalidValue

// ========= Complex Data Types =========

// Tuple types are pairs, triples, etc. Tuples use commas.
let myTuple = (42, "hello")
let number, message = myTuple

type MyRecord = { Number: int; Message: string }

let myRecord = { Number = 42; Message = "hello" }
let newRecord = { myRecord with Message = "hi" }

let twoTuple = 1,2
let threeTuple = "a",2,true

// Record types have named fields. Semicolons are separators.
type Person = {First:string; Last:string}
let person1 = {First="john"; Last="Doe"}

// Union types have choices. Vertical bars are separators.
type Temp = 
   | DegreesC of float
   | DegreesF of float
let temp = DegreesF 98.6

type DivisionResult =
| DivisionSuccess of result: int
| DivisionError of message: string

let divide' x y =
    match y with
    | 0 -> DivisionError(message = "Divide by zero")
    | _ -> DivisionSuccess(result = x / y)

let result''' = divide' 4 2
let result'''' = divide' 4 0



// Types can be combined recursively in complex ways.
// E.g. here is a union type that contains a list of the same type:
type Employee = 
  | Worker of Person
  | Manager of Employee list
let jdoe = {First="John";Last="Doe"}
let worker = Worker jdoe

// ========= Printing =========
// The printf/printfn functions are similar to the
// Console.Write/WriteLine functions in C#.
printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
printfn "A string %s, and something generic %A" "hello" [1;2;3;4]

// all complex types have pretty printing built in
printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A" 
         twoTuple person1 temp worker

// There are also sprintf/sprintfn functions for formatting data
// into a string, similar to String.Format.

//declare it
type IntAndBool = {intPart: int; boolPart: bool}

//use it
let x = {intPart=1; boolPart=false}


//declare it
type IntOrBool = 
  | IntChoice of int
  | BoolChoice of bool

//use it
let y = IntChoice 42
let z = BoolChoice true



// Discriminated Unions
module Discriminated_Unions =
    type Suit = Hearts | Clubs | Diamonds | Spades

    type Rank =
            | Value of int
            | Ace
            | King
            | Queen
            | Jack
            static member GetAllRanks() =
                [ yield Ace
                  for i in 2 .. 10 do yield Value i
                  yield Jack
                  yield Queen
                  yield King ]

    type Card = { Suit:Suit; Rank:Rank }

    let fullDeck =
            [ for suit in [ Hearts; Diamonds; Clubs; Spades] do
                  for rank in Rank.GetAllRanks() do
                      yield { Suit=suit; Rank=rank } ]

// Pattern matching
module Pattern_matching =
    let fizzBuzz n =
        let divisibleBy m = n % m = 0
        match divisibleBy 3,divisibleBy 5 with
            | true, false -> "Fizz"
            | false, true -> "Buzz"
            | true, true -> "FizzBuzz"
            | false, false -> sprintf "%d" n

    let fizzBuzz' n =
        match n with
        | _ when (n % 15) = 0 -> "FizzBuzz"
        | _ when (n % 3) = 0 -> "Fizz"
        | _ when (n % 5) = 0 -> "Buzz"
        | _ -> sprintf "%d" n

    [1..20] |> List.iter(fun s -> printfn "%s" (fizzBuzz' s))

     //  Active patterns
    let (|DivisibleBy|_|) divideBy n =
       if n % divideBy = 0 then Some DivisibleBy else None


    let fizzBuzz'' n =
        match n with
        | DivisibleBy 3 & DivisibleBy 5 -> "FizzBuzz"
        | DivisibleBy 3 -> "Fizz"
        | DivisibleBy 5 -> "Buzz"
        | _ -> sprintf "%d" n

    [1..20] |> List.iter(fun s -> printfn "%s" (fizzBuzz'' s))

    let (|Fizz|Buzz|FizzBuzz|Val|) n =
        match n % 3, n % 5 with
        | 0, 0 -> FizzBuzz
        | 0, _ -> Fizz
        | _, 0 -> Buzz
        | _ -> Val n



// Class and inheritance
module Class_and_inheritance =
    type Person(firstName, lastName, age) =
        member this.FirstName = firstName
        member this.LastName = lastName
        member this.Age = age

        member this.UpdateAge(n:int) =
            Person(firstName, lastName, age + n)

        override this.ToString() =
            sprintf "%s %s" firstName lastName


    type Student(firstName, lastName, age, grade) =
        inherit Person(firstName, lastName, age)

        member this.Grade = grade

// Abstract classes and inheritance
module Abstract_class_and_inheritance =
    [<AbstractClass>]
    type Shape(weight :float, height :float) =
        member this.Weight = weight
        member this.Height = height

        abstract member Area : unit -> float
        default this.Area() = weight * height

    type Rectangle(weight :float, height :float) =
        inherit Shape(weight, height)

    type Circle(radius :float) =
        inherit Shape(radius, radius)
        override this.Area() = radius * radius * Math.PI

// Interfaces
module Interfaces =
    type IPerson =
       abstract FirstName : string
       abstract LastName : string
       abstract FullName : unit -> string

    type Person(firstName : string, lastName : string) =
        interface IPerson with
            member this.FirstName = firstName
            member this.LastName = lastName
            member this.FullName() = sprintf "%s %s" firstName lastName

    let fred = Person("Fred", "Flintstone")

    (fred :> IPerson).FullName()

// Object expressions
module Object_expressions =
    open System

    let print color =
        let current = Console.ForegroundColor
        Console.ForegroundColor <- color
        {   new IDisposable with
                 member x.Dispose() =
                    Console.ForegroundColor <- current
        }

    using(print ConsoleColor.Red) (fun _ -> printf "Hello in red!!")
    using(print ConsoleColor.Blue) (fun _ -> printf "Hello in blue!!")

// Casting
module Castings =
    open Interfaces

    let testPersonType (o:obj) =
           match o with
           | :? IPerson as person -> printfn "this object is an IPerson"
           | _ -> printfn "this is not an IPerson"




let rec quicksort list =
   match list with
   | [] ->                            // If the list is empty
        []                            // return an empty list
   | firstElem::otherElements ->      // If the list is not empty  
        let smallerElements =         // extract the smaller ones  
            otherElements             
            |> List.filter (fun e -> e < firstElem) 
            |> quicksort              // and sort them
        let largerElements =          // extract the large ones
            otherElements 
            |> List.filter (fun e -> e >= firstElem)
            |> quicksort              // and sort them
        // Combine the 3 parts into a new list and return it
        List.concat [smallerElements; [firstElem]; largerElements]

//test
printfn "%A" (quicksort [1;5;23;18;9;1;3])

let rec quicksort2 = function
   | [] -> []                         
   | first::rest -> 
        let smaller,larger = List.partition ((>=) first) rest 
        List.concat [quicksort2 smaller; [first]; quicksort2 larger]

printfn "%A" (quicksort2 [1;5;23;18;9;1;3])        



module DataETL =
   open System
   open System.IO
   Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

   let file = "Data/FootballResults.csv"
   type Result =
       { Date : DateTime
         Home : string
         Away : string
         HomeGoals : int
         AwayGoals : int }

   let data =
       file
       |> File.ReadAllLines
       |> Seq.skip 1
       |> Seq.map(fun row ->
           let fields = row.Split ','
           { Date = DateTime.ParseExact(fields.[0], "MM/dd/yyyy", null)
             Home = fields.[1]
             Away = fields.[2]
             HomeGoals = int fields.[3]
             AwayGoals = int fields.[4] })

   let etl = 
      data
      |> Seq.filter(fun row -> row.HomeGoals > row.AwayGoals)
      |> Seq.countBy(fun row -> row.Home)
      |> Seq.sortByDescending snd
      |> Seq.take 3
   
   etl |> Seq.toList



module OOPfeatures =
      
   open System

   type Person(age, firstname, surname) =    
       let fullName = sprintf "%s %s" firstname surname

       member __.PrintFullName() =
           printfn "%s is %d years old" fullName age
       
       member this.Age = age
       member that.Name = fullName
       member val FavouriteColour = System.Drawing.Color.Green with get,set


   type IQuack = 
       abstract member Quack : unit -> unit

   // A class that implements interfaces and overrides methods
   type Duck() =
       interface IQuack with
           member this.Quack() = printfn "QUACK!"

   module Quackers =
       let superQuack =
           { new IQuack with
               member this.Quack() = printfn "What type of animal am I?" }
           

   [<AbstractClass>]
   type Employee(name:string) =
       member __.Name = name
       abstract member Work : unit -> string
       member this.DoWork() =
           printfn "%s is working hard: %s!" name (this.Work())

   type ProjectManager(name:string) =
       inherit Employee(name)
       override this.Work() = "Creating a project plan"

   // Exception Handling
   module Exceptions =
       let riskyCode() =
           raise(ApplicationException())
           ()
       let runSafely() =
           try riskyCode()
           with
           | :? ApplicationException as ex -> printfn "Got an application exception! %O" ex
           | :? System.MissingFieldException as ex -> printfn "Got a missing field exception! %O" ex
           | ex -> printfn "Got some other type of exception! %O" ex


   // Resource Management
   module ResourceManagement =
       let createDisposable() =
           printfn "Created!"
           { new IDisposable with member __.Dispose() = printfn "Disposed!" }
       
       let foo() =
           use x = createDisposable()
           printfn "inside!"
       
       let bar() =
           using (createDisposable()) (fun x ->
               printfn "inside!")

   // Casting
   module Casting =
       let anException = Exception()
       let upcastToObject = anException :> obj


   // Active Patterns
   module ActivePatterns =
       let (|Long|Medium|Short|) (value:string) =
           if value.Length < 5 then Short
           elif value.Length < 10 then Medium
           else Long

       match "Hello" with
       | Short -> "This is a short string!"
       | Medium -> "This is a medium string!"
       | Long -> "This is a long string!"

   // Lazy Computations
   module Lazy =
       let lazyText =
           lazy
               let x = 5 + 5
               printfn "%O: Hello! Answer is %d" System.DateTime.UtcNow x
               x
       
       let text = lazyText.Value
       let text2 = lazyText.Value

   // Recursion
   module Recursion =
       let rec factorial number total =
           if number = 1 then total
           else
               printfn "Number %d" number
               factorial (number - 1) (total * number)
       
       factorial 5 1

module AsyncPatterns = 

   open System.IO
   open System.Net

   let httpSync (url:string) =
       let req = WebRequest.Create(url)
       let resp = req.GetResponse()
       use stream = resp.GetResponseStream()
       use reader = new StreamReader(stream)
       let text = reader.ReadToEnd()
       (url, text)

   let httpAsync (url : string) = async {   
       let req = WebRequest.Create(url)
       let! resp = req.AsyncGetResponse()
       use stream = resp.GetResponseStream()
       use reader = new StreamReader(stream)
       let! text = reader.ReadToEndAsync() |> Async.AwaitTask
       return (url, text)
   }

   let sites =  
       [   "http://www.live.com";      "http://www.fsharp.org";
           "http://news.live.com";     "http://www.digg.com";
           "http://www.yahoo.com";     "http://www.amazon.com"
           "http://news.yahoo.com";    "http://www.microsoft.com";
           "http://www.google.com";    "http://www.netflix.com";
           "http://news.google.com";   "http://www.maps.google.com";
           "http://www.bing.com";      "http://www.microsoft.com";
           "http://www.facebook.com";  "http://www.docs.google.com";
           "http://www.youtube.com";   "http://www.gmail.com";
           "http://www.reddit.com";    "http://www.twitter.com";   ]


   #time "on"
   
   let runSync () =
       sites
       |> List.map httpSync
       |> List.iter(fun (url, html) -> printfn "Downloaded %s - Html size %d" url html.Length)

   runSync() 

   let runAsync () =
       sites
       |> Seq.map httpAsync     
       |> Async.Parallel        
       |> Async.RunSynchronously
       |> Array.iter(fun (url, html) -> printfn "Downloaded %s - Html size %d" url html.Length)

   runAsync () 

module basicAgent = 

    type Agent<'T> = MailboxProcessor<'T>


    let printingAgent = 
           Agent.Start(fun inbox ->
             async { while true do 
                       let! msg = inbox.Receive()
                       if msg = "100000" then 
                          printfn "got message %s" msg } )


    printingAgent.Post "three"
    printingAgent.Post "four"

    for i in 0 .. 100000 do 
        printingAgent.Post (string i)

    let agents = 
       [ for i in 0 .. 100000 ->
           Agent.Start(fun inbox ->
             async { while true do 
                       let! msg = inbox.Receive()
                       printfn "%d got message %s" i msg })]

    for agent in agents do
        agent.Post "Hello"

    /// This is a mailbox processing agent that accepts integer messages
    let countingAgent = 
        Agent.Start (fun inbox ->
            let rec loop(state) =         
                async { printfn "Agent, current state = %d" state
                        let! msg = inbox.Receive()
                        
                        printfn "Agent, current state = %d" state
                        return! loop(state+msg) } 

            loop(0)
        )

    countingAgent.Post 3
    countingAgent.Post 4

    for i = 0 to 100 do
        countingAgent.Post i


