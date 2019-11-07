namespace MyCompany.Visitors.Client.UniversalApp.Selectors
{
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Template selector for the visits gridview.
    /// </summary>
    public class VisitsTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Data template for the first group of visits.
        /// </summary>
        public DataTemplate FirstGroupTemplate { get; set; }

        /// <summary>
        /// Data template for the second group of visits.
        /// </summary>
        public DataTemplate SecondGroupTemplate { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VisitsTemplateSelector()
        {
            FirstGroupTemplate = null;
            SecondGroupTemplate = null;
        }

        /// <summary>
        /// Select template core.
        /// </summary>
        /// <param name="item">Item</param>
        /// <param name="container">Container</param>
        /// <returns></returns>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            VisitItem currentItem = (VisitItem)item;

            if (currentItem.GroupId == 1)
                return FirstGroupTemplate;
            else
                return SecondGroupTemplate;
        }
    }
}
