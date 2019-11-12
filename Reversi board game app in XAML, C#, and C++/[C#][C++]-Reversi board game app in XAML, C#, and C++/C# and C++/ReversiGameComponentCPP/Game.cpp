// Game.cpp
#include "pch.h"
#include "game.h"


using namespace ReversiGameModel;
using namespace ReversiGameComponentCPP;
using namespace Platform;
using namespace Platform::Collections;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace concurrency;
using namespace std;
using namespace ReversiCPP;

/// <summary>
/// Default constructor.
/// </summary>
ReversiGameWrapper::ReversiGameWrapper() : m_Game(ReversiGameEngine(8, 8))
{
    // Trivial properties (those without explicit backing fields) 
    // cannot be initialized in an initializer list.
    RowCount = 8;
    ColumnCount = 8;
}

/// <summary>
/// Construct a square game board of the specified size.
/// </summary>
ReversiGameWrapper::ReversiGameWrapper(int size) : m_Game(ReversiGameEngine(size, size))
{
    if (size < 2 || size > 9)
        throw ref new InvalidArgumentException("size must be >= 2 or <= 9.");
    RowCount = size;
    ColumnCount = size;
}

/// <summary>
/// Initializes a new instance of the ReversiGameWrapper class using the specified
/// board size.
/// </summary>
/// <param name="rowCount">The number of rows on the board.</param>
/// <param name="columnCount">The number of columns on the board.</param>
ReversiGameWrapper::ReversiGameWrapper(int rowCount, int columnCount) : m_Game(ReversiGameEngine(rowCount, columnCount))
{
    if (rowCount < 2) throw ref new
        InvalidArgumentException("row count must be 2 or greater.");
    if (columnCount < 2) throw ref new
        InvalidArgumentException("column count must be 2 or greater.");

    RowCount = rowCount;
    ColumnCount = columnCount;
}

/// <summary>
/// Gets the board as a two-dimensional Vector in a very inefficient manner. 
/// Use only when you can't avoid it.
/// </summary>
IVector<IVector<State>^>^ ReversiGameWrapper::Board::get()
{
    Vector<IVector<State>^>^ boardAsVector = ref new Vector<IVector<State>^>();
    for (int row = 0; row < RowCount; row++)
    {
        auto v = ref new Vector<State>();
        for (int col = 0; col < ColumnCount; col++)
        {
            v->Append(static_cast<State>(m_Game.GetAt(row, col)));
        }
        boardAsVector->Append(v);
    }
    return boardAsVector;
}

/// <summary>
/// Sets the board in a very inefficient manner. Use only when you can't avoid it. 
/// In this implementation, the  CLRSerializableGameCPP class sets this property 
//  when the app is resuming after termination. 
/// </summary>
void ReversiGameWrapper::Board::set(IVector<IVector<State>^>^ board)
{
    // Make sure that we don't try to set a board whose size is different
    // from the current board size. 
    if (RowCount != board->Size)
    {
        RowCount = static_cast<int>(board->Size);
        ColumnCount = static_cast<int>(board->GetAt(0)->Size);
        m_Game.ResetBoard(RowCount, ColumnCount);
    }

    for (int row = 0; row < RowCount; row++)
    {
        auto inputRow = board->GetAt(row);
        for (int col = 0; col < ColumnCount; col++)
        {
            m_Game.SetAt(static_cast<ReversiState>(inputRow->GetAt(col)), row, col);
        }
    }
}

/// <summary>
/// Gets the list of moves that have been made by both players up til now.
/// </summary>
IVector<ISpace^>^ ReversiGameWrapper::Moves::get()
{
    auto vec = ref new Vector<ISpace^>();

    for (const auto& sp : m_Game.GetMoves())
    {
        if (sp.Column != -1)
        {
            vec->Append(ref new Space(sp.Row, sp.Column));
        }
        else
        {
            vec->Append(static_cast<ISpace^>(nullptr));
        }
    }

    return vec;
}

