using System;
using System.Windows.Input;
using Exemplo.WPF.MVVM.Repository;
using Exemplo.WPF.MVVM.ViewModel;

namespace Exemplo.WPF.MVVM.Commands
{
    public class DeletarClubeCommand : ICommand
    {
        #region Fields

        private ClubeDeFutebolViewModel m_ViewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DeletarClubeCommand(ClubeDeFutebolViewModel viewModel)
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
        /// Remove um clube da lista.
        /// </summary>
        public void Execute(object parameter)
        {
            var clube = m_ViewModel.Clube;
            
            var clubeRepository = new ClubeRepository(m_ViewModel);
            
            // Remove um clube da lista.
            clubeRepository.Delete(clube);

            m_ViewModel.Nome = string.Empty;
            m_ViewModel.Tecnico = string.Empty;
            m_ViewModel.Estado = null;
            m_ViewModel.IDClube = 0;
        }

        #endregion
    }
}
