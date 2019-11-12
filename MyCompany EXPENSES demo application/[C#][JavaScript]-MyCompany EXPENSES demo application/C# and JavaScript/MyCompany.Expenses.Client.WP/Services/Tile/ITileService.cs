namespace MyCompany.Expenses.Client.WP.Services.Tile
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Tile service contract
    /// </summary>
    public interface ITileService
    {
        /// <summary>
        /// Update the main app tile with the text, image and info passed.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="backCaption"></param>
        /// <param name="backTitle"></param>
        /// <param name="count"></param>
        void UpdateMainTile(string image, string backCaption, string backTitle, int count);

        /// <summary>
        /// get if a url is pinned or not.
        /// </summary>
        /// <param name="navUrl"></param>
        /// <returns></returns>
        bool IsPinned(string navUrl);

        /// <summary>
        /// Create a secondary tile.
        /// </summary>
        /// <param name="navUrl"></param>
        /// <param name="frontImage"></param>
        /// <param name="frontTitle"></param>
        /// <param name="backTitle"></param>
        /// <param name="backCaption"></param>
        void PinSecondaryTile(string navUrl, string frontImage, string frontTitle, string backTitle, string backCaption);

        /// <summary>
        /// Remove a secondary tile.
        /// </summary>
        /// <param name="navUrl"></param>
        void UnpinSecondaryTile(string navUrl);
    }
}
