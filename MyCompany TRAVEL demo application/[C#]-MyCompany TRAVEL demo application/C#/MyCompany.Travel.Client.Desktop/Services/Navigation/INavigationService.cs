namespace MyCompany.Travel.Client.Desktop.Services.Navigation
{
    /// <summary>
    /// Contract for navigation service.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigate to the previous content in the stack.
        /// </summary>
        void NavigateBack();

        /// <summary>
        /// Open Travel Request form
        /// <param name="travelRequest">travelRequest</param>
        /// </summary>
        void NavigateToTravelRequest(TravelRequest travelRequest);

        /// <summary>
        /// Open Travel Request list
        /// </summary>
        void NavigateToTravelRequestList();
    }
}
