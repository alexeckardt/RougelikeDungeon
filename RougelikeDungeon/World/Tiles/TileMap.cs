using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Tiles
{
    internal class TileMap
    {
        int TileMapWidth;
        int TileSize;
        Dictionary<int, TileData> Tiles;
        float Depth;

        public bool IsEmpty { get => Tiles.Count == 0; }

        public Dictionary<int, TileData> TileDict { get => Tiles; }

        // ------------------------------------------------------------------------------

        public TileMap(int tileMapWidth, int tileSize)
        {

            TileMapWidth = tileMapWidth;
            TileSize = tileSize;
            Tiles = new();
        }

        private int positionToId(int i, int j)
        {
            if (i < 0 || j < 0) return -1;
            if (i >= TileMapWidth || j >= TileMapWidth) return -1;

            return i + j * TileMapWidth;
        }

        private Vector2 idToMapPosition(int id)
        {
            return new Vector2(id % TileMapWidth, id / TileMapWidth);
        }

        public TileData GetTile(int i, int j)
        {
            int pos = positionToId(i, j);
            return Tiles[pos];
        }

        public void SetTile(int i, int j, TileData tile)
        {
            int pos = positionToId(i, j);
            Tiles[pos] = tile;
        }

        public void Draw(SpriteBatch spriteBatch, TileSet TileSet, float tileDepth, Vector2 Origin)
        {
            foreach (var tileIds in TileDict)
            {
                //Get Data
                TileData tile = tileIds.Value;

                //Get TileId
                int tileId = tile.Id;

                //Calulcate Tile Row and Column in Set
                int column = tileId % TileSet.SetWidth;
                int row = tileId / TileSet.SetWidth;

                //Get Sprite Cull Position
                Rectangle tilesetRect = new Rectangle(TileSize * column, TileSize * row, TileSize, TileSize);

                //Position From Tile Id in Map
                var tilePosition = Origin + idToMapPosition(tileIds.Key) * TileSize;

                //Draw
                spriteBatch.Draw(TileSet.Texture, new Rectangle((int)tilePosition.X, (int)tilePosition.Y, TileSize, TileSize), tilesetRect, tile.blend, 0.0f, Vector2.Zero, SpriteEffects.None, tileDepth);
            }
        }
    }
}
