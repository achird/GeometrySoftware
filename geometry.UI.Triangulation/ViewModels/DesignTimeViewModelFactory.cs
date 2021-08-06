using AutoMapper;

namespace geometry.UI.Triangulation.ViewModels
{
    public static class DesignTimeViewModelFactory
    {
        /// <summary>
        /// Модель главного окна
        /// </summary>
        public static TriangulationViewModel MainWindowViewModel => new(new MapperConfiguration(cfg => cfg.AddProfile(new Core.Triangulation.Application.Mapping.MapperProfile())).CreateMapper());
    }
}
