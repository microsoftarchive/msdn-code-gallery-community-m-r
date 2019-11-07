Namespace Common

    ''' <summary>
    ''' <see cref="RichTextBlock"/> のラッパーは、使用可能なコンテンツに合わせて、
    ''' 必要なオーバーフロー列を追加で作成します。
    ''' </summary>
    ''' <example>
    ''' 以下では、400 ピクセル幅の列に 50 ピクセルの余白が指定されたコレクションを作成します。
    ''' これには、データ バインドされた任意のコンテンツが含まれます:
    ''' <code>
    ''' <RichTextColumns>
    '''     <RichTextColumns.ColumnTemplate>
    '''         <DataTemplate>
    '''             <RichTextBlockOverflow Width="400" Margin="50,0,0,0"/>
    '''         </DataTemplate>
    '''     </RichTextColumns.ColumnTemplate>
    '''     
    '''     <RichTextBlock Width="400">
    '''         <Paragraph>
    '''             <Run Text="{Binding Content}"/>
    '''         </Paragraph>
    '''     </RichTextBlock>
    ''' </RichTextColumns>
    ''' </code>
    ''' </example>
    ''' <remarks>通常、バインドされていない領域で必要なすべての列を作成できる、
    ''' 水平方向のスクロール領域で使用されます。垂直方向のスクロール領域で使用する場合、
    ''' 列を追加で作成することはできません。</remarks>
    <Windows.UI.Xaml.Markup.ContentProperty(Name:="RichTextContent")>
    Public NotInheritable Class RichTextColumns
        Inherits Panel

        ''' <summary>
        ''' <see cref="RichTextContent"/> 依存関係プロパティを識別します。
        ''' </summary>
        Public Shared ReadOnly RichTextContentProperty As DependencyProperty =
            DependencyProperty.Register("RichTextContent", GetType(RichTextBlock),
            GetType(RichTextColumns), New PropertyMetadata(Nothing, AddressOf ResetOverflowLayout))

        ''' <summary>
        ''' <see cref="ColumnTemplate"/> 依存関係プロパティを識別します。
        ''' </summary>
        Public Shared ReadOnly ColumnTemplateProperty As DependencyProperty =
            DependencyProperty.Register("ColumnTemplate", GetType(DataTemplate),
            GetType(RichTextColumns), New PropertyMetadata(Nothing, AddressOf ResetOverflowLayout))

        ''' <summary>
        ''' <see cref="RichTextColumns"/> クラスの新しいインスタンスを初期化します。
        ''' </summary>
        Public Sub New()
            Me.HorizontalAlignment = HorizontalAlignment.Left
        End Sub

        ''' <summary>
        ''' 最初のリッチ テキスト コンテンツを 1 つ目の列として使用するように取得または設定します。
        ''' </summary>
        Public Property RichTextContent As RichTextBlock
            Get
                Return DirectCast(Me.GetValue(RichTextContentProperty), RichTextBlock)
            End Get
            Set(value As RichTextBlock)
                Me.SetValue(RichTextContentProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' 追加の <see cref="RichTextBlockOverflow"/> インスタンスを
        ''' 作成するために使用するテンプレートを取得または設定します。
        ''' </summary>
        Public Property ColumnTemplate As DataTemplate
            Get
                Return DirectCast(Me.GetValue(ColumnTemplateProperty), DataTemplate)
            End Get
            Set(value As DataTemplate)
                Me.SetValue(ColumnTemplateProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' 列のレイアウトを再作成するため、コンテンツまたはオーバーフローのテンプレートを変更するときに呼び出されます。
        ''' </summary>
        ''' <param name="d">変更が発生した <see cref="RichTextColumns"/> の
        ''' インスタンス。</param>
        ''' <param name="e">特定の変更を説明するイベント データ。</param>
        Public Shared Sub ResetOverflowLayout(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim target As RichTextColumns = TryCast(d, RichTextColumns)
            If target IsNot Nothing Then
                ' 大幅な変更が行われた場合は、最初からレイアウトをビルドし直します
                target._overflowColumns = Nothing
                target.Children.Clear()
                target.InvalidateMeasure()
            End If
        End Sub

        ''' <summary>
        ''' 既に作成されたオーバーフロー列を一覧表示します。
        ''' 最初の子として RichTextBlock を含む <see cref="Panel.Children"/> コレクションのインスタンスは
        ''' 1:1 の関係を保持する必要があります。
        ''' </summary>
        Private _overflowColumns As List(Of RichTextBlockOverflow) = Nothing

        ''' <summary>
        ''' 追加のオーバーフロー列が必要かどうか、および既存の列を削除できるかどうかを
        ''' 指定します。
        ''' </summary>
        ''' <param name="availableSize">空き領域のサイズは、作成できる追加の列の
        ''' 数の制限に使用されます。</param>
        ''' <returns>元のコンテンツと追加の列を合わせた最終的なサイズ。</returns>
        Protected Overrides Function MeasureOverride(availableSize As Size) As Size
            If Me.RichTextContent Is Nothing Then Return New Size(0, 0)

            ' RichTextBlock を子に指定するようにします。このとき、
            ' 未完了であることを示すため、追加の列の一覧の不足箇所を
            ' 使用します
            If Me._overflowColumns Is Nothing Then
                Me.Children.Add(Me.RichTextContent)
                Me._overflowColumns = New List(Of RichTextBlockOverflow)()
            End If

            ' 最初は元の RichTextBlock コンテンツを基準にします
            Me.RichTextContent.Measure(availableSize)
            Dim maxWidth As Double = Me.RichTextContent.DesiredSize.Width
            Dim maxHeight As Double = Me.RichTextContent.DesiredSize.Height
            Dim hasOverflow As Boolean = Me.RichTextContent.HasOverflowContent

            ' オーバーフロー列を十分に確保します
            Dim overflowIndex As Integer = 0
            While hasOverflow AndAlso maxWidth < availableSize.Width AndAlso Me.ColumnTemplate IsNot Nothing

                ' 既存のオーバーフロー列がなくなるまで使用した後、
                ' 指定のテンプレートから作成します
                Dim overflow As RichTextBlockOverflow
                If Me._overflowColumns.Count > overflowIndex Then
                    overflow = Me._overflowColumns(overflowIndex)
                Else
                    overflow = DirectCast(Me.ColumnTemplate.LoadContent(), RichTextBlockOverflow)
                    Me._overflowColumns.Add(overflow)
                    Me.Children.Add(overflow)
                    If overflowIndex = 0 Then
                        Me.RichTextContent.OverflowContentTarget = overflow
                    Else
                        Me._overflowColumns(overflowIndex - 1).OverflowContentTarget = overflow
                    End If
                End If

                ' 新しい列を基準にして、必要に応じて繰り返しの設定を行います
                overflow.Measure(New Size(availableSize.Width - maxWidth, availableSize.Height))
                maxWidth += overflow.DesiredSize.Width
                maxHeight = Math.Max(maxHeight, overflow.DesiredSize.Height)
                hasOverflow = overflow.HasOverflowContent
                overflowIndex += 1
            End While

            ' 不要な列をオーバーフロー チェーンから切断し、列のプライベート リストから削除して、
            ' 子として削除します
            If Me._overflowColumns.Count > overflowIndex Then
                If overflowIndex = 0 Then
                    Me.RichTextContent.OverflowContentTarget = Nothing
                Else
                    Me._overflowColumns(overflowIndex - 1).OverflowContentTarget = Nothing
                End If

                While Me._overflowColumns.Count > overflowIndex
                    Me._overflowColumns.RemoveAt(overflowIndex)
                    Me.Children.RemoveAt(overflowIndex + 1)
                End While
            End If

            ' 最終的に決定したサイズを報告します
            Return New Size(maxWidth, maxHeight)
        End Function

        ''' <summary>
        ''' 元のコンテンツと追加されたすべての列を整列します。
        ''' </summary>
        ''' <param name="finalSize">中で子を整列する必要がある領域のサイズを
        ''' 定義します。</param>
        ''' <returns>子が実際に必要とする領域のサイズ。</returns>
        Protected Overrides Function ArrangeOverride(finalSize As Size) As Size
            Dim maxWidth As Double = 0
            Dim maxHeight As Double = 0
            For Each child As UIElement In Children
                child.Arrange(New Rect(maxWidth, 0, child.DesiredSize.Width, finalSize.Height))
                maxWidth += child.DesiredSize.Width
                maxHeight = Math.Max(maxHeight, child.DesiredSize.Height)
            Next
            Return New Size(maxWidth, maxHeight)
        End Function

    End Class

End Namespace
