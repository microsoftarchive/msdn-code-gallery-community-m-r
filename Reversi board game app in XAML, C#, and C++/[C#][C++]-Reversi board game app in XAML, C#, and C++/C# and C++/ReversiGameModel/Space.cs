using System;
using System.Runtime.Serialization;

namespace ReversiGameModel
{
    /// <summary>
    /// Represents a space on the game board.
    /// </summary>
    [DataContract]
    public sealed class Space : ISpace
    {
        /// <summary>
        /// The row of the space.
        /// </summary>
        [DataMember]
        public int Row { get; set; }

        /// <summary>
        /// The column of the space.
        /// </summary>
        [DataMember]
        public int Column { get; set; }

        /// <summary>
        /// Initializes a new Space.
        /// </summary>
        /// <param name="row">The row of the space.</param>
        /// <param name="column">The column of the space.</param>
        public Space(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Gets a string representation of the space in "(row,column)" format.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return String.Format("({0},{1})", Row, Column);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var space = obj as Space;
            if (space == null) return false;
            return this.Row == space.Row && this.Column == space.Column;
        }

        /// <summary>
        /// Serves as a hash function for the Space type.
        /// </summary>
        /// <returns>A hash code for the current Space.</returns>
        public override int GetHashCode()
        {
            return Row ^ Column;
        }
    }
}
