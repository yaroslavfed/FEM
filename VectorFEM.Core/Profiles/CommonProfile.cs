using AutoMapper;
using FEM.Common.Enums;
using FEM.Common.Extensions;
using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Profiles;

public class CommonProfile : Profile
{
    public CommonProfile()
    {
        CreateFiniteElementMapper();
    }

    private void CreateFiniteElementMapper() =>
        CreateMap<FiniteElement, FiniteElementBounds>()
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