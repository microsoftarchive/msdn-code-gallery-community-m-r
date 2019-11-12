// Comment/uncomment the following line to switch between the C# and C++ game components.
//#define CSHARP

using ReversiGameModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Models
{
    /// <summary>
    /// Provides an IGame instance, using either the C# or C++ game component.
    /// </summary>
    public class GameFactory
    {
        /// <summary>
        /// Gets an IGame implementation with default configuration.
        /// </summary>
        /// <returns>The IGame.</returns>
        public static IGame GetGame() 
        {
            return GetGame(8);
        }

        /// <summary>
        /// Gets an IGame implementation with the specified board edge size.
        /// </summary>
        /// <param name="boardEdgeSize">The number of rows and columns on the (square) board.</param>
        /// <returns>The IGame.</returns>
        public static IGame GetGame(int boardEdgeSize)
        {
            return GetGame(boardEdgeSize, boardEdgeSize);
        }

        /// <summary>
        /// Gets an IGame implementation with the specified row and column count.
        /// </summary>
        /// <param name="rowCount">The number of rows on the board.</param>
        /// <param name="columnCount">The number of columns on the board.</param>
        /// <returns>The IGame.</returns>
        public static IGame GetGame(int rowCount, int columnCount)
        {
            #if CSHARP
                return (IGame) new ReversiGameComponentCS.Game(rowCount, columnCount);
            #else
                return (IGame) new CLRSerializableCPPGame(rowCount, columnCount);
            #endif
        }

        /// <summary>
        /// Gets the concrete type of the IGame implementation the factory is currently configured to use.
        /// </summary>
        /// <returns>The concrete IGame type.</returns>
        /// <remarks>This method is used to configure serialization in App.xaml.cs.</remarks>
        public static Type GetGameType()
        {
            #if CSHARP
                return typeof(ReversiGameComponentCS.Game);
            #else
                return typeof(CLRSerializableCPPGame);
            #endif
        }
    }
}
