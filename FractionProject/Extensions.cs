namespace FractionProject
{
    public static class IntExtensions
    {
        public static int Length(this int digit)
        {
            return digit == 0 ? 1 : (int)Math.Log10(Math.Abs((double)digit));
        }
    }

    public static class DoubleExtensions
    {
        public static bool IsEqual(this double doubleNumber, double digit, double e)
        {
            return Math.Abs(doubleNumber - digit) < e;
        }
    }
}
