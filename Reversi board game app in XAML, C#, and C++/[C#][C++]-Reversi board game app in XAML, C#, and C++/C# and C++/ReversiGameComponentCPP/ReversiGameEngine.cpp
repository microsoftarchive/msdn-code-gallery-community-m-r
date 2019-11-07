#include "pch.h"
#include "ReversiGameEngine.h"

using namespace ReversiCPP;
using namespace concurrency;
using namespace std;


/// <summary>
/// Initializes a new instance of the ReversiGameWrapper class using the specified
/// board size.
/// </summary>
/// <param name="rowCount">The number of rows on the board.</param>
/// <param name="columnCount">The number of columns on the board.</param>
ReversiGameEngine::ReversiGameEngine(int rowCount, int columnCount): m_columnCount(columnCount), m_rowCount(rowCount), m_randomGenerator(rd())
{			
    if (rowCount < 2)
    {
        // Demonstrates throwing std::exception types. 
        // An alternative to throwing logic_error is to assert.
        throw logic_error("Row count must be 2 or greater.");
    }
    if (columnCount < 2)
    {
        throw logic_error("Column count must be 2 or greater.");
    }

    InitializeBoard();
}

/// <summary>
/// Enables board to be reset after it has been default-constructed.
/// </summary>
void ReversiGameEngine::ResetBoard(int rows, int cols)
{
    m_rowCount = rows;
    m_columnCount = cols;
    InitializeBoard();
}

/// <summary>
/// Methods to manipulate the Moves and MoveStack lists. 
/// </summary>
void ReversiGameEngine::ResetMoves(vector<ReversiSpace> v)
{
    m_Moves.assign(begin(v), end(v));
}

void ReversiGameEngine::ResetMoveStack(vector<ReversiSpace> v)
{
    m_MoveStack.assign(begin(v), end(v));
}

void ReversiGameEngine::ResetMoveStackToMoveList()
{
    m_MoveStack.assign(begin(m_Moves), end(m_Moves));
}

void ReversiGameEngine::PushMove(ReversiSpace sp)
{
    m_Moves.push_back(sp);
}

void ReversiGameEngine::PushToMoveStack(ReversiSpace sp)
{
    m_MoveStack.push_back(sp);
}

/// <summary>
/// Public method that doesn't require knowledge of the internal board. This always refers to
/// the score of the actual game, not an AI hypothetical outcome.
/// </summary>
/// <param name="board">The board to score.</param>
/// <returns>The score of the game.</returns>
ReversiScore ReversiGameEngine::GetScore() const
{
    return GetScore(m_board);
}

/// <summary>
/// Gets the score of the specified game board.
/// </summary>
/// <param name="board">The board to score.</param>
/// <returns>The score.</returns>
ReversiScore ReversiGameEngine::GetScore(const ReversiBoard& board) const
{
    int playerOneScore = 0;
    int playerTwoScore = 0;

    for (const auto& state : board)
    {
        if (state == ReversiState::One)
        {
            ++playerOneScore;
        }
        else if ( state == ReversiState::Two)
        {
            ++playerTwoScore;
        }
    }

    return ReversiScore(playerOneScore, playerTwoScore);
}

/// <summary>
/// Gets the value of the specified space on the game board.
/// </summary>
ReversiState ReversiGameEngine::GetAt(size_t row, size_t col) 
{
    size_t spaceNumber = row * m_columnCount + col;
    if (spaceNumber < 0 || spaceNumber >= m_board.size())
    {
        throw logic_error("Invalid board position.");
    }
    return m_board[spaceNumber];
}

/// <summary>
/// Gets a value that indicates the winner of the game represented by the specified game board.
/// </summary>
/// <param name="board"></param>
/// <returns></returns>
ReversiState ReversiGameEngine::GetWinner(const ReversiBoard& board) const
{
    auto score = GetScore(board);
    if (!IsGameOver(board) || score.PlayerOne == score.PlayerTwo) 
    {
        return ReversiState::None;
    }
    return score.PlayerOne > score.PlayerTwo ? ReversiState::One : ReversiState::Two;
}

/// <summary>
/// Sets the value of the specified space on the game board.
/// </summary>
void ReversiGameEngine::SetAt(ReversiState state, size_t row, size_t col)
{
    size_t spaceNumber = row * m_columnCount + col;
    if (spaceNumber < 0 || spaceNumber >= m_board.size())
    {
        throw logic_error("Invalid board position.");
    }
    m_board[spaceNumber] = state;
}

