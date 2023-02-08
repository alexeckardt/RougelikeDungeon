using RougelikeDungeon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Chunks
{
    internal interface IActiveChunk
    {
        public HashSet<IGameObject> ChunkObjects { get; }
    }
}
