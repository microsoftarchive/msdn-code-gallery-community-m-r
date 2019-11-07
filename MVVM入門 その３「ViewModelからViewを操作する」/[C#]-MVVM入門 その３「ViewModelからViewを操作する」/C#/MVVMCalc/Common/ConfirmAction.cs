namespace MVVMCalc.Common
{
    using System.Windows;
    using System.Windows.Interactivity;

    /// <summary>
    /// 確認ダイアログを表示するアクション
    /// </summary>
    public class ConfirmAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            // MessageEventArgs意外の場合は何もしない
            var args = parameter as MessageEventArgs;
            if (args == null)
            {
                return;
            }

            // メッセージボックスを表示して
            var result = MessageBox.Show(
                args.Message.Body.ToString(), 
                "確認", 
                MessageBoxButton.OKCancel);

            // ボタンの押された結果をResponseに格納して
            args.Message.Response = result == MessageBoxResult.OK;
            // コールバックを呼ぶ
            args.Callback(args.Message);
        }
    }
}
