﻿using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Models.Parallelepipedal.MassMatrix;

public interface IMassMatrix<TData>
{
    IReadOnlyList<IReadOnlyList<double>> MassMatrixBase { get; }

    Task<TData> GetMassMatrixAsync(double gamma, FiniteElement finiteElement);
}