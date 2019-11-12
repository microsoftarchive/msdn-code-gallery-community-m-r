using PrismWPFSampleApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace PrismWPFSampleApp.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly AppContext Model = AppContext.Instance;

        public ReadOnlyReactiveCollection<PersonViewModel> People { get; private set; }

        public ReactiveProperty<PersonViewModel> SelectedPerson { get; private set; }

        public ReactiveCommand LoadCommand { get; private set; }

        public ReactiveCommand DeleteCommand { get; private set; }

        public ReactiveCommand EditCommand { get; private set; }

        public ReactiveCommand AddCommand { get; private set; }

        public ReactiveProperty<PersonViewModel> InputPerson { get; private set; }

        public InteractionRequest<Confirmation> ConfirmRequest { get; private set; }

        public InteractionRequest<INotification> EditRequest { get; private set; }

        public MainWindowViewModel()
        {
            this.People = this.Model.Master.People
                .ToReadOnlyReactiveCollection(x => new PersonViewModel(x));

            this.ConfirmRequest = new InteractionRequest<Confirmation>();
            this.EditRequest = new InteractionRequest<INotification>();

            this.SelectedPerson = new ReactiveProperty<PersonViewModel>();

            this.InputPerson = this.Model.Master.ObserveProperty(x => x.InputPerson)
                .Select(x => new PersonViewModel(x))
                .ToReactiveProperty();

            this.AddCommand = this.InputPerson
                .SelectMany(x => x.HasErrors)
                .Select(x => !x)
                .ToReactiveCommand();
            this.AddCommand.Subscribe(_ => this.Model.Master.AddPerson());

            this.LoadCommand = new ReactiveCommand();
            this.LoadCommand.Subscribe(_ => this.Model.Master.Load());

            this.DeleteCommand = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveCommand();
            this.DeleteCommand
                .SelectMany(_ => this.ConfirmRequest.RaiseAsObservable(new Confirmation
                    {
                        Title = "確認",
                        Content = "削除しますか"
                    }))
                .Where(x => x.Confirmed)
                .Select(_ => this.SelectedPerson.Value.Model.ID)
                .Subscribe(x => this.Model.Master.Delete(x));

            this.EditCommand = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveCommand();
            this.EditCommand
                .Subscribe(_ =>
                {
                    this.Model.Detail.SetEditTarget(this.SelectedPerson.Value.Model.ID);
                    this.EditRequest.Raise(new Notification { Title = "編集" });
                });
        }
    }
}
