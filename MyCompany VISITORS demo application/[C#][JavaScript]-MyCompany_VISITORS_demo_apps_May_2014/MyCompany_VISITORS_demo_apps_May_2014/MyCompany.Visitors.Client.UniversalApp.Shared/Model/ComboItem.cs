namespace MyCompany.Visitors.Client.UniversalApp.Model
{
    /// <summary>
    /// Represents a combo item.
    /// </summary>
    public class ComboItem
    {
        private int id;
        private string name;

        /// <summary>
        /// Item id.
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        /// <summary>
        /// Item display name.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
