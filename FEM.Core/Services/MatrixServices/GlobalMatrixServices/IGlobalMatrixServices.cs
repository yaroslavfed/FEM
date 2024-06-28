using FEM.Core.Enums;
using FEM.Shared.Domain.Data;
using FEM.Shared.Domain.MathModels;

namespace FEM.Core.Services.MatrixServices.GlobalMatrixServices;

public interface IGlobalMatrixServices
{
    Matrix GetGlobalMatrix(double mu, double gamma, FiniteElement element, EFemType femType);
}