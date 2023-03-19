using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation.Rooms.Door
{
    internal class RoomDoor
    {

        public Vector2 Position;
        public RoomDoorJigsaw DoorDirection = RoomDoorJigsaw.None;
        public RoomType RoomTypeWant = RoomType.SimpleRoom;
        public readonly Room Owner;

        public int X { get => (int)Position.X; }
        public int Y { get => (int)Position.Y; }

        public RoomDoor(Vector2 position, RoomDoorJigsaw doorDirection, RoomType roomWant, Room owner)
        {
            Position = position;
            DoorDirection = doorDirection;
            RoomTypeWant = roomWant;
            Owner = owner;
        }
    }
}
