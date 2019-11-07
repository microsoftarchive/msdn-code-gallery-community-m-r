using Livet;
using Livet.Messaging.Windows;
using LivetWPFSampleApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Linq;

namespace LivetWPFSampleApp.ViewModels
{
    public class EditWindowViewModel : ViewModel
    {
        /// <summary>
        /// Modelのインスタンス
        /// </summary>
        private readonly AppContext Model = AppContext.Instance;

        /// <summary>
        /// 編集対象
        /// </summary>
        public ReactiveProperty<PersonViewModel> EditTarget { get; private set; }

        /// <summary>
        /// 更新
        /// </summary>
        public ReactiveProperty<bool> UpdateEnabled { get; private set; }

        public EditWindowViewModel()
        {
            // EditTargetをVMに変換してReactiveProperty化
            this.EditTarget = this.Model.Detail
                .ObserveProperty(x => x.EditTarget)
                .Select(x => new PersonViewModel(x))
                .ToReactiveProperty()
                .AddTo(this.CompositeDisposable);

            // EditTargetにエラーがないときだけ押せる
            this.UpdateEnabled = this.EditTarget
                .SelectMany(x => x.HasErrors)
                .Select(x => !x)
                .ToReactiveProperty()
                .AddTo(this.CompositeDisposable);
        }

        public void Initialize()
        {
        }

        public void Update()
        {
            // 更新して外部へ変更通知
            this.Model.Detail.Update();
            this.Messenger.Raise(new WindowActionMessage(WindowAction.Close, "WindowClose"));
        }
    }
}
