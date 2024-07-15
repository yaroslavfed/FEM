using AutoMapper;

namespace FEM.Storage.Converter;

public class YamlConverter : IConverter
{
    private readonly IMapper _mapper;

    public YamlConverter(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<TOutput> ConvertTo<TInput, TOutput>(TInput input)
    {
        return Task.FromResult(_mapper.Map<TOutput>(input));
    }
}