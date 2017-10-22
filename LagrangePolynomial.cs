using System;

namespace FunctionalInterpolation
{
	public static class LagrangePolynomial
	{
		public static void Method(double[] x, double[] f)
		{
			Polynomial result = new Polynomial();

			for (int i = 0; i < x.Length; i++)
			{
				Polynomial multi = new Polynomial(1);

				for (int j = 0; j < x.Length; j++)
					if (i != j)
						multi *= 1.0d / (x [i] - x [j]) * new Polynomial (-x [j], 1);
				
				multi *= f[i];


				result += multi;
			}
			result.WritePoly();
		}

	}
}

