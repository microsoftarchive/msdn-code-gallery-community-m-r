# Prismで複合アプリケーションを作り始めるまでの手順
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- WPF
## Topics
- Prism
## Updated
- 01/13/2012
## Description

<h1>はじめに</h1>
<p><a href="http://compositewpf.codeplex.com/">Prism</a>は、MVVMアプリケーションを作るための機能を提供しているライブラリですが、本来はComposite Application Guidance for WPFとして登場した複合アプリケーションを作るためのものになります。</p>
<p>ここでは、MEFをベースにしてPrismで複合アプリケーションを作り始めるまでの手順を示します。</p>
<h1>プロジェクトの作成から参照の設定まで</h1>
<p>まず、WPFアプリケーションを作成します。ここではPrismEduという名前で作成しました。そして、NuGetでPrismで検索して出てくる下記のライブラリを追加します。</p>
<ul>
<li>Prism </li><li>Prism.MefExtensions </li></ul>
<p>そして、参照の追加からMEFを追加します。追加するのは、下記のものになります。</p>
<ul>
<li>System.ComponentModel.Composition </li></ul>
<h1>Shellの作成</h1>
<p>次に、Shellを作成します。MainWindow.xamlを消して新規作成からWindowをShellという名前で作成します。作成したら、XAMLを下記のように修正します。（RegionなどPrismの基本についてはヘルプなどを参照してください）</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>
<pre class="hidden">&lt;Window x:Class=&quot;PrismEdu.Shell&quot;
        xmlns=&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;
        xmlns:x=&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;
        xmlns:prism=&quot;http://www.codeplex.com/prism&quot;
        Title=&quot;Shell&quot; Height=&quot;300&quot; Width=&quot;300&quot;&gt;
    &lt;Grid&gt;
        &lt;!-- メインのリージョン --&gt;
        &lt;ContentControl
            Focusable=&quot;False&quot;
            prism:RegionManager.RegionName=&quot;Main&quot; /&gt;
    &lt;/Grid&gt;
&lt;/Window&gt;
</pre>
<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;Window</span>&nbsp;x:<span class="xaml__attr_name">Class</span>=<span class="xaml__attr_value">&quot;PrismEdu.Shell&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">xmlns</span>=<span class="xaml__attr_value">&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">x</span>=<span class="xaml__attr_value">&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">prism</span>=<span class="xaml__attr_value">&quot;http://www.codeplex.com/prism&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Title</span>=<span class="xaml__attr_value">&quot;Shell&quot;</span>&nbsp;<span class="xaml__attr_name">Height</span>=<span class="xaml__attr_value">&quot;300&quot;</span>&nbsp;<span class="xaml__attr_name">Width</span>=<span class="xaml__attr_value">&quot;300&quot;</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Grid</span><span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__comment">&lt;!--&nbsp;メインのリージョン&nbsp;--&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;ContentControl</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Focusable</span>=<span class="xaml__attr_value">&quot;False&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;prism:RegionManager.<span class="xaml__attr_name">RegionName</span>=<span class="xaml__attr_value">&quot;Main&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/Grid&gt;</span>&nbsp;
<span class="xaml__tag_end">&lt;/Window&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<p class="endscriptcode">そして、MEFからこのShellを取得できるようにExport属性をShellクラスに追加します。</p>
<p class="endscriptcode"></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System.ComponentModel.Composition;
using System.Windows;

