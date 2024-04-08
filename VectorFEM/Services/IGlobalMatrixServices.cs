using VectorFEM.Data;
using VectorFEM.Enums;

namespace VectorFEM.Services;

public interface IGlobalMatrixServices
{
    Matrix GetGlobalMatrix(double mu, double gamma, FiniteElement element, EFemType femType);
}