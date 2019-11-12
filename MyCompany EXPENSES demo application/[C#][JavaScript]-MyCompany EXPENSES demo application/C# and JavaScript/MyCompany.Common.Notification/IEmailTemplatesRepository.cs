namespace MyCompany.Common.Notification
{
    using System.Collections.Generic;

    /// <summary>
    /// Email template repository interface
    /// </summary>
    public interface IEmailTemplatesRepository
    {
        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        string GetTemplate(string templateName);

        /// <summary>
        /// Processes the template.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="mergers">The mergers.</param>
        /// <returns></returns>
        string ProcessTemplate(string templateName, Dictionary<string, string> mergers);
    }
}
