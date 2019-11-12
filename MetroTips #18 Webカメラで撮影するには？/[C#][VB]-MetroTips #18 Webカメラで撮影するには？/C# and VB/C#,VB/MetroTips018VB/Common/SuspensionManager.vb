Namespace Common

    ''' <summary>
    ''' SuspensionManager は、グローバル セッション状態をキャプチャし、アプリケーションのプロセス継続時間管理を簡略化します。
    ''' セッション状態は、さまざまな条件下で自動的にクリアされます。
    ''' また、セッション間で伝達しやすく、アプリケーションのクラッシュや
    ''' アップグレード時には破棄が必要な情報を格納する場合にのみ
    ''' アップグレードされます。
    ''' </summary>
    Friend NotInheritable Class SuspensionManager

        Private Shared _sessionState As New Dictionary(Of String, Object)()
        Private Shared _knownTypes As New List(Of Type)()
        Private Const sessionStateFilename As String  = "_sessionState.xml"

        ''' <summary>
        ''' 現在のセッションのグローバル セッション状態へのアクセスを提供します。
        ''' この状態は、<see cref="SaveAsync"/> によってシリアル化され、
        ''' <see cref="RestoreAsync"/> によって復元されます。したがって、値は
        '''  <see cref="Runtime.Serialization.DataContractSerializer"/> によってシリアル化可能で、できるだけコンパクトになっている必要があります。
        ''' 文字列などの独立したデータ型を使用することを強くお勧めします。
        ''' </summary>
        Public Shared ReadOnly Property SessionState As Dictionary(Of String, Object)
            Get
                Return _sessionState
            End Get
        End Property

        ''' <summary>
        ''' セッション状態の読み取りおよび書き込み時に <see cref="Runtime.Serialization.DataContractSerializer"/> に提供されるカスタムの型の一覧です。
        ''' 最初は空になっています。型を追加することができます。
        ''' 型を追加して、シリアル化プロセスをカスタマイズできます。
        ''' </summary>
        Public Shared ReadOnly Property KnownTypes As List(Of Type)
            Get
                Return _knownTypes
            End Get
        End Property

        ''' <summary>
        ''' 現在の <see cref="SessionState"/> を保存します。
        ''' <see cref="RegisterFrame"/> で登録された <see cref="Frame"/> インスタンスは、現在のナビゲーション スタックも保存します。
        ''' これは、アクティブな <see cref="Page"/> に状態を保存する機会を
        ''' 順番に提供します。
        ''' </summary>
        ''' <returns>セッション状態が保存されたときに反映される非同期タスクです。</returns>
        Public Shared Async Function SaveAsync() As Task
            Try

                ' 登録されているすべてのフレームのナビゲーション状態を保存します
                For Each weakFrameReference As WeakReference(Of Frame) In _registeredFrames
                    Dim frame As Frame = Nothing
                    If weakFrameReference.TryGetTarget(frame) Then
                        SaveFrameNavigationState(frame)
                    End If
                Next

                ' セッション状態を同期的にシリアル化して、共有状態への非同期アクセスを
                ' 状態
                Dim sessionData As New MemoryStream()
                Dim serializer As New Runtime.Serialization.DataContractSerializer(GetType(Dictionary(Of String, Object)), _knownTypes)
                serializer.WriteObject(sessionData, _sessionState)

                ' SessionState ファイルの出力ストリームを取得し、状態を非同期的に書き込みます
                Dim file As Windows.Storage.StorageFile = Await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync(
                    sessionStateFilename, Windows.Storage.CreationCollisionOption.ReplaceExisting)
                Using fileStream As Stream = Await file.OpenStreamForWriteAsync()
                    sessionData.Seek(0, SeekOrigin.Begin)
                    Await sessionData.CopyToAsync(fileStream)
                    Await fileStream.FlushAsync()
                End Using
            Catch ex As Exception
                Throw New SuspensionManagerException(ex)
            End Try
        End Function

        ''' <summary>
        ''' 以前保存された <see cref="SessionState"/> を復元します。
        ''' <see cref="RegisterFrame"/> で登録された <see cref="Frame"/> インスタンスは、前のナビゲーション状態も復元します。
        ''' これは、アクティブな <see cref="Page"/> に状態を復元する機会を順番に提供します。
        ''' ます。
        ''' </summary>
        ''' <returns>セッション状態が読み取られたときに反映される非同期タスクです。
        ''' このタスクが完了するまで、<see cref="SessionState"/> のコンテンツには
        ''' 完了します。</returns>
        Public Shared Async Function RestoreAsync() As Task
            _sessionState = New Dictionary(Of String, Object)()

            Try

                ' SessionState ファイルの入力ストリームを取得します
                Dim file As Windows.Storage.StorageFile = Await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(sessionStateFilename)
                If file Is Nothing Then Return

                Using inStream As Windows.Storage.Streams.IInputStream = Await file.OpenSequentialReadAsync()

                    ' セッション状態を逆シリアル化します
                    Dim serializer As New Runtime.Serialization.DataContractSerializer(GetType(Dictionary(Of String, Object)), _knownTypes)
                    _sessionState = DirectCast(serializer.ReadObject(inStream.AsStreamForRead()), Dictionary(Of String, Object))
                End Using

                ' 登録されているフレームを保存された状態に復元します
                For Each weakFrameReference As WeakReference(Of Frame) In _registeredFrames
                    Dim frame As Frame = Nothing
                    If weakFrameReference.TryGetTarget(frame) Then
                        frame.ClearValue(FrameSessionStateProperty)
                        RestoreFrameNavigationState(frame)
                    End If
                Next
            Catch ex As Exception
                Throw New SuspensionManagerException(ex)
            End Try
        End Function

        Private Shared FrameSessionStateKeyProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("_FrameSessionStateKey", GetType(String), GetType(SuspensionManager), Nothing)
        Private Shared FrameSessionStateProperty As DependencyProperty =
            DependencyProperty.RegisterAttached("_FrameSessionState", GetType(Dictionary(Of String, Object)), GetType(SuspensionManager), Nothing)
        Private Shared _registeredFrames As New List(Of WeakReference(Of Frame))()

        ''' <summary>
        ''' <see cref="Frame"/> インスタンスを登録し、ナビゲーション履歴を <see cref="SessionState"/> に保存して、
        ''' ここから復元できるようにします。
        ''' フレームは、セッション状態管理に参加する場合、作成直後に 1 回登録する必要があります。
        ''' 登録されしだい、指定されたキーに対して状態が既に復元されていれば、
        ''' ナビゲーション履歴が直ちに復元されます。
        ''' <see cref="RestoreAsync"/> はナビゲーション履歴も復元します。
        ''' </summary>
        ''' <param name="frame">ナビゲーション履歴を管理する必要があるインスタンスです
        ''' <see cref="SuspensionManager"/></param>
        ''' <param name="sessionStateKey">ナビゲーション関連情報を格納するのに
        ''' 使用される <see cref="SessionState"/> への一意キーです。</param>
        Public Shared Sub RegisterFrame(frame As Frame, sessionStateKey As String)
            If frame.GetValue(FrameSessionStateKeyProperty) IsNot Nothing Then
                Throw New InvalidOperationException("Frames can only be registered to one session state key")
            End If

            If frame.GetValue(FrameSessionStateProperty) IsNot Nothing Then
                Throw New InvalidOperationException("Frames must be either be registered before accessing frame session state, or not registered at all")
            End If

            ' 依存関係プロパティを使用してセッション キーをフレームに関連付け、
            ' ナビゲーション状態を管理する必要があるフレームの一覧を保持します
            frame.SetValue(FrameSessionStateKeyProperty, sessionStateKey)
            _registeredFrames.Add(New WeakReference(Of Frame)(frame))

            ' ナビゲーション状態が復元可能かどうか確認します
            RestoreFrameNavigationState(frame)
        End Sub

        ''' <summary>
        ''' <see cref="SessionState"/> から <see cref="RegisterFrame"/> によって以前登録された <see cref="Frame"/> の関連付けを解除します。
        ''' 以前キャプチャされたナビゲーション状態は
        ''' 削除されます。
        ''' </summary>
        ''' <param name="frame">ナビゲーション履歴を管理する必要がなくなった
        ''' 管理されます。</param>
        Public Shared Sub UnregisterFrame(frame As Frame)

            ' セッション状態を削除し、(到達不能になった弱い参照と共に) ナビゲーション状態が保存される
            ' フレームの一覧からフレームを削除します
            SessionState.Remove(DirectCast(frame.GetValue(FrameSessionStateKeyProperty), String))
            _registeredFrames.RemoveAll(Function(weakFrameReference)
                                            Dim testFrame As Frame = Nothing
                                            Return Not weakFrameReference.TryGetTarget(testFrame) OrElse testFrame Is frame
                                        End Function)
        End Sub

        ''' <summary>
        ''' 指定された <see cref="Frame"/> に関連付けられているセッション状態のストレージを提供します。
        ''' <see cref="RegisterFrame"/> で以前登録されたフレームには、
        ''' グローバルの <see cref="SessionState"/> の一部として自動的に保存および復元されるセッション状態があります。
        ''' 登録されていないフレームは遷移状態です。
        ''' 遷移状態は、ナビゲーション キャッシュから破棄されたページを復元する場合に
        ''' ナビゲーション キャッシュ。
        ''' </summary>
        ''' <remarks>アプリケーションは、フレームのセッション状態を直接処理するのではなく、<see cref="LayoutAwarePage"/> に依存して
        ''' ページ固有の状態を管理するように選択できます。</remarks>
        ''' <param name="frame">セッション状態が必要なインスタンスです。</param>
        ''' <returns><see cref="SessionState"/> と同じシリアル化機構の影響を受ける状態の
        ''' <see cref="SessionState"/>。</returns>
        Public Shared Function SessionStateForFrame(frame As Frame) As Dictionary(Of String, Object)
            Dim frameState As Dictionary(Of String, Object) = DirectCast(frame.GetValue(FrameSessionStateProperty), Dictionary(Of String, Object))

            If frameState Is Nothing Then
                Dim frameSessionKey As String = DirectCast(frame.GetValue(FrameSessionStateKeyProperty), String)
                If frameSessionKey IsNot Nothing Then
                    If Not _sessionState.ContainsKey(frameSessionKey) Then

                        ' 登録されているフレームは、対応するセッション状態を反映します
                        _sessionState(frameSessionKey) = New Dictionary(Of String, Object)()
                    End If
                    frameState = DirectCast(_sessionState(frameSessionKey), Dictionary(Of String, Object))
                Else

                    ' 登録されていないフレームは遷移状態です
                    frameState = New Dictionary(Of String, Object)()
                End If
                frame.SetValue(FrameSessionStateProperty, frameState)
            End If
            Return frameState
        End Function

        Private Shared Sub RestoreFrameNavigationState(frame As Frame)
            Dim frameState As Dictionary(Of String, Object) = SessionStateForFrame(frame)
            If frameState.ContainsKey("Navigation") Then
                frame.SetNavigationState(DirectCast(frameState("Navigation"), String))
            End If
        End Sub

        Private Shared Sub SaveFrameNavigationState(frame As Frame)
            Dim frameState As Dictionary(Of String, Object) = SessionStateForFrame(frame)
            frameState("Navigation") = frame.GetNavigationState()
        End Sub

    End Class
    Public Class SuspensionManagerException
        Inherits Exception
        Public Sub New()
        End Sub

        Public Sub New(ByRef e As Exception)
            MyBase.New("SuspensionManager failed", e)
        End Sub
    End Class
End Namespace
