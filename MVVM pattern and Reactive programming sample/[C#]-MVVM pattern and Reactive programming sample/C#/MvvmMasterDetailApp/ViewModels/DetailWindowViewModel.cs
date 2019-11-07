using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using GalaSoft.MvvmLight;
using MvvmMasterDetailApp.Models;
using System;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;

namespace MvvmMasterDetailApp.ViewModels
{
    public class DetailWindowViewModel : ViewModelBase
    {
        public ReactiveProperty<PersonViewModel> EditTarget { get; private set; }

        public ReactiveCommand CommitCommand { get; private set; }

        public ReactiveCommand DeleteCommand { get; private set; }

        public ReactiveCommand CancelCommand { get; private set; }

        public DetailWindowViewModel(AppContext app)
        {
            this.EditTarget = app.Detail
                .ObserveProperty(x => x.EditTarget)
                .Where(x => x != null)
                .Select(x => new PersonViewModel(x))
                .ToReactiveProperty();

            this.CommitCommand = this.EditTarget
                .SelectMany(x => x.HasError)
                .Select(x => !x)
                .ToReactiveCommand();
            this.CommitCommand.Subscribe(_ =>
                {
                    app.Detail.Update();
                    this.MessengerInstance.Send(new MessageBase(this, "CloseWindow"));
                });

            this.DeleteCommand = new ReactiveCommand();
            this.DeleteCommand.Subscribe(_ =>
                {
                    app.Detail.Delete();
                    this.MessengerInstance.Send(new MessageBase(this, "CloseWindow"));
                });

            this.CancelCommand = new ReactiveCommand();
            this.CancelCommand.Subscribe(_ => this.MessengerInstance.Send(new MessageBase(this, "CloseWindow")));
        }
    }
}