namespace PrismEdu
{
    [Export]
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}
</pre>
<div class="preview">
<pre class="js">using&nbsp;System.ComponentModel.Composition;&nbsp;
using&nbsp;System.Windows;&nbsp;
&nbsp;
namespace&nbsp;PrismEdu&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Export]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;partial&nbsp;class&nbsp;Shell&nbsp;:&nbsp;Window&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;Shell()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p></p>
<h1 class="endscriptcode">&nbsp;Bootstrapperの作成</h1>
<div class="endscriptcode">次に、Bootstrapperを作成します。MEFを使用する場合のBootstrapperの基本クラスは「Microsoft.Practices.Prism.MefExtensions.MefBootstrapper」になります。このクラスを継承してスタートアップの処理を追加します。</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System;
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
                new DirectoryCatalog(&quot;.&quot;));
        }

        // Shellの作成
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue&lt;Shell&gt;();
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
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Linq;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Text;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Microsoft.Practices.Prism.MefExtensions;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Windows;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.ComponentModel.Composition.Hosting;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Reflection;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;PrismEdu&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;Bootstrapper&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;MefBootstrapper&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;MEFのカタログの初期化</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ConfigureAggregateCatalog()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">base</span>.ConfigureAggregateCatalog();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;自分自身とカレントディレクトリを対象にして全てのアセンブリを取り込む</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.AggregateCatalog.Catalogs.Add(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;AssemblyCatalog(Assembly.GetExecutingAssembly()));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.AggregateCatalog.Catalogs.Add(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;DirectoryCatalog(<span class="cs__string">&quot;.&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Shellの作成</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;DependencyObject&nbsp;CreateShell()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.Container.GetExportedValue&lt;Shell&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;ウィンドウの表示</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;InitializeShell()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">base</span>.InitializeShell();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;ShellをMainWindowに設定して表示</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Application.Current.MainWindow&nbsp;=&nbsp;<span class="cs__keyword">this</span>.Shell&nbsp;<span class="cs__keyword">as</span>&nbsp;Window;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Application.Current.MainWindow.Show();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;そして、App.xamlでStartupUriを消してStartupイベントを登録します。</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>
<pre class="hidden">&lt;Application x:Class=&quot;PrismEdu.App&quot;
             xmlns=&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;
             xmlns:x=&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;
             Startup=&quot;Application_Startup&quot;&gt;
    &lt;Application.Resources&gt;
         
    &lt;/Application.Resources&gt;
&lt;/Application&gt;
</pre>
<div class="preview">
<pre class="js">&lt;Application&nbsp;x:Class=<span class="js__string">&quot;PrismEdu.App&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xmlns=<span class="js__string">&quot;http://schemas.microsoft.com/winfx/2006/xaml/presentation&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xmlns:x=<span class="js__string">&quot;http://schemas.microsoft.com/winfx/2006/xaml&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Startup=<span class="js__string">&quot;Application_Startup&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;Application.Resources&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/Application.Resources&gt;&nbsp;
&lt;/Application&gt;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;Startupイベントでは先ほど作成したBootstrapperを使ってアプリケーションの開始処理を書きます。コードは下記のようになります。</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System.Windows;

