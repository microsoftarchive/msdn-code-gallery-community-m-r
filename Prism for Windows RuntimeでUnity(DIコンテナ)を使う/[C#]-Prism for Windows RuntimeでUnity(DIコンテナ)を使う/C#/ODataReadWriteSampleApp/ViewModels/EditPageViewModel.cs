using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Unity;
using ODataReadWriteSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;

namespace ODataReadWriteSampleApp.ViewModels
{
    /// <summary>
    /// 編集ページ
    /// </summary>
    public class EditPageViewModel : ViewModel
    {
        /// <summary>
        /// Modelのルート
        /// </summary>
        private AppContext model;

        /// <summary>
        /// 画面遷移のためのサービス
        /// </summary>
        private INavigationService navigationService;

        /// <summary>
        /// 変更をサーバーに反映する処理のコマンド
        /// </summary>
        public DelegateCommand CommitCommand { get; private set; }

        private PersonViewModel editTarget;

        /// <summary>
        /// 現在の編集対象
        /// </summary>
        public PersonViewModel EditTarget
        {
            get { return this.editTarget; }
            set { this.SetProperty(ref this.editTarget, value); }
        }

        /// <summary>
        /// デザインタイム用コンストラクタ
        /// </summary>
        public EditPageViewModel()
        {
            if (!DesignMode.DesignModeEnabled)
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// モデルと画面遷移のサービスで初期化するコンストラクタ
        /// </summary>
        /// <param name="model"></param>
        /// <param name="navigationService"></param>
        [InjectionConstructor]
        public EditPageViewModel(AppContext model, INavigationService navigationService)
        {
            this.model = model;
            this.navigationService = navigationService;

            this.CommitCommand = DelegateCommand.FromAsyncHandler(this.CommitExecuteAsync);
        }

        public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
            // サスペンドからの復帰のときはデータを読み込む
            if (navigationMode == NavigationMode.Refresh)
            {
                await this.model.PeopleModel.LoadPeopleAsync();
            }

            // 指定したIDのPersonを編集対象に設定する
            var id = (int)navigationParameter;

            this.model.PeopleModel.SetEditTarget(id);
            this.EditTarget = new PersonViewModel(this.model.PeopleModel.EditTarget);
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);
            // 編集対象をリセットする
            this.model.PeopleModel.EditTarget = null;
        }

        /// <summary>
        /// サーバーに変更を反映する
        /// </summary>
        /// <returns></returns>
        private async Task CommitExecuteAsync()
        {
            // VM -> Mへ変更内容を反映
            this.EditTarget.ApplyChanged();
            // 編集対象のデータの変更をサーバーに反映
            await this.model.PeopleModel.SaveEditTargetChangsAsync();
            // 一覧ページへ戻る
            this.navigationService.GoBack();
        }

    }
}
