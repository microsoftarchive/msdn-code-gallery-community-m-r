using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MVVMLightWPFSampleApp.Commons
{
    public class MessageTrigger<TMessage> : TriggerBase<DependencyObject>
        where TMessage : MessageBase
    {
        public object Target
        {
            get { return (object)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(object), typeof(MessageTrigger<TMessage>), new PropertyMetadata(null));


        protected override void OnAttached()
        {
            base.OnAttached();
            Messenger.Default.Register<TMessage>(this, this.ReceiveAction);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Messenger.Default.Unregister(this);
        }

        private void ReceiveAction(TMessage message)
        {
            if (this.Target == message.Target || (this.Target != null && this.Target.Equals(message.Target)))
            {
                this.InvokeActions(message);
            }
        }

    }
}
