using System.Configuration;
using Microsoft.Synchronization.Services;

namespace DefaultScope {
    
    #region SyncService: Configuration and setup

    public class DefaultScopeSyncService : Microsoft.Synchronization.Services.SyncService<DefaultScopeOfflineEntities> 
    {
        public static void InitializeService(Microsoft.Synchronization.Services.ISyncServiceConfiguration config) 
        {
            // TODO: MUST set these values
            config.ServerConnectionString = ConfigurationManager.ConnectionStrings["ListDbConnectionString"].ToString();
                
            config.SetEnableScope("DefaultScope");
            config.AddFilterParameterConfiguration("userid", "User", "@ID", typeof(System.Guid));
            config.AddFilterParameterConfiguration("userid", "Item", "@UserID", typeof(System.Guid));
            config.AddFilterParameterConfiguration("userid", "List", "@UserID", typeof(System.Guid));
            config.AddFilterParameterConfiguration("userid", "TagItemMapping", "@UserID", typeof(System.Guid));
            // 
            // 
            // TODO: Optional.
            // config.UseVerboseErrors = true;
            // config.EnableDiagnosticPage = true;
            // config.SetDefaultSyncSerializationFormat(Microsoft.Synchronization.Services.SyncSerializationFormat.ODataJson);
            // config.SetConflictResolutionPolicy(Microsoft.Synchronization.Services.ConflictResolutionPolicy.ServerWins);
        }

        #region Interceptors

        // The code below demonstrates how to use the interceptor attributes to add custom logic to your sync service.
        // You can modify the code inside each of the method below as per your requirements.
        // For more information refer to documentation about the interceptor attributes.

        /// <summary>This method is invoked on a download request before getting changes from the local store.</summary>
        /// <param name="context">Instance of SyncOperationContext class that enables inspection of the request</param>
        [SyncRequestInterceptor("DefaultScope", SyncOperations.Download)]
        public void Download_Request(SyncOperationContext context)
        {
            // Add a dummy header to the response indicating that the interceptor method was invoked.
            context.ResponseHeaders.Add("DownloadRequestInterceptorFired", "true");
        }

        /// <summary>
        /// This method is invoked on an upload request before applying changes to the store. 
        /// This can be used to perform validations on the incoming entities and reject certain entities
        /// or abort the request.
        /// </summary>
        /// <param name="context">Instance of SyncOperationContext class that gives information about the current operation.</param>
        [SyncRequestInterceptor("DefaultScope", SyncOperations.Upload)]
        public void Upload_Request(SyncOperationContext context)
        {
            // Add a dummy header to the response indicating that the interceptor method was invoked.
            // Note: type casting the context to SyncUploadResponseOperationContext gives you access to the incoming entities and 
            // also enables you to reject entities based on custom logic.
            ((SyncUploadRequestOperationContext)context).ResponseHeaders.Add("UploadRequestInterceptorFired", "true");
        }

        /// <summary>
        /// This method is invoked on an download operation after enumerating changes from the store. 
        /// This can be used to look at the result of the download operation and run custom code.
        /// </summary>
        /// <param name="context">Instance of SyncOperationContext class that gives information about the current operation.</param>
        [SyncResponseInterceptor("DefaultScope", SyncOperations.Download)]
        public void Download_Response(SyncOperationContext context)
        {
            // Add a dummy header to the response indicating that the interceptor method was invoked.
            // Note: type casting the context to SyncUploadResponseOperationContext gives you access to the outgoing changes.
            ((SyncDownloadResponseOperationContext)context).ResponseHeaders.Add("DownloadResponseInterceptorFired", "true");
        }

        /// <summary>
        /// This method is invoked on an upload operation after applying changes to the store. 
        /// This can be used to look at the result of the upload operation and run custom code.
        /// </summary>
        /// <param name="context">Instance of SyncOperationContext class that gives information about the current operation.</param>
        [SyncResponseInterceptor("DefaultScope", SyncOperations.Upload)]
        public void Upload_Response(SyncOperationContext context)
        {
            // Add a dummy header to the response indicating that the interceptor method was invoked.
            // Note: type casting the context to SyncUploadResponseOperationContext gives you access to the conflicts and outgoing changes.
            ((SyncUploadResponseOperationContext)context).ResponseHeaders.Add("UploadResponseInterceptorFired", "true");
        }

        /// <summary>
        /// This method is invoked if a conflict is detected when applying changes on an upload operation. 
        /// This can be used to resolve the conflict by choosing the winner or returning a new entity as the winner.
        /// </summary>
        /// <param name="context">Instance of SyncOperationContext class that gives information about the current operation.</param>
        /// <param name="mergedEntity">
        /// The entity that was chosen to be applied as winner of the conflict
        /// when the method returns SyncConflictResolution.Merge. Ths value of this parameter
        /// is ignored when the resolution is SyncConflictResolution.ClientWins or SyncConflictResolution.ServerWins
        /// </param>
        [SyncConflictInterceptor("DefaultScope")]
        public SyncConflictResolution ConflictHandler(SyncConflictContext context, out IOfflineEntity mergedEntity)
        {
            // Add a dummy header to the response indicating that the interceptor method was invoked.
            context.ResponseHeaders.Add("ConflictInterceptorFired", "true");

            // The mergedEntity is used as the winner of the conflict
            // when this method returns SyncConflictResolution.Merge as the resolution.
            // For other resolutions such as SyncConflictResolution.ClientWins and SyncConflictResolution.ServerWins
            // the mergedEntity should be set to null.
            mergedEntity = null;
            
            return SyncConflictResolution.ClientWins;
        }

        #endregion
    }
    #endregion
}