/// <summary>
/// Returns the game to the state it was in after the specified number of turns.
/// </summary>
/// <param name="turnCount">The number of turns.</param>
/// <returns>A list of the vector<ReversiSpace> affected by the last move, starting with the space 
/// of the move and including the vector<ReversiSpace> of all the pieces captured by the move.</returns>
vector<ReversiSpace> ReversiGameEngine::Reset(size_t turnCount)
{
    if (turnCount < 0 || turnCount > m_MoveStack.size())
    {
        throw logic_error("must be 0 or greater and no higher than m_MoveStack.size().");
    }

    if (turnCount == m_Moves.size()) 
    {
        throw logic_error("must be different than m_Moves.size().");
    }
    vector<ReversiSpace> newMoves;
    size_t moveCount = m_Moves.size();

    if (turnCount < moveCount)
    {
        // Backward navigation.
        InitializeBoard();
        auto end = begin(m_Moves) + turnCount;
        newMoves.assign(begin(m_Moves), end);
        m_Moves.clear();
    }
    else
    {
        // Forward navigation.
        auto beg = begin(m_MoveStack) + moveCount;
        auto end = beg + (turnCount - moveCount);
        newMoves.assign(beg, end);		
    }
    return Move(newMoves);
}

vector<ReversiSpace> ReversiGameEngine::Pass()
{ 
    return Move(ReversiSpace(-1, -1)); 
}

/// <summary>
/// Performs a move on the specified board.
/// </summary>
/// <returns>The ReversiSpaces that area affected by the move, starting with the space 
/// of the move and including the spaces of all the pieces captured by the move.
/// </returns>
vector<ReversiSpace> ReversiGameEngine::Move(int row, int col)
{
    return Move(ReversiSpace(row, col));
}

/// <summary>
/// Performs a move on the specified board.
/// </summary>
/// <returns>The ReversiSpaces that area affected by the move, starting with the space 
/// of the move and including the spaces of all the pieces captured by the move.
/// </returns>
vector<ReversiSpace> ReversiGameEngine::Move(const ReversiSpace& move)
{
    auto changedSpaces = Move(m_board, move, GetCurrentPlayer());
    m_Moves.push_back(move);

    // If the move diverges from the move stack, synchronize
    // the Moves and MoveStack lists.  
    if (m_Moves.size() > m_MoveStack.size())
    {
        m_MoveStack.push_back(move);
    }
    else if (!(m_MoveStack[m_Moves.size() - 1] == move))
    {
        m_MoveStack = vector<ReversiSpace>(m_Moves);
    }

    return changedSpaces;
}

/// <summary>
/// Performs the moves indicated by the specified list.
/// </summary>
/// <param name="moves">The moves to perform.</param>
/// <returns>The ReversiSpaces that area affected by the move, starting with the space 
/// of the move and including the spaces of all the pieces captured by the move.
/// </returns>
vector<ReversiSpace> ReversiGameEngine::Move(const vector<ReversiSpace>& moves)
{
    vector<ReversiSpace> affectedSpaces;

    unsigned int size = moves.size();
    for (unsigned int i = 0; i < size; ++i)
    {
        if (i < size - 1)
            Move(moves[i]);
        else
            affectedSpaces = Move(moves[i]);
    }
    return affectedSpaces;
}

/// <summary>
/// Performs the moves indicated by the specified string.
/// </summary>
/// <param name="moves">The moves to perform, in "rc,rc,rc,..." format, 
/// where each "rc" is the row and column of a move.</param>
/// <remarks>
/// This method supports testing specific board states that result from 
/// a series of moves. However, it does not support 10x10 or larger boards.
/// </remarks>
void ReversiGameEngine::Move(const wstring& moves)
{
    // We only support board sizes up to 10x10 even though this
    // restriction is not actually enforced yet.
    // Therefore no row or column value is > 9. So all are single digits.
    // Example call: game.Move("13,01,00,03,02,23,33") 
    // where "13" in this case means Player 1 moves to Row 1, Col 3,
    // Player 2 moves to Row 0, Col 1, and so on.

    // The separator character to split on.
    const wregex r(L",");

    // -1 in the iterator constructor means take everything after the last 
    // separator (or the beginning of the string if there is no previous separator).
    // The loop iterates over each 2-character token in the split string.
    // cbegin() and cend() are const iterators and are used here because we only read 
    // from the source.
    for (wsregex_token_iterator i(cbegin(moves), cend(moves), r, -1), end; i != end; ++i)
    {
        if (i->str() == L"--")
        {
            Pass();
        }
        else
        {
            // Convert each character to int.
            // The [] access is ok because we control the strings
            // and know they don't contain a surrogate pair.
            wchar_t digit = i->str()[0];
            int row = stoi(&digit);
            digit = i->str()[1];
            int col = stoi(&digit);

            // Perform the move.
            Move(row, col);
        }
    }
}

