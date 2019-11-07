using System.Runtime.Serialization;

namespace ReversiGameModel
{
    /// <summary>
    /// Represents the current score of a game.
    /// </summary>
    public interface IScore
    {
        /// <summary>
        /// Gets or sets the score for player one.
        /// </summary>
        int PlayerOne { get; set; }

        /// <summary>
        /// Gets or sets the score for player two.
        /// </summary>
        int PlayerTwo { get; set; }
    }
}
