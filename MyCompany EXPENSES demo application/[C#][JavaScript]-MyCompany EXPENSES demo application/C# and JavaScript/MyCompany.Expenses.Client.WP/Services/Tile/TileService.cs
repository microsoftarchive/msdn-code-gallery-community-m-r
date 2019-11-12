namespace MyCompany.Expenses.Client.WP.Services.Tile
{
    using Microsoft.Phone.Shell;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Tile service contract implementation
    /// </summary>
    public class TileService : ITileService
    {
        /// <summary>
        /// Update the main app tile with the text, image and info passed.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="backCaption"></param>
        /// <param name="backTitle"></param>
        /// <param name="count"></param>
        public void UpdateMainTile(string image, string backCaption, string backTitle, int count)
        {
            var tileToUpdate = ShellTile.ActiveTiles.First();
            
            var updateTileData = new FlipTileData()
            {
                WideBackContent = backCaption,
                BackContent = backCaption,
                BackTitle = backTitle
            };

            tileToUpdate.Update(updateTileData);
        }

        /// <summary>
        /// Create a secondary tile.
        /// </summary>
        /// <param name="navUrl"></param>
        /// <param name="frontImage"></param>
        /// <param name="frontTitle"></param>
        /// <param name="backTitle"></param>
        /// <param name="backCaption"></param>
        public void PinSecondaryTile(string navUrl, string frontImage, string frontTitle, string backTitle, string backCaption)
        {
            var tileToUpdate = ShellTile.ActiveTiles.Where(t => t.NavigationUri.OriginalString.Contains(navUrl)).FirstOrDefault();

            if (tileToUpdate == null)
            {
                var tileData = new StandardTileData()
                {
                    BackgroundImage = new Uri(frontImage, UriKind.Relative),
                    Title = frontTitle,
                    BackTitle = backTitle,
                    BackContent = backCaption
                };

                ShellTile.Create(new Uri(navUrl, UriKind.Relative), tileData);
            }
        }

        /// <summary>
        /// get if a url is pinned or not.
        /// </summary>
        /// <param name="navUrl"></param>
        /// <returns></returns>
        public bool IsPinned(string navUrl)
        {
            var tileToUpdate = ShellTile.ActiveTiles.Where(t => t.NavigationUri.OriginalString.Contains(navUrl)).FirstOrDefault();

            return (tileToUpdate != null);
        }

        /// <summary>
        /// Remove a secondary tile.
        /// </summary>
        /// <param name="navUrl"></param>
        public void UnpinSecondaryTile(string navUrl)
        {
            var tileToUpdate = ShellTile.ActiveTiles.Where(t => t.NavigationUri.OriginalString.Contains(navUrl)).FirstOrDefault();

            tileToUpdate.Delete();
        }
    }
}
