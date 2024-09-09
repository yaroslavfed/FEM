﻿using FEM.Common.Data.Domain;

namespace FEM.Core.Models.Parallelepipedal.BasicFunction;

public interface IBasicFunction<out TData>
{
    TData GetBasicFunctions(int? number = null, Sensor? sensor = null);
}