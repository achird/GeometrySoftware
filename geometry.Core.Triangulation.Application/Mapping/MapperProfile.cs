using AutoMapper;
using geometry.Core.Triangulation.Application.Use.Query;
using geometry.Core.Triangulation.Domain;
using geometry.Core.Triangulation.Domain.Common;

namespace geometry.Core.Triangulation.Application.Mapping
{
    /// <summary>
    /// Маппинг компонента
    /// </summary>
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Преобразование из Dto и в Dto
            CreateMap<TriangleDto, Triangle>(MemberList.Source).ReverseMap();
            CreateMap<PointDto, Point>(MemberList.Source).ReverseMap();
            CreateMap<Delaunay, TriangulationDto>(MemberList.Destination);
        }
    }
}
