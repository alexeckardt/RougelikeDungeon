using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.World.Level;
using RougelikeDungeon.World.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Chunks
{
    internal class ChunkTiles
    {
        private int TilesWide;
        private int TileWidth;

        Dictionary<float, TileMap> TileMaps;

        public ChunkTiles(int tilesWide, int tileWidth)
        {
            TilesWide = tilesWide;
            TileMaps = new();
            TileWidth = tileWidth;
        }

        public void SetTile(TileData tileData, Vector2 position)
        {
            //Get Depth
            float tileSet = tileData.TileSetKey;

            //Get Map from Key
            TileMap map = null;
            TileMaps.TryGetValue(tileSet, out map);

            if (map == null)
            {
                map = new TileMap(TilesWide, TileWidth);
                TileMaps[tileSet] = map;
            }

            //Set / Override
            map.SetTile((int)position.X, (int)position.Y, tileData);
        }

        public void DrawTiles(SpriteBatch spriteBatch, LevelTileSets tileSets, Vector2 ChunkPosition)
        {

            foreach (var tileSetDepth in TileMaps.Keys)
            {
                TileMap tileMap = null;
                TileMaps.TryGetValue(tileSetDepth, out tileMap);

                if (tileMap == null) continue;

                var tileSet = tileSets.GetTileSet(tileSetDepth);
                tileMap.Draw(spriteBatch, tileSet, tileSetDepth, ChunkPosition);
            }

        }
    }
}
