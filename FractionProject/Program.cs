namespace FractionProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // < --- Check tests --- >

            var fraction = new Fraction(2, -4);
            WriteLine(fraction.Visualize());
        }
    }

}