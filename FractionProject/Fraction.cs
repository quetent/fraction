using System.Threading.Tasks.Sources;

namespace FractionProject
{
    public class Fraction
    {
        private readonly int _numerator;
        public int Numerator { get { return _numerator; } }

        private readonly int _denominator;
        public int Denominator { get { return _denominator; } } 

        private readonly int signCoeff;

        private const double epsilon = 10e-12;
        private static readonly int[] dividers = { 2, 3, 5, 7 };

        private string? view;

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("denominator should be not equal to zero", nameof(denominator));

            signCoeff = numerator * denominator >= 0 ? 1 : -1;

            (var absNumerator, var absDenominator) = Abs(numerator, denominator);
            (var newNumerator, var newDenominator) = Simplify(absNumerator, absDenominator);

            _numerator = signCoeff * newNumerator;
            _denominator = newDenominator;
        }

        public static Fraction Abs(Fraction fraction)
        {
            (var absNumerator, var absDenominator) = Abs(fraction._numerator,
                                                         fraction._denominator);

            return new Fraction(absNumerator, absDenominator);
        }

        public static int GetWholePart(int number1, int number2)
        {
            return number1 / number2;
        }

        public static Fraction operator +(Fraction fraction1, Fraction fraction2)
        {
            if (IsDenominatorsEqual(fraction1, fraction2))
                return new Fraction(fraction1._numerator + fraction2._numerator,
                                    fraction1._denominator);

            var newDenominator = fraction1._denominator * fraction2._denominator;

            var newNumerator = CrossMultiplication(fraction1, fraction2)
                             + CrossMultiplication(fraction2, fraction1);

            return new Fraction(newNumerator, newDenominator);
        }

        public static Fraction operator -(Fraction fraction1, Fraction fraction2)
        {
            return fraction1 + fraction2.WithNumerator(fraction2._numerator * -1);
        }

        public static Fraction operator *(Fraction fraction1, Fraction fraction2)
        {
            return new Fraction(fraction1._numerator * fraction2._numerator,
                                fraction1._denominator * fraction2._denominator);
        }

        public static Fraction operator /(Fraction fraction1, Fraction fraction2)
        {
            return fraction1 * fraction2.Reverse();
        }

        public static bool operator >(Fraction fraction1, Fraction fraction2)
        {
            if (IsDenominatorsEqual(fraction1, fraction2))
                return fraction1._numerator > fraction2._numerator;

            return CrossMultiplication(fraction1, fraction2)
                 > CrossMultiplication(fraction2, fraction1);
        }

        public static bool operator <(Fraction fraction1, Fraction fraction2)
        {
            return fraction2 > fraction1;
        }

        public static bool operator >=(Fraction fraction1, Fraction fraction2)
        {
            return fraction1 > fraction2
                || fraction1 == fraction2;
        }

        public static bool operator <=(Fraction fraction1, Fraction fraction2)
        {
            return fraction2 >= fraction1;
        }

        public static bool operator ==(Fraction fraction1, Fraction fraction2)
        {
            return IsNumeratorsEqual(fraction1, fraction2)
                && IsDenominatorsEqual(fraction1, fraction2)
                && IsSignCoeffsEqual(fraction1, fraction2);
        }

        public static bool operator !=(Fraction fraction1, Fraction fraction2)
        {
            return !(fraction1 == fraction2);
        }

        public override string ToString()
        {
            var sign = signCoeff == 1 ? string.Empty : "- ";
            (var absNumerator, var absDenominator) = Abs(_numerator, _denominator);

            return $"{sign}{absNumerator} / {absDenominator}";
        }

        public string Visualize()
        {
            return GetView();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Fraction fraction)
                return this == fraction;