/// <summary>
/// Gets the player whose turn it is now.
/// </summary>
ReversiState ReversiGameEngine::GetCurrentPlayer() const
{
    if (m_Moves.empty())
    {
        return ReversiState::One;
    }

    return m_Moves.size() % 2 == 0 ? ReversiState::One : ReversiState::Two;
}

///<summary>
/// Returns true if the board is full or if there are no valid moves left.
///</summary>
bool ReversiGameEngine::IsGameOver(const ReversiBoard& board) const
{
    size_t notUsed;
    return IsBoardFilled(board) ||
        (GetValidMoveSpaces(board, ReversiState::One, notUsed).empty() &&
        GetValidMoveSpaces(board, ReversiState::Two, notUsed).empty());
}

///<summary>
/// Public function called from wrapper class that doesn't require knowledge
/// of the internal board. When this is called from outside it's always referring
/// to the actual game board, not a copy being evaluated during the AI recursion.
///</summary>
bool ReversiGameEngine::IsGameOver() const
{    
    return IsGameOver(m_board);
}

/// <summary>
/// Gets a value that indicates whether the specified move is legal.
/// </summary>
bool ReversiGameEngine::IsValidMove(const ReversiSpace& move) const
{
    return move.Column != -1 ? 
        IsValidMoveSpace(m_board, GetCurrentPlayer(), move) : 
        IsPassValid(GetCurrentPlayer());
}

/// <summary>
/// Gets a value that indicates whether the specified move is a legal for the given player and game board.
/// </summary>
/// <param name="board">The game board.</param>
/// <param name="player">The player whose turn it is for the specified board.</param>
/// <param name="move">The move to assess.</param>
/// <returns>True if the move is legal; otherwise, false.</returns>
bool ReversiGameEngine::IsValidMoveSpace(const ReversiBoard& board, ReversiState  player, const ReversiSpace& move) const
{
    return IsSpaceOnBoard(board, move) &&
        IsSpaceUnoccupied(board, move) &&
        DoesMoveHaveCapture(board, player, move);
}

/// <summary>
/// Gets the best move that the AI can find for the specified search depth.
/// </summary>
/// <param name="searchDepth">The AI search depth to use.</param>
/// <returns>The space of the move.</returns>
ReversiSpace  ReversiGameEngine::GetBestMove(ReversiBoard& board, int searchDepth)
{
    if (searchDepth < 1)
    {
        throw logic_error("search depth must be 1 or greater.");
    }

    // If it is the AI's turn and we're not at the end of the move stack,
    // just use the next move in the stack. This is necessary to preserve
    // the forward stack, but it also prevents the AI from having to search again.
    size_t movesSize = m_Moves.size();

    if (movesSize < m_MoveStack.size()) 
    {        
        return m_MoveStack[movesSize];
    }

    return GetBestMove(board, GetCurrentPlayer(), searchDepth);
}

/// <summary>
/// Gets the best move that the AI can find for the specified board, player, and search depth.
/// </summary>
ReversiSpace ReversiGameEngine::GetBestMove(ReversiState player, int searchDepth)
{
    return GetBestMove(m_board, player, searchDepth);
}

///<summary>
/// Prints out a diagram of the current board state. We do this in a 
/// nested loop as a math-free way to format the line breaks.
///</summary>
wstring ReversiGameEngine::DebugPrintBoard(const wstring& msg) const
{
    wostringstream wos;
    wos << msg << L"\n";

    for (int i = 0; i < m_rowCount; ++i)
    {
        for (int j = 0; j < m_columnCount; ++j)
        {
            wos << static_cast<int>( m_board[i * m_columnCount + j]) << L" ";
        }
        wos << L"\n";
    }
    OutputDebugString(wos.str().c_str());

    return wos.str();
}

