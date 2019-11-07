using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

namespace MetroTips018
{

  // Web カメラからのビデオをプレビューする方法 
  // http://msdn.microsoft.com/ja-jp/library/windows/apps/xaml/hh868171.aspx



  /// <summary>
  /// 多くのアプリケーションに共通の特性を指定する基本ページ。
  /// </summary>
  public sealed partial class MainPage : MetroTips018.Common.LayoutAwarePage
  {
    public MainPage()
    {
      this.InitializeComponent();

      Windows.ApplicationModel.Core.CoreApplication.Resuming += CoreApplication_Resuming;
      Window.Current.VisibilityChanged += Current_VisibilityChanged;
    }

    async void Current_VisibilityChanged(object sender, Windows.UI.Core.VisibilityChangedEventArgs e)
    {
      if (e.Visible)
      { 
        // バックグラウンドからフォラグラウンドに切り替わった

        await RestartPreviewAsync();
      }
    }

    async void CoreApplication_Resuming(object sender, object e)
    {
      // サスペンド状態からの復帰

      await RestartPreviewAsync();
    }

    private async Task RestartPreviewAsync()
    {
      Windows.Media.Capture.MediaCapture capture = capturePreview.Source;
      if (capture == null)
        return; //以前はキャプチャしていなかった

      // キャプチャオブジェクトを作り直し
      capture = new Windows.Media.Capture.MediaCapture();

      try
      {
        // Webカメラの初期化
        await capture.InitializeAsync();

        // 作り直した MediaCapture インスタンスを CaptureElement コントロールに設定する
        capturePreview.Source = capture;

        // 本当はフィルタも再設定しなければいけない

        // プレビューを再開する
        await capture.StartPreviewAsync();
      }
      catch
      {
        //(void)
      }
    }

    /// <summary>
    /// このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
    /// 再作成する場合は、保存状態も指定されます。
    /// </summary>
    /// <param name="navigationParameter">このページが最初に要求されたときに
    /// <see cref="Frame.Navigate(Type, Object)"/> に渡されたパラメーター値。
    /// </param>
    /// <param name="pageState">前のセッションでこのページによって保存された状態の
    /// ディクショナリ。ページに初めてアクセスするとき、状態は null になります。</param>
    protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
    {

    }


    /// <summary>
    /// アプリケーションが中断される場合、またはページがナビゲーション キャッシュから破棄される場合、
    /// このページに関連付けられた状態を保存します。値は、
    /// <see cref="SuspensionManager.SessionState"/> のシリアル化の要件に準拠する必要があります。
    /// </summary>
    /// <param name="pageState">シリアル化可能な状態で作成される空のディクショナリ。</param>
    protected override void SaveState(Dictionary<String, Object> pageState)
    {
    }





    // CameraCaptureUI コントロールを使って撮影
    private async void buttonCameraCaptureUI_Click(object sender, RoutedEventArgs e)
    {
      // http://coelacanth.heteml.jp/blog/metrostyleapp%E5%85%A5%E9%96%80-vol33-%E3%82%AB%E3%83%A1%E3%83%A9%E3%81%A7%E6%92%AE%E5%BD%B1%E3%81%97%E3%81%9F%E7%94%BB%E5%83%8F%E3%82%92%E4%BF%9D%E5%AD%98%E3%81%99%E3%82%8B/

      // ダイアログ生成
      var dialog = new Windows.Media.Capture.CameraCaptureUI();

      // 写真のフォーマットを指定
      dialog.PhotoSettings.Format = Windows.Media.Capture.CameraCaptureUIPhotoFormat.Png;

      //// クロップ枠を固定できる (縦横比だけ固定することも可能)
      //var size = new Size(500.0, 100.0);
      //dialog.PhotoSettings.CroppedSizeInPixels = size;
      ////dialog.PhotoSettings.CroppedAspectRatio = size;

      // 写真撮影モードでダイアログを起動
      var file = await dialog.CaptureFileAsync(Windows.Media.Capture.CameraCaptureUIMode.Photo);

      // 撮影できると、保存されたファイルの StorageFile が返ってくる。
      // 保存先は ApplicationData.Current.TemporaryFolder
      if (file != null)
      {
        var msgbox = new Windows.UI.Popups.MessageDialog(file.Path, "撮影結果");
        await msgbox.ShowAsync();
      }
    }




    private async void buttonPreviewClick(object sender, RoutedEventArgs e)
    {
      // 静止画撮影、動画録画をおこなうキャプチャーオブジェクト
      var _capture = new Windows.Media.Capture.MediaCapture();

      try
      {
        // Webカメラの初期化
        await _capture.InitializeAsync();
      }
      catch (UnauthorizedAccessException ex)
      {
        // "アクセスが拒否されました。 (HRESULT からの例外: 0x80070005 (E_ACCESSDENIED))"
        // Web カメラがユーザーに許可されていないと、
        // UnauthorizedAccessException が出る。
        ShowRequestMessageAsync(ex);
        return;
      }

      // MediaCapture インスタンスを CaptureElement コントロールに設定する
      capturePreview.Source = _capture;

      // プレビューを開始する
      await _capture.StartPreviewAsync();
    }