            return ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Abs(_numerator, _denominator), signCoeff);
        }

        public Fraction WithNumerator(int numerator)
        {
            if (signCoeff == -1 && numerator < 0)
                numerator = Math.Abs(numerator);

            return new Fraction(numerator, _denominator);
        }

        public Fraction WithDenominator(int denominator)
        {
            return new Fraction(_numerator, denominator);
        }

        public int GetWholePart()
        {
            return _numerator / _denominator;
        }

        public float AsFloat()
        {
            return (float)_numerator / _denominator;
        }

        public double AsDouble()
        {
            return (double)_numerator / _denominator;
        }

        public decimal AsDecimal()
        {
            return (decimal)_numerator / _denominator;
        }

        public Fraction Reverse()
        {
            return new Fraction(_denominator, _numerator);
        }

        private static string GetSpaceLine(int numeratorLength, int denominatorLength, int maxLength)
        {
            string spaces;
            if (numeratorLength == denominatorLength)
                spaces = string.Empty;
            else
                spaces = new string(' ', maxLength / 2);

            return spaces;
        }

        private static void AddSpaces(int numeratorLength, int denominatorLength,
                                      ref string numeratorAsString, ref string denominatorAsString,
                                      string spaces)
        {
            if (numeratorLength < denominatorLength)
                numeratorAsString = spaces + numeratorAsString;
            else
                denominatorAsString = spaces + denominatorAsString;
        }

        private static int CrossMultiplication(Fraction fraction1, Fraction fraction2)
        {
            return fraction1._numerator * fraction2._denominator;
        }

        private static (int, int) Simplify(int numerator, int denominator)
        {
            if (numerator == denominator)
                return (1, 1);

            if (IsDividedWithoutRemainder(numerator, denominator))
                return (GetWholePart(numerator, denominator), 1);

            if (IsDividedWithoutRemainder(denominator, numerator))
                return (1, GetWholePart(denominator, numerator));

            return BruteForceDividers(numerator, denominator);
        }

        private static (int, int) BruteForceDividers(int numerator, int denominator)
        {
            foreach (var divider in dividers)
            {
                while ((IsDividedWithoutRemainder(numerator, divider)
                     && IsDividedWithoutRemainder(denominator, divider)))
                {
                    numerator /= divider;
                    denominator /= divider;
                }
            }

            return (numerator, denominator);
        }

        private static bool IsDividedWithoutRemainder(int number, int divider)
        {
            return ((double)number / divider).IsEqual(GetWholePart(number, divider), epsilon);
        }

        private static (int, int) Abs(int number1, int number2)
        {
            return (Math.Abs(number1), Math.Abs(number2));
        }

        private static bool IsSignCoeffsEqual(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.signCoeff == fraction2.signCoeff;
        }

        private static bool IsNumeratorsEqual(Fraction fraction1, Fraction fraction2)
        {
            return IsAbsNumbersEqual(fraction1._numerator, fraction2._numerator);
        }

        private static bool IsDenominatorsEqual(Fraction fraction1, Fraction fraction2)
        {
            return IsAbsNumbersEqual(fraction1._denominator, fraction2._denominator);
        }

        private static bool IsAbsNumbersEqual(int number1, int number2)
        {
            return Math.Abs(number1) == Math.Abs(number2);
        }

        private bool IsWholeNumber()
        {
            return _numerator == 0 || _denominator == 1 || IsAbsNumbersEqual(_numerator, _denominator);
        }

        private string GetView()
        {
            if (view is null)
            {
                if (IsWholeNumber())
                {
                    view = _numerator.ToString();
                    return view;
                }

                var numeratorLength = _numerator.Length();
                var denominatorLength = _denominator.Length();
                var maxLength = Math.Max(numeratorLength, denominatorLength);

                var delimiter = new string('—', maxLength + 1);
                var spaces = GetSpaceLine(numeratorLength, denominatorLength, maxLength);

                (var numeratorAsString, var denominatorAsString) = GetFractionPartsAsString();

                AddSpaces(numeratorLength, denominatorLength,
                          ref numeratorAsString, ref denominatorAsString,
                          spaces);

                AddSign(ref delimiter, ref numeratorAsString, ref denominatorAsString);

                view = $"{numeratorAsString}\n{delimiter}\n{denominatorAsString}";
            }

            return view;
        }

        private (string, string) GetFractionPartsAsString()
        {
            (var absNumerator, var absDenominator) = Abs(_numerator, _denominator);

            var numeratorAsString = absNumerator.ToString();
            var denominatorAsString = absDenominator.ToString();

            return (numeratorAsString, denominatorAsString);
        }

        private void AddSign(ref string delimiter,
                             ref string numeratorAsString,
                             ref string denominatorAsString)
        {
            if (signCoeff == -1)
            {
                delimiter = "- " + delimiter;
                numeratorAsString = "  " + numeratorAsString;
                denominatorAsString = "  " + denominatorAsString;
            }
        }
    }
}
