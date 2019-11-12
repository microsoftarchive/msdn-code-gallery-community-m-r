using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPrismSampleApp.Models
{
    public class AppContext : BindableBase
    {
        private EventAggregator EventAggregator { get; set; }

        public PubSubEvent<Exception> ErrorEvent
        {
            get { return this.EventAggregator.GetEvent<PubSubEvent<Exception>>(); }
        }

        public Calculator Calculator { get; private set; }

        public AppContext(Calculator calculator, EventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
            this.Calculator = calculator;
        }

        public void Reset()
        {
            this.Calculator.Rhs = 0;
            this.Calculator.Lhs = 0;
            this.Calculator.Answer = 0;
        }
    }
}
