// <copyright file="MockCaretaker.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockCaretaker.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Mocks
{
    using Timeline.Commands;

    public class MockCaretaker : ICaretaker
    {
        public bool UndoCalled { get; set; }

        public bool RedoCalled { get; set; }

        public bool ExecuteCommandCalled { get; set; }

        public UndoableCommand ExecuteCommandArgument { get; set; }

        public int ExecuteCommandNumberOfCalls { get; set; }

        public bool SetUndoLevelCalled { get; set; }

        public int SetUndoLevelArgument { get; set; }

        public void SetUndoLevel(int undoLevel)
        {
            this.SetUndoLevelCalled = true;
            this.SetUndoLevelArgument = undoLevel;
        }

        public void ExecuteCommand(UndoableCommand command)
        {
            command.Execute();
            this.ExecuteCommandCalled = true;
            this.ExecuteCommandArgument = command;
            this.ExecuteCommandNumberOfCalls++;
        }

        public void Undo()
        {
            this.UndoCalled = true;
        }

        public void Redo()
        {
            this.RedoCalled = true;
        }
    }
}
