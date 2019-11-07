using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using System;

namespace LightweightPrismSampleApp.Models
{
    public class Calculator : BindableBase
    {
        private EventAggregator eventAggregator;

        private double lhs;

        public double Lhs
        {
            get { return this.lhs; }
            set { this.SetProperty(ref this.lhs, value); }
        }

        private double rhs;

        public double Rhs
        {
            get { return this.rhs; }
            set { this.SetProperty(ref this.rhs, value); }
        }

        private double answer;

        public double Answer
        {
            get { return this.answer; }
            set { this.SetProperty(ref this.answer, value); }
        }

        public Calculator(EventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void Div()
        {
            if (this.Rhs == 0.0)
            {
                // 0除算エラー通知
                this.eventAggregator.GetEvent<PubSubEvent<Exception>>().Publish(new DivideByZeroException());
                return;
            }

            this.Answer = this.Lhs / this.Rhs;
        }
    }
}
