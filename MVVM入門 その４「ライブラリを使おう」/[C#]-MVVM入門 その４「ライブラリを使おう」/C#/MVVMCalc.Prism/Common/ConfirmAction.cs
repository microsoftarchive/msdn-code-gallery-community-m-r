namespace MVVMCalc.Common
{
    using System.Windows;
    using System.Windows.Interactivity;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

    /// <summary>
    /// 確認ダイアログを表示するアクション
    /// </summary>
    public class ConfirmAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            // InteractionRequestedEventArgs意外の場合は何もしない
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            // Confirmation意外の場合は何もしない
            var context = args.Context as Confirmation;
            if (context == null)
            {
                return;
            }

            // メッセージボックスを表示して
            var result = MessageBox.Show(
                args.Context.Content.ToString(), 
                args.Context.Title, 
                MessageBoxButton.OKCancel);

            // ボタンの押された結果をResponseに格納して
            context.Confirmed = result == MessageBoxResult.OK;
            // コールバックを呼ぶ
            args.Callback();
        }
    }
}
