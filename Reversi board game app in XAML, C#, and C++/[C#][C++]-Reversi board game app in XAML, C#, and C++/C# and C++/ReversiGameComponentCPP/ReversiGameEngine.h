#pragma once
#include "pch.h"
#include <initializer_list>
namespace ReversiCPP
{
    /// <summary>
    /// Represents whether a board position is occupied by Player One, Player Two, or no player.
    /// </summary>
    enum class ReversiState
    {
        None,
        One,
        Two
    };

    /// <summary>
    /// Represents the current score of the game.
    /// </summary>
    struct ReversiScore 
    {
        ReversiScore() :  PlayerOne(0), PlayerTwo(0)
        {}

        ReversiScore(int playerOne, int playerTwo) : PlayerOne(playerOne), PlayerTwo(playerTwo){}

        /// <summary>
        /// Gets or sets the score for player one.
        /// </summary>
        int PlayerOne;

        /// <summary>
        /// Gets or sets the score for player two.
        /// </summary>
        int PlayerTwo;
    };

    /// <summary>
    /// Represents a space on the board.
    /// </summary>
    struct ReversiSpace
    {
        ReversiSpace() : Row(0), Column(0)
        {}

        int Row;
        int Column;

        bool operator==(const ReversiSpace& other)
        {
            return Row == other.Row &&
                Column == other.Column;
        }

        /// <summary>
        /// Initializes a new Space.
        /// </summary>
        /// <param name="row">The row of the space.</param>
        /// <param name="column">The column of the space.</param>
        ReversiSpace(int row, int column) : Row(row), Column(column)
        {
        }

        /// <summary>
        /// Gets a comma-separated string representation of the space. 
        /// </summary>
        /// <returns>A string representation of the space.</returns>
        std::wstring ToString() const 
        {
            std::wostringstream wos;
            wos << '(' << Row << ',' << Column << ')';
            return wos.str();            
        }
    };

    // We use this typedef to indicate that we are operating
    // on an entire board, either m_board or a hypothetical board.
    typedef std::vector<ReversiState> ReversiBoard;

    class ReversiGameEngine
    {
    public:
        ReversiGameEngine(int row, int col);

        const std::vector<ReversiSpace>& GetMoves() { return m_Moves; }
        const std::vector<ReversiSpace>& GetMoveStack() { return m_MoveStack; }
        void ResetBoard(int rows, int cols);
        void ResetMoves(std::vector<ReversiSpace> v);
        void ResetMoveStack(std::vector<ReversiSpace> v);
        void ResetMoveStackToMoveList();
        void PushMove(ReversiSpace sp);
        void PushToMoveStack(ReversiSpace sp);

        ReversiScore GetScore() const;
        ReversiState GetAt(size_t row, size_t col);
        void SetAt(ReversiState state, size_t row, size_t col);
        ReversiState GetWinner() { return GetWinner(m_board); }

        std::vector<ReversiSpace> Reset(size_t turnCount);
        std::vector<ReversiSpace> Pass();
        void Move(const std::wstring& moves);
        std::vector<ReversiSpace> Move(const ReversiSpace& move);
        std::vector<ReversiSpace> Move(const ReversiSpace& move, ReversiState player);
       
        bool IsGameOver() const;            
        bool IsValidMove(const ReversiSpace& space) const;
        bool IsPassValid(ReversiState player) const;
        bool IsValidMoveSpace(const ReversiBoard& board,  ReversiState player, const ReversiSpace& move) const;
        ReversiSpace GetBestMove(ReversiState player, int searchDepth);
        ReversiSpace GetBestMove(int searchDepth);
        std::wstring DebugPrintBoard(const std::wstring& msg) const;
        std::wstring ToString();

    private:

        ReversiBoard m_board;  
        int m_columnCount;
        int m_rowCount;
        std::vector<ReversiSpace> m_Moves;
        std::vector<ReversiSpace> m_MoveStack;
        std::map<std::pair<int, int>, std::vector<ReversiSpace>> m_allSpaces;

        
        // Values for moving in any given direction on the board. These is used 
        // to look around in every direction (up, down, right, left, diags) from 
        // a piece's current position and see whether there is a capture.
        std::vector<ReversiSpace> m_Directions = std::vector<ReversiSpace> { { 
            {-1, -1 }, {-1, 0 }, {-1, 1 },
            { 0, -1 },           { 0, 1 },
            { 1, -1 }, { 1, 0 }, { 1, 1 } } };

        // Random number generator based on mersenne_twister
        std::mt19937 m_randomGenerator; 
        std::random_device rd;

        void InitializeBoard();

        bool IsGameOver(const ReversiBoard& board) const;  
        bool IsSpaceOnBoard(const ReversiBoard& board,  const ReversiSpace& space) const;	
        bool IsSpaceUnoccupied(const ReversiBoard& board,  const ReversiSpace& space) const;
        bool IsBoardFilled(const ReversiBoard& board) const;
        bool DoesMoveHaveCapture(const ReversiBoard& board,  ReversiState player, const ReversiSpace& move) const;
        bool DoesMoveHaveCaptureInDirection(const ReversiBoard& board,  ReversiState player, const ReversiSpace& move,  const ReversiSpace& direction) const;

        std::vector<std::pair<ReversiSpace, int>> GetMoveEvaluations(
            ReversiBoard& board, ReversiState player, int searchDepth, int maxValue = INT_MAX);
        std::pair<ReversiSpace, int> GetEval(ReversiBoard& board, ReversiState player, int searchDepth, int maxValue, ReversiSpace move);

        ReversiState GetCurrentPlayer() const;
        ReversiScore GetScore(const ReversiBoard& board) const;
        ReversiState GetWinner(const ReversiBoard& board) const;
        int GetCornersValue(ReversiBoard& board,  ReversiState player);
        ReversiSpace  GetBestMove(ReversiBoard& board,  ReversiState player, int searchDepth);
        ReversiSpace  GetBestMove(ReversiBoard& board, int searchDepth);

        int GetMoveValue(ReversiBoard board,  const ReversiSpace& move, ReversiState player, int searchDepth, int maxValue = INT_MAX);
        int GetBoardValue(ReversiBoard& board,  ReversiState player);
        int GetWinValue(ReversiBoard& board,  ReversiState player);
        std::vector<ReversiSpace> GetAllSpaces(ReversiBoard& board);
        std::vector<ReversiSpace> GetValidMoveSpaces(const ReversiBoard& board, ReversiState player, size_t& size) const;
        int GetScoreDifference(const ReversiBoard& board, ReversiState player) const;

        std::vector<ReversiSpace> Move(const std::vector<ReversiSpace>& moves);
        std::vector<ReversiSpace> Move(int row, int col);
        std::vector<ReversiSpace> Move(ReversiBoard& board, const ReversiSpace& move, ReversiState player);
        std::vector<ReversiSpace> Undo();
        std::vector<ReversiSpace> Redo();        

        template  <typename T>
        void DbgPrint(const std::wstring& msg, const std::wstring& moveValue, T val) const;
    };
}