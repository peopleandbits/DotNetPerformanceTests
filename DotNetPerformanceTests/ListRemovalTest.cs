using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotNetPerformanceTests
{
    [TestClass]
    public class ListRemovalTest
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _Numbers = GenerateNumbers(200000, 1, 999) as IList<int>;
        }

        static IList<int> _Numbers;
        static int _ExpectedResult = 50148;

        [TestMethod]
        public void EasyRemoveFromListTest()
        {
            // assign
            Func<int, bool> _FilterRejectingNumbersOver250 = c => c <= 250;

            // act
            var sw = new Stopwatch();
            sw.Start();

            var result = _Numbers.Where(_FilterRejectingNumbersOver250).ToList();

            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);

            // assert
            Assert.AreEqual(_ExpectedResult, result.Count);
        }

        [TestMethod]
        public void ComplexRemoveFromList()
        {
            // assign
            var copyOfAll = new List<int>(_Numbers);
            Func<int, bool> _FilterAllowingNumbersOver250 = c => c > 250;
            var toBeRemoved = copyOfAll.Where(_FilterAllowingNumbersOver250).Reverse().ToArray();

            // act
            var sw = new Stopwatch();
            sw.Start();
                        
            // generously only measuring the time spent actually removing...
            foreach (var item in toBeRemoved)
                copyOfAll.Remove(item);

            sw.Stop();
            Debug.WriteLine(sw.ElapsedMilliseconds);

            // assert
            Assert.AreEqual(_ExpectedResult, copyOfAll.Count);
        }

        static IEnumerable<int> GenerateNumbers(int len, int min, int max)
        {
            var list = new List<int>();
            var rnd = new Random(100);

            for (var i = 0; i < len; i++)
                list.Add(rnd.Next(min, max));

            return list;
        }
    }
}
