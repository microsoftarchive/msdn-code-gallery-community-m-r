namespace MyCompany.Visitors.Client.UniversalApp.Model.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight.Messaging;

    /// <summary>
    /// Visitor picture changed message
    /// </summary>
    public class VisitorPicturesChanged : NotificationMessage<ICollection<VisitorPicture>>
    {
        /// <summary>
        /// Visitor picture changed message contructor
        /// </summary>
        /// <param name="content"></param>
        public VisitorPicturesChanged(ICollection<VisitorPicture> content)
            : base(content, string.Empty)
        {
        }
    }
}
