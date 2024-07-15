using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Core.Enums;
using FEM.Shared.Domain.Data;

namespace FEM.Core.Services.MatrixServices.GlobalMatrixServices;

public interface IGlobalMatrixServices
{
    Matrix GetGlobalMatrix(double mu, double gamma, FiniteElement element, EFemType femType);
}