///<summary>
/// Returns the state of all spaces on the board: 0, 1 or 2
///</summary>
wstring ReversiGameEngine::ToString()
{
    wostringstream wos;
    for (const auto elem : m_board)
    {
        wos << static_cast<int>(elem);
    }

    return wos.str();
}

/// <summary>
/// Gets a value that indicates whether a space is within the bounds of the specified game board.
/// </summary>
/// <param name="board">The game board.</param>
/// <param name="space">The space to assess.</param>
/// <returns>True if the space is within the bounds of the game board; otherwise, false.</returns>
bool ReversiGameEngine::IsSpaceOnBoard(const ReversiBoard& board, const ReversiSpace& space) const
{
    return space.Row >= 0 && space.Row < m_rowCount &&
        space.Column >= 0 && space.Column < m_columnCount;
}

/// <summary>
/// Gets a value that indicates whether a space is empty on the specified game board.
/// </summary>
/// <param name="board">The game board.</param>
/// <param name="space">The space to assess.</param>
/// <returns>True if the space is empty; otherwise, false.</returns>
bool ReversiGameEngine::IsSpaceUnoccupied(const ReversiBoard& board, const ReversiSpace& space) const
{
    assert(space.Row < m_rowCount && space.Column < m_columnCount);
    return board[space.Row * m_columnCount + space.Column] == ReversiState::None;
}

/// <summary>
/// Gets a value that indicates whether specified board has pieces in all of its spaces.
/// </summary>
/// <param name="board"></param>
/// <returns></returns>
bool ReversiGameEngine::IsBoardFilled(const ReversiBoard& board) const
{
    auto score = GetScore(board);
    return score.PlayerOne + score.PlayerTwo == board.size();
}


/// <summary>
/// Gets a value that indicates whether a move has any captures on the specified game board.
/// </summary>Board board
/// <param name="board">The game board.</param>
/// <param name="player">The player whose turn it is for the specified board.</param>
/// <param name="move">The move to assess.</param>
/// <returns>True if the move has captures; otherwise, false.</returns>
bool ReversiGameEngine::DoesMoveHaveCapture(const ReversiBoard& board, ReversiState  player, const ReversiSpace& move) const
{
    for (const auto& dir : m_Directions)
    {
        if (DoesMoveHaveCaptureInDirection(board, player, move, dir))
        {
            return true;
        }
    }
    return false;
}

/// <summary>
/// Gets a value that indicates whether a move has any captures in a particular direction 
/// on the specified game board.
/// </summary>
/// <param name="board">The game board.</param>
/// <param name="player">The player whose turn it is for the specified board.</param>
/// <param name="move">The move to assess.</param>
/// <param name="direction">The direction to assess (as a space with a relative position).</param>
/// <returns>True if the move has any captures in the given direction; otherwise, false.</returns>
bool ReversiGameEngine::DoesMoveHaveCaptureInDirection(const ReversiBoard& board, ReversiState  player,
                                                       const ReversiSpace& move, const ReversiSpace& direction) const
{
    int nextRow = move.Row;
    int nextCol = move.Column;
    bool hasCapture = false;

    // If the first space is our piece then there is no capture.
    bool firstSpace = true;

    // Walk down the specified direction up to an edge
    // unless we can break before that.
    while ((nextRow += direction.Row) >= 0 &&
        nextRow < m_rowCount &&
        (nextCol += direction.Column) >= 0 &&
        nextCol < m_columnCount)
    {

        if (board[nextRow * m_columnCount + nextCol] == ReversiState::None)
        {
            // We have reached an empty space without having
            // encountered an opponent. So no capture in this direction.
            break;
        }

        if (board[nextRow * m_columnCount + nextCol] == player)
        {
            // We have encountered an opponent before encountering
            // an empty space. That's a capture
            if (!firstSpace)
            {
                hasCapture = true;
            }
            break;
        }

        firstSpace = false;
    }

    return hasCapture;
}

