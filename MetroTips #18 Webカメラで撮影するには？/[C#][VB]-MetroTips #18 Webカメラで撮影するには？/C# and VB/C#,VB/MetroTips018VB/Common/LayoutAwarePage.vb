Namespace Common

    ''' <summary>
    ''' Page を一般的な方法で実装すると、重要かつ便利な機能をいくつか使用できます:
    ''' <list type="bullet">
    ''' <item>
    ''' <description>アプリケーションのビューステートと表示状態のマップ</description>
    ''' </item>
    ''' <item>
    ''' <description>GoBack、GoForward、および GoHome イベント ハンドラー</description>
    ''' </item>
    ''' <item>
    ''' <description>ナビゲーション用のマウスおよびキーボードのショートカット</description>
    ''' </item>
    ''' <item>
    ''' <description>ナビゲーションの状態管理およびプロセス継続時間管理</description>
    ''' </item>
    ''' <item>
    ''' <description>既定のビュー モデル</description>
    ''' </item>
    ''' </list>
    ''' </summary>
    <Windows.Foundation.Metadata.WebHostHidden>
    Public Class LayoutAwarePage
        Inherits Page

        ''' <summary>
        ''' <see cref="DefaultViewModel"/> 依存関係プロパティを識別します。
        ''' </summary>
        Public Shared ReadOnly DefaultViewModelProperty As DependencyProperty =
            DependencyProperty.Register("DefaultViewModel", GetType(IObservableMap(Of String, Object)),
            GetType(LayoutAwarePage), Nothing)

        Private _layoutAwareControls As List(Of Control)

        ''' <summary>
        ''' このページがビジュアル ツリーの一部である場合、次の 2 つの変更を行います:
        ''' 1) アプリケーションのビューステートをページの表示状態にマップする
        ''' 2) キーボードおよびマウスのナビゲーション要求を処理する
        ''' </summary>
        ''' <param name="sender">要求を開始したオブジェクト。</param>
        ''' <param name="e">要求がどのように行われたかを説明するイベント データ。</param>
        Private Sub OnLoaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
            Me.StartLayoutUpdates(sender, e)

            ' キーボードおよびマウスのナビゲーションは、ウィンドウ全体を使用する場合のみ適用されます
            If Me.ActualHeight = Window.Current.Bounds.Height AndAlso
                    Me.ActualWidth = Window.Current.Bounds.Width Then

                ' ウィンドウで直接待機するため、フォーカスは不要です
                AddHandler Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated,
                    AddressOf Me.CoreDispatcher_AcceleratorKeyActivated
                AddHandler Window.Current.CoreWindow.PointerPressed,
                    AddressOf Me.CoreWindow_PointerPressed
            End If
        End Sub

        ''' <summary>
        ''' ページが表示されない場合、<see cref="OnLoaded"/> の変更を元に戻します。
        ''' </summary>
        ''' <param name="sender">要求を開始したオブジェクト。</param>
        ''' <param name="e">要求がどのように行われたかを説明するイベント データ。</param>
        Private Sub OnUnloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
            Me.StopLayoutUpdates(sender, e)
            RemoveHandler Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated,
                AddressOf Me.CoreDispatcher_AcceleratorKeyActivated
            RemoveHandler Window.Current.CoreWindow.PointerPressed,
                AddressOf Me.CoreWindow_PointerPressed
        End Sub

        ''' <summary>
        ''' <see cref="LayoutAwarePage"/> クラスの新しいインスタンスを初期化します。
        ''' </summary>
        Public Sub New()
            If Windows.ApplicationModel.DesignMode.DesignModeEnabled Then Return

            ' 空の既定のビュー モデルを作成します
            Me.DefaultViewModel = New ObservableDictionary(Of String, Object)
        End Sub

        ''' <summary>
        ''' <see cref="IObservableMap(Of String, Object)"/> の実装です。これは、
        ''' 単純なビュー モデルとして使用されるように設計されています。
        ''' </summary>
        Protected Property DefaultViewModel As IObservableMap(Of String, Object)
            Get
                Return DirectCast(Me.GetValue(DefaultViewModelProperty), IObservableMap(Of String, Object))
            End Get
            Set(value As IObservableMap(Of String, Object))
                Me.SetValue(DefaultViewModelProperty, value)
            End Set
        End Property

