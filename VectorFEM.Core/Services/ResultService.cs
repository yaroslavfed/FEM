namespace VectorFEM.Core.Services
{
    internal class ResultService
    {
        public double GetSolveDifference(int layer)
        {
            int size = NumericalSolves[0].Length;
            Vector temp = new Vector(size);

            for (int i = 0; i < size; i++)
                temp[i] = NumericalSolves[layer][i] - AnalyticsSolves[layer][i];

            return temp.GetNorm() / NumericalSolves[layer].GetNorm();
        }
    }
}
