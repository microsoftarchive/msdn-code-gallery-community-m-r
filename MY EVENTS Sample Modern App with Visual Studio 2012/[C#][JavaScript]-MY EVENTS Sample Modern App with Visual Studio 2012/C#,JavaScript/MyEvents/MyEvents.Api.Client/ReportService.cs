using System;
using System.Collections.Generic;
using System.Globalization;
using MyEvents.Api.Client.Web;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// <see cref="MyEvents.Api.Client.IReportService"/>
    /// </summary>
    internal class ReportService : BaseRequest, IReportService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="urlPrefix">server urlPrefix</param>
        /// <param name="authenticationToken">Authentication Token</param>
        public ReportService(string urlPrefix, string authenticationToken)
            : base(urlPrefix, authenticationToken)
        {

        }

        /// <summary>
        /// <see cref="MyEvents.Api.Client.IReportService"/>
        /// </summary>
        /// <param name="organizerId"><see cref="MyEvents.Api.Client.IReportService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IReportService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IReportService"/></returns>
        public IAsyncResult GetTopTagsAsync(int organizerId, Action<IList<Tag>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/tags/{1}", _urlPrefix, organizerId);

            return base.DoGet(url, callback);
        }
        
        /// <summary>
        /// <see cref="MyEvents.Api.Client.IReportService"/>
        /// </summary>
        /// <param name="organizerId"><see cref="MyEvents.Api.Client.IReportService"/></param>
        /// <param name="callback"><see cref="MyEvents.Api.Client.IReportService"/></param>
        /// <returns><see cref="MyEvents.Api.Client.IReportService"/></returns>
        public IAsyncResult GetTopSpeakersAsync(int organizerId, Action<IList<Speaker>> callback)
        {
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/speakers/{1}", _urlPrefix, organizerId);

            return base.DoGet(url, callback);
        }
    }
}
