using System;
using System.Windows.Input;
using Exemplo.WPF.MVVM.ViewModel;

namespace Exemplo.WPF.MVVM.Commands
{
    public class CarregarDadosClubeCommand: ICommand
    {
        #region Fields

        // Member variables
        private ClubeDeFutebolViewModel m_ViewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CarregarDadosClubeCommand(ClubeDeFutebolViewModel viewModel)
        {
            m_ViewModel = viewModel;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// 
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return m_ViewModel.Clube != null;
        }

        /// <summary>
        /// Actions to take when CanExecute() changes.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Carrega dados de um clube para edição.
        /// </summary>
        public void Execute(object parameter)
        {
            var clube = m_ViewModel.Clube;

            m_ViewModel.Nome = clube.Nome;
            m_ViewModel.Tecnico = clube.Tecnico;
            m_ViewModel.Estado = clube.IDEstado;
            m_ViewModel.IDClube = clube.IdClube;
            m_ViewModel.Clube = null;
        }

        #endregion
}
}
