using Cirrious.MvvmCross.Plugins.Messenger;

namespace MyShuttle.Client.Core.Messages
{
    public class ReloadDataMessage : MvxMessage
    {
        public ReloadDataMessage(object sender)
            : base(sender)
        { }
    }
}
