using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougelikeDungeon.World.Tiles;

namespace RougelikeDungeon.World.Level
{
    internal class LevelTileSets
    {
        Dictionary<float, TileSet> tileSets;

        public LevelTileSets()
        {
            tileSets = new();
        }

        public TileSet GetTileSet(float key)
        {
            return tileSets[key];
        }

        public void Add(float key, TileSet newTiles)
        {
            tileSets[key] = newTiles;
        }
    }
}