namespace PrismEdu
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // 開始処理
            var b = new Bootstrapper();
            b.Run();
        }
    }
}
</pre>
<div class="preview">
<pre class="js">using&nbsp;System.Windows;&nbsp;
&nbsp;
namespace&nbsp;PrismEdu&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;partial&nbsp;class&nbsp;App&nbsp;:&nbsp;Application&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;<span class="js__operator">void</span>&nbsp;Application_Startup(object&nbsp;sender,&nbsp;StartupEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;開始処理</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;b&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;Bootstrapper();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;b.Run();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">&nbsp;実行して動作確認</h1>
<p class="endscriptcode">これで、空のShellだけが起動するアプリケーションが完成です。実行すると、下図のようにウィンドウが表示されます。</p>
<p class="endscriptcode">&nbsp;</p>
<div class="endscriptcode"><img src="48475-ws000000.jpg" alt="" width="300" height="299"></div>
</div>
<div class="endscriptcode"></div>
</div>
<h1 class="endscriptcode">モジュールの追加</h1>
<div class="endscriptcode">MEFをベースにしたPrismのアプリケーションを作成する土台が出来ました。最後にモジュールの追加方法について説明します。モジュールは、MEFを使った場合は属性を追加することで簡単に作成できます。</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace PrismEdu
{
    [ModuleExport(typeof(SampleModule))]
    public class SampleModule : IModule
    {
        public void Initialize()
        {
        }
    }
}
</pre>
<div class="preview">
<pre class="js">using&nbsp;System;&nbsp;
using&nbsp;System.Collections.Generic;&nbsp;
using&nbsp;System.Linq;&nbsp;
using&nbsp;System.Text;&nbsp;
using&nbsp;Microsoft.Practices.Prism.Modularity;&nbsp;
using&nbsp;Microsoft.Practices.Prism.MefExtensions.Modularity;&nbsp;
&nbsp;
namespace&nbsp;PrismEdu&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[ModuleExport(<span class="js__operator">typeof</span>(SampleModule))]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;class&nbsp;SampleModule&nbsp;:&nbsp;IModule&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;<span class="js__operator">void</span>&nbsp;Initialize()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;このようにMEFを使った場合は簡単に、モジュールを追加することが出来ます。動作確認のためSampleViewというボタンを置いただけのUserControlを表示するように書き換えてみます。まず、SampleViewクラスにExport属性を追加してMEFでの管理対象にします。このとき名前をつけてExportするようにします。</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace PrismEdu
{
    /// &lt;summary&gt;
    /// SampleView.xaml の相互作用ロジック
    /// &lt;/summary&gt;
    [Export(&quot;SampleView&quot;, typeof(SampleView))]
    public partial class SampleView : UserControl
    {
        public SampleView()
        {
            InitializeComponent();
        }
    }
}
</pre>
<div class="preview">
<pre class="js">using&nbsp;System.ComponentModel.Composition;&nbsp;
using&nbsp;System.Windows.Controls;&nbsp;
&nbsp;
namespace&nbsp;PrismEdu&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">///&nbsp;SampleView.xaml&nbsp;の相互作用ロジック</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Export(<span class="js__string">&quot;SampleView&quot;</span>,&nbsp;<span class="js__operator">typeof</span>(SampleView))]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;partial&nbsp;class&nbsp;SampleView&nbsp;:&nbsp;UserControl&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;SampleView()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">ここで指定した名前を使って画面に表示します。モジュールのコードを下記に示します。</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">スクリプトの編集</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace PrismEdu
{
    [ModuleExport(typeof(SampleModule))]
    public class SampleModule : IModule
    {
        // PrismのコンポーネントはMEFから設定してもらう
        [Import]
        public IRegionManager RegionManager { get; set; }

        public void Initialize()
        {
            // MainのRegionにSampleViewを表示する
            this.RegionManager.RequestNavigate(
                &quot;Main&quot;, &quot;SampleView&quot;);
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System.ComponentModel.Composition;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Microsoft.Practices.Prism.MefExtensions.Modularity;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Microsoft.Practices.Prism.Modularity;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Microsoft.Practices.Prism.Regions;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;PrismEdu&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[ModuleExport(<span class="cs__keyword">typeof</span>(SampleModule))]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;SampleModule&nbsp;:&nbsp;IModule&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;PrismのコンポーネントはMEFから設定してもらう</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[Import]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;IRegionManager&nbsp;RegionManager&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Initialize()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;MainのRegionにSampleViewを表示する</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.RegionManager.RequestNavigate(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;Main&quot;</span>,&nbsp;<span class="cs__string">&quot;SampleView&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;実行するとSampleViewが表示されます。</div>
<div class="endscriptcode"></div>
</div>
</div>
<div class="endscriptcode"><img src="48476-ws000001.jpg" alt="" width="300" height="302"></div>
</div>
</div>
<div class="endscriptcode"></div>
<h1 class="endscriptcode">まとめ</h1>
<div class="endscriptcode">以上で、簡単にですがMEFをベースにしたPrismアプリケーションの土台を作るまでの手順を説明しました。これをベースにRegionの設計をしたりModule分割をどうするか設計してアプリケーションを作りこんでいくことになると思います。以上です。</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
