using System;
using System.Windows.Input;
using Exemplo.WPF.MVVM.ViewModel;

namespace Exemplo.WPF.MVVM.Commands
{
    public class CancelarEdicaoCommand: ICommand
    {
        #region Fields

        private ClubeDeFutebolViewModel m_ViewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CancelarEdicaoCommand(ClubeDeFutebolViewModel viewModel)
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
            if (!string.IsNullOrEmpty(m_ViewModel.Nome) && !string.IsNullOrEmpty(m_ViewModel.Tecnico) && m_ViewModel.Estado != null)
                return true;
            else
                return false;
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
        ///Cancela a edição de um clube.
        /// </summary>
        public void Execute(object parameter)
        {
            m_ViewModel.Nome = string.Empty;
            m_ViewModel.Tecnico = string.Empty;
            m_ViewModel.Estado = null;
            m_ViewModel.IDClube = 0;
        }

        #endregion
    }
}
