using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Unity;
using ODataReadWriteSampleApp.Models;
using ODataReadWriteSampleApp.Models.OData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;

namespace ODataReadWriteSampleApp.ViewModels
{
    /// <summary>
    /// 一覧ページ
    /// </summary>
    public class MainPageViewModel : ViewModel
    {
        /// <summary>
        /// Modelのルート
        /// </summary>
        private AppContext model;

        /// <summary>
        /// 画面遷移サービス
        /// </summary>
        private INavigationService navigationService;

        private ReadOnlyObservableCollection<Person> people;

        /// <summary>
        /// Personを表示するためのコレクション
        /// </summary>
        public ReadOnlyObservableCollection<Person> People
        {
            get { return this.people; }
            set { this.SetProperty(ref this.people, value); }
        }

        /// <summary>
        /// データを読み込むコマンド
        /// </summary>
        public DelegateCommand LoadPeopleCommand { get; private set; }

        /// <summary>
        /// 編集ページへの画面遷移コマンド
        /// </summary>
        public DelegateCommand<Person> NavigateEditPageCommand { get; private set; }

        /// <summary>
        /// デザインタイム用コンストラクタ
        /// </summary>
        public MainPageViewModel()
        {
            if (!DesignMode.DesignModeEnabled)
            {
                throw new InvalidOperationException("design time only");
            }
        }

        /// <summary>
        /// Modelと画面遷移サービスで初期化するコンストラクタ
        /// </summary>
        /// <param name="model"></param>
        /// <param name="navigationService"></param>
        [InjectionConstructor]
        public MainPageViewModel(AppContext model, INavigationService navigationService)
        {
            this.model = model;
            this.navigationService = navigationService;

            this.LoadPeopleCommand = DelegateCommand.FromAsyncHandler(this.LoadPeopleAsync);
            this.NavigateEditPageCommand = new DelegateCommand<Person>(this.NavigateEditPage);
        }

        /// <summary>
        /// 画面遷移コマンドの処理
        /// </summary>
        /// <param name="target"></param>
        private void NavigateEditPage(Person target)
        {
            // タップされたPersonのIDをパラメータとして渡して編集ページへ遷移する
            this.navigationService.Navigate("Edit", target.ID);
        }

        /// <summary>
        /// データ読み込みコマンドの処理
        /// </summary>
        /// <returns></returns>
        private async Task LoadPeopleAsync()
        {
            // データを読み込んでコレクションに設定する
            await this.model.PeopleModel.LoadPeopleAsync();
            this.People = new ReadOnlyObservableCollection<Person>(this.model.PeopleModel.People);
        }

        public override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
            // すでに読み込まれたデータがある場合はそれを表示する
            if (this.model.PeopleModel.People != null)
            {
                this.People = new ReadOnlyObservableCollection<Person>(this.model.PeopleModel.People);
            }
        }
    }
}
