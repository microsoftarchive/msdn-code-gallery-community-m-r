using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// 空のアプリケーション テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234227 を参照してください

namespace MetroTips091CS
{
  /// <summary>
  /// 既定の Application クラスに対してアプリケーション独自の動作を実装します。
  /// </summary>
  public sealed partial class App : Application
  {
#if WINDOWS_PHONE_APP
    private TransitionCollection transitions;
#endif

    /// <summary>
    /// 単一アプリケーション オブジェクトを初期化します。これは、実行される作成したコードの
    /// 最初の行であり、main() または WinMain() と論理的に等価です。
    /// </summary>
    public App()
    {
      this.InitializeComponent();
      this.Suspending += this.OnSuspending;
    }

    /// <summary>
    /// アプリケーションがエンド ユーザーによって正常に起動されたときに呼び出されます。他のエントリ ポイントは、
    /// アプリケーションが特定のファイルを開くために呼び出されたときに
    /// 検索結果やその他の情報を表示するために使用されます。
    /// </summary>
    /// <param name="e">起動要求とプロセスの詳細を表示します。</param>
    protected override async void OnLaunched(LaunchActivatedEventArgs e)
    {
      // Metro TIPS #81
      // 追加したOnActivatedメソッドと共通するコードを、CreateRootFrameAsyncメソッドに切り出した
      await CreateRootFrameAsync(e);

      // 現在のウィンドウがアクティブであることを確認します
      Window.Current.Activate();
    }

    // Metro TIPS #81
    // OnLaunchedメソッドとOnActivatedメソッドに共通するコードを切り出した。
    // 引数はOnLaunchedメソッドではLaunchActivatedEventArgsクラスだが、
    // OnActivatedメソッドからも使えるようにするためにIActivatedEventArgsインターフェイスに変更した。
    private async System.Threading.Tasks.Task CreateRootFrameAsync(IActivatedEventArgs e)
    {
#if DEBUG
      if (System.Diagnostics.Debugger.IsAttached)
      {
        this.DebugSettings.EnableFrameRateCounter = true;
      }
#endif




      Frame rootFrame = Window.Current.Content as Frame;

      // ウィンドウに既にコンテンツが表示されている場合は、アプリケーションの初期化を繰り返さずに、
      // ウィンドウがアクティブであることだけを確認してください
      if (rootFrame == null)
      {
        // ナビゲーション コンテキストとして動作するフレームを作成し、最初のページに移動します
        rootFrame = new Frame();

        // 既定の言語を設定します
        rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

        // Metro TIPS #81 追加
        //フレームを SuspensionManager キーに関連付けます 
        Common.SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

        // TODO: この値をアプリケーションに適切なキャッシュ サイズに変更します
        rootFrame.CacheSize = 1;

        if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
        {
          //TODO: 以前中断したアプリケーションから状態を読み込みます。

          // Metro TIPS #81 追加
          try
          {
            await Common.SuspensionManager.RestoreAsync();
          }
          catch (Common.SuspensionManagerException)
          {
            //Something went wrong restoring state.
            //Assume there is no state and continue
          }
        }

        // フレームを現在のウィンドウに配置します
        Window.Current.Content = rootFrame;
      }

      if (rootFrame.Content == null)
      {
#if WINDOWS_PHONE_APP
        // スタートアップのターンスタイル ナビゲーションを削除します。
        if (rootFrame.ContentTransitions != null)
        {
          this.transitions = new TransitionCollection();
          foreach (var c in rootFrame.ContentTransitions)
          {
            this.transitions.Add(c);
          }
        }

        rootFrame.ContentTransitions = null;
        rootFrame.Navigated += this.RootFrame_FirstNavigated;
#endif

        // ナビゲーションの履歴スタックが復元されていない場合、最初のページに移動します。
        // このとき、必要な情報をナビゲーション パラメーターとして渡して、新しいページを
        // 作成します

        // Metro TIPS #81 追加／変更
        // メソッドの引数をIActivatedEventArgsインターフェイスに変更したため、
        // 常にe.Argumentsが存在するとは限らなくなった。
        // ILaunchActivatedEventArgsインターフェイスでもあるとき（＝Argumentsプロパティが存在するとき）
        // のみ、引数を取り出して使うようにする。
        string launchArguments = string.Empty;
        var lea = e as ILaunchActivatedEventArgs;
        if(lea != null)
          launchArguments = lea.Arguments;

        if (!rootFrame.Navigate(typeof(MainPage), launchArguments))
        {
          throw new Exception("Failed to create initial page");
        }
      }
    }


#if WINDOWS_PHONE_APP
    /// <summary>
    /// アプリを起動した後のコンテンツの移行を復元します。
    /// </summary>
    /// <param name="sender">ハンドラーがアタッチされたオブジェクト。</param>
    /// <param name="e">ナビゲーション イベントの詳細。</param>
    private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
    {
      var rootFrame = sender as Frame;
      rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
      rootFrame.Navigated -= this.RootFrame_FirstNavigated;
    }
#endif

    /// <summary>
    /// アプリケーションの実行が中断されたときに呼び出されます。アプリケーションの状態は、
    /// アプリケーションが終了されるのか、メモリの内容がそのままで再開されるのか
    /// わからない状態で保存されます。
    /// </summary>
    /// <param name="sender">中断要求の送信元。</param>
    /// <param name="e">中断要求の詳細。</param>
    private void OnSuspending(object sender, SuspendingEventArgs e)
    {
      var deferral = e.SuspendingOperation.GetDeferral();

      // TODO: アプリケーションの状態を保存してバックグラウンドの動作があれば停止します

      deferral.Complete();
    }



    // Metro TIPS #81
    protected override async void OnActivated(IActivatedEventArgs args)
    {
      base.OnActivated(args);

      // Phoneでは、ファイルオープンピッカーを開いている間に、アプリ自体が中断→終了
      // されてしまうことがある（［ライフサイクルイベント］ドロップダウンでエミュレート可能）。
      // そのため、↓このように必ずフレームの作成処理を行わねばならない。
      await CreateRootFrameAsync(args);

      Window.Current.Activate();
    }

  }
}