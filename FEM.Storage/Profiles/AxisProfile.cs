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

        CreateMap<Splitting, Common.Data.InputModels.Splitting>()
            .ForMember(
                dest => dest.MultiplyCoefficient,
                opt => opt.MapFrom(src => src.Kr)
            )
            .ForMember(
                dest => dest.SplittingCoefficient,
                opt => opt.MapFrom(src => src.StepCount)
            )
            .ReverseMap()
            .ForMember(
                dest => dest.Kr,
                opt => opt.MapFrom(src => src.MultiplyCoefficient)
            )
            .ForMember(
                dest => dest.StepCount,
                opt => opt.MapFrom(src => src.SplittingCoefficient)
            );

        CreateMap<AdditionalParameters, Common.Data.InputModels.AdditionalParameters>().ReverseMap();
    }
}