using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MVVMLightWPFSampleApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMLightWPFSampleApp.ViewModels
{
    public class EditWindowViewModel : ViewModelBase
    {
        // Model
        private readonly AppContext Model = AppContext.Instance;

        public ReactiveProperty<PersonViewModel> EditTarget { get; private set; }

        public ReactiveCommand CommitCommand { get; private set; }

        public EditWindowViewModel()
        {
            this.EditTarget = this.Model.Detail
                .ObserveProperty(x => x.EditTarget)
                .Where(x => x != null)
                .Select(x => new PersonViewModel(x))
                .ToReactiveProperty();

            this.CommitCommand = this.EditTarget
                .Where(x => x != null)
                .SelectMany(x => x.HasErrors)
                .Select(x => !x)
                .ToReactiveCommand();
            this.CommitCommand.Subscribe(_ =>
                {
                    this.Model.Detail.Update();
                    this.MessengerInstance.Send(new MessageBase(this, "CloseWindow"));
                });
        }
    }
}
