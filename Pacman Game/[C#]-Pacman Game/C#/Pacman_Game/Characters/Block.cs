using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packman_Game.Characters
{
    public class Block:System.Windows.Forms.Control,IBlock
    {
        public Block()
        {
            this.BackColor = Block_Color;
        }

        public Block(int width, int height)
            : this()
        {
            this.Width = width;
            this.Height = height;
        }

        public Block(int width, int height, System.Drawing.Point location)
            : this(width,height)
        {
            this.Location = location;
        }

        System.Drawing.Color m_Block_Color = System.Drawing.Color.Brown;
        public System.Drawing.Color Block_Color
        {
            get
            {
                return m_Block_Color;
            }
            set
            {
                m_Block_Color = value;
            }
        }
    }
}
