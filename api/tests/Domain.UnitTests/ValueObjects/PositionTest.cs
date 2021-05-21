using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.ValueObjects
{
    public class PositionTest
    {
        public static IEnumerable<object[]> AddData()
        {
            yield return new Position[] { new(1, 1), new(1, 1), new(2, 2) };
            yield return new Position[] { new(1, 1), new(2, 2), new(3, 3) };
            yield return new Position[] { new(-1, -1), new(1, 1), new(0, 0) };
            yield return new Position[] { new(-1, -1), new(-1, -1), new(-2, -2) };
        }

        public static IEnumerable<object[]> SubstractData()
        {
            yield return new Position[] { new(1, 1), new(1, 1), new(0, 0) };
            yield return new Position[] { new(1, 1), new(2, 2), new(-1, -1) };
            yield return new Position[] { new(-1, -1), new(1, 1), new(-2, -2) };
            yield return new Position[] { new(-1, -1), new(-1, -1), new(0, 0) };
        }

        public static IEnumerable<object[]> TopLeftData()
        {
            yield return new Position[] { new(0, 0), new(-1, -1) };
            yield return new Position[] { new(-1, -1), new(-2, -2) };
            yield return new Position[] { new(1, 0), new(0, -1) };
        }

        public static IEnumerable<object[]> TopData()
        {
            yield return new Position[] { new(0, 0), new(-1, 0) };
            yield return new Position[] { new(-1, -1), new(-2, -1) };
            yield return new Position[] { new(1, 0), new(0, 0) };
        }

        public static IEnumerable<object[]> TopRightData()
        {
            yield return new Position[] { new(0, 0), new(-1, 1) };
            yield return new Position[] { new(-1, -1), new(-2, 0) };
            yield return new Position[] { new(1, 0), new(0, 1) };
        }

        public static IEnumerable<object[]> RightData()
        {
            yield return new Position[] { new(0, 0), new(0, 1) };
            yield return new Position[] { new(-1, -1), new(-1, 0) };
            yield return new Position[] { new(1, 0), new(1, 1) };
        }

        public static IEnumerable<object[]> BottomRightData()
        {
            yield return new Position[] { new(0, 0), new(1, 1) };
            yield return new Position[] { new(-1, -1), new(0, 0) };
            yield return new Position[] { new(1, 0), new(2, 1) };
        }

        public static IEnumerable<object[]> BottomData()
        {
            yield return new Position[] { new(0, 0), new(1, 0) };
            yield return new Position[] { new(-1, -1), new(0, -1) };
            yield return new Position[] { new(1, 0), new(2, 0) };
        }

        public static IEnumerable<object[]> BottomLeftData()
        {
            yield return new Position[] { new(0, 0), new(1, -1) };
            yield return new Position[] { new(-1, -1), new(0, -2) };
            yield return new Position[] { new(1, 0), new(2, -1) };
        }

        public static IEnumerable<object[]> LeftData()
        {
            yield return new Position[] { new(0, 0), new(0, -1) };
            yield return new Position[] { new(-1, -1), new(-1, -2) };
            yield return new Position[] { new(1, 0), new(1, -1) };
        }

        public static IEnumerable<object[]> AbsoluteData()
        {
            yield return new Position[] { new(-1, -1), new(1, 1) };
            yield return new Position[] { new(-1, 0), new(1, 0) };
            yield return new Position[] { new(1, 1), new(1, 1) };
        }

        public static IEnumerable<object[]> ManhattenDistanceData()
        {
            yield return new object[] { new Position(1, 1), new Position(1, 1), 0 };
            yield return new object[] { new Position(1, 1), new Position(2, 2), 2 };
            yield return new object[] { new Position(-1, -1), new Position(1, 1), 4 };
            yield return new object[] { new Position(10, 8), new Position(-1, 1), 18 };
        }

        [Theory]
        [MemberData(nameof(AddData))]
        public void Add(Position p1, Position p2, Position expected)
        {
            Assert.Equal(expected, p1 + p2);
        }

        [Theory]
        [MemberData(nameof(SubstractData))]
        public void Substract(Position p1, Position p2, Position expected)
        {
            Assert.Equal(expected, p1 - p2);
        }

        [Theory]
        [MemberData(nameof(TopLeftData))]
        public void TopLeft(Position p, Position expected)
        {
            Assert.Equal(expected, p.TopLeft);
        }

        [Theory]
        [MemberData(nameof(TopData))]
        public void Top(Position p, Position expected)
        {
            Assert.Equal(expected, p.Top);
        }

        [Theory]
        [MemberData(nameof(TopRightData))]
        public void TopRight(Position p, Position expected)
        {
            Assert.Equal(expected, p.TopRight);
        }

        [Theory]
        [MemberData(nameof(RightData))]
        public void Right(Position p, Position expected)
        {
            Assert.Equal(expected, p.Right);
        }

        [Theory]
        [MemberData(nameof(BottomRightData))]
        public void BottomRight(Position p, Position expected)
        {
            Assert.Equal(expected, p.BottomRight);
        }

        [Theory]
        [MemberData(nameof(BottomData))]
        public void Bottom(Position p, Position expected)
        {
            Assert.Equal(expected, p.Bottom);
        }

        [Theory]
        [MemberData(nameof(BottomLeftData))]
        public void BottomLeft(Position p, Position expected)
        {
            Assert.Equal(expected, p.BottomLeft);
        }

        [Theory]
        [MemberData(nameof(LeftData))]
        public void Left(Position p, Position expected)
        {
            Assert.Equal(expected, p.Left);
        }

        [Theory]
        [MemberData(nameof(AbsoluteData))]
        public void Absolute(Position p, Position expected)
        {
            Assert.Equal(expected, p.Absolute);
        }

        [Theory]
        [MemberData(nameof(ManhattenDistanceData))]
        public void ManhattenDistance(Position p1, Position p2, int expected)
        {
            Assert.Equal(expected, p1.ManhattenDistance(p2));
        }
    }
}