/// <summary>
/// Gets a list of all the spaces that a piece can move to in this turn.
/// </summary>
/// <param name="board">The game board.</param>
/// <param name="player">The player who is moving.</param>
/// <param name="size">An out parameter that is used only when this method is called from GetMoveEvaluations. 
/// This enables the caller to avoid a vector copy. The parameter is not used when this method is called elsewhere.</param>
/// <returns>The list of spaces.</returns>
/// <remarks>
/// This method creates lists for particular board sizes on demand, then
/// reuses the lists as needed.
/// </remarks>
vector<ReversiSpace> ReversiGameEngine::GetValidMoveSpaces(const ReversiBoard& board, ReversiState  player, size_t& size) const
{  
    vector<ReversiSpace> validSpaces; 
    for (int row = 0; row < m_rowCount; ++row)
    {
        for (int col = 0; col < m_columnCount; ++col)
        {
            ReversiSpace reversiMove(row, col);
            if (IsValidMoveSpace(board, player, reversiMove))
            {
                validSpaces.push_back(reversiMove);
            }
        }
    }

    size += validSpaces.size();
    return validSpaces;		
}

/// <summary>
/// Gets a list of all the vector<ReversiSpace> on the specified game board.
/// </summary>
/// <param name="board">The game board.</param>
/// <returns>The list of spaces.</returns>
/// <remarks>
/// This method creates lists for particular board sizes on demand, then
/// reuses the lists as needed.
/// </remarks>
vector<ReversiSpace> ReversiGameEngine::GetAllSpaces(ReversiBoard& board)
{
    pair<int, int> boardDimensions( m_rowCount, m_columnCount);

    // See whether we have already stored a board with these dimensions.
    // If so, then use it. If not, then make a new one.
    if (m_allSpaces.find(boardDimensions) == m_allSpaces.end())
    {
        vector<ReversiSpace> spaces;
        for (int i = 0; i < m_rowCount; ++i) 
        {
            for (int j = 0; j < m_columnCount; ++j)
            {
                spaces.emplace_back(i, j);
            }
        }
        spaces.swap(m_allSpaces[boardDimensions]);

        return spaces;
    }

    return m_allSpaces[boardDimensions];
}

/// <summary>
/// Gets the difference in score for the specified game board.
/// </summary>
/// <param name="board">The game board to assess.</param>
/// <param name="player">The player for whom to assess the game board.</param>
/// <returns>The score difference of the game board from the perspective of the specified player;
/// that is, a positive value if the player is winning, a negative value if the player is losing.</returns>
int ReversiGameEngine::GetScoreDifference(const ReversiBoard& board, ReversiState  player) const
{
    auto score = GetScore(board);
    return player == ReversiState::One ?
        score.PlayerOne - score.PlayerTwo :
    score.PlayerTwo - score.PlayerOne;
}

/// <summary>
/// Gets the value contributed by the corner positions of the specified board.
/// </summary>
/// <param name="board">The game board to assess.</param>
/// <param name="player">The player for whom to assess the game board.</param>
/// <returns>The value of the corner positions from the perspective of the specified player;
/// that is, a positive value for the corners owned by the player, and a negative value for
/// the corners owned by the player's opponent.</returns>
int ReversiGameEngine::GetCornersValue(ReversiBoard& board, ReversiState  player)
{
    // Use a corner value a little higher than the maximum possible 
    // move value. This is just an arbitrary guess at the actual value.
    // This value means that a series of high-value moves not including 
    // the corner could produce a value higher than a corner move. 
    int shorter = min(m_rowCount, m_columnCount);
    int longer = max(m_rowCount, m_columnCount);
    int cornerValue = shorter * 2 + longer;

    // Top left, top right, bottom left, bottom right or end
    array<ReversiState, 4> cornerStates = {board[0], board[m_columnCount - 1],
        board[(m_rowCount - 1) * m_columnCount],  board[m_rowCount * m_columnCount - 1] };

    int sumCornersValue = 0;
    for (const auto& state : cornerStates)
    {
        if (state != ReversiState::None)
            sumCornersValue += (cornerValue * (state == player ? 1 : -1));
    }

    return sumCornersValue;
}

/// <summary>
/// Gets the best move that the AI can find for the specified board, player, and search depth.
/// </summary>
ReversiSpace ReversiGameEngine::GetBestMove(int searchDepth)
{
    return GetBestMove(m_board, searchDepth);
}

