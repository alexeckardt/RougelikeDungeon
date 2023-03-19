using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.World.Generation.Rooms.Door;
using RougelikeDungeon.World.Generation.Rooms.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation.Rooms
{
    internal class RoomGenerator
    {
        public readonly DoorHolder doorHolder;
        public readonly List<Room> Rooms;
        public readonly Random random;

        public int Count { get => Rooms.Count; }

        public RoomGenerator(DoorHolder doorHolder)
        {
            this.doorHolder = doorHolder;
            Rooms = new();
            random = new();
        }

        public void Add(Room newRoom)
        {
            Rooms.Add(newRoom);
        }

        public Room GetIndex(int index) => Rooms[index];


        //
        //
        //

        public bool RoomIntersectsAnyOther(Room checking, Room allowedToIntersect = null)
        {
            foreach (Room room in Rooms)
            {
                if (room != allowedToIntersect)
                {
                    if (room.Collides(checking, 1)) {
                        return true;
                    }
                }
            }

            return false;
        }

        //
        //
        //

        public Room GenerateRoomAtDoor(RoomDoor doorAt)
        {
            switch (doorAt.RoomTypeWant)
            {
                default:
                    return null;

                case RoomType.SimpleRoom:
                    return null;

                case RoomType.Hallway:
                    return GenerateHallway(doorAt);

                case RoomType.Boss:
                    return null;
            }
        }

        public Room GenerateHallway(RoomDoor from)
        {
            //Get Direction
            bool MakeLHallway = random.NextDouble() < 0.1; //10% chance
            bool Horizontal = from.DoorDirection == RoomDoorJigsaw.Right || from.DoorDirection == RoomDoorJigsaw.Left;

            int HallwayHeight = 5;
            int HallwayBaseLength = 15;

            int trysLeft = 200;

            //
            while (trysLeft > 0)
            {
                int extra = random.NextRange(-2, 5);
                int hallwayWidth = HallwayBaseLength + extra;

                int width = (Horizontal) ? hallwayWidth : HallwayHeight;
                int height = (Horizontal) ? HallwayHeight : hallwayWidth;

                //Get Scale
                Vector2 dimentionScale = new Vector2(1, 1);
                if (from.DoorDirection == RoomDoorJigsaw.Left) dimentionScale.X = -1; 
                if (from.DoorDirection == RoomDoorJigsaw.Top) dimentionScale.Y = -1;

                //Placement
                Vector2 pos = from.Position;

                int realWidth = width * (int)dimentionScale.X;
                int realHeight = height * (int)dimentionScale.Y;

                Vector2 offsetPos = Vector2.Zero;
                var hallwayHeightH = (HallwayHeight+1) / 2;
                switch (from.DoorDirection)
                {
                    case RoomDoorJigsaw.Left:
                        offsetPos = new Vector2(1, -hallwayHeightH); break;
                    case RoomDoorJigsaw.Right:
                        offsetPos = new Vector2(0,-hallwayHeightH); break;
                    case RoomDoorJigsaw.Top:
                        offsetPos = new Vector2(-hallwayHeightH, 1); break;
                    case RoomDoorJigsaw.Bottom:
                        offsetPos = new Vector2(-hallwayHeightH, 0); break;
                }

                Room newRoom = new ExplicitRectangleRoom(pos+offsetPos, doorHolder, 0, realWidth, realHeight);

                //End Position
                Vector2 endPos = (Horizontal) ? pos + new Vector2(realWidth, 0) : pos + new Vector2(0, realHeight);
                RoomDoorJigsaw doorEnd = from.DoorDirection;

                //

                //
                if (MakeLHallway)
                {

                    //Update
                    endPos = endPos;
                }

                //
                //Check If Room Collides With Anything
                //If Fail, Regenerate the Hallway
                if (RoomIntersectsAnyOther(newRoom, from.Owner)) {
                    trysLeft--;
                    continue;
                }



                //
                //Add Extra Doors If Want



                //Place Explicit Door At End Position



                var endDoor = new RoomDoor(endPos, RoomDoorJigsaw.None, RoomType.SimpleRoom, newRoom);
                doorHolder.Add(endDoor);

                return newRoom;
                
            }

            //Fail - No Hallway Is Generated.
            return null;
        }

        //
        //
        //

        public void Draw(SpriteBatch spriteBatch, int tileSize)
        {
            foreach (var room in Rooms)
            {
                room.Draw(spriteBatch, tileSize);
            }
        }
    }
}
