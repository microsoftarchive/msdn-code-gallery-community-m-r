namespace MyShuttle.Client.Core.Infrastructure.Abstractions.Services
{
    public interface IApplicationStorageService
    {
        string SecurityToken { get; set; }

        void Refresh();
    }
}
