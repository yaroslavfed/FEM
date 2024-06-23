using AutoMapper;
using FEM.Core.Enums;
using FEM.Core.Extensions;
using FEM.Shared.Domain;
using FEM.Shared.Domain.Dto;

namespace FEM.Core.Profiles;

public class CommonProfile : Profile
{
    public CommonProfile()
    {
        CreateFiniteElementMapper();
    }

    private void CreateFiniteElementMapper()
    {
        CreateMap<FiniteElement, FiniteElementDto>()
            .ForMember(dest => dest.HighCoordinate.X,
                opt => opt.MapFrom(src =>
                    src.Edges.SelectMany(edge => edge.Nodes).ToArray()
                        .GetBoundsPoint(x => x.Coordinate.X, EPosition.Last)))
            .ForMember(dest => dest.HighCoordinate.Y,
                opt => opt.MapFrom(src =>
                    src.Edges.SelectMany(edge => edge.Nodes).ToArray()
                        .GetBoundsPoint(x => x.Coordinate.Y, EPosition.Last)))
            .ForMember(dest => dest.HighCoordinate.Z,
                opt => opt.MapFrom(src =>
                    src.Edges.SelectMany(edge => edge.Nodes).ToArray()
                        .GetBoundsPoint(x => x.Coordinate.Z, EPosition.Last)))
            .ForMember(dest => dest.LowCoordinate.X,
                opt => opt.MapFrom(src =>
                    src.Edges.SelectMany(edge => edge.Nodes).ToArray()
                        .GetBoundsPoint(x => x.Coordinate.X, EPosition.First)))
            .ForMember(dest => dest.LowCoordinate.Y,
                opt => opt.MapFrom(src =>
                    src.Edges.SelectMany(edge => edge.Nodes).ToArray()
                        .GetBoundsPoint(x => x.Coordinate.Y, EPosition.First)))
            .ForMember(dest => dest.LowCoordinate.Z,
                opt => opt.MapFrom(src =>
                    src.Edges.SelectMany(edge => edge.Nodes).ToArray()
                        .GetBoundsPoint(x => x.Coordinate.Z, EPosition.First)));
    }
}