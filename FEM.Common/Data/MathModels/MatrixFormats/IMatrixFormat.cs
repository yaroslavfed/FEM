namespace FEM.Common.Data.MathModels.MatrixFormats;

public interface IMatrixFormat
{
    Task<IMatrixFormat> CreateProfileArraysAsync(List<List<int>> positionsList);

    Task AddElementToGlobalMatrixAsync(int i, int j, double element);

    Task AddElementToRightPartAsync(int index, double coefficient);
}