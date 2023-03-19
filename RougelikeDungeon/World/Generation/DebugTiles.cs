using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation
{
    internal class DebugTiles
    {
        HashSet<DebugTile> tiles = new HashSet<DebugTile>();

        //Pallete
        Color DoorCol = Color.PaleVioletRed;


        public DebugTiles() { }

        public void AddDoorTile(Vector2 pos)
        {
            var tile = new DebugTile(pos, DoorCol);
            tiles.Add(tile);
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch, int tileSize)
        {
            foreach (var tile in tiles)
            {
                tile.Draw(spriteBatch, tileSize);
            }


        }
    }
}
