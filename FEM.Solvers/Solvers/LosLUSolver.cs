using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;

namespace FEM.Solvers.Solvers
{
    public class LosLUSolver : ISolver
    {
        private readonly int _maxItCount;
        private readonly double _eps;

        public LosLUSolver(int maxItCount, double eps)
        {
            _maxItCount = maxItCount;
            _eps = eps;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // TODO: тестовая хрень
        private MatrixProfileFormat temp = new()
        {
            Di =
            {
                82.69444444444444, 82.69444444444444, 82.69444444444444, 82.69444444444444, 82.69444444444444,
                82.69444444444444, 82.69444444444444, 82.69444444444444
            },
            Gg =
            {
                36.34722222222222, 34.34722222222222, 14.673611111111112, 14.673611111111112, 34.34722222222222,
                36.34722222222222, -46.77777777777778, -25.88888888888889, -26.88888888888889, -14.694444444444445,
                -25.88888888888889, -46.77777777777778, -14.694444444444445, -26.88888888888889, 36.34722222222222,
                -26.88888888888889, -14.694444444444445, -46.77777777777778, -25.88888888888889, 34.34722222222222,
                14.673611111111112, -14.694444444444445, -26.88888888888889, -25.88888888888889, -46.77777777777778,
                14.673611111111112, 34.34722222222222, 36.34722222222222
            },
            Ig = { 0, 0, 1, 3, 6, 10, 15, 21, 28 },
            Jg = { 0, 0, 1, 0, 1, 2, 0, 1, 2, 3, 0, 1, 2, 3, 4, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 6 },
            F = { -1987.96875, -2119.6875, -1377.1875, 958.125, 1976.71875, 6147.1875, 7024.6875, 18436.875 }
        };

        public (Vector solve, double discrepancy, int iterCount) Solve(MatrixProfileFormat slae)
        {
            var (d, l, u) = ILUsqModi(slae); // TODO: заменить обратно
            return LOSLU(slae, d, l, u);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private (Vector solve, double discrepancy, int iterCount) LOSLU(MatrixProfileFormat slae, Vector d, Vector l,
            Vector u)
        {
            var size = slae.Size;
            var solve = new Vector(size);

            var k = 0;

            var r = ForwardStepModi(slae, slae.F, d, l);
            var z = BackStepModi(slae, r, u);
            var p = ForwardStepModi(slae, (slae * z).Data, d, l);

            do
            {
                k++;

                var a = GetCoefficient(p, r, p, p);
                solve += a * z;
                r -= a * p;

                var ur = BackStepModi(slae, r, u);
                var laUr = ForwardStepModi(slae, (slae * ur).Data, d, l);

                var b = -GetCoefficient(p, laUr, p, p);

                z = ur + (b * z);

                p = laUr + (b * p);
            } while (r * r > _eps && k <= _maxItCount);

            return (solve, r * r, k);
        }

        private double GetCoefficient(Vector a1, Vector b1, Vector a2, Vector b2) => a1 * b1 / (a2 * b2);

        private (Vector, Vector, Vector) ILUsqModi(MatrixProfileFormat matrix)
        {
            var D = new Vector(matrix.Di);
            var L = new Vector(matrix.Gg);
            var U = new Vector(matrix.Gg);

            for (var i = 0; i < matrix.Size; i++)
            {
                double d = 0;
                var temp = matrix.Ig[i];
                for (var j = matrix.Ig[i]; j < matrix.Ig[i + 1]; j++)
                {
                    double ls = 0;
                    double us = 0;
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

        private Vector ForwardStepModi(MatrixProfileFormat matrix, List<double> rightSide, Vector d, Vector l)
        {
            var result = new Vector(matrix.Size);

            for (var i = 0; i < matrix.Size; i++)
            {
                var sum = .0;
                for (var j = matrix.Ig[i]; j < matrix.Ig[i + 1]; j++)
                    sum += result[matrix.Jg[j]] * l[j];

                result[i] = (rightSide[i] - sum) / d[i];
            }

            return result;
        }

        private Vector BackStepModi(MatrixProfileFormat matrix, Vector rightSide, Vector u)
        {
            var result = new Vector(rightSide.Data);

            for (var i = matrix.Size - 1; i >= 0; i--)
            for (var j = matrix.Ig[i]; j < matrix.Ig[i + 1]; j++)
                result[matrix.Jg[j]] -= result[i] * u[j];

            return result;
        }
    }
}