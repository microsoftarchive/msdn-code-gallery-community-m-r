using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MvvmMasterDetailApp.Models;
using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MvvmMasterDetailApp.Commons;

namespace MvvmMasterDetailApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ReadOnlyReactiveCollection<PersonViewModel> People { get; private set; }

        public ReactiveProperty<PersonViewModel> SelectedPerson { get; private set; }

        public ReactiveCommand LoadCommand { get; private set; }

        public ReactiveCommand EditCommand { get; private set; }

        public ReactiveCommand AddCommand { get; private set; }

        public MainWindowViewModel(AppContext app)
        {
            this.People = app
                .Master
                .People
                .ToReadOnlyReactiveCollection(x => new PersonViewModel(x));

            this.SelectedPerson = new ReactiveProperty<PersonViewModel>();

            this.LoadCommand = new ReactiveCommand();
            this.LoadCommand.Subscribe(_ => app.Master.Load());

            this.EditCommand = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveCommand();
            this.EditCommand.Subscribe(_ => 
                {
                    app.Detail.SetEditTarget(this.SelectedPerson.Value.Person.Id);
                    this.MessengerInstance.Send(new MessageBase(this, "EditWindow"));
                });

            this.AddCommand = new ReactiveCommand();
            this.AddCommand.Subscribe(_ => app.Master.AddPerson());
        }
    }
}
