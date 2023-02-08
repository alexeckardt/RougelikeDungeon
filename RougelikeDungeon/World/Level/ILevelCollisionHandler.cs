using RougelikeDungeon.World.Chunks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RougelikeDungeon.World.Level
{
    internal interface ILevelCollisionHandler
    {
        public ChunkHandler ChunkHandler { get; }
        public ObjectHandler Objects { get; }
    }
}