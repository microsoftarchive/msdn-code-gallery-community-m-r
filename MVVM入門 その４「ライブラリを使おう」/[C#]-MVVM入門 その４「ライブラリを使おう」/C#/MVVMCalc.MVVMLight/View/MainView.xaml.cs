namespace MVVMCalc.View
{
    using System.Windows;
    using GalaSoft.MvvmLight.Messaging;
    using MVVMCalc.ViewModel;

    /// <summary>
    /// MainView.xaml の相互作用ロジック
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            // ViewModelからErrorMessageTokenが渡された時の処理の登録
            Messenger.Default.Register<DialogMessage>(
                this,
                MainViewModel.ErrorMessageToken,
                m =>
                {
                    // メッセージを表示して、結果をコールバック
                    var r = MessageBox.Show(m.Content, m.Caption, m.Button);
                    m.Callback(r);
                });
        }
    }
}
