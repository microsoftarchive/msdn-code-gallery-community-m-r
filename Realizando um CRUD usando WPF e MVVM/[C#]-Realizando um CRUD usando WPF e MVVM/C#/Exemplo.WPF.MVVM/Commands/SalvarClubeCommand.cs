using System;
using System.Linq;
using System.Windows.Input;
using Exemplo.WPF.MVVM.Model;
using Exemplo.WPF.MVVM.Repository;
using Exemplo.WPF.MVVM.ViewModel;

namespace Exemplo.WPF.MVVM.Commands
{
    public class SalvarClubeCommand : ICommand
    {
        #region Fields

        // Member variables
        private ClubeDeFutebolViewModel m_ViewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SalvarClubeCommand(ClubeDeFutebolViewModel viewModel)
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
        /// Inclui um novo clube ou altera um existente.
        /// </summary>
        public void Execute(object parameter)
        {
            var clube = new ClubeDeFutebol();
            clube.Nome = m_ViewModel.Nome;
            clube.Tecnico = m_ViewModel.Tecnico;
            clube.IDEstado = m_ViewModel.Estado;
            
            var clubeRepository = new ClubeRepository(m_ViewModel);
            
            //Valida se é uma edição ou inclusão de novo registro
            if (m_ViewModel.IDClube == 0)
            {
                clube.IdClube = m_ViewModel.ListClubesDeFutebol.Count + 1;
                clubeRepository.Insert(clube);
            }
            else
            {
                clube.IdClube = m_ViewModel.IDClube;
                clubeRepository.Update(clube);
            }

            m_ViewModel.IDClube = 0;
            m_ViewModel.Nome = string.Empty;
            m_ViewModel.Tecnico = string.Empty;
            m_ViewModel.Estado = null;
        }

        #endregion
    }
}
