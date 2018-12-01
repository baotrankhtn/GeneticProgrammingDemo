using System;
namespace GeneticProgrammingDemo
{
    public class NumberUtils
    {
		public static double GenerateRandomDouble(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        
		public static int GenerateRamdomInteger(int minimum, int maximum) {
			Random rnd = new Random();
			return rnd.Next(minimum, maximum + 1);
		}
    }
}
