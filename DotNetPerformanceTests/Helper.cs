using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotNetPerformanceTests
{
    public class Helper
    {
        public static IEnumerable<int> GenerateSameNumbers(int len, int min, int max)
        {
            var list = new List<int>();
            var rnd = new Random(100);

            for (var i = 0; i < len; i++)
                list.Add(rnd.Next(min, max));

            return list;
        }

        public static void OutputElapsedMs(Stopwatch sw) => Debug.WriteLine($"{sw.ElapsedMilliseconds} ms");
        public static void OutputElapsedTicks(Stopwatch sw) => Debug.WriteLine($"{sw.ElapsedTicks} ticks");
    }
}