#Region "ナビゲーション サポート"

        ''' <summary>
        ''' イベント ハンドラーとして呼び出され、ページの関連付けられた <see cref="Frame"/> で前に戻ります。
        ''' ナビゲーション スタックの上部に到達するまで戻ります。
        ''' </summary>
        ''' <param name="sender">イベントをトリガーしたインスタンス。</param>
        ''' <param name="e">イベントが発生する条件を説明するイベント データ。</param>
        Protected Overridable Sub GoHome(sender As Object, e As RoutedEventArgs)

            ' ナビゲーション フレームを使用して最上位のページに戻ります
            If Me.Frame IsNot Nothing Then
                While Me.Frame.CanGoBack
                    Me.Frame.GoBack()
                End While
            End If
        End Sub

        ''' <summary>
        ''' イベント ハンドラーとして呼び出され、このページの <see cref="Frame"/> に関連付けられた
        ''' ナビゲーション スタックで前に戻ります。
        ''' </summary>
        ''' <param name="sender">イベントをトリガーしたインスタンス。</param>
        ''' <param name="e">イベントが発生する条件を説明するイベント データ。</param>
        Protected Overridable Sub GoBack(sender As Object, e As RoutedEventArgs)

            ' ナビゲーション フレームを使用して前のページに戻ります
            If Me.Frame IsNot Nothing AndAlso Me.Frame.CanGoBack Then
                Me.Frame.GoBack()
            End If
        End Sub

        ''' <summary>
        ''' イベント ハンドラーとして呼び出され、このページの <see cref="Frame"/> に関連付けられた
        ''' ナビゲーション スタックで前に戻ります。
        ''' </summary>
        ''' <param name="sender">イベントをトリガーしたインスタンス。</param>
        ''' <param name="e">イベントが発生する条件を説明するイベント データ。</param>
        Protected Overridable Sub GoForward(sender As Object, e As RoutedEventArgs)

            ' ナビゲーション フレームを使用して次のページに進みます
            If Me.Frame IsNot Nothing AndAlso Me.Frame.CanGoForward Then
                Me.Frame.GoForward()
            End If
        End Sub

        ''' <summary>
        ''' このページがアクティブで、ウィンドウ全体を使用する場合、Alt キーの組み合わせなどのシステム キーを含む、
        ''' キーボード操作で呼び出されます。ページがフォーカスされていないときでも、
        ''' ページ間のキーボード ナビゲーションの検出に使用されます。
        ''' </summary>
        ''' <param name="sender">イベントをトリガーしたインスタンス。</param>
        ''' <param name="args">イベントが発生する条件を説明するイベント データ。</param>
        Private Sub CoreDispatcher_AcceleratorKeyActivated(sender As Windows.UI.Core.CoreDispatcher,
                                                           args As Windows.UI.Core.AcceleratorKeyEventArgs)
            Dim virtualKey As Windows.System.VirtualKey = args.VirtualKey

            ' 左方向キーや右方向キー、または専用に設定した前に戻るキーや次に進むキーを押した場合のみ、
            ' 詳細を調査します
            If (args.EventType = Windows.UI.Core.CoreAcceleratorKeyEventType.SystemKeyDown OrElse
                args.EventType = Windows.UI.Core.CoreAcceleratorKeyEventType.KeyDown) AndAlso
                (virtualKey = Windows.System.VirtualKey.Left OrElse
                virtualKey = Windows.System.VirtualKey.Right OrElse
                virtualKey = 166 OrElse
                virtualKey = 167) Then

                ' 押された修飾子キーを確認します
                Dim coreWindow As Windows.UI.Core.CoreWindow = Window.Current.CoreWindow
                Dim downState As Windows.UI.Core.CoreVirtualKeyStates = Windows.UI.Core.CoreVirtualKeyStates.Down
                Dim menuKey As Boolean = (coreWindow.GetKeyState(Windows.System.VirtualKey.Menu) And downState) = downState
                Dim controlKey As Boolean = (coreWindow.GetKeyState(Windows.System.VirtualKey.Control) And downState) = downState
                Dim shiftKey As Boolean = (coreWindow.GetKeyState(Windows.System.VirtualKey.Shift) And downState) = downState
                Dim noModifiers As Boolean = Not menuKey AndAlso Not controlKey AndAlso Not shiftKey
                Dim onlyAlt As Boolean = menuKey AndAlso Not controlKey AndAlso Not shiftKey

                If (virtualKey = 166 AndAlso noModifiers) OrElse
                    (virtualKey = Windows.System.VirtualKey.Left AndAlso onlyAlt) Then

                    ' 前に戻るキーまたは Alt キーを押しながら左方向キーを押すと前に戻ります
                    args.Handled = True
                    Me.GoBack(Me, New RoutedEventArgs())
                ElseIf (virtualKey = 167 AndAlso noModifiers) OrElse
                    (virtualKey = Windows.System.VirtualKey.Right AndAlso onlyAlt) Then

                    ' 次に進むキーまたは Alt キーを押しながら右方向キーを押すと次に進みます
                    args.Handled = True
                    Me.GoForward(Me, New RoutedEventArgs())
                End If
            End If
        End Sub

        ''' <summary>
        ''' このページがアクティブで、ウィンドウ全体を使用する場合、マウスのクリック、タッチ スクリーンのタップなどの
        ''' 操作で呼び出されます。ページ間を移動するため、マウス ボタンのクリックによるブラウザー スタイルの
        ''' 次に進むおよび前に戻る操作の検出に使用されます。
        ''' </summary>
        ''' <param name="sender">イベントをトリガーしたインスタンス。</param>
        ''' <param name="args">イベントが発生する条件を説明するイベント データ。</param>
        Private Sub CoreWindow_PointerPressed(sender As Windows.UI.Core.CoreWindow,
                                              args As Windows.UI.Core.PointerEventArgs)
            Dim properties As Windows.UI.Input.PointerPointProperties = args.CurrentPoint.Properties

            ' 左、右、および中央ボタンを使用したボタン操作を無視します
            If properties.IsLeftButtonPressed OrElse properties.IsRightButtonPressed OrElse
                properties.IsMiddleButtonPressed Then Return

            ' [戻る] または [進む] を押すと適切に移動します (両方同時には押しません)
            Dim backPressed As Boolean = properties.IsXButton1Pressed
            Dim forwardPressed As Boolean = properties.IsXButton2Pressed
            If backPressed Xor forwardPressed Then
                args.Handled = True
                If backPressed Then Me.GoBack(Me, New RoutedEventArgs())
                If forwardPressed Then Me.GoForward(Me, New RoutedEventArgs())
            End If
        End Sub

