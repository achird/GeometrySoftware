using AutoMapper;
using Xunit;

namespace geometry.Test.Core.Triangulation.Application
{
    public class MappingTest
    {
        [Fact]
        public void MappingConfigTest()
        {
            new MapperConfiguration(cfg => 
                cfg.AddProfile(new geometry.Core.Triangulation.Application.Mapping.MapperProfile())
            ).AssertConfigurationIsValid();
        }
    }
}