    private async void buttonPreviewWithVideoStabilizationClick(object sender, RoutedEventArgs e)
    {
      var _capture = new Windows.Media.Capture.MediaCapture();
      try
      {
        await _capture.InitializeAsync();
      }
      catch (UnauthorizedAccessException ex)
      {
        ShowRequestMessageAsync(ex);
        return;
      }



      // キャプチャしたビデオに手ブレ補正効果を追加する
      // http://msdn.microsoft.com/ja-jp/library/windows/apps/xaml/hh868169.aspx
      await _capture.AddEffectAsync(
                      Windows.Media.Capture.MediaStreamType.VideoRecord,
                      Windows.Media.VideoEffects.VideoStabilization,
                      null);



      capturePreview.Source = _capture;
      await _capture.StartPreviewAsync();
    }

    private async void buttonTakePhotoClick(object sender, RoutedEventArgs e)
    {
      if (capturePreview.Source == null)
        return;

      // プレビューに付けた MediaCapture オブジェクトを取り出す
      Windows.Media.Capture.MediaCapture capture = capturePreview.Source;

      // 保存する画像ファイルのフォーマット (pngファイルを指定)
      var imageProperties = Windows.Media.MediaProperties.ImageEncodingProperties.CreatePng();


      // 撮影する。撮影した画像の stream が返ってくる。
      // 自前のコードで WriteableBitmap に変換
      using (var memStream = new Windows.Storage.Streams.InMemoryRandomAccessStream())
      {
        try
        {
          await capture.CapturePhotoToStreamAsync(imageProperties, memStream);
          // キャプチャ開始から撮影までの間に許可を取り消されると、ここで例外が出る
          // 「指定されたファイル ハンドルへのアクセスが取り消されました。 (HRESULT からの例外: 0x80070326)」
        }
        catch (Exception ex)
        {
          ShowRequestMessageAsync(ex);
          return;
        }

        captureImage.Source = await memStream.ToWriteableBitmap();
      }


      // 撮影する。素直にいったんファイルに落とす方法

      //StorageFile file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync("test.png", CreationCollisionOption.GenerateUniqueName);
      //try
      //{
      //  await capture.CapturePhotoToStorageFileAsync(imageProperties, file);
      //  // キャプチャ開始から撮影までの間に許可を取り消されると、ここで例外が出る
      //  // 「指定されたファイル ハンドルへのアクセスが取り消されました。 (HRESULT からの例外: 0x80070326)」
      //}
      //catch (Exception ex)
      //{
      //  ShowRequestMessageAsync(ex);
      //  return;
      //}
      //using (var stream = await file.OpenReadAsync())
      //{
      //  var bi = new BitmapImage();
      //  bi.SetSource(stream);
      //  captureImage.Source = bi;
      //}
    }


    private async void ShowRequestMessageAsync(Exception ex)
    {
      var content = string.Format(
@"Web カメラを使えるようにしてください。

例外: {0}",
        ex.ToString());

      var msgbox = new Windows.UI.Popups.MessageDialog(content, "Web カメラが使えません");
      await msgbox.ShowAsync();
    }
  }



  // http://d.hatena.ne.jp/ch3cooh393/20120730/1343579773

  static class WriteableBitmapExtensions
  {
    public static async Task<WriteableBitmap> ToWriteableBitmap(this IRandomAccessStream stream)
    {
      // ストリームからピクセルデータを読み込む
      var decoder = await BitmapDecoder.CreateAsync(stream);
      var transform = new BitmapTransform();
      var pixelData = await decoder.GetPixelDataAsync(decoder.BitmapPixelFormat, decoder.BitmapAlphaMode,
          transform, ExifOrientationMode.RespectExifOrientation, ColorManagementMode.ColorManageToSRgb);
      var pixels = pixelData.DetachPixelData();

      // WriteableBitmapへピクセルデータをコピーする
      var bitmap = new WriteableBitmap((int)decoder.OrientedPixelWidth, (int)decoder.OrientedPixelHeight);
      using (var bmpStream = bitmap.PixelBuffer.AsStream()) // AsStream() には System.Runtime.InteropServices.WindowsRuntime が必要
      {
        bmpStream.Seek(0, SeekOrigin.Begin);
        bmpStream.Write(pixels, 0, (int)bmpStream.Length);
      }
      return bitmap;
    }
  }


}
