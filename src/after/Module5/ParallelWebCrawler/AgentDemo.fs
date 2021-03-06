namespace ParallelWebCrawler

module AgentDemo = 

    let printerAgent = MailboxProcessor.Start(fun inbox->
        // the message processing function
        let rec messageLoop() = async{
            // read a message
            let! msg = inbox.Receive()

            do! Async.Sleep(500)

            // process a message
            printfn "message is: %s" msg
            // loop to top
            return! messageLoop()
            }
        // start the loop
        messageLoop()
        )

    printerAgent.Post "hello"
    printerAgent.Post "hello again"
    printerAgent.Post "hello a third time"


    /// Represents different messages
    /// handled by the stats agent
    type StatsMessage =
      | Add of float
      | Clear
      | GetAverage of AsyncReplyChannel<float>

    let stats =
      MailboxProcessor.Start(fun inbox ->
        // Loops, keeping a list of numbers
        let rec loop nums = async {
          let! msg = inbox.Receive()
          match msg with
          | Add num ->
              let newNums = num::nums
              return! loop newNums
          | GetAverage repl ->
              repl.Reply(List.average nums)
              return! loop nums
          | Clear ->
              return! loop [] }

        loop [] )

    // Add error handler
    stats.Error.Add(fun e -> printfn "Oops: %A" e)

    // Post messages
    stats.Post(Add(10.0))
    stats.Post(Add(7.0))
    
    let average = stats.PostAndReply(GetAverage)
    printfn "%A" average
    
    stats.Post(Clear)

    let error = stats.PostAndReply(GetAverage)
    
    

    #load "AsyncDemo.fs"
    open ParallelWebCrawler.AsyncDemo
    open System.Threading

    let parallelWorker (workers:int) f = 
        let agent = new MailboxProcessor<'a>((fun inbox ->
            let agents = Array.init workers (fun _ -> MailboxProcessor.Start(f))            
            let rec loop i = async {
                let! msg = inbox.Receive()
                agents.[i].Post(msg)
                return! loop((i+1) % workers)
            }
            loop 0))
        agent.Start()
        agent

            
    let f =
        fun (inbox: MailboxProcessor<_>) -> 
            let rec loop () = async {
                let! msg = inbox.Receive()
                let! len =  httpasync msg
                printfn "the size of the %s web page is %d - Thread id#%d" msg len Thread.CurrentThread.ManagedThreadId
                return! loop ()
            }
            loop ()
        
        
    let agentWorker = parallelWorker 8 f
    
    sites
    |> List.iter agentWorker.Post    