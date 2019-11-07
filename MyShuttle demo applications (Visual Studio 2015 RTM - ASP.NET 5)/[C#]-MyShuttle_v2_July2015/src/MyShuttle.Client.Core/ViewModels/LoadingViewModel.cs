using MyShuttle.Client.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.Client.Core.ViewModels
{
    public class LoadingViewModel : NavegableViewModel
    {
        public const int DELAY = 4000;

        public LoadingViewModel() : base()
        {
        }

        public override void Start()
        {
            base.Start();

            Task.Delay(DELAY).ContinueWith(_ => this.Close(this));
        }
    }
}
