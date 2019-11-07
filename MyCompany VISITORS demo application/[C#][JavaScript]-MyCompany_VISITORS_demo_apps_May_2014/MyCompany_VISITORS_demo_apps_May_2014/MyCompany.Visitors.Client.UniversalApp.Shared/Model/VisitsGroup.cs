namespace MyCompany.Visitors.Client.UniversalApp.Model
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Group of visits to show in gridview
    /// </summary>
    public class VisitsGroup
    {
        /// <summary>
        /// Group Id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Collections of VisitItems
        /// </summary>
        public ObservableCollection<VisitItem> Items { get; set; }
    }
}