/// <summary>
/// Gets the best move that the AI can find for the specified board, player, and search depth.
/// </summary>
/// <param name="board">The board to search.</param>
/// <param name="player">The player whose turn it is for the specified board.</param>
/// <param name="searchDepth">The AI search depth to use.</param>
/// <returns>The space of the move.</returns>
ReversiSpace ReversiGameEngine::GetBestMove(ReversiBoard& board, ReversiState player, 
                                            int searchDepth)
{
    auto moveEvaluations = GetMoveEvaluations(
        board, player, searchDepth);

    // Use cbegin and cend if you don't modify the source elements.
    auto bestPair = max_element(cbegin(moveEvaluations), cend(moveEvaluations),
        [] (const pair<ReversiSpace, int>& elem1, const pair<ReversiSpace, int>& elem2)
    {
        return elem1.second < elem2.second;
    });

    int bestValue = bestPair->second;

    vector<ReversiSpace> bestMoves;
    for (const auto& e : moveEvaluations)
    {		
        if (e.second == bestValue)
        {
            bestMoves.push_back(e.first);
        }
    }

    // If there are multiple, equivalent best moves, 
    // select one at random. 
    size_t len = bestMoves.size();

    if (len == 1)
        return bestMoves[0];

    if (len > 1)
    { 
        // Create the distribution with a range
        // and call it, passing in the generator
        uniform_int_distribution<int> uid(0, len - 1);
        return bestMoves[uid(m_randomGenerator)];        
    }

    return ReversiSpace(-1, -1);
}

/// <summary>
/// Assesses all legal moves for the specified game board, assigning
/// each one an integer value. 
/// </summary>
/// <param name="board">The board to assess.</param>
/// <param name="player">The player whose turn it is for the specified board.</param>
/// <param name="searchDepth">The AI search depth to use.</param>
/// <param name="maxValue">The current maximum possible value of a move for the specified board.</param>
/// <returns>A list of moves that includes all the moves with the highest assessed value.</returns>
/// <remarks>
/// This method (along with GetMoveValue) implements the minimax algorithm with 
/// alpha-beta pruning, so the return value will contain the move or moves with the 
/// highest evaluation, but will not necessarily contain all of the lesser moves. 
/// </remarks>

vector<pair<ReversiSpace, int>> ReversiGameEngine::GetMoveEvaluations(
    ReversiBoard& board, ReversiState player, int searchDepth, int maxValue)
{
    // Must use negative INT_MAX instead of INT_MIN
    // because INT_MIN * -1 > INT_MAX.
    size_t validSize = 0; //TEMP
    int maxSoFar = -INT_MAX;

    // Holds the list of best moves
    vector<pair<ReversiSpace,int>> evaluations;

    // Evaluate and store all possible valid moves.
    for (const auto& move : GetValidMoveSpaces(board, player, validSize))
    {
        auto evaluation = GetEval(board, player, searchDepth, maxSoFar, move);

        // Prune the list so that it includes only the best moves
		// discovered so far. 

		// The current result is over the maximum value.
		// This means that the opponent has at least one
		// move that will result in a better outcome by avoiding 
		// this branch completely. Therefore, there is no 
		// point in searching it further, so stop searching
		// and yield the evaluation. (This is known as 
		// "alpha-beta pruning".)
		if (evaluation.second > maxValue)
        {
            evaluations.push_back(evaluation);
            break;
        }

		// The current result is worse than one the current player
        // has already found, so skip it.
        if (evaluation.second < maxSoFar) continue;

        maxSoFar = max(evaluation.second, maxSoFar);
        evaluations.push_back(evaluation);
    }

    if (validSize == 0)
    {
        // Pass is the only valid move.
        // We use ReversiSpace(-1, -1) to mean "pass".
        evaluations.emplace_back(GetEval(board, player, searchDepth, maxSoFar, ReversiSpace(-1,-1)));
    }

    return evaluations;
}

/// <summary>
/// Associates a space with its move value.
/// </summary>
/// <param name="board">The game board.</param>
/// <param name="move">The move to assess.</param>
/// <param name="player">The player whose turn it is for the specified board.</param>
/// <param name="searchDepth">The AI search depth to use.</param>
/// <param name="maxValue">The current maximum possible value of a move for the specified board.</param>
/// <returns>A ReversiSpace and the assessed value of a hypothetical move to that space.</returns>
/// <remarks>
/// This method simply associates the result of GetMoveValue with the Space that is
/// being evaluated. 
/// </remarks>
pair<ReversiSpace, int> ReversiGameEngine::GetEval(ReversiBoard& board, ReversiState player, int searchDepth,
                                                   int maxSoFar, ReversiSpace move)
{
    // This is the only context in which we copy the board, to evaluate
    // a potential move. However, we do this a lot.
    return make_pair<ReversiSpace, int>(ReversiSpace(move), GetMoveValue(board, move,
        player, searchDepth, -maxSoFar));
}

/// <summary>
/// Gets the value of a move for the specified game board.
/// </summary>
/// <param name="board">The game board.</param>
/// <param name="move">The move to assess.</param>
/// <param name="player">The player whose turn it is for the specified board.</param>
/// <param name="searchDepth">The AI search depth to use.</param>
/// <param name="maxValue">The current maximum possible value of a move for the specified board.</param>
/// <returns>The assessed value of the move.</returns>
/// <remarks>
/// This method (along with GetMoveEvaluations) implements the minimax algorithm with 
/// alpha-beta pruning. The two methods recursively call one another to search the game
/// tree to the specified depth.  
/// </remarks>
int ReversiGameEngine::GetMoveValue(ReversiBoard board, const ReversiSpace& move, ReversiState player,
                                    int searchDepth, int maxValue)
{
    // All the create_async calls create a new internal
    // cancellation_token on each invocation. We can check its value by using
    // this free function, and then cancelling if appropriate.
    if (is_task_cancellation_requested())
    {
        cancel_current_task();
    }

    // GetMoveValue is only called with a cloned board that was created in GetEval.
    // The changes that Move makes are to that copy, not to m_board.
    // And therefore, the return value changedSpaces is ignored because we
    // haven't made a move on the game board yet and changedSpaces is only
    // relevant in that context.
    Move(board, move, player);

    // Decrement the searchDepth then check whether the search is over.
    if (--searchDepth == 0 || IsGameOver(board)) 
        return GetBoardValue(board, player);

    // Return the opposite of the opponent's best move value.
    auto opponent = (player == ReversiState::One ? ReversiState::Two : ReversiState::One);

    // Recursively call back to GetMoveEvaluations passing back the board
    // it passed in here but with the new move entered on it.
    auto moveEvaluations = GetMoveEvaluations(
        board, opponent, searchDepth, maxValue);

    // Find the best available move and use it to evaluate the next depth level.
    int max = INT_MIN;
    for (const auto& moveEval : moveEvaluations)
    {
        if (moveEval.second > max)
        {
            max = moveEval.second;
        }
    };
   
    return -max;
}

/// <summary>
/// Gets the value of the specified game board.
/// </summary>
/// <param name="board">The game board to assess.</param>
/// <param name="player">The player for whom to assess the game board.</param>
/// <returns>The value of the game board from the perspective of the specified player.</returns>
/// <remarks>
/// This method encapsulates the Reversi-specific heuristics, and would be replaced 
/// for other games. The AI code higher in the call stack is generic and can be used 
/// as the basis of an AI implementation for any two-player, zero-sum game. 
/// </remarks>
int ReversiGameEngine::GetBoardValue(ReversiBoard& board, ReversiState player)
{
	auto scoreValue = GetScoreDifference(board, player);
	auto cornersValue = GetCornersValue(board, player);
	auto winValue = GetWinValue(board, player);
	return scoreValue + cornersValue + winValue;
}

/// <summary>
/// Gets the value of a winning game board.
/// </summary>
/// <param name="board">The game board to assess.</param>
/// <param name="player">The player for whom to assess the game board.</param>
/// <returns>The value of a win from the perspective of the specified player;
/// that is, a positive value if the specified player has won, and a 
/// negative value if the player's opponent has won. </returns>
int ReversiGameEngine::GetWinValue(ReversiBoard& board, ReversiState player)
{
    if (!IsGameOver(board)) return 0;
    return INT_MAX / 2 * (GetWinner(board) == player ? 1 : -1);	
}

/// <summary>
/// Gets a value that indicates whether a pass move is legal for the specified game board.
/// </summary>
/// <param name="board">The game board.</param>
/// <param name="player">The player whose turn it is for the specified board.</param>
/// <returns>True if a pass move is legal for the given board; otherwise, false.</returns>
bool ReversiGameEngine::IsPassValid(ReversiState player) const
{
    size_t notUsed;
    return !IsGameOver(m_board) &&
        GetValidMoveSpaces(m_board, player, notUsed).empty();
}

