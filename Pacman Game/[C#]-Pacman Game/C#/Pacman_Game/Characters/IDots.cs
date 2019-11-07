using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packman_Game.Characters
{
    public interface IDots:IDisposable
    {
        int Points { get; }
        System.Drawing.Color Dot_Color { get; set; }
    }
}
