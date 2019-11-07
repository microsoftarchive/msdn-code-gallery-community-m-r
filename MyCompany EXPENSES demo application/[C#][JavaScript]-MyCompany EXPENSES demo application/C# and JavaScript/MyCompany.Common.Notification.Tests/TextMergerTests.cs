using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MyCompany.Common.Notification.Tests
{
    [TestClass]
    public class TextMergerTests
    {
        [TestMethod]
        public void Merge_SubstitutesTheMergeTagsWithTheValues()
        {
            const string originalText = "It's an incredible test! " +
                                        "It's not, Mr. !SurName!? " +
                                        "!Name! " +
                                        "blah, blah !Name! ";

            const string expectedText = "It's an incredible test! " +
                                        "It's not, Mr. My Surname? " +
                                        "My Name " +
                                        "blah, blah My Name ";

            var textMerger = new TextMerger();
            var mergers = new Dictionary<string, string>()
                              {
                                  {"Name", "My Name"},
                                  {"SurName", "My Surname"}
                              };
            string result = textMerger.Merge(originalText, mergers);
            Assert.AreEqual(expectedText, result);
        }
    }
}
