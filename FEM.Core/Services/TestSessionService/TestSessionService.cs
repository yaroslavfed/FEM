﻿using FEM.Common.Data.TestSession;
using FEM.Core.Data.Parallelepipedal;
using FEM.Core.Services.Parallelepipedal.MeshService;

namespace FEM.Core.Services.TestSessionService;

public class TestSessionService : ITestSessionService
{
    private readonly IMeshService _meshService;

    public TestSessionService(IMeshService meshService)
    {
        _meshService = meshService;
    }

    public async Task<TestSession<Mesh>> CreateTestSessionAsync()
    {
        var mesh = await _meshService.GenerateMeshAsync();
        var testConfiguration = await _meshService.GenerateTestConfiguration();

        return await Task.FromResult(
            new TestSession<Mesh>
            {
                Mesh = mesh,
                Mu = testConfiguration.AdditionalParameters.Mu,
                Gamma = testConfiguration.AdditionalParameters.Gamma,
                BoundaryCondition = testConfiguration.AdditionalParameters.BoundaryCondition - 1
            }
        );
    }
}