using System;

namespace FunctionalInterpolation
{
	public static class MinSquares
	{
		public static void Method(double[] x, double[] f, int m) 
		{
			double[,] aMatrix = new double[m + 1, m + 1];
			double[] right = new double[m + 1];

			for (int i = 0; i <= m; i++)
			{
				for (int p = 0; p < x.Length; p++)
					right[i] += Math.Pow(x[p], i) * f[p];

				for (int j = 0; j <= m; j++)
					for (int p = 0; p < x.Length; p++)
						aMatrix[i, j] += Math.Pow(x[p], i + j);
			}

			Matrix A = aMatrix, 
			F = right;

			Polynomial result = new Polynomial((double[])GaussMethod(A, F));
			result.WritePoly ();
		}

		public static Matrix GaussMethod(Matrix matrix, Matrix f)
		{
			int[] order = new int[matrix.ColumnsCount];

			for (int i = 0; i < order.Length; i++)
				order[i] = i;

			for (int i = 0; i < matrix.RowsCount; i++)
			{
				if (matrix[i, i] == 0)
				{
					int index = FindNoZeroColumn(matrix, i);

					matrix.SwapColumns(i, index);

					int tmp = order[i];
					order[i] = order[index];
					order[index] = i;
				}

				MakeCoeficientEqualToOne(ref matrix, ref f, i);
				MakeDownEqualToZero(ref matrix, ref f, i);
			}

			Matrix solution = RetrieveSolution(matrix, f);
			for (int i = 0; i < matrix.ColumnsCount; i++)
			{
				double tmp = solution[order[i], 0];
				solution[order[i], 0] = solution[i, 0];
				solution[i, 0] = tmp;
			}

			return solution;
		}
		private static int FindNoZeroColumn(Matrix matrix, int startIndex)
		{
			for (int i = startIndex; i < matrix.ColumnsCount; i++)
			{
				if (matrix[startIndex, i] != 0)
					return i;
			}

			return -1;
		}
		private static void MakeCoeficientEqualToOne(ref Matrix matrix, ref Matrix f, int row)
		{
			if (matrix[row, row] == 1)
				return;

			double coef = 1 / matrix[row, row];

			for (int i = row; i < matrix.ColumnsCount; i++)
				matrix[row, i] *= coef;

			f[row, 0] *= coef;
		}
		private static void MakeDownEqualToZero(ref Matrix matrix, ref Matrix f, int row)
		{
			for (int i = row + 1; i < matrix.RowsCount; i++)
			{
				double coef = -matrix[i, row];

				for (int j = row; j < matrix.ColumnsCount; j++)
					matrix[i, j] += matrix[row, j] * coef;

				f[i, 0] += f[row, 0] * coef;
			}
		}

		private static Matrix RetrieveSolution(Matrix matrix, Matrix f)
		{
			double[] solution = new double[matrix.ColumnsCount];
			bool[] isInitialize = new bool[matrix.ColumnsCount];

			for (int i = 0; i < solution.Length; i++)
			{
				solution[i] = 1;
				isInitialize[i] = false;
			}

			for (int i = matrix.RowsCount - 1; i >= 0; i--)
			{
				int index = FindNoZeroColumn(matrix, i);

				double newSolution = 0;

				for (int j = index + 1; j < matrix.ColumnsCount; j++)
				{
					newSolution -= matrix[i, j] * solution[j];
				}

				newSolution += f[i, 0];
				newSolution /= matrix[i, index];

				solution[index] = newSolution;
				isInitialize[i] = true;
			}

			return solution;
		}
	}
}

