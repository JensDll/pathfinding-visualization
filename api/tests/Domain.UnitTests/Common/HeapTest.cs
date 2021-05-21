using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.Common
{
    public class HeapTest
    {
        public static IEnumerable<object[]> HeapTestData()
        {
            yield return new object[] { new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4 } };
            yield return new object[] { new int[] { 4, 3, 2, 1 }, new int[] { 1, 2, 3, 4 } };

            var rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                var testData = new int[1000];
                var expectedValues = new int[1000];

                for (int j = 0; j < testData.Length; j++)
                {
                    testData[j] = rand.Next(int.MinValue, int.MaxValue);
                }

                testData.CopyTo(expectedValues, 0);
                Array.Sort(expectedValues);

                yield return new object[] { testData, expectedValues };
            }
        }

        [Theory]
        [MemberData(nameof(HeapTestData))]
        public void MinHeap_when_passed_an_enumerable(int[] testData, int[] expectedValues)
        {
            Heap<int> sut = new(testData);

            var result = testData.Select(_ => sut.RemoveTop());
            Assert.Equal(expectedValues, result);
        }

        [Theory]
        [MemberData(nameof(HeapTestData))]
        public void MinHeap_when_added_manuel(int[] testData, int[] expectedValues)
        {
            Heap<int> sut = new();

            foreach (int x in testData)
            {
                sut.Add(x);
            }

            var result = testData.Select(_ => sut.RemoveTop());
            Assert.Equal(expectedValues, result);
        }

        [Theory]
        [MemberData(nameof(HeapTestData))]
        public void MaxHeap_when_passed_an_enumerable(int[] testData, int[] expectedValues)
        {
            Heap<int> sut = new(testData, (a, b) =>
            {
                if (a == b) return 0;
                if (a > b) return -1;
                return 1;
            });

            var result = testData.Select(_ => sut.RemoveTop());
            Assert.Equal(expectedValues.Reverse(), result);
        }

        [Theory]
        [MemberData(nameof(HeapTestData))]
        public void MaxHeap_when_added_manuel(int[] testData, int[] expectedValues)
        {
            Heap<int> sut = new((a, b) =>
            {
                if (a == b) return 0;
                if (a > b) return -1;
                return 1;
            });

            foreach (int x in testData)
            {
                sut.Add(x);
            }

            var result = testData.Select(_ => sut.RemoveTop());
            Assert.Equal(expectedValues.Reverse(), result);
        }
    }
}
