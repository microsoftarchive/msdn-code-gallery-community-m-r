using GalaSoft.MvvmLight;
using MyCompany.Vacation.Client.WindowsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Vacation.Client.WindowsStore
{
    /// <summary>
    /// Main viewmodel.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<VacationRequest> vacationRequests;

        /// <summary>
        /// Main view model contructor.
        /// </summary>
        public MainViewModel() {
            this.InitializeData();
        }

        /// <summary>
        /// Vacation requests.
        /// </summary>
        public ObservableCollection<VacationRequest> VacationRequests
        {
            get { return vacationRequests; }
            set
            {
                vacationRequests = value;
                RaisePropertyChanged(() => VacationRequests);
            }
        }

        private async void InitializeData()
        {
            VacationRequests = new ObservableCollection<VacationRequest>(await GetVacationRequests());
        }

        private async Task<IEnumerable<VacationRequest>> GetVacationRequests()
        {
            // TODO: Get Items code here
            return await Task.Run(() => new List<VacationRequest>());
        }

    }
}
