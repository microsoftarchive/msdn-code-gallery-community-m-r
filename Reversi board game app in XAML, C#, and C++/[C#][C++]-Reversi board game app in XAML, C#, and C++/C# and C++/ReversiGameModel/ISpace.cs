using System;
using System.Runtime.Serialization;

namespace ReversiGameModel
{
    /// <summary>
    /// Represents a space on the game board.
    /// </summary>
    public interface ISpace
    {
        /// <summary>
        /// The row of the space.
        /// </summary>
        int Row { get; set; }

        /// <summary>
        /// The column of the space.
        /// </summary>
        int Column { get; set; }

        /// <summary>
        /// Gets a string representation of the space in "(row,column)" format.
        /// </summary>
        /// <returns>The string representation.</returns>
        string ToString();

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        bool Equals(object obj);

        /// <summary>
        /// Serves as a hash function for the Space type.
        /// </summary>
        /// <returns>A hash code for the current Space.</returns>
        int GetHashCode();
    }
}