/// <summary>
/// Sets the list of moves that have been made by both players up til now.
/// This setter is needed for the IGame interface but never used in C++
/// because the inner class does the resetting.
/// </summary>
void ReversiGameWrapper::Moves::set(IVector<ISpace^>^ value)
{
    // Prevent other threads from modifying move stack
    // while we are modifying it.
    lock_guard<mutex> lg(m_moveMutex);
    vector<ReversiSpace> v;
    for (const auto& sp : value)
    {
        v.emplace_back(sp->Row, sp->Column);
    }

    m_Game.ResetMoves(v);
}

/// <summary>
/// Gets the list of moves and passes and undos that have been made by both players up til now.
/// </summary>
IVector<ReversiGameModel::ISpace^>^ ReversiGameWrapper::MoveStack::get()
{
    auto vec = ref new Vector<ReversiGameModel::ISpace^>();

    for (const auto& sp : m_Game.GetMoveStack())
    {
        vec->Append(ref new ReversiGameModel::Space(sp.Row, sp.Column));
    }

    return vec;
}

/// <summary>
/// Sets the list of moves and passes and undos that have been made by both players up til now.
/// </summary>
void ReversiGameWrapper::MoveStack::set(IVector<ReversiGameModel::ISpace^>^ value)
{
    // Prevent other threads from modifying move stack
    // while we are modifying it.
    lock_guard<mutex> lg(m_moveMutex);
    vector<ReversiSpace> v;
    for (const auto& sp : value)
    {
        v.emplace_back(sp->Row, sp->Column);
    }

    m_Game.ResetMoveStack(v);
}

/// <summary>
/// Gets the player whose turn it is now.
/// </summary>
State ReversiGameWrapper::CurrentPlayer::get()
{
    return (m_Game.GetMoves().size() % 2 == 0 ? ReversiGameModel::State::One : ReversiGameModel::State::Two);
}


/// <summary>
/// Gets the opponent of the player whose turn it is now.
/// </summary>
State ReversiGameWrapper::CurrentOpponent::get()
{
    return (m_Game.GetMoves().size() % 2 == 0 ? ReversiGameModel::State::Two : ReversiGameModel::State::One);
}

/// <summary>
/// Gets the winner of the game.
/// </summary>
State ReversiGameWrapper::Winner::get()
{
    // Cast the standard C++ enum to a Windows Runtime enum.
    return static_cast<ReversiGameModel::State>(m_Game.GetWinner());
}

/// <summary>
/// Gets the score of the game.
/// </summary>
IScore^ ReversiGameWrapper::GetScore()
{
    ReversiScore rs = m_Game.GetScore();
    return ref new Score(rs.PlayerOne, rs.PlayerTwo);
}

/// <summary>
/// Gets the owner of the specied space, if any.
/// </summary>
State ReversiGameWrapper::GetSpaceState(int row, int column)
{
    return static_cast<State>(m_Game.GetAt(row, column)); //CHANGED 07-03
}


/// <summary>
/// Gets a value that indicates whether a move to the specified board position is legal.
/// </summary>
/// <param name="row">The row of the move space.</param>
/// <param name="column">The column of the move space.</param>
/// <returns>True if the move is legal; otherwise, false.</returns>
bool ReversiGameWrapper::IsValidMove(int row, int column)
{
    return m_Game.IsValidMove(ReversiSpace(row, column));
}

/// <summary>
/// Gets a value that indicates whether a move is legal.
/// </summary>
/// <param name="move">The move-></param>
/// <returns>True if the move is legal; otherwise, false.</returns>
bool ReversiGameWrapper::IsValidMove(ISpace^ move)
{
    if (move == nullptr)
    {
        return m_Game.IsValidMove(ReversiSpace(-1, -1));
    }

    return m_Game.IsValidMove(ReversiSpace(move->Row, move->Column));
}

