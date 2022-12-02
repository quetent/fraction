namespace TestsProject
{
    [TestFixture]
    public class Tests
    {
        private static void Test<T>(T actualResult, T expectedResult)
        {
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public static void TestSimplifying()
        {
            Test(new Fraction(4, 16), new Fraction(1, 4));
            Test(new Fraction(-5, 15), new Fraction(-1, 3));
            Test(new Fraction(-7, -49), new Fraction(1, 7));
            Test(new Fraction(2, 4), new Fraction(1, 2));
            Test(new Fraction(20, 10), new Fraction(2, 1));
            Test(new Fraction(1, 17), new Fraction(1, 17));
            Test(new Fraction(-5, 10), new Fraction(-1, 2));
            Test(new Fraction(17, 34), new Fraction(1, 2));
            Test(new Fraction(-19, -19), new Fraction(1, 1));
            Test(new Fraction(-22, 11), new Fraction(-2, 1));
        }

        [Test]
        public static void TestAdd()
        {
            Test(new Fraction(1, 2) + new Fraction(1, 4),
                 new Fraction(3, 4));
            Test(new Fraction(1, 8) + new Fraction(4, 16),
                 new Fraction(3, 8));
            Test(new Fraction(3, 10) + new Fraction(-4, 10),
                 new Fraction(-1, 10));
        }

        [Test]
        public static void TestSubtraction()
        {
            Test(new Fraction(2, 5) - new Fraction(1, 5),
                 new Fraction(1, 5));
            Test(new Fraction(2, 3) - new Fraction(1, 3),
                 new Fraction(1, 3));
            Test(new Fraction(2, 3) - new Fraction(-1, 6),
                 new Fraction(5, 6));
        }

        [Test]
        public static void TestDivision()
        {
            Test(new Fraction(2, 3) / new Fraction(2, 3),
                 new Fraction(1, 1));
            Test(new Fraction(5, 7) / new Fraction(-5, 14),
                 new Fraction(-2, 1));
            Test(new Fraction(-7, 11) / new Fraction(5, 6),
                 new Fraction(-42, 55));
        }

        [Test]
        public static void TestMultiplication()
        {
            Test(new Fraction(2, 3) * new Fraction(4, 9),
                 new Fraction(8, 27));
            Test(new Fraction(-1, 7) * new Fraction(49, 6),
                 new Fraction(-7, 6));
            Test(new Fraction(-5, 6) * new Fraction(-2, 3),
                 new Fraction(5, 9));
        }

        [Test]
        public static void TestAbsolute()
        {
            Test(Fraction.Abs(new Fraction(2, 3)),
                              new Fraction(2, 3));
            Test(Fraction.Abs(new Fraction(-5, 7)),
                              new Fraction(5, 7));
            Test(Fraction.Abs(new Fraction(-2, 3)),
                              new Fraction(2, 3));
        }

        [Test]
        public static void TestOperatorLessThan()
        {
            Test(new Fraction(-5, 6) < new Fraction(-2, 3),
                 true);
            Test(new Fraction(1, 5) < new Fraction(2, 3),
                 true);
            Test(new Fraction(-2, 3) < new Fraction(-1, 6),
                 true);
        }

        [Test]
        public static void TestOperatorBiggerThan()
        {
            Test(new Fraction(-1, 2) > new Fraction(-2, 3),
                 true);
            Test(new Fraction(1, 5) > new Fraction(2, 3),
                 false);
            Test(new Fraction(-2, 3) > new Fraction(-4, 5),
                 true);
        }

        [Test]
        public static void TestOperatorLessOrEqualThan()
        {
            Test(new Fraction(-1, 2) <= new Fraction(-1, 2),
                 true);
            Test(new Fraction(2, 3) <= new Fraction(2, 3),
                 true);
            Test(new Fraction(-5, 6) <= new Fraction(-10, 11),
                 false);
        }

        [Test]
        public static void TestOperatorBiggerOrEqualThan()
        {
            Test(new Fraction(-1, 3) >= new Fraction(-1, 3),
                 true);
            Test(new Fraction(1, 5) >= new Fraction(2, 3),
                 false);
            Test(new Fraction(-2, 3) >= new Fraction(-2, 1),
                 true);
        }

        [Test]
        public static void TestReverse()
        {
            Test(new Fraction(1, 2).Reverse(), new Fraction(2, 1));
            Test(new Fraction(-7, 4).Reverse(), new Fraction(-4, 7));
            Test(new Fraction(-8, 5).Reverse(), new Fraction(-5, 8));
        }

        [Test]
        public static void TestCreationFractionWithNewNumerator()
        {
            Test(new Fraction(1, 2).WithNumerator(3), new Fraction(3, 2));
            Test(new Fraction(4, 7).WithNumerator(-5), new Fraction(-5, 7));
            Test(new Fraction(2, -2).WithNumerator(-1), new Fraction(1, 1));
        }

        [Test]
        public static void TestCreationFractionWithNewDenominator()
        {
            Test(new Fraction(5, 2).WithDenominator(3), new Fraction(5, 3));
            Test(new Fraction(2, 9).WithDenominator(-3), new Fraction(-2, 3));
            Test(new Fraction(4, -4).WithDenominator(-1), new Fraction(1, 1));
        }

        [Test]
        public static void TestGetWholeNumber()
        {
            Test(new Fraction(5, 2).GetWholePart(), 2);
            Test(new Fraction(1, 2).GetWholePart(), 0);
            Test(new Fraction(-6, 2).GetWholePart(), -3);
            Test(new Fraction(-1, 3).GetWholePart(), 0);
            Test(new Fraction(5, 5).GetWholePart(), 1);
            Test(new Fraction(10, 2).GetWholePart(), 5);
            Test(new Fraction(3, 2).GetWholePart(), 1);
        }
    }
}