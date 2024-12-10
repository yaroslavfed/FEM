using FEM.SharedDTO.Domain;
using NonStationary.DTO.InputModels;

namespace NonStationary.Core.Services.CoilService;

public interface ICoilService
{
    Task<List<(int, Edge)>> GetCoilGridAsync(CoilParameters parameters);
}