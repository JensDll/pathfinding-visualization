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
            yield return new object[] { new int[] { 4, 3, 2, 1 }, new int[] { 1, 2, 3, 4 } };
            yield return new object[] { new int[] { 1, 2, 3, 4 }, new int[] { 1, 2, 3, 4 } };
            yield return new object[] { new int[] { 1, 2, -2, -1 }, new int[] { -2, -1, 1, 2 } };
            yield return new object[] { new int[] { 2, 2, -4, -4, 8, 8, -16, -16 }, new int[] { -16, -16, -4, -4, 2, 2, 8, 8 } };
        }

        [Theory]
        [MemberData(nameof(HeapTestData))]
        public void MinHeap_ShouldWorkWhenPassedAnEnumerable(int[] testData, int[] expectedValues)
        {
            Heap<int> sut = new((a, b) => a - b, testData);

            var result = testData.Select(_ => sut.RemoveTop());
            Assert.Equal(expectedValues, result);
        }

        [Theory]
        [MemberData(nameof(HeapTestData))]
        public void MinHeap_ShouldWorkWhenAddedManual(int[] testData, int[] expectedValues)
        {
            Heap<int> sut = new((a, b) => a - b);

            foreach (int x in testData)
            {
                sut.Add(x);
            }

            var result = testData.Select(_ => sut.RemoveTop());
            Assert.Equal(expectedValues, result);
        }

        [Theory]
        [MemberData(nameof(HeapTestData))]
        public void MaxHeap_ShouldWorkWhenPassedAnEnumerable(int[] testData, int[] expectedValues)
        {
            Heap<int> sut = new((a, b) => b - a, testData);

            var result = testData.Select(_ => sut.RemoveTop());
            Assert.Equal(expectedValues.Reverse(), result);
        }

        [Theory]
        [MemberData(nameof(HeapTestData))]
        public void MaxHeap_ShouldWorkWhenAddedManual(int[] testData, int[] expectedValues)
        {
            Heap<int> sut = new((a, b) => b - a);

            foreach (int x in testData)
            {
                sut.Add(x);
            }

            var result = testData.Select(_ => sut.RemoveTop());
            Assert.Equal(expectedValues.Reverse(), result);
        }
    }
}
