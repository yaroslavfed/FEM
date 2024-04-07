﻿using VectorFEM.Enums;
using VectorFEM.Models;
using VectorFEM.Models.VectorFEM;

namespace VectorFEM.Services.BasicFunctionResolver;

public class BasicFunctionsResolver<TData> : IBasicFunctionsResolver<TData>
{
    public IBasicFunction<TData> ResolveBasicFunctionStrategy(EFemType femType)
    {
        return femType switch
        {
            EFemType.Vector => (IBasicFunction<TData>)new BasicVectorFunction(),
            _ => throw new TypeAccessException()
        };
    }
}