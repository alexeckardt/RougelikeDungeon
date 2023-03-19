using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation
{
    internal class DebugTile
    {
        Vector2 pos;
        Color col;
        public DebugTile(Vector2 pos, Color col)
        {
            this.pos = pos;
            this.col = col;
        }

        public void Draw(SpriteBatch spriteBatch, int tileSize)
        {
            //spriteBatch.Draw(GameConstants.Instance.Pixel, pos * tileSize, null, col, 0f, Vector2.Zero, tileSize, SpriteEffects.None, .996f);
        }
    }
}
