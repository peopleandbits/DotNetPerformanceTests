using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotNetPerformanceTests
{
    [TestClass]
    public class LookupSpeedTest
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _Numbers = Helper.GenerateSameNumbers(200000, 1, 99999) as IList<int>;
        }

        static IList<int> _Numbers;
        static int _ItemWithHighestIndex = 71039;
        static int _HighestIndex = 199985;
        
        Predicate<int> _FilterAllowingNumber71039 = c => c == _ItemWithHighestIndex;

        [TestMethod]
        public void ContainsInList()
        {
            // assign

            // act
            var sw = new Stopwatch();
            sw.Start();

            // let's try to see if the item with highest index it exists
            var found = _Numbers.ToList().Contains(_ItemWithHighestIndex);

            sw.Stop();
            Helper.OutputElapsedTicks(sw);

            // assert
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void FindInList()
        {
            // assign

            // act
            var sw = new Stopwatch();
            sw.Start();

            // let's try to see if we can get hold of the item with highest index
            int item = _Numbers.ToList().Find(_FilterAllowingNumber71039);

            sw.Stop();
            Helper.OutputElapsedTicks(sw);

            // assert
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void GetItemInList()
        {
            // assign

            // act
            var sw = new Stopwatch();
            sw.Start();

            // let's try to see if we can get hold of the item with highest index
            int item = _Numbers.ToList()[_HighestIndex];

            sw.Stop();
            Helper.OutputElapsedTicks(sw);

            // assert
            Assert.AreEqual(_ItemWithHighestIndex, item);
        }

        [TestMethod]
        public void GetItemInDictionary()
        {
            // assign
            var dict = _Numbers.Distinct().ToDictionary(k => k);
            
            // act
            var sw = new Stopwatch();
            sw.Start();

            // let's try to see if we can get hold of the item with highest index
            int item = dict[_ItemWithHighestIndex];

            sw.Stop();
            Helper.OutputElapsedTicks(sw);

            // assert
            Assert.AreEqual(_ItemWithHighestIndex, item);
        }

        [TestMethod]
        public void FindHishestIndex()
        {
            int hi = 0; 

            foreach (var n in _Numbers)
            {
                int index = _Numbers.IndexOf(n);
                if (index > hi)
                    hi = index;
            }

            Assert.AreEqual(_HighestIndex, hi);
            Assert.AreEqual(_ItemWithHighestIndex, _Numbers[hi]);
        }
    }
}
