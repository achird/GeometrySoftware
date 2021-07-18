using AutoMapper;
using geometry.Core.Triangulation.Application.Interactor;
using geometry.Core.Triangulation.Application.Use.Query;
using geometry.UI.Triangulation.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace geometry.UI.Triangulation.ViewModels
{
    /// <summary>
    /// ViewModel для визуализации полученной триангуляции
    /// </summary>
    public class TriangulationViewModel : INotifyPropertyChanged
    {
        public TriangulationViewModel(IMapper mapper)
        {
            IFindTriangulationQueryHandler findTriangulationQueryHandler = new FindTriangulationQueryHandler(mapper);
            Caption = "Триангуляция Делоне (нажмите \"F1\", чтобы создать новую триангуляцию).";
            GenerateCommand = new CreateCommand(async () =>
            {
                Triangulation = await findTriangulationQueryHandler.HandleAsync(new FindTriangulationQuery()
                {
                    PointCount = 50,
                    LeftUpCorner = new PointDto() { X = 50, Y = 50 },
                    RightBottomCorner = new PointDto() { X = 850, Y = 650 }
                });

                OnPropertyChanged(nameof(Triangulation));
            });
            GenerateCommand.Execute(this);
        }

        /// <summary>
        /// Команда формирующая триангуляцию
        /// </summary>
        public ICommand GenerateCommand { get; private set; }

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Данные триангуляции
        /// </summary>
        public TriangulationDto Triangulation { get; private set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
