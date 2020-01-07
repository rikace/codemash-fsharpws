namespace FunctionalCSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    
    
    public static class Extensions
    {
        public static T WithRetry<T>(this Func<T> action)
        {
            var result = default(T);
            int retryCount = 0;

            bool succesful = false;
            do
            {
                try
                {
                    result = action();
                    succesful = true;
                }
                catch (Exception ex)
                {
                    retryCount++;
                }
            } while (retryCount < 3 && !succesful);

            return result;
        }
        
        public static Func<TResult> Partial<TParam1, TResult>(
            this Func<TParam1, TResult> func, TParam1 parameter)
        {
            return () => func(parameter); 
            // return a function that wraps the execution
            // of the func passed as parameter and passing the parameter expected 
            // as result with return Func of TResult
        }

        public static Func<TParam1, Func<TResult>> Curry<TParam1, TResult>
            (this Func<TParam1, TResult> func)
        {
            return parameter => () => func(parameter);
        }
    }

    public class RetryTest
    {
        public static void ExecuteFileTest()
        {
            // retry is an higerh order function because it takes a function is a parametr
            var filePath = "TextFile.txt";
            Func<string> read = () => System.IO.File.ReadAllText(filePath);
            var text = read.WithRetry();

            
            
            
            
            
            
            Console.WriteLine("Length text {0}", text.Length);
            // what's happend if I have a function that take a parameter ??

            Func<string, string> read2 = (path) => System.IO.File.ReadAllText(path);

            Func<Func<string, string>, string, Func<string>> partial = (f, s) => () => f(s);

            
            var text2 = partial(read2, filePath).WithRetry();
            
            
            
            
            string text3 = read2.Partial(filePath).WithRetry();
            
            

            Console.WriteLine("Length text 3 {0}", text3.Length);

        }
    }
}