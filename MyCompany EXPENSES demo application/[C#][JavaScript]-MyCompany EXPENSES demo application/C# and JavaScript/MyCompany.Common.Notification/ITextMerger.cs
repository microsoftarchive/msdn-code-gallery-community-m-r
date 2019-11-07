namespace MyCompany.Common.Notification
{
    using System.Collections.Generic;

    /// <summary>
    /// Text merger interface
    /// </summary>
    public interface ITextMerger
    {
        /// <summary>
        /// Merges the text with the merger
        /// </summary>
        /// <param name="text"></param>
        /// <param name="mergers"></param>
        /// <returns></returns>
        string Merge(string text, Dictionary<string, string> mergers);
    }
}
