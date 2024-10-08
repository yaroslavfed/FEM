﻿using Client.Shared.Data;
using Client.Shared.HttpClientContext;

namespace Client.Shared.Services.TestingService;

public interface ITestingService
{
    Task<FemResponse> CreateSessionAsync(Data.TestSession testSession);

    Task<TestResult?> GetSessionResultAsync(Guid id);
}