using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MvvmMasterDetailApp.Commons
{
    public class MessageTriggerBase<T, TMessage> : TriggerBase<T>
        where T : DependencyObject
        where TMessage : MessageBase
    {
        public object Target
        {
            get { return (object)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(object), typeof(MessageTriggerBase<T, TMessage>), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            Messenger.Default.Register<TMessage>(this, this.MessageReceived);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Messenger.Default.Unregister<TMessage>(this, this.MessageReceived);
        }

        protected virtual void MessageReceived(TMessage message)
        {
            if (this.Target == null || this.Target.Equals(message.Target))
            {
                this.InvokeActions(message);
            }
        }

    }
}
