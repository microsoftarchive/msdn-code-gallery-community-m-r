using System.Windows;
using Exemplo.WPF.MVVM.ViewModel;

namespace Exemplo.WPF.MVVM.View
{
    public partial class MainWindow : Window
    {
        #region Construtor

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ClubeDeFutebolViewModel();
        }

        #endregion
    }
}