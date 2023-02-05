using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World
{
    internal class TileMap
    {
        Texture2D TileTexture;
        int TileSize;

        public TileMap(int tileSize) {
            TileSize = tileSize;
        }

        public void DrawTile(Vector2 tilePosition, int tileId) { }

        private Vector2 GetSpriteSheetPosition(int tileId) => Vector2.Zero;
    }
}
