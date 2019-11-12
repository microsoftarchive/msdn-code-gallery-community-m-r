using System.Collections.ObjectModel;
using System.Linq;
using Exemplo.WPF.MVVM.Model;
using Exemplo.WPF.MVVM.ViewModel;

namespace Exemplo.WPF.MVVM.Repository
{
    public class ClubeRepository
    {
        // Member variables
        private ClubeDeFutebolViewModel m_ViewModel;

        public ClubeRepository(ClubeDeFutebolViewModel viewModel)
        {
            m_ViewModel = viewModel;
        }

        public void Insert(ClubeDeFutebol entity)
        {
            ObservableCollection<ClubeDeFutebol> list = m_ViewModel.ListClubesDeFutebol;
            list.Add(entity);
            m_ViewModel.ListClubesDeFutebol = list;
        }

        public void Delete(ClubeDeFutebol entity)
        {
            ObservableCollection<ClubeDeFutebol> list = m_ViewModel.ListClubesDeFutebol;
            list.Remove(entity);
            m_ViewModel.ListClubesDeFutebol = list;
        }

        public void Update(ClubeDeFutebol entity)
        {
            ObservableCollection<ClubeDeFutebol> list = m_ViewModel.ListClubesDeFutebol;
            ClubeDeFutebol clube = m_ViewModel.ListClubesDeFutebol.FirstOrDefault(x => x.IdClube == entity.IdClube);
            list.Remove(clube);
            list.Add(entity);
            m_ViewModel.ListClubesDeFutebol = list;
        }
    }
}
