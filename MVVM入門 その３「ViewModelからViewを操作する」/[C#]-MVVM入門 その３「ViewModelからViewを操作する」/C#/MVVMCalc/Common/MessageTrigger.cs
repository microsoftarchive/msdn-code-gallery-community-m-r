namespace MVVMCalc.Common
{
    using System.Windows.Interactivity;

    /// <summary>
    /// MessengerのRaisedイベントを受信するトリガー
    /// </summary>
    public class MessageTrigger : EventTrigger
    {
        protected override string GetEventName()
        {
            return "Raised";
        }
    }
}
