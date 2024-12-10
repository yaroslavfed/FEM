using FEM.SharedDTO.Domain;
using NonStationary.DTO.InputModels;

namespace NonStationary.Core.Services.CoilService;

public class CoilService : ICoilService
{

    public Task<List<(int, Edge)>> GetCoilGridAsync(CoilParameters parameters)
    {
        throw new NotImplementedException();
    }
}