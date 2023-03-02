using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Tiles
{
    internal class TileData
    {
        public int Id;
        public float TileSetKey;
        public Color blend;

        public TileData(int id, float tilesetKey)
        {
            Id = id;
            TileSetKey = tilesetKey;
            blend = Color.White;
        }
    }
}
