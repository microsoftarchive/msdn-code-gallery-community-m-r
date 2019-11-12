using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPrismSampleApp.Utils
{
    public static class InteractionRequestExtensions
    {
        public static IObservable<T> RaiseAsObservable<T>(this InteractionRequest<T> self, T n)
            where T : INotification
        {
            return Observable.Create<T>(o =>
            {
                self.Raise(n, result => { o.OnNext(result); o.OnCompleted(); });
                return Disposable.Empty;
            });
        }

    }
}
