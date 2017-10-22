using System;

namespace FunctionalInterpolation
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

			double[] x = { -3, -1, 0, 1 };
			double[] f = { 3, -2, 0, 3 };
			LagrangePolynomial.Method (x, f);
			NewtonPolynomial.Method (x, f);
			//MinSquares.Method(x, f, 1);
		}
	}
}
