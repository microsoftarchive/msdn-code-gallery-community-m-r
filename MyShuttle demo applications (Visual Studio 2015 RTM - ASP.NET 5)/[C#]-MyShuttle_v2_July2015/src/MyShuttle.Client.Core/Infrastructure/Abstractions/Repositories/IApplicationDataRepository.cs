namespace MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories
{
    public interface IApplicationDataRepository
    {
        string GetStringFromApplicationData(string key, string defaultValue);

        bool? GetOptionalBooleanFromApplicationData(string key, bool? defaultValue);

        int? GetOptionalIntegerFromApplicationData(string key, int? defaultValue);

        void SetStringToApplicationData(string key, string value);
    }
}