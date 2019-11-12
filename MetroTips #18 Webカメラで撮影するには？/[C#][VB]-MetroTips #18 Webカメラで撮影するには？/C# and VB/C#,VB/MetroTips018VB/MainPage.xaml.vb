Imports Windows.Storage

' 基本ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234237 を参照してください

''' <summary>
''' 多くのアプリケーションに共通の特性を指定する基本ページ。
''' </summary>
Public NotInheritable Class MainPage
  Inherits Common.LayoutAwarePage

  ''' <summary>
  ''' このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
  ''' 再作成する場合は、保存状態も指定されます。
  ''' </summary>
  ''' <param name="navigationParameter">このページが最初に要求されたときに
  ''' パラメーター値は <see cref="Frame.Navigate"/> に渡されました。
  ''' </param>
  ''' <param name="pageState">前のセッションでこのページによって保存された状態の
  ''' ディクショナリ。ページに初めてアクセスするとき、状態は null になります。</param>
  Protected Overrides Sub LoadState(navigationParameter As Object, pageState As Dictionary(Of String, Object))

  End Sub

  ''' <summary>
  ''' アプリケーションが中断される場合、またはページがナビゲーション キャッシュから破棄される場合、
  ''' このページに関連付けられた状態を保存します。値は、
  ''' <see cref="Common.SuspensionManager.SessionState"/> のシリアル化の要件に準拠する必要があります。
  ''' </summary>
  ''' <param name="pageState">シリアル化可能な状態で作成される空のディクショナリ。</param>
  Protected Overrides Sub SaveState(pageState As Dictionary(Of String, Object))

  End Sub



  ' CameraCaptureUI コントロールを使って撮影
  Private Async Sub buttonCameraCaptureUI_Click(sender As Object, e As RoutedEventArgs)
    ' http://coelacanth.heteml.jp/blog/metrostyleapp%E5%85%A5%E9%96%80-vol33-%E3%82%AB%E3%83%A1%E3%83%A9%E3%81%A7%E6%92%AE%E5%BD%B1%E3%81%97%E3%81%9F%E7%94%BB%E5%83%8F%E3%82%92%E4%BF%9D%E5%AD%98%E3%81%99%E3%82%8B/

    ' ダイアログ生成
    Dim dialog = New Windows.Media.Capture.CameraCaptureUI()

    ' 写真のフォーマットを指定
    dialog.PhotoSettings.Format _
      = Windows.Media.Capture.CameraCaptureUIPhotoFormat.Png

    '' クロップ枠を固定できる (縦横比だけ固定することも可能)
    'Dim size = New Size(500.0, 100.0)
    'dialog.PhotoSettings.CroppedSizeInPixels = size
    ''dialog.PhotoSettings.CroppedAspectRatio = size

    ' 写真撮影モードでダイアログを起動
    Dim file = Await dialog.CaptureFileAsync(
                        Windows.Media.Capture.CameraCaptureUIMode.Photo)

    ' 撮影できると、保存されたファイルの StorageFile が返ってくる。
    ' 保存先は ApplicationData.Current.TemporaryFolder
    If (file IsNot Nothing) Then
      Dim msgbox = New Windows.UI.Popups.MessageDialog(file.Path, "撮影結果")
      Await msgbox.ShowAsync()
    End If
  End Sub



  Private Async Sub buttonPreviewClick(sender As Object, e As RoutedEventArgs)
    ' 静止画撮影、動画録画をおこなうキャプチャーオブジェクト
    Dim _capture = New Windows.Media.Capture.MediaCapture()

    Try
      ' Webカメラの初期化
      Await _capture.InitializeAsync()

    Catch ex As UnauthorizedAccessException
      ' "アクセスが拒否されました。 (HRESULT からの例外: 0x80070005 (E_ACCESSDENIED))"
      ' Web カメラがユーザーに許可されていないと、
      ' UnauthorizedAccessException が出る。
      ShowRequestMessageAsync(ex)
      Return
    End Try

    ' MediaCapture インスタンスを CaptureElement コントロールに設定する
    capturePreview.Source = _capture

    ' プレビューを開始する
    Await _capture.StartPreviewAsync()
  End Sub

  Private Async Sub buttonPreviewWithVideoStabilizationClick(sender As Object, e As RoutedEventArgs)
    ' 静止画撮影、動画録画をおこなうキャプチャーオブジェクト
    Dim _capture = New Windows.Media.Capture.MediaCapture()

    Try
      ' Webカメラの初期化
      Await _capture.InitializeAsync()

    Catch ex As UnauthorizedAccessException
      ' "アクセスが拒否されました。 (HRESULT からの例外: 0x80070005 (E_ACCESSDENIED))"
      ' Web カメラがユーザーに許可されていないと、
      ' UnauthorizedAccessException が出る。
      ShowRequestMessageAsync(ex)
      Return
    End Try


    ' キャプチャしたビデオに手ブレ補正効果を追加する
    ' http://msdn.microsoft.com/ja-jp/library/windows/apps/xaml/hh868169.aspx
    Await _capture.AddEffectAsync(
                    Windows.Media.Capture.MediaStreamType.VideoRecord,
                    Windows.Media.VideoEffects.VideoStabilization,
                    Nothing)


    ' MediaCapture インスタンスを CaptureElement コントロールに設定する
    capturePreview.Source = _capture

    ' プレビューを開始する
    Await _capture.StartPreviewAsync()
  End Sub

  Private Async Sub buttonTakePhotoClick(sender As Object, e As RoutedEventArgs)
    If (capturePreview.Source Is Nothing) Then
      Return
    End If

    ' プレビューに付けた MediaCapture オブジェクトを取り出す
    Dim capture As Windows.Media.Capture.MediaCapture = capturePreview.Source

    ' 保存する画像ファイルのフォーマット (pngファイルを指定)
    Dim imageProperties = Windows.Media.MediaProperties.ImageEncodingProperties.CreatePng()

    ' 撮影する。一度ファイルに保存する。
    Dim file As StorageFile = Await ApplicationData.Current.TemporaryFolder.CreateFileAsync("test.png", CreationCollisionOption.GenerateUniqueName)
    Try
      Await capture.CapturePhotoToStorageFileAsync(imageProperties, file)
      ' キャプチャ開始から撮影までの間に許可を取り消されると、ここで例外が出る
      ' 「指定されたファイル ハンドルへのアクセスが取り消されました。 (HRESULT からの例外: 0x80070326)」
    Catch ex As Exception
      ShowRequestMessageAsync(ex)
      Return
    End Try

    '撮影したファイルを読み込んで右側のImageコントロールに表示する
    Using stream = Await file.OpenReadAsync()
      Dim bi = New BitmapImage()
      bi.SetSource(stream)
      captureImage.Source = bi
    End Using
  End Sub


  Private Async Sub ShowRequestMessageAsync(ex As Exception)
    Dim content = String.Format(
      "Web カメラを使えるようにしてください。" + vbCrLf + "例外: {0}", ex.ToString())

    Dim msgbox = New Windows.UI.Popups.MessageDialog(content, "Web カメラが使えません")
    Await msgbox.ShowAsync()
  End Sub

End Class