#End Region

#Region "表示状態の切り替え"

        ''' <summary>
        ''' これは通常、ページ内の <see cref="Control"/> の
        ''' <see cref="Loaded"/> イベントでイベント ハンドラーとして呼び出され、送信元がアプリケーションの
        ''' ビューステートの変更に対応する表示状態管理の変更を受信開始する必要があることを示します。
        ''' </summary>
        ''' <param name="sender">ビューステートに対応する表示状態管理をサポートする 
        ''' <see cref="Control"/> のインスタンス。</param>
        ''' <param name="e">要求がどのように行われたかを説明するイベント データ。</param>
        ''' <remarks>現在のビューステートは、レイアウトの更新が要求されると、
        ''' 対応する表示状態を設定するためすぐに使用されます。対応する 
        ''' <see cref="Unloaded"/> イベント ハンドラーを <see cref="StopLayoutUpdates"/> に接続しておくことを
        ''' 強くお勧めします。<see cref="LayoutAwarePage"/> のインスタンスは、
        ''' Loaded および Unloaded イベントでこれらのハンドラーを自動的に呼び出します。</remarks>
        ''' <seealso cref="DetermineVisualState"/>
        ''' <seealso cref="InvalidateVisualState"/>
        Public Sub StartLayoutUpdates(sender As Object, e As RoutedEventArgs)
            If Windows.ApplicationModel.DesignMode.DesignModeEnabled Then Return

            Dim control As Control = TryCast(sender, Control)
            If control Is Nothing Then Return

            If Me._layoutAwareControls Is Nothing Then

                ' 更新の対象となるコントロールがある場合、ビューステートの変更の待機を
                ' 開始します
                AddHandler Window.Current.SizeChanged, AddressOf Me.WindowSizeChanged
                Me._layoutAwareControls = New List(Of Control)
            End If
            Me._layoutAwareControls.Add(control)

            ' コントロールの最初の表示状態を設定します
            VisualStateManager.GoToState(control, DetermineVisualState(ApplicationView.Value), False)
        End Sub

        Private Sub WindowSizeChanged(sender As Object, e As Windows.UI.Core.WindowSizeChangedEventArgs)
            Me.InvalidateVisualState()
        End Sub

        ''' <summary>
        ''' これは通常、ページ内の <see cref="Control"/> の
        ''' <see cref="Unloaded"/> イベントでイベント ハンドラーとして呼び出され、送信元がアプリケーションのビューステートの変更に対応する
        ''' 表示状態管理の変更を受信開始する必要があることを示します。
        ''' </summary>
        ''' <param name="sender">ビューステートに対応する表示状態管理をサポートする 
        ''' <see cref="Control"/> のインスタンス。</param>
        ''' <param name="e">要求がどのように行われたかを説明するイベント データ。</param>
        ''' <remarks>現在のビューステートは、レイアウトの更新が要求されると、
        ''' 表示状態を設定するためすぐに使用されます。</remarks>
        ''' <seealso cref="StartLayoutUpdates"/>
        Public Sub StopLayoutUpdates(sender As Object, e As RoutedEventArgs)
            If Windows.ApplicationModel.DesignMode.DesignModeEnabled Then Return

            Dim control As Control = TryCast(sender, Control)
            If control Is Nothing OrElse Me._layoutAwareControls Is Nothing Then Return

            Me._layoutAwareControls.Remove(control)
            If Me._layoutAwareControls.Count = 0 Then

                ' 更新の対象となるコントロールがない場合、ビューステートの変更の待機を停止します
                Me._layoutAwareControls = Nothing
                RemoveHandler Window.Current.SizeChanged, AddressOf Me.WindowSizeChanged
            End If
        End Sub

        ''' <summary>
        ''' <see cref="ApplicationViewState"/> 値を、ページ内の表示状態管理で使用できる文字列に
        ''' 変換します。既定の実装では列挙値の名前を使用します。
        ''' サブクラスでこのメソッドをオーバーライドして、使用されているマップ スキームを制御する場合があります。
        ''' </summary>
        ''' <param name="viewState">表示状態が必要なビューステート。</param>
        ''' <returns><see cref="VisualStateManager"/> の実行に使用される表示状態の名前</returns>
        ''' <seealso cref="InvalidateVisualState"/>
        Protected Overridable Function DetermineVisualState(viewState As ApplicationViewState) As String
            Return viewState.ToString()
        End Function

        ''' <summary>
        ''' 適切な表示状態を使用した表示状態の変更を待機しているすべてのコントロールを更新し
        ''' ます。
        ''' </summary>
        ''' <remarks>
        ''' 通常、ビューステートが変更されていない場合でも、別の値が返される可能性がある事を知らせるために
        ''' <see cref="DetermineVisualState"/> をオーバーライドすることで
        ''' 使用されます。
        ''' </remarks>
        Public Sub InvalidateVisualState()
            If Me._layoutAwareControls IsNot Nothing Then
                Dim visualState As String = DetermineVisualState(ApplicationView.Value)
                For Each layoutAwareControl As Control In Me._layoutAwareControls
                    VisualStateManager.GoToState(layoutAwareControl, visualState, False)
                Next
            End If
        End Sub

#End Region

#Region "プロセス継続時間管理"

        Private _pageKey As String

        ''' <summary>
        ''' このページがフレームに表示されるときに呼び出されます。
        ''' </summary>
        ''' <param name="e">このページにどのように到達したかを説明するイベント データ。Parameter 
        ''' プロパティは、表示するグループを示します。</param>
        Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)

            ' ナビゲーションを通じてキャッシュ ページに戻るときに、状態の読み込みが発生しないようにします
            If Me._pageKey IsNot Nothing Then Return

            Dim frameState As Dictionary(Of String, Object) = SuspensionManager.SessionStateForFrame(Me.Frame)
            Me._pageKey = "Page-" & Me.Frame.BackStackDepth

            If e.NavigationMode = Navigation.NavigationMode.New Then

                ' 新しいページをナビゲーション スタックに追加するとき、次に進むナビゲーションの
                ' 既存の状態をクリアします
                Dim nextPageKey As String = Me._pageKey
                Dim nextPageIndex As Integer = Me.Frame.BackStackDepth
                While (frameState.Remove(nextPageKey))
                    nextPageIndex += 1
                    nextPageKey = "Page-" & nextPageIndex
                End While


                ' ナビゲーション パラメーターを新しいページに渡します
                Me.LoadState(e.Parameter, Nothing)
            Else

                ' ナビゲーション パラメーターおよび保存されたページの状態をページに渡します。
                ' このとき、中断状態の読み込みや、キャッシュから破棄されたページの再作成と同じ対策を
                ' 使用します
                Me.LoadState(e.Parameter, DirectCast(frameState(Me._pageKey), Dictionary(Of String, Object)))
            End If
        End Sub

        ''' <summary>
        ''' このページがフレームに表示されなくなるときに呼び出されます。
        ''' </summary>
        ''' <param name="e">このページにどのように到達したかを説明するイベント データ。Parameter 
        ''' プロパティは、表示するグループを示します。</param>
        Protected Overrides Sub OnNavigatedFrom(e As Navigation.NavigationEventArgs)
            Dim frameState As Dictionary(Of String, Object) = SuspensionManager.SessionStateForFrame(Me.Frame)
            Dim pageState As New Dictionary(Of String, Object)()
            Me.SaveState(pageState)
            frameState(_pageKey) = pageState
        End Sub

        ''' <summary>
        ''' このページには、移動中に渡されるコンテンツを設定します。前のセッションからページを
        ''' 再作成する場合は、保存状態も指定されます。
        ''' </summary>
        ''' <param name="navigationParameter">このページが最初に要求されたときに
        ''' パラメーター値は <see cref="Frame.Navigate"/> に渡されました。
        ''' </param>
        ''' <param name="pageState">前のセッションでこのページによって保存された状態の
        ''' ディクショナリ。ページに初めてアクセスするとき、状態は null になります。</param>
        Protected Overridable Sub LoadState(navigationParameter As Object, pageState As Dictionary(Of String, Object))

        End Sub

        ''' <summary>
        ''' アプリケーションが中断される場合、またはページがナビゲーション キャッシュから破棄される場合、
        ''' このページに関連付けられた状態を保存します。値は、
        ''' <see cref="SuspensionManager.SessionState"/> のシリアル化の要件に準拠する必要があります。
        ''' </summary>
        ''' <param name="pageState">シリアル化可能な状態で作成される空のディクショナリ。</param>
        Protected Overridable Sub SaveState(pageState As Dictionary(Of String, Object))

        End Sub

