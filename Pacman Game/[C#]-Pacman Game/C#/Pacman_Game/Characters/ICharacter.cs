using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packman_Game.Characters
{
    public enum CharacterType
    {
        Packman,
        Enemy
    }

    public enum MovementWay
    {
        Up,
        Down,
        Left,
        Right
    }

    public interface ICharacter : IDisposable
    {
        int TotalPoints { get; set; }
        int Speed { get; set; }
        CharacterType Type { get; }
        void Move(MovementWay way);
    }
}
