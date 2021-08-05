using AutoMapper;
using CSharpFunctionalExtensions;
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
            Height = 700;
            Width = 900;
            CanExecute = true;

            GenerateCommand = new CreateCommand(async () =>
            {
                CanExecute = false;
                OnPropertyChanged(nameof(CanExecute));
                MessageInfo = "Выполняется триангуляция...";
                OnPropertyChanged(nameof(MessageInfo));

                (await findTriangulationQueryHandler.HandleAsync(new FindTriangulationQuery()
                {
                    PointCount = PointCount,
                    LeftUpCorner = new PointDto() { X = 50, Y = 50 },
                    RightBottomCorner = new PointDto() { X = Width - 100, Y = Height - 100 }
                })).Tap(triangulation =>
                {
                    MessageInfo = "Триангуляция успешно выполнена";
                    Triangulation = triangulation;
                }).OnFailure(error =>
                {
                    MessageInfo = error;
                    Triangulation = default;
                });

                CanExecute = true;
                OnPropertyChanged(nameof(Triangulation));
                OnPropertyChanged(nameof(MessageInfo));
                OnPropertyChanged(nameof(CanExecute));
            });

            PointCount = 100;
            GenerateCommand.Execute(this);
        }

        /// <summary>
        /// Команда для запуска триангуляции
        /// </summary>
        public ICommand GenerateCommand { get; private set; }

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Caption { get; }

        /// <summary>
        /// Высота
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Ширина
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Команда выполняется?
        /// </summary>
        public bool CanExecute { get; private set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string MessageInfo { get; private set; }

        /// <summary>
        /// Количество точек
        /// </summary>
        public int PointCount { get; set; }


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