/// <summary>
/// Gets a value that indicates whether a pass move is legal in the current game.
/// </summary>
/// <returns>True if a pass move is legal; otherwise, false.</returns>
bool ReversiGameWrapper::IsPassValid()
{
    return m_Game.IsPassValid(static_cast<ReversiState>(CurrentPlayer));
}

///<summary>
/// Public method called from ViewModel.
///</summary>
bool ReversiGameWrapper::IsGameOver()
{
    return m_Game.IsGameOver();
}

/// <summary>
/// Moves to the specified location. 
/// </summary>
IAsyncOperation<IVector<ISpace^>^>^ ReversiGameWrapper::MoveAsync(ISpace^ move)
{
    return create_async([this, move]() -> IVector<ISpace^>^
    {
        // Use a mutex to prevent the Reset method from modifying the game 
        // state at the same time a different thread is in this method.
        lock_guard<mutex> lg(m_moveMutex);
        auto vec = ref new Vector<ISpace^>();
        ReversiSpace space(move->Row, move->Column);
        auto changedSpaces = m_Game.Move(space, static_cast<ReversiState>(CurrentPlayer));
        m_Game.PushMove(space);

        // If the move diverges from the move stack, synchronize
        // the Moves and MoveStack lists.  
        if (m_Game.GetMoves().size() > m_Game.GetMoveStack().size())
        {
            m_Game.PushToMoveStack(space);
        }
        else if (!(space == (m_Game.GetMoveStack().at(m_Game.GetMoves().size() - 1))))
        {
            m_Game.ResetMoveStackToMoveList();
        }

        for (const auto& space : changedSpaces)
        {
            vec->Append(ref new Space(space.Row, space.Column));
        }

        return vec;
    });
}

