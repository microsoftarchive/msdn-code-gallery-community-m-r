using System;

namespace MyEvents.Api.Client
{
    /// <summary>
    /// Interfaz to access to the controllers exposed by MyEvents.API
    /// </summary>
    public interface IMyEventsClient
    {
        /// <summary>
        /// Interfaz to access to the Event Definition Service 
        /// </summary>
        IEventDefinitionService EventDefinitionService { get; }

        /// <summary>
        /// Interfaz to access to the Registered User Service 
        /// </summary>
        IRegisteredUserService RegisteredUserService { get; }

        /// <summary>
        /// Interfaz to access to the Session Service
        /// </summary>
        ISessionService SessionService { get; }

        /// <summary>
        /// Interfaz to access to the Material Service
        /// </summary>
        IMaterialService MaterialService { get; }

        /// <summary>
        /// Interfaz to access to the Comment Service
        /// </summary>
        ICommentService CommentService { get; }

        /// <summary>
        /// Property to access to the Report Service client
        /// </summary>
        IReportService ReportService { get; }

        /// <summary>
        /// Property to access to the Authentication Service client
        /// </summary>
        IAuthenticationService AuthenticationService { get; }

        /// <summary>
        /// Property to access to the RoomPoint Service client
        /// </summary>
        IRoomPointService RoomPointService { get; }

        /// <summary>
        /// Set Access Token
        /// </summary>
        /// <param name="token"></param>
        void SetAccessToken(string token);
    }
}
