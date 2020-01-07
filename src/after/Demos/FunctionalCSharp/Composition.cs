using System;

namespace FunctionalCSharp
{
    public class Composition
    {
        Func<int, int, int> addL = (x, y) => x + y;
        Func<int, Func<int, int>> curriedAddL = x => y => x + y;
        
        
        int CalcB(int a)
        {
            return a * 3;
        }
        int CalcC(int b)
        {
            return b + 27;
        }

        int CalcCfromA(int a)
        {
            return CalcC(CalcB(a));
        }


        public void Test()
        {

            int a = 10;
            int b = CalcB(a); int c = CalcC(b);

            Func<int, int> calcFunc = x => CalcC(CalcB(x));

            Func<int, int> calcM = x => x * 3;
            Func<int, int> calcA = x => x + 27;
            
            var calcCFromA = ComposeEx.Compose(calcM, calcA); // Alternatively:
            var calcCFromA_ = calcM.Compose(calcA);


            var add_always_3 = curriedAddL(3);

            Func<int, int> add_3 = (n) => add_always_3(n);

        }
    }

    public static class ComposeEx
    {
        public static Func<TS, TR> Compose<TS, TI, TR>(
            this Func<TS, TI> func1, Func<TI, TR> func2) => sourceParam => func2(func1(sourceParam));

    }
}