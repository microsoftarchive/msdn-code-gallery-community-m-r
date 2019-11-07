namespace MVVMCalc.Common
{
    /// <summary>
    /// ViewModelとViewの間での情報のやり取りを行うメッセージ
    /// </summary>
    public class Message
    {
        /// <summary>
        /// メッセージの本体
        /// </summary>
        public object Body { get; private set; }

        /// <summary>
        /// ViewからViewModelへのメッセージのレスポンス
        /// </summary>
        public object Response { get; set; }

        /// <summary>
        /// Bodyを指定してMessageを作成する
        /// </summary>
        /// <param name="body"></param>
        public Message(object body)
        {
            this.Body = body;
        }
    }
}
