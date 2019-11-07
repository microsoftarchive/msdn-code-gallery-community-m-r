namespace Reversi.ViewModels
{
    /// <summary>
    /// Defines values that indicate the visual state of a board space.
    /// </summary>
    public enum BoardSpaceState
    {
        None,
        PlayerOne,
        PlayerTwo,
        PlayerOneHint,
        PlayerTwoHint,
        PlayerOneNewPiece,
        PlayerTwoNewPiece,
        PlayerOneNewCapture,
        PlayerTwoNewCapture
    }
}