/// <summary>
/// Performs a move on the specified board.
/// </summary>
IAsyncOperation<IVector<ISpace^>^>^ ReversiGameWrapper::MoveAsync(int row, int column)
{
    return MoveAsync(ref new Space(row, column));
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
IAsyncAction^ ReversiGameWrapper::MoveAsync(String^ moves)
{
    return create_async([this, moves]()
    {
        m_Game.Move(moves->Data());
    });
}

/// <summary>
/// Performs an AI move using the specified search depth.
/// </summary>
/// <param name="searchDepth">The AI search depth to use.</param>
Windows::Foundation::IAsyncAction^ ReversiGameWrapper::AiMoveAsync(int searchDepth)
{
    // create_async returns a IAsyncAction interface with an internal 
    // cancellation_token. If the client calls the Cancel() method on  
    // the interface, the token that is created here will reflect that request.
    // We check for cancellation by calling the free function 
    // is_task_cancellation_requested() inside GetMoveValue.
    return create_async([this, searchDepth]()
    {
        m_Game.Move(m_Game.GetBestMove(searchDepth));
    });
}

/// <summary>
/// Gets the best move that the AI can find for the specified search depth.
/// </summary>
/// <param name="searchDepth">The AI search depth to use.</param>
/// <returns>An operation that returns the space of the move.</returns>
IAsyncOperation<ISpace^>^ ReversiGameWrapper::GetBestMoveAsync(int searchDepth)
{
    return create_async([this, searchDepth]() -> ISpace^
    {
        if (searchDepth < 1) throw ref new InvalidArgumentException(
            "must be 1 or greater.");

        // If it is the AI's turn and we're not at the end of the move stack,
        // just use the next move in the stack. This is necessary to preserve
        // the forward stack, but it also prevents the AI from having to search again. 
        if (m_Game.GetMoves().size() < m_Game.GetMoveStack().size())
        {
            int nextMove = m_Game.GetMoves().size();
            auto sp = m_Game.GetMoveStack()[nextMove];
            return ref new Space(sp.Row, sp.Column);
        }

        ReversiSpace s = m_Game.GetBestMove(static_cast<ReversiState>(CurrentPlayer), searchDepth);
        return ref new Space(s.Row, s.Column);
    });
};

/// <summary>
/// Passes the current move.
/// </summary>
/// <returns>An empty list, indicating that there are no spaces affected by the move.</returns>
IAsyncOperation<IVector<ISpace^>^>^ ReversiGameWrapper::PassAsync()
{
    return create_async([this]() -> IVector<ISpace^>^
    {
        // ReversiSpace is not a pointer, so
        // we can't use nullptr to signify "Pass".
        m_Game.Move(ReversiSpace(-1, -1));

        // A pass always returns an empty list, so 
        // just create a Vector<ISpace^> and return it.
        return ref new Vector<ISpace^>();
    });
}

/// <summary>
/// Returns the game to the state it was in after the last move. 
/// </summary>
/// <returns>A list of the spaces affected by the move, starting with the space 
/// of the move and including the spaces of all the pieces captured by the move.</returns>
IAsyncOperation<IVector<ISpace^>^>^ ReversiGameWrapper::UndoAsync()
{
    return create_async([this]() -> IVector<ISpace^>^
    {
        // Prevent other threads from modifying move stack
        // while we are modifying it.
        lock_guard<mutex> lg(m_moveMutex);
        int i = m_Game.GetMoves().size() - 1;
        auto spaces = m_Game.Reset(i);
        auto vec = ref new Vector<ISpace^>();

        for (const auto& space : spaces)
        {
            if (space.Column != -1)
            {
                vec->Append(ref new Space(space.Row, space.Column));
            }
            else
            {
                vec->Append(static_cast<ISpace^>(nullptr));
            }
        }
        return vec;
    });
}

/// <summary>
/// Returns the game to the state it was in after the next move. 
/// </summary>
/// <returns>A list of the spaces affected by the move, starting with the space 
/// of the move and including the spaces of all the pieces captured by the move.</returns>
IAsyncOperation<IVector<ISpace^>^>^ ReversiGameWrapper::RedoAsync()
{
    return create_async([this]() -> IVector<ISpace^>^
    {
        // Prevent other threads from modifying move stack
        // while we are modifying it.
        lock_guard<mutex> lg(m_moveMutex);
        auto spaces = m_Game.Reset(m_Game.GetMoves().size() + 1);

        // Convert a vector<ReversiSpace> to a Vector<ISpace>.
        auto vec = ref new Vector<ISpace^>();
        for (const auto& s : spaces)
        {
            vec->Append(ref new Space(s.Row, s.Column));
        }
        return vec;
    });
}

/// <summary>
/// Returns a representation of the current board state as a string.
/// </summary>
String^ ReversiGameWrapper::ToString()
{
    return ref new String(m_Game.ToString().c_str());
}

/// <summary>
/// Initializes the Board property using the specified state string.
/// </summary>
/// <param name="state">A string that represents a board state.</param>
/// <remarks>
/// This method is used by unit tests to confirm various behaviors for specific board states.
/// The state string includes one number for each space on the board, 
/// with 0 representing an empty space, and 1 or 2 representing player 1 or 2.
/// </remarks>
void ReversiGameWrapper::LoadSerializedBoardState(String^ state)
{
    if (static_cast<int>(state->Length()) != RowCount * ColumnCount)
        throw ref new InvalidArgumentException(L"The string is not the correct length for the board");

    int row = 0, col = 0;
    for (const auto& ch : state)
    {
        if (ch == L'0')
            m_Game.SetAt(ReversiState::None, row, col);
        else if (ch == L'1')
            m_Game.SetAt(ReversiState::One, row, col);
        else if (ch == L'2')
            m_Game.SetAt(ReversiState::Two, row, col);
        else
            throw ref new InvalidArgumentException(L"The space value is not 0,1, or 2");

        // Are we at the end of the row?
        if (col < ColumnCount - 1)
        {
            ++col;
        }
        else
        {
            col = 0;
            ++row;
        }
    }
#if defined verbose
    m_Game.DebugPrintBoard(L"Board after LoadSerializedBoardState:");
#endif
}




