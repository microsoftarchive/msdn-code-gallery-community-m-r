namespace MyCompany.Visitors.Client.UniversalApp.Services.Tiles
{
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using System.Collections.Generic;
    using Windows.UI.StartScreen;

    /// <summary>
    /// Tiles service contract.
    /// </summary>
    public interface ITilesService
    {
        /// <summary>
        /// Update the main tile with next visits.
        /// </summary>
        /// <param name="visits">Visits collection.</param>
        void UpdateMainTile(IList<Visit> visits);

        /// <summary>
        /// Update the pin tile of a visit.
        /// </summary>
        /// <param name="visitItem">Visit item</param>
        /// <param name="tile">Tile</param>
        void UpdatePinTile(VisitItem visitItem, SecondaryTile tile);
    }
}
