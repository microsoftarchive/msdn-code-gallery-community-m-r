using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packman_Game.Characters
{
    public interface IBlock:IDisposable
    {
        System.Drawing.Color Block_Color { get; set; }
    }
}
