

using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;

namespace SlaeSolver.Implementations.Solvers
{
    public class LosLU
    {
        private readonly int maxItCount;
        private readonly double eps;

        public LosLU(int maxItCount, double eps)
        {
            this.maxItCount = maxItCount;
            this.eps = eps;
        }

        public Vector Solve(MatrixProfileFormat slae)
        {
            var vectors = ILUsqModi(slae);
            var D = vectors.Item1;
            var L = vectors.Item2;
            var U = vectors.Item3;
            return LOSLU(slae, D, L, U);
        }


        private Vector LOSLU(MatrixProfileFormat slae, Vector D, Vector L, Vector U)
        {
            var size = slae.Size;
            double a, b;
            Vector solve = new Vector(size);
            Vector r, z, p, Ur, LAUr;

            int k = 0;

            r = ForwardStepModi(slae, slae.F, D, L);
            z = BackStepModi(slae, r, U);
            p = ForwardStepModi(slae, (slae * z).Data, D, L);

            do
            {
                k++;

                a = GetCoefficent(p, r, p, p);
                solve += a * z;
                r -= a * p;

                Ur = BackStepModi(slae, r, U);
                LAUr = ForwardStepModi(slae, (slae * Ur).Data, D, L);

                b = -GetCoefficent(p, LAUr, p, p);

                z = Ur + (b * z);

                p = LAUr + (b * p);

            } while (r * r > eps && k <= maxItCount);
            Console.WriteLine($"Solver iterations count - {k}");
            return solve;
        }

        private double GetCoefficent(Vector a1, Vector b1, Vector a2, Vector b2) => a1 * b1 / (a2 * b2);

        private (Vector, Vector, Vector) ILUsqModi(MatrixProfileFormat matrix)
        {
            var D = new Vector(matrix.Di);
            var L = new Vector(matrix.Gg);
            var U = new Vector(matrix.Gg);

            for (int i = 0; i < matrix.Size; i++)
            {
                double ls, us, d = 0;
                int temp = matrix.Ig[i];
                for (int j = matrix.Ig[i]; j < matrix.Ig[i + 1]; j++)
                {
                    ls = 0;
                    us = 0;
                    for (int h = matrix.Ig[matrix.Jg[j]], k = temp; h < matrix.Ig[matrix.Jg[j] + 1] && k < j;)
                        if (matrix.Jg[k] == matrix.Jg[h])
                        {
                            ls += L[k] * U[h];
                            us += L[h++] * U[k++];
                        }
                        else
                        {
                            if (matrix.Jg[k] < matrix.Jg[h])
                                k++;
                            else
                                h++;
                        }
                    L[j] -= ls;
                    U[j] = (U[j] - us) / D[matrix.Jg[j]];
                    d += L[j] * U[j];
                }
                D[i] -= d;
            }
            return (D, L, U);
        }

        private Vector ForwardStepModi(MatrixProfileFormat matrix, List<double> rightSide, Vector D, Vector L)
        {
            Vector result = new Vector(matrix.Size);

            for (int i = 0; i < matrix.Size; i++)
            {
                var sum = .0;
                for (int j = matrix.Ig[i]; j < matrix.Ig[i + 1]; j++)
                    sum += result[matrix.Jg[j]] * L[j];

                result[i] = (rightSide[i] - sum) / D[i];
            }
            return result;
        }

        private Vector BackStepModi(MatrixProfileFormat matrix, Vector rightSide, Vector U)
        {
            Vector result = new Vector(rightSide.Data);

            for (int i = matrix.Size - 1; i >= 0; i--)
                for (int j = matrix.Ig[i]; j < matrix.Ig[i + 1]; j++)
                    result[matrix.Jg[j]] -= result[i] * U[j];

            return result;
        }
    }
}