using AutoMapper;

namespace FEM.Storage.Converter;

public class ConverterService : IConverterService
{
    private readonly IMapper _mapper;

    public ConverterService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<TOutput> ConvertTo<TInput, TOutput>(TInput input) => Task.FromResult(_mapper.Map<TOutput>(input));
}