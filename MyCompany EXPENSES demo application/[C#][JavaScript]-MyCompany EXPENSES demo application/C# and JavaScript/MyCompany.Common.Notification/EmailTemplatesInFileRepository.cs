namespace MyCompany.Common.Notification
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Email templates repository
    /// </summary>
    public class EmailTemplatesInFileRepository : IEmailTemplatesRepository
    {
        private readonly ITextMerger _textMerger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplatesInFileRepository" /> class.
        /// </summary>
        /// <param name="textMerger">The text merger.</param>
        public EmailTemplatesInFileRepository(ITextMerger textMerger)
        {
            _textMerger = textMerger;
        }

        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <returns></returns>
        public string GetTemplate(string templateName)
        {
            string basePath = Path.Combine(GetBasePath(), "EmailTemplates"); 

            string[] paths = new string[]
                                 {
                                     string.Format(@"{0}\{1}.{2}", basePath, "header", "html"),
                                     string.Format(@"{0}\{1}.{2}", basePath, templateName, "html"),
                                     string.Format(@"{0}\{1}.{2}", basePath, "footer", "html")
                                 };

            string text = string.Empty;
            foreach (var path in paths)
            {
                var streamReader = new StreamReader(path);
                text = text + streamReader.ReadToEnd();
                streamReader.Close();
            }
            return text;
        }

        /// <summary>
        /// Processes the template.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="mergers">The mergers.</param>
        /// <returns></returns>
        public string ProcessTemplate(string templateName, Dictionary<string, string> mergers)
        {
            string text = GetTemplate(templateName);
            return _textMerger.Merge(text, mergers);
        }

        private static string GetBasePath()
        {
            if (System.Web.HttpContext.Current == null) 
                return AppDomain.CurrentDomain.BaseDirectory;
            else 
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
        } 
    }
}
