#pragma once
#include "pch.h"
#include "ReversiGameEngine.h"

namespace ReversiGameComponentCPP
{
    namespace WF = Windows::Foundation;
    namespace WFC = Windows::Foundation::Collections;

    public ref class ReversiGameWrapper sealed : ReversiGameModel::IGame
    {

    private:

        ReversiCPP::ReversiGameEngine m_Game;
        std::mutex m_moveMutex;
       
    public:
        ReversiGameWrapper();
        ReversiGameWrapper(int);
        ReversiGameWrapper(int row, int col);


        // IGame interface:

        /// <summary>
        /// Gets or sets the game board.
        /// </summary>
        property WFC::IVector<WFC::IVector<ReversiGameModel::State>^>^ Board
        {
            virtual WFC::IVector<WFC::IVector<ReversiGameModel::State>^>^ get();
            virtual void set(WFC::IVector<WFC::IVector<ReversiGameModel::State>^>^);
        }

        /// <summary>
        /// Gets or sets the number of rows in the game.
        /// </summary>
        /// <remarks>
        /// This value doesn't change for the lifetime of the Game object
        /// except immediately after default construction by the framework deserialization.
        /// </remarks>
        virtual property int RowCount;

        /// <summary>
        /// Gets or sets the number of columns in the game.
        /// </summary>
        /// <remarks>
        /// This value doesn't change for the lifetime of the Game object
        /// except immediately after default construction by the framework deserialization.
        /// </remarks>
        virtual property int ColumnCount;
       
        /// <summary>
        /// Gets or sets a list of the moves that have been made in the game.
        /// </summary>
        /// <remarks>
        /// The Moves list contains only moves including and prior to the current move.
        /// Moves that have been undone are included in the MoveStack list.
        /// </remarks>
        virtual property WFC::IVector<ReversiGameModel::ISpace^>^ Moves
        {
            virtual WFC::IVector<ReversiGameModel::ISpace^>^ get();
            virtual void set(WFC::IVector<ReversiGameModel::ISpace^>^);
        }

        /// <summary>
        /// Gets or sets a copy of the Moves list that includes moves that have been undone. 
        /// </summary>
        /// <remarks>
        /// The MoveStack enables forward and backward navigation through the Moves list.
        /// </remarks>
        virtual property WFC::IVector<ReversiGameModel::ISpace^>^ MoveStack
        {
            WFC::IVector<ReversiGameModel::ISpace^>^ get();
            void set (WFC::IVector<ReversiGameModel::ISpace^>^);
        }

        /// <summary>
        /// Gets a value that indicates the current player.
        /// </summary>
        property ReversiGameModel::State CurrentPlayer
        {
            virtual ReversiGameModel::State get();
        }

        /// <summary>
        /// Gets a value that indicates the opponent of the current player. Note: This is used
        /// only for test runs.
        /// </summary>
        property ReversiGameModel::State CurrentOpponent
        {
            virtual ReversiGameModel::State get();
        }

        /// <summary>
        /// Gets a value that indicates the winner of the game.
        /// </summary>
        property ReversiGameModel::State Winner
        {
            virtual ReversiGameModel::State get();
        }

        virtual ReversiGameModel::IScore^ GetScore();
        virtual ReversiGameModel::State GetSpaceState(int row, int column);
        virtual bool IsValidMove(ReversiGameModel::ISpace^ move);
        virtual bool IsValidMove(int row, int column);        
        virtual bool IsPassValid();
        virtual bool IsGameOver();

        [WF::Metadata::DefaultOverloadAttribute]
        virtual WF::IAsyncOperation<WFC::IVector<ReversiGameModel::ISpace^>^>^ MoveAsync(int row, int column);
        virtual WF::IAsyncOperation<WFC::IVector<ReversiGameModel::ISpace^>^>^ MoveAsync(ReversiGameModel::ISpace^ move);
        virtual WF::IAsyncAction^ MoveAsync(Platform::String^ moves);
        virtual WF::IAsyncAction^ AiMoveAsync(int searchDepth);
        virtual WF::IAsyncOperation<ReversiGameModel::ISpace^>^ GetBestMoveAsync(int searchDepth);

        virtual WF::IAsyncOperation<WFC::IVector<ReversiGameModel::ISpace^>^>^ PassAsync();
        virtual Windows::Foundation::IAsyncOperation<WFC::IVector<ReversiGameModel::ISpace^>^>^ UndoAsync();
        virtual Windows::Foundation::IAsyncOperation<WFC::IVector<ReversiGameModel::ISpace^>^>^ RedoAsync();

        virtual Platform::String^ ToString() = IGame::ToString;	
        virtual void LoadSerializedBoardState(Platform::String^ state);
    };
}