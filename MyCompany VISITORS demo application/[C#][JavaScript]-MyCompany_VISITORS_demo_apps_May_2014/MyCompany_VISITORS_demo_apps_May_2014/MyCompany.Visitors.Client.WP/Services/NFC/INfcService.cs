namespace MyCompany.Visitors.Client.WP.Services.NFC
{
    using MyCompany.Visitors.Client.WP.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contract for NFC Service
    /// </summary>
    public interface INfcService
    {
        /// <summary>
        /// Start peer discovering
        /// </summary>
        /// <param name="pInfo"></param>
        void StartPeerFinder(Visitor pInfo);

        /// <summary>
        /// Send personal information to windows 8 application.
        /// </summary>
        /// <param name="pInfo"></param>
        /// <returns></returns>
        void SendInfo(Visitor pInfo);
    }
}
