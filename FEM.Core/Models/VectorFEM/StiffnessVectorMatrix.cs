﻿using AutoMapper;
using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Common.Extensions;
using FEM.Core.Data.Dto;
using FEM.Core.Extensions;

namespace FEM.Core.Models.VectorFEM;

internal class StiffnessVectorMatrix : IStiffnessMatrix<Matrix>
{
    private readonly FiniteElementBounds _feBounds;

    private readonly IReadOnlyList<IReadOnlyList<double>> _stiffnessMatrix1 =
    [
        [2, 1, -2, -1],
        [1, 2, -1, -2],
        [-2, -1, 2, 1],
        [-1, -2, 1, 2]
    ];

    private readonly IReadOnlyList<IReadOnlyList<double>> _stiffnessMatrix2 =
    [
        [2, -2, 1, -1],
        [-2, 2, -1, 1],
        [1, -1, 2, -2],
        [-1, 1, -2, 2]
    ];

    private readonly IReadOnlyList<IReadOnlyList<double>> _stiffnessMatrix3 =
    [
        [-2, 2, -1, 1],
        [-1, 1, -2, 2],
        [2, -2, 1, -1],
        [1, -1, 2, -2]
    ];

    public StiffnessVectorMatrix(
        FiniteElement finiteElement,
        IMapper mapper
    )
    {
        _feBounds = mapper.Map<FiniteElementBounds>(finiteElement);
    }

    public Matrix GetStiffnessMatrix(double mu)
    {
        var x0 = _feBounds.LowCoordinate.X;
        var xn = _feBounds.HighCoordinate.X;

        var y0 = _feBounds.LowCoordinate.Y;
        var yn = _feBounds.HighCoordinate.Y;

        var z0 = _feBounds.LowCoordinate.Z;
        var zn = _feBounds.HighCoordinate.Z;

        var m111 = new Matrix { Data = _stiffnessMatrix1 };
        m111 *= (xn - x0) * (yn - y0) / 6 * (zn - z0);
        var m112 = new Matrix { Data = _stiffnessMatrix2 };
        m112 *= (xn - x0) * (zn - z0) / 6 * (yn - y0);
        var m11 = m111 + m112;

        var m221 = new Matrix { Data = _stiffnessMatrix1 };
        m221 *= (xn - x0) * (yn - y0) / 6 * (zn - z0);
        var m222 = new Matrix { Data = _stiffnessMatrix2 };
        m222 *= (yn - y0) * (zn - z0) / 6 * (xn - x0);
        var m22 = m221 + m222;

        var m331 = new Matrix { Data = _stiffnessMatrix1 };
        m331 *= (xn - x0) * (zn - z0) / 6 * (yn - y0);
        var m332 = new Matrix { Data = _stiffnessMatrix2 };
        m332 *= (yn - y0) * (zn - z0) / 6 * (xn - x0);
        var m33 = m331 + m332;

        var m121 = new Matrix { Data = _stiffnessMatrix2 };
        m121 *= (zn - z0) / -6;
        var m12 = m121;

        var m131 = new Matrix { Data = _stiffnessMatrix3 };
        m131 *= (yn - y0) / 6;
        var m13 = m131;

        var m31 = m13.Transpose();

        var m231 = new Matrix { Data = _stiffnessMatrix1 };
        m231 *= (xn - x0) / -6;
        var m23 = m231;

        var array = new double[12, 12];

        for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                array[i, j] = m11.Data[i][j];

        for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                array[i + 4, j + 4] = m22.Data[i][j];

        for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                array[i + 8, j + 8] = m33.Data[i][j];

        for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                array[i, j + 4] = m12.Data[i][j];

        for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                array[i, j + 8] = m13.Data[i][j];

        for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                array[i + 4, j] = m12.Data[i][j];

        for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                array[i + 4, j + 8] = m23.Data[i][j];

        for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                array[i + 8, j] = m31.Data[i][j];

        for (var i = 0; i < 4; i++)
            for (var j = 0; j < 4; j++)
                array[i + 8, j + 4] = m23.Data[i][j];

        var matrix = new Matrix { Data = array.ArrayToList() };

        matrix *= 1 / mu;

        return matrix;
    }
}