namespace MVVMCalc.Common
{
    using System;

    /// <summary>
    /// Messengerの通知イベント用のイベント引数
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// 送信するメッセージ
        /// </summary>
        public Message Message { get; private set; }

        /// <summary>
        /// ViewModelのコールバック
        /// </summary>
        public Action<Message> Callback { get; private set; }

        /// <summary>
        /// メッセージとコールバックを指定してイベント引数を作成する
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="callback">コールバック</param>
        public MessageEventArgs(Message message, Action<Message> callback)
        {
            this.Message = message;
            this.Callback = callback;
        }
    }
}
