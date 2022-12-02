namespace FractionProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // < --- Check tests --- >

            var fraction = new Fraction(-123, 2);
            WriteLine(fraction.Visualize());
        }
    }

}