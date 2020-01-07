namespace ParallelWebCrawler

module AsyncDemo = 

    open System.IO
    open System
    open System.Net

    let httpsync url =
        let req =  WebRequest.Create(Uri url)
        use resp = req.GetResponse()
        use stream = resp.GetResponseStream()
        use reader = new StreamReader(stream)
        let contents = reader.ReadToEnd()
        printfn "(sync) %s - %d" url  contents.Length
        contents.Length

    let httpasync url = async {
        let req =  WebRequest.Create(Uri url)
        use! resp = req.AsyncGetResponse()
        use stream = resp.GetResponseStream()
        use reader = new StreamReader(stream)
        let contents = reader.ReadToEnd()
        printfn "(async) %s - %d" url  contents.Length
        return contents.Length }
     
    let lenAsync () =
        let len =
            httpasync "http://www.google.com"
            |> Async.RunSynchronously        
        printfn "the size of the google.com web page is %d" len
    
    // lenAsync ()
    
    let lenSync () =
        let len = httpsync "http://www.google.com"        
        printfn "the size of the google.com web page is %d" len

    // lenSync ()
    
    
    let sites = [
        "http://www.bing.com"; 
        "http://www.google.com"; 
        "http://www.yahoo.com";
        "http://www.facebook.com"; 
        "http://www.microsoft.com"
        "http://www.bing.com"; 
        "http://www.google.com"; 
        "http://www.yahoo.com";
        "http://www.facebook.com"; 
        "http://www.youtube.com"; 
        "http://www.reddit.com"; 
        "http://www.digg.com"; 
        "http://www.twitter.com"; 
        "http://www.gmail.com"; 
        "http://www.docs.google.com"; 
        "http://www.maps.google.com"; 
        "http://www.microsoft.com"; 
        "http://www.netflix.com"; 
        "http://www.hulu.com" ]        
     
    #time "on"

    let htmlOfSitesSync () =
        [for site in sites -> httpsync site]

    // htmlOfSitesSync () |> ignore
    
    let htmlOfSites () =
        sites
        |> Seq.map (httpasync)
        |> Async.Parallel
        |> Async.RunSynchronously
        
    // htmlOfSites () |> ignore