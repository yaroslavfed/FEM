using FEM.Common.Data.MathModels;
using FEM.Common.Data.MatrixFormats;

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
        
        public (Vector solve, double discrepancy, int iterCount) Solve(MatrixProfileFormat slae)
        {
            var (d, l, u) = ILUsqModi(slae);
            return LOSLU(slae, d, l, u);
        }

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