using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Tiles
{
    internal class TileSet
    {
        public Texture2D Texture { get => texture; }
        public int SetWidth { get => texture.Width / tileWidth; }
        public int TileWidth { get => tileWidth; }

        Texture2D texture;
        int tileWidth;

        public TileSet(Texture2D _texture, int _tileWidth)
        {
            texture = _texture;
            tileWidth = _tileWidth;
        }
    }
}
