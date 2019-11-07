using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.MefExtensions;
using System.Windows;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace PrismEdu
{
    public class Bootstrapper
        : MefBootstrapper
    {
        // MEFのカタログの初期化
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            // 自分自身とカレントディレクトリを対象にして全てのアセンブリを取り込む
            this.AggregateCatalog.Catalogs.Add(
                new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            this.AggregateCatalog.Catalogs.Add(
                new DirectoryCatalog("."));
        }

        // Shellの作成
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        // ウィンドウの表示
        protected override void InitializeShell()
        {
            base.InitializeShell();
            // ShellをMainWindowに設定して表示
            Application.Current.MainWindow = this.Shell as Window;
            Application.Current.MainWindow.Show();
        }
    }
}
