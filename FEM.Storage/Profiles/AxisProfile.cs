using AutoMapper;
using FEM.Storage.FileStorage.YamlStorage.Data;

namespace FEM.Storage.Profiles;

public class AxisProfile : Profile
{
    public AxisProfile()
    {
        CreateYamlProfileMap();
    }

    private void CreateYamlProfileMap()
    {
        CreateMap<Positioning, Common.Data.InputModels.Positioning>().ReverseMap();
        CreateMap<Splitting, Common.Data.InputModels.Splitting>().ReverseMap();
        CreateMap<AdditionalParameters, Common.Data.InputModels.AdditionalParameters>().ReverseMap();
        CreateMap<TestingSettings, Common.Data.InputModels.TestingSettings>().ReverseMap();
    }
}