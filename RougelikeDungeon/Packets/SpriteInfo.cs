using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Packets
{
    internal class SpriteInfo
    {
        public Texture2D Sprite;
        public Vector2 Position;
        public Color Color;

        public SpriteInfo(Texture2D sprite, Vector2 position)
        {
            this.Sprite = sprite;
            this.Position = position;
            this.Color = Color.White;
        }

        public SpriteInfo(Texture2D sprite, Vector2 position, Color color)
        {
            this.Sprite = sprite;
            this.Position = position;
            this.Color = color;
        }
    }
}
