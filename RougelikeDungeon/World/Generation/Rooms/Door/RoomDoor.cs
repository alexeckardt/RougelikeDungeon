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
        public RoomType RoomTypeWant = RoomType.GenericRoom;
        public readonly Room Owner;

        public int X { get => (int)Position.X; }
        public int Y { get => (int)Position.Y; }

        public RoomDoorJigsaw OppositeDirection {
            get
            {
                switch (DoorDirection)
                {
                    case RoomDoorJigsaw.None:
                    default:
                        return RoomDoorJigsaw.None;

                    case RoomDoorJigsaw.Left:
                        return RoomDoorJigsaw.Right;
                    
                    case RoomDoorJigsaw.Right:
                        return RoomDoorJigsaw.Left;
                    
                    case RoomDoorJigsaw.Bottom:
                        return RoomDoorJigsaw.Top;
                    
                    case RoomDoorJigsaw.Top:
                        return RoomDoorJigsaw.Bottom;
                }
            }
        }

        public RoomDoorJigsaw DirectionClockWise
        {
            get
            {
                switch (DoorDirection)
                {
                    case RoomDoorJigsaw.None:
                    default:
                        return RoomDoorJigsaw.None;

                    case RoomDoorJigsaw.Left:
                        return RoomDoorJigsaw.Top;

                    case RoomDoorJigsaw.Top:
                        return RoomDoorJigsaw.Right;

                    case RoomDoorJigsaw.Right:
                        return RoomDoorJigsaw.Bottom;

                    case RoomDoorJigsaw.Bottom:
                        return RoomDoorJigsaw.Left;
                }
            }
        }
        public RoomDoorJigsaw DirectionCounterClockWise
        {
            get
            {
                switch (DoorDirection)
                {
                    case RoomDoorJigsaw.None:
                    default:
                        return RoomDoorJigsaw.None;

                    case RoomDoorJigsaw.Left:
                        return RoomDoorJigsaw.Bottom;

                    case RoomDoorJigsaw.Top:
                        return RoomDoorJigsaw.Left;

                    case RoomDoorJigsaw.Right:
                        return RoomDoorJigsaw.Top;

                    case RoomDoorJigsaw.Bottom:
                        return RoomDoorJigsaw.Right;
                }
            }
        }

        public RoomDoor(Vector2 position, RoomDoorJigsaw doorDirection, RoomType roomWant, Room owner)
        {
            Position = position;
            DoorDirection = doorDirection;
            RoomTypeWant = roomWant;
            Owner = owner;
        }
    }
}
