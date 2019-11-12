Namespace My
    Partial Class MyApplication
        Public ReadOnly Property IsRunningUnderDebugger() As Boolean
            Get
                Return System.Diagnostics.Debugger.IsAttached
            End Get
        End Property
    End Class
End Namespace