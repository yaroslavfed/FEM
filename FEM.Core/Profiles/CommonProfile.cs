using AutoMapper;
using FEM.Common.Data.Domain;
using FEM.Core.Data.Dto;
using FEM.Core.Enums;
using FEM.Core.Extensions;

namespace FEM.Core.Profiles;

public class CommonProfile : Profile
{
    public CommonProfile()
    {
        CreateFiniteElementMapper();
    }

    private void CreateFiniteElementMapper() =>
        CreateMap<FiniteElement, FiniteElementDto>()
            .ForPath(
                dest => dest.HighCoordinate.X,
                opt => opt.MapFrom(
                    src =>
                        src.Edges.SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.X, EPosition.Last)
                )
            )
            .ForPath(
                dest => dest.HighCoordinate.Y,
                opt => opt.MapFrom(
                    src =>
                        src.Edges.SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.Y, EPosition.Last)
                )
            )
            .ForPath(
                dest => dest.HighCoordinate.Z,
                opt => opt.MapFrom(
                    src =>
                        src.Edges.SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.Z, EPosition.Last)
                )
            )
            .ForPath(
                dest => dest.LowCoordinate.X,
                opt => opt.MapFrom(
                    src =>
                        src.Edges.SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.X, EPosition.First)
                )
            )
            .ForPath(
                dest => dest.LowCoordinate.Y,
                opt => opt.MapFrom(
                    src =>
                        src.Edges.SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.Y, EPosition.First)
                )
            )
            .ForPath(
                dest => dest.LowCoordinate.Z,
                opt => opt.MapFrom(
                    src =>
                        src.Edges.SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.Z, EPosition.First)
                )
            );
}