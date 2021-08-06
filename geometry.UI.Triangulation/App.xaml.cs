using AutoMapper;
using geometry.UI.Triangulation.ViewModels;
using System.Windows;

namespace geometry.UI.Triangulation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new MainWindow
            {
                DataContext = new TriangulationViewModel(new MapperConfiguration(cfg => cfg.AddProfile(new Core.Triangulation.Application.Mapping.MapperProfile())).CreateMapper())
            }.Show();
        }
    }
}
