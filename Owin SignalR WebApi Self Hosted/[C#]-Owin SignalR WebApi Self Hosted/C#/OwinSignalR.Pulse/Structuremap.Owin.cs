using System.Threading.Tasks;

using Microsoft.Owin;

using OwinSignalR.Data.Models;

namespace OwinSignalR.Pulse
{
    public class StructureMapOWINMiddleware
        : OwinMiddleware
    {
        public StructureMapOWINMiddleware(
            OwinMiddleware next)
            : base(next)
        {

        }

        public async override Task Invoke(IOwinContext context)
        {
            StructureMap.ObjectFactory.GetInstance<IDataContext>().Initialize(new Data.Models.OwinSignalrDbContext());
            await Next.Invoke(context);
        }
    }
}