/// <summary>
/// Performs a move on the specified board. Public method that always refers to actual board.
/// </summary>
/// <returns>The ReversiSpaces that area affected by the move, starting with the space 
/// of the move and including the spaces of all the pieces captured by the move.
/// </returns>
vector<ReversiSpace> ReversiGameEngine::Move(const ReversiSpace& move, ReversiState player)
{
    return Move(m_board, move, player);
}

/// <summary>
/// Performs a move on the specified board.
/// </summary>
/// <param name="board">The board to perform the move on.</param>
/// <param name="move">The move.</param>
/// <param name="player">The player whose turn it is for the specified board.</param>
/// <returns>The ReversiSpaces that area affected by the move, starting with the space 
/// of the move and including the spaces of all the pieces captured by the move.
/// </returns>
/// <remarks>
/// This method is for actual moves and for analysis of potential moves, so it 
/// modifies the specified board, but does not add anything to the Moves list. when
/// evaluating potential moves, the caller ignores the return value.
/// </remarks>
vector<ReversiSpace> ReversiGameEngine::Move(ReversiBoard& board, const ReversiSpace& move, ReversiState player)
{
    // There is no need for the return value when evaluating potential moves
    // e.g. when this is called from GetMoveEvaluations & GetMoveValue, therefore
    // it is ignored in those cases.
    vector<ReversiSpace> changedSpaces;

    // If move is a pass (-1, -1), return the empty list. 
    if (move.Column == -1)
    {
        return changedSpaces;
    }

    auto currentOpponentState = player == ReversiState::One ? ReversiState::Two : ReversiState::One;

    // Place a piece at the move space.
    board[move.Row * m_columnCount + move.Column] = player;
    changedSpaces.emplace_back(move.Row, move.Column);

    // Flip any captured opponent pieces. 
    for (unsigned int i = 0; i < m_Directions.size(); ++i)
    {
        auto direction = m_Directions[i];
        if (!DoesMoveHaveCaptureInDirection(board, player, move, direction))
            continue;
        int nextRow = move.Row;
        int nextCol = move.Column;		
        while (IsSpaceOnBoard(board, ReversiSpace(
            nextRow += direction.Row, nextCol += direction.Column)) &&
            (board[nextRow * m_columnCount + nextCol] == currentOpponentState))
        {
            board[nextRow * m_columnCount + nextCol] = player;
            changedSpaces.emplace_back(nextRow, nextCol);
        }
    }

    return changedSpaces;
}

/// <summary>
/// Returns the game to the ReversiState it was in after the last move. 
/// </summary>
/// <returns>A list of the vector<ReversiSpace> affected by the move, starting with the space 
/// of the move and including the vector<ReversiSpace> of all the pieces captured by the move.</returns>
vector<ReversiSpace> ReversiGameEngine::Undo()
{    
    return Reset(static_cast<int>(m_Moves.size()) - 1);
}

/// <summary>
/// Returns the game to the ReversiState it was in after the next move. 
/// </summary>
/// <returns>A list of the vector<ReversiSpace> affected by the move, starting with the space 
/// of the move and including the vector<ReversiSpace> of all the pieces captured by the move.</returns>
vector<ReversiSpace> ReversiGameEngine::Redo()
{
    return Reset(m_Moves.size() + 1);
}

/// <summary>
/// Populates the board and sets up the initial board state.
/// </summary>
void ReversiGameEngine::InitializeBoard()
{    
    m_board.clear();
    m_board.resize(m_rowCount * m_columnCount);

    // Set up the default initial board ReversiState all empty, then
    // the middle four squares populated with game pieces.
    for (auto& element : m_board)
    {
        element = (ReversiState::None);
    }

    m_board[(m_rowCount / 2 - 1) * m_columnCount + m_columnCount / 2 - 1] = ReversiState::One;    
    m_board[(m_rowCount / 2) * m_columnCount + (m_columnCount/ 2)] = ReversiState::One;    
    m_board[(m_rowCount / 2 - 1) *  m_columnCount + (m_columnCount / 2)] = ReversiState::Two;    
    m_board[(m_rowCount / 2) * m_columnCount + (m_columnCount / 2 - 1)] = ReversiState::Two;
}

template <typename T>
void ReversiGameEngine::DbgPrint(const wstring& msg, const wstring& moveValue, T val) const
{
    wostringstream strm;
    strm << msg << L" " << moveValue << L" " << val <<  L"\r\n";
    OutputDebugString(strm.str().c_str());
}























