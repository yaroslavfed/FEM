using AutoMapper;
using Client.Shared.HttpClientContext;

namespace Client.Shared.Profiles;

public class TestSessionProfile : Profile
{
    public TestSessionProfile()
    {
        CreateMap<TestSession, Data.TestSession>().ReverseMap();
        CreateMap<MeshParameters, Data.MeshParameters>().ReverseMap();
        CreateMap<SplittingParameters, Data.SplittingParameters>().ReverseMap();
        CreateMap<AdditionParameters, Data.AdditionParameters>().ReverseMap();
    }
}