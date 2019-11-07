namespace MyCompany.Visitors.Client.UniversalApp.Model
{
    using System;

    /// <summary>
    /// Received visitor event args
    /// </summary>
    public class VisitorEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="visitor">RVisitor received</param>
        public VisitorEventArgs(Visitor visitor)
        {
            Visitor = visitor;
        }

        /// <summary>
        /// Visitor received.
        /// </summary>
        public Visitor Visitor { get; set; }
    }
}
