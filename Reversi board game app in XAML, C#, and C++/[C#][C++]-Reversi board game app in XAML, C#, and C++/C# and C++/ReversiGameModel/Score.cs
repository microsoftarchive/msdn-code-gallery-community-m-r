using System.Runtime.Serialization;

namespace ReversiGameModel
{
    /// <summary>
    /// Represents the current score of a game.
    /// </summary>
    [DataContract]
    public sealed class Score : IScore
    {
        /// <summary>
        /// Gets or sets the score for player one.
        /// </summary>
        [DataMember]
        public int PlayerOne { get; set; }

        /// <summary>
        /// Gets or sets the score for player two.
        /// </summary>
        [DataMember]
        public int PlayerTwo { get; set; }

        /// <summary>
        /// Initializes a new Score object. 
        /// </summary>
        public Score() { }

        /// <summary>
        /// Initializes a new Score object with the specified score values.
        /// </summary>
        /// <param name="playerOne">Player one's score.</param>
        /// <param name="playerTwo">Player two's score.</param>
        public Score(int playerOne, int playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }
    }
}
