' 空のアプリケーション テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234227 を参照してください

''' <summary>
''' 既定の Application クラスを補完するアプリケーション固有の動作を提供します。
''' </summary>
NotInheritable Class App
    Inherits Application

    ''' <summary>
    ''' アプリケーションがエンド ユーザーによって正常に起動されたときに呼び出されます。他のエントリ ポイントは、
    ''' アプリケーションが特定のファイルを開くために呼び出されたときに
    ''' 検索結果やその他の情報を表示するために使用されます。
    ''' </summary>
    ''' <param name="args">起動要求とプロセスの詳細を表示します。</param>
    Protected Overrides Sub OnLaunched(args As Windows.ApplicationModel.Activation.LaunchActivatedEventArgs)
        Dim rootFrame As Frame = Window.Current.Content

        ' ウィンドウに既にコンテンツが表示されている場合は、アプリケーションの初期化を繰り返さずに、
        ' ウィンドウがアクティブであることだけを確認してください

        If rootFrame Is Nothing Then
            ' ナビゲーション コンテキストとして動作するフレームを作成し、最初のページに移動します
            rootFrame = New Frame()
            If args.PreviousExecutionState = ApplicationExecutionState.Terminated Then
                'TODO: 以前中断したアプリケーションから状態を読み込みます。
            End If
            ' フレームを現在のウィンドウに配置します
            Window.Current.Content = rootFrame
        End If
        If rootFrame.Content Is Nothing Then
            ' ナビゲーション スタックが復元されていない場合、最初のページに移動します。
            ' このとき、必要な情報をナビゲーション パラメーターとして渡して、新しいページを
            ' 構成します
            If Not rootFrame.Navigate(GetType(MainPage), args.Arguments) Then
                Throw New Exception("Failed to create initial page")
            End If
        End If

        ' 現在のウィンドウがアクティブであることを確認します
        Window.Current.Activate()
    End Sub

    ''' <summary>
    ''' アプリケーションの実行が中断されたときに呼び出されます。アプリケーションの状態は、
    ''' アプリケーションが終了されるのか、メモリの内容がそのままで再開されるのか
    ''' わからない状態で保存されます。
    ''' </summary>
    ''' <param name="sender">中断要求の送信元。</param>
    ''' <param name="e">中断要求の詳細。</param>
    Private Sub OnSuspending(sender As Object, e As SuspendingEventArgs) Handles Me.Suspending
        Dim deferral As SuspendingDeferral = e.SuspendingOperation.GetDeferral()
        ' TODO: アプリケーションの状態を保存してバックグラウンドの動作があれば停止します
        deferral.Complete()
    End Sub

End Class
