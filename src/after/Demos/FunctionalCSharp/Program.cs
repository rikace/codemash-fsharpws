using System;
using System.Net;

namespace FunctionalCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // retry is an higerh order function because it takes a function is a parametr
            var msft = "http://microsoft.com";
            var client = new WebClient();
            Func<string> download = () => client.DownloadString(msft);
            var data = download.WithRetry();
            
            
            
#region Partial
            // what's happend if I have a function that take a parameter
            // one soultion is to write a new Retry with different parameter
            // or adapt the incoming function in the retry function in a FP way
            // samething that you can do with partial function application
            // Func<TResult> Partial<TParam1, TResult>
           
            
            Func<string, string> download2 = url => client.DownloadString(url);
            var data2 = download2.Partial(msft).WithRetry(); 
            
            
            // Partial is adapting this function into a function that doesn't need a parameter
            // and instead to write a new different version of Retry we can write different extension methods
#endregion

#region Curry

            // an other techinc is Curry (very similar to partial application)
            // with curry we can transform a function that takes n parameters into 
            // a function that you invoke to apply a parameter and get back a function
            // that takes n-1 parameters
            // Func<TParam1, Func<TResult>> Curry<TParam1, TResult>
            Func<string, Func<string>> downloadCurry = download2.Curry();
            var data3 = downloadCurry(msft).WithRetry();
            
            download2.Curry()(msft).WithRetry();
#endregion            
            
        }
    }
}