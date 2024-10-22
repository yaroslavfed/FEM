using AutoMapper;
using FEM.Common.DTO.Enums;
using FEM.Common.DTO.Models.MeshModels;
using FEM.Common.Extensions;

namespace FEM.Server.Profiles;

/// <summary>
/// Общий профиль автомаппера
/// </summary>
public class CommonProfile : Profile
{
    /// <inheritdoc />
    public CommonProfile()
    {
        CreateFiniteElementMapper();
    }

    private void CreateFiniteElementMapper() =>
        CreateMap<FiniteElement, FiniteElementBounds>()
            .ForPath(
                dest => dest.HighCoordinate.X,
                opt => opt.MapFrom(
                    src => src
                           .Edges
                           .SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.X, EPositions.Last)
                )
            )
            .ForPath(
                dest => dest.HighCoordinate.Y,
                opt => opt.MapFrom(
                    src => src
                           .Edges
                           .SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.Y, EPositions.Last)
                )
            )
            .ForPath(
                dest => dest.HighCoordinate.Z,
                opt => opt.MapFrom(
                    src => src
                           .Edges
                           .SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.Z, EPositions.Last)
                )
            )
            .ForPath(
                dest => dest.LowCoordinate.X,
                opt => opt.MapFrom(
                    src => src
                           .Edges
                           .SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.X, EPositions.First)
                )
            )
            .ForPath(
                dest => dest.LowCoordinate.Y,
                opt => opt.MapFrom(
                    src => src
                           .Edges
                           .SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.Y, EPositions.First)
                )
            )
            .ForPath(
                dest => dest.LowCoordinate.Z,
                opt => opt.MapFrom(
                    src => src
                           .Edges
                           .SelectMany(edge => edge.Nodes)
                           .ToArray()
                           .GetBoundsPoint(x => x.Coordinate.Z, EPositions.First)
                )
            );
}