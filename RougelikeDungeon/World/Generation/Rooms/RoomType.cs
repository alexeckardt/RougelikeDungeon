using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation.Rooms
{
    internal enum RoomType
    {
        None,


        GenericRoom, // holder, splits into other types of rooms
        SmallSimpleRoom,
        LargeSimpleRoom,

        Hallway,

        Boss,
        BossHallway, //like in gungeon

        ComplexRoom,
        
    }
}
