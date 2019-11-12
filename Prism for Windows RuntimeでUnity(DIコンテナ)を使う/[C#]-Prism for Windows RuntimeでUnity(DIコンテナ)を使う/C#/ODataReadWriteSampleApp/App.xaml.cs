using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using ODataReadWriteSampleApp.Models;
using ODataReadWriteSampleApp.ViewModels;
using ODataReadWriteSampleApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空のアプリケーション テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234227 を参照してください

namespace ODataReadWriteSampleApp
{
    /// <summary>
    /// 既定の Application クラスに対してアプリケーション独自の動作を実装します。
    /// </summary>
    sealed partial class App : MvvmAppBase
    {
        private UnityContainer container = new UnityContainer();

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            this.NavigationService.Navigate("Main", args.Arguments);
            return Task.FromResult<object>(null);
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // Modelのインスタンスをコンテナでシングルトンに管理してもらう
            container.RegisterType<AppContext>(new ContainerControlledLifetimeManager());

            // NavigationServiceのインスタンスをコンテナに登録
            container.RegisterInstance(this.NavigationService);

            return Task.FromResult<object>(null);
        }

        protected override object Resolve(Type type)
        {
            return this.container.Resolve(type);
        }
    }
}
