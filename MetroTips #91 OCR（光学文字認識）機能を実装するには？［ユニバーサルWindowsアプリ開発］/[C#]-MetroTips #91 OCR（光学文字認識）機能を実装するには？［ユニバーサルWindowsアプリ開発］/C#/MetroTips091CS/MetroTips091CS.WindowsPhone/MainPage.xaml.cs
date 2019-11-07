using MetroTips091CS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 基本ページの項目テンプレートについては、http://go.microsoft.com/fwlink/?LinkID=390556 を参照してください

namespace MetroTips091CS
{
  /// <summary>
  /// Frame 内へナビゲートするために利用する空欄ページ。
  /// </summary>
  public sealed partial class MainPage : Page
  {
    private NavigationHelper navigationHelper;
    private ObservableDictionary defaultViewModel = new ObservableDictionary();

    public MainPage()
    {
      this.InitializeComponent();

      this.navigationHelper = new NavigationHelper(this);
      this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
      this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
    }

    /// <summary>
    /// この <see cref="Page"/> に関連付けられた <see cref="NavigationHelper"/> を取得します。
    /// </summary>
    public NavigationHelper NavigationHelper
    {
      get { return this.navigationHelper; }
    }

    /// <summary>
    /// この <see cref="Page"/> のビュー モデルを取得します。
    /// これは厳密に型指定されたビュー モデルに変更できます。
    /// </summary>
    public ObservableDictionary DefaultViewModel
    {
      get { return this.defaultViewModel; }
    }

    /// <summary>
    /// このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
    /// 再作成する場合は、保存状態も指定されます。
    /// </summary>
    /// <param name="sender">
    /// イベントのソース (通常、<see cref="NavigationHelper"/>)
    /// </param>
    /// <param name="e">このページが最初に要求されたときに
    /// <see cref="Frame.Navigate(Type, Object)"/> に渡されたナビゲーション パラメーターと、
    /// 前のセッションでこのページによって保存された状態の辞書を提供する
    /// イベント データ。ページに初めてアクセスするとき、状態は null になります。</param>
    private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
    {
    }

    /// <summary>
    /// アプリケーションが中断される場合、またはページがナビゲーション キャッシュから破棄される場合、
    /// このページに関連付けられた状態を保存します。値は、
    /// <see cref="SuspensionManager.SessionState"/> のシリアル化の要件に準拠する必要があります。
    /// </summary>
    /// <param name="sender">イベントのソース (通常、<see cref="NavigationHelper"/>)</param>
    /// <param name="e">シリアル化可能な状態で作成される空のディクショナリを提供するイベント データ
    ///。</param>
    private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
    {
    }

    #region NavigationHelper の登録

    /// <summary>
    /// このセクションに示したメソッドは、NavigationHelper がページの
    /// ナビゲーション メソッドに応答できるようにするためにのみ使用します。
    /// <para>
    /// ページ固有のロジックは、
    /// <see cref="NavigationHelper.LoadState"/>
    /// および <see cref="NavigationHelper.SaveState"/>。
    /// LoadState メソッドでは、前のセッションで保存されたページの状態に加え、
    /// ナビゲーション パラメーターを使用できます。
    /// </para>
    /// </summary>
    /// <param name="e">ナビゲーション要求を取り消すことのできないナビゲーション メソッドおよびイベント
    /// ハンドラーにデータを提供します。</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      this.navigationHelper.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      this.navigationHelper.OnNavigatedFrom(e);
    }

    #endregion


  }
}