#End Region

        ''' <summary>
        ''' IObservableMap の実装では、既定のビュー モデルとして使用するため、再入をサポート
        ''' しています。 
        ''' </summary>
        Private Class ObservableDictionary(Of K, V)
            Implements IObservableMap(Of K, V)

            Private Class ObservableDictionaryChangedEventArgs
                Implements IMapChangedEventArgs(Of K)

                Private _change As CollectionChange
                Private _key As K

                Public Sub New(change As CollectionChange, key As K)
                    Me._change = change
                    Me._key = key
                End Sub

                ReadOnly Property CollectionChange As CollectionChange Implements IMapChangedEventArgs(Of K).CollectionChange
                    Get
                        Return _change
                    End Get
                End Property

                ReadOnly Property Key As K Implements IMapChangedEventArgs(Of K).Key
                    Get
                        Return _key
                    End Get
                End Property

            End Class

            Public Event MapChanged(sender As IObservableMap(Of K, V), [event] As IMapChangedEventArgs(Of K)) Implements IObservableMap(Of K, V).MapChanged
            Private _dictionary As New Dictionary(Of K, V)()

            Private Sub InvokeMapChanged(change As CollectionChange, key As K)
                RaiseEvent MapChanged(Me, New ObservableDictionaryChangedEventArgs(change, key))
            End Sub

            Public Sub Add(key As K, value As V) Implements IDictionary(Of K, V).Add
                Me._dictionary.Add(key, value)
                Me.InvokeMapChanged(CollectionChange.ItemInserted, key)
            End Sub

            Public Sub AddKeyValuePair(item As KeyValuePair(Of K, V)) Implements ICollection(Of KeyValuePair(Of K, V)).Add
                Me.Add(item.Key, item.Value)
            End Sub

            Public Function Remove(key As K) As Boolean Implements IDictionary(Of K, V).Remove
                If Me._dictionary.Remove(key) Then
                    Me.InvokeMapChanged(CollectionChange.ItemRemoved, key)
                    Return True
                End If
                Return False
            End Function

            Public Function RemoveKeyValuePair(item As KeyValuePair(Of K, V)) As Boolean Implements ICollection(Of KeyValuePair(Of K, V)).Remove
                Dim currentValue As V
                If Me._dictionary.TryGetValue(item.Key, currentValue) AndAlso
                    Object.Equals(item.Value, currentValue) AndAlso Me._dictionary.Remove(item.Key) Then

                    Me.InvokeMapChanged(CollectionChange.ItemRemoved, item.Key)
                    Return True
                End If
                Return False
            End Function

            Default Public Property Item(key As K) As V Implements IDictionary(Of K, V).Item
                Get
                    Return Me._dictionary(key)
                End Get
                Set(value As V)
                    Me._dictionary(key) = value
                    Me.InvokeMapChanged(CollectionChange.ItemChanged, key)
                End Set
            End Property

            Public Sub Clear() Implements ICollection(Of KeyValuePair(Of K, V)).Clear
                Dim priorKeys As K() = Me._dictionary.Keys.ToArray()
                Me._dictionary.Clear()
                For Each key As K In priorKeys
                    Me.InvokeMapChanged(CollectionChange.ItemRemoved, key)
                Next
            End Sub

            Public Function Contains(item As KeyValuePair(Of K, V)) As Boolean Implements ICollection(Of KeyValuePair(Of K, V)).Contains
                Return Me._dictionary.Contains(item)
            End Function

            Public ReadOnly Property Count As Integer Implements ICollection(Of KeyValuePair(Of K, V)).Count
                Get
                    Return Me._dictionary.Count
                End Get
            End Property

            Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of KeyValuePair(Of K, V)).IsReadOnly
                Get
                    Return False
                End Get
            End Property

            Public Function ContainsKey(key As K) As Boolean Implements IDictionary(Of K, V).ContainsKey
                Return Me._dictionary.ContainsKey(key)
            End Function

            Public ReadOnly Property Keys As ICollection(Of K) Implements IDictionary(Of K, V).Keys
                Get
                    Return Me._dictionary.Keys
                End Get
            End Property

            Public Function TryGetValue(key As K, ByRef value As V) As Boolean Implements IDictionary(Of K, V).TryGetValue
                Return Me._dictionary.TryGetValue(key, value)
            End Function

            Public ReadOnly Property Values As ICollection(Of V) Implements IDictionary(Of K, V).Values
                Get
                    Return Me._dictionary.Values
                End Get
            End Property

            Public Function GetGenericEnumerator() As IEnumerator(Of KeyValuePair(Of K, V)) Implements IEnumerable(Of KeyValuePair(Of K, V)).GetEnumerator
                Return Me._dictionary.GetEnumerator()
            End Function

            Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
                Return Me._dictionary.GetEnumerator()
            End Function

            Public Sub CopyTo(array() As KeyValuePair(Of K, V), arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of K, V)).CopyTo
                Dim arraySize As Integer = array.Length
                For Each pair As KeyValuePair(Of K, V) In Me._dictionary
                    If arrayIndex >= arraySize Then Exit For
                    array(arrayIndex) = pair
                    arrayIndex += 1
                Next
            End Sub

        End Class

    End Class

End Namespace
