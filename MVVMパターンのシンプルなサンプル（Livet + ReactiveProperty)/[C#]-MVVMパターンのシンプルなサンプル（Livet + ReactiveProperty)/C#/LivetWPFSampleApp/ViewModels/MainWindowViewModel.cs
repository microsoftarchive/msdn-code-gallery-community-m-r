using Livet;
using Livet.Messaging;
using LivetWPFSampleApp.Models;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Windows;

namespace LivetWPFSampleApp.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        /// <summary>
        /// 編集対象のデータ一覧
        /// </summary>
        public ReadOnlyReactiveCollection<PersonViewModel> People { get; private set; }

        /// <summary>
        /// モデルのインスタンス
        /// </summary>
        private readonly AppContext Model = AppContext.Instance;

        /// <summary>
        /// 追加用の入力ホルダー
        /// </summary>
        public ReactiveProperty<PersonViewModel> InputPerson { get; private set; }

        /// <summary>
        /// 選択中のデータ
        /// </summary>
        public ReactiveProperty<PersonViewModel> SelectedPerson { get; private set; }

        /// <summary>
        /// 追加
        /// </summary>
        public ReactiveProperty<bool> AddEnabled { get; private set; }

        /// <summary>
        /// 削除
        /// </summary>
        public ReactiveProperty<bool> DeleteEnabled { get; private set; }

        /// <summary>
        /// 編集
        /// </summary>
        public ReactiveProperty<bool> EditEnabled { get; private set; }

        public MainWindowViewModel()
        {
            // MのコレクションをVMのコレクションに変換
            this.People = this.Model.Master.People
                .ToReadOnlyReactiveCollection(x => new PersonViewModel(x))
                .AddTo(this.CompositeDisposable);

            this.SelectedPerson = new ReactiveProperty<PersonViewModel>();

            // 入力対象のPerson
            this.InputPerson = this.Model.Master
                .ObserveProperty(x => x.InputPerson)
                .Select(x => new PersonViewModel(x))
                .ToReactiveProperty()
                .AddTo(this.CompositeDisposable);

            // 入力対象のPersonにエラーがないときだけ押せる
            this.AddEnabled = this.InputPerson
                .Where(x => x != null)
                .SelectMany(x => x.HasErrors)
                .Select(x => !x)
                .ToReactiveProperty(false)
                .AddTo(this.CompositeDisposable);

            // 選択中の項目があるときだけ押せる
            this.DeleteEnabled = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveProperty()
                .AddTo(this.CompositeDisposable);

            // 選択中の項目があるときだけ押せる
            this.EditEnabled = this.SelectedPerson
                .Select(x => x != null)
                .ToReactiveProperty()
                .AddTo(this.CompositeDisposable);
        }

        public void Initialize()
        {
            this.Model.Master.Load();
        }

        public void Add()
        {
            this.Model.Master.AddPerson();
        }

        public void Delete(ConfirmationMessage message)
        {
            if (message.Response != true) { return; }

            this.Model.Master.Delete(this.SelectedPerson.Value.Model.ID);
        }

        public void Edit()
        {
            // 編集のターゲットを設定して画面を表示するメッセージを投げる
            this.Model.Detail.SetEditTarget(this.SelectedPerson.Value.Model.ID);
            this.Messenger.Raise(new TransitionMessage("EditWindowOpen"));
        }
    }
}
