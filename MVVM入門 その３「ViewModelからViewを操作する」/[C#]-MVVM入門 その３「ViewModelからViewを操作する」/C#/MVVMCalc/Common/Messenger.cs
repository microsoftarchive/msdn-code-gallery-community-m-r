namespace MVVMCalc.Common
{
    using System;

    /// <summary>
    /// Messageを送信するクラス。
    /// </summary>
    public class Messenger
    {
        /// <summary>
        /// メッセージが送信されたことを通知するイベント
        /// </summary>
        public event EventHandler<MessageEventArgs> Raised;

        /// <summary>
        /// 指定したメッセージとコールバックでメッセージを送信する
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="callback">コールバック</param>
        public void Raise(Message message, Action<Message> callback)
        {
            var h = this.Raised;
            if (h != null)
            {
                h(this, new MessageEventArgs(message, callback));
            }
        }
    }
}
