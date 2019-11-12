using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismWPFSampleApp.ViewModels
{
    public static class InteractionRequestExtensions
    {
        public static IObservable<T> RaiseAsObservable<T>(this InteractionRequest<T> self, T notification)
            where T : INotification
        {
            var s = new AsyncSubject<T>();
            self.Raise(notification, x => { s.OnNext(x); s.OnCompleted(); });
            return s.AsObservable();
        }
    }
}
