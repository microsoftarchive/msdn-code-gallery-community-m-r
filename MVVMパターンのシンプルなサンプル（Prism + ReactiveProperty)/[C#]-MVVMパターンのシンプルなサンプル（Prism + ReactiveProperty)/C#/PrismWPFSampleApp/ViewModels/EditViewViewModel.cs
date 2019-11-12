using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using PrismWPFSampleApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace PrismWPFSampleApp.ViewModels
{
    public class EditViewViewModel : IInteractionRequestAware
    {
        // Model
        private readonly AppContext Model = AppContext.Instance;

        // IInteractionRequestAware
        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }

        public ReactiveProperty<PersonViewModel> EditTarget { get; private set; }

        public ReactiveCommand CommitCommand { get; private set; }

        public EditViewViewModel()
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
                    this.FinishInteraction();
                });
        }
    }
}
