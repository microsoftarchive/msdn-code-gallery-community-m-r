namespace MyCompany.Common.Notification
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Text Merger
    /// </summary>
    public class TextMerger : ITextMerger
    {
        private const string TagDelimiter = "!";

        /// <summary>
        /// Merges the text with the mergers.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="mergers"></param>
        /// <returns></returns>
        public string Merge(string text, Dictionary<string, string> mergers)
        {
            foreach (var merger in mergers)
            {
                if (!string.IsNullOrEmpty(merger.Value))
                {
                    string pattern = string.Format("{0}{1}{0}", TagDelimiter, merger.Key);
                    text = Regex.Replace(text, pattern, merger.Value);
                }
            }
            return text;
        }
    }
}
