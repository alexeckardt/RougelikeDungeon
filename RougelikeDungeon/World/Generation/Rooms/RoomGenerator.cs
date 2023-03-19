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

        public const int GenerationRetrysBeforeFail = 200;

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

                case RoomType.GenericRoom:
                    return GenerateSimpleRoom(doorAt);

                case RoomType.Hallway:
                    return GenerateHallway(doorAt);

                case RoomType.Boss:
                    return GenerateBossArena(doorAt);

                case RoomType.BossHallway:
                    return GenerateHallway(doorAt);
            }
        }

        public Room GenerateSimpleRoom(RoomDoor doorAt)
        {
            bool MakeLargeRoom = random.NextDouble() < 0.1; //10% chance


            int maxSize = 25 + 25 * Convert.ToInt32(MakeLargeRoom);
            int minSize = maxSize / 2;
            int trysLeft = GenerationRetrysBeforeFail;

            //
            while (trysLeft > 0)
            {
                trysLeft--;

                //
                int w = random.NextRange(minSize, maxSize);
                int h = random.NextRange(minSize, maxSize);

                //Origin
                Vector2 pos = doorAt.Position;

                //Create
                Room newRoom = new ExplicitRectangleRoom(pos, doorHolder, 0, w, h);
                newRoom.MatchToDoorPosition(doorAt);

                //Make Sure Not Causing any Issues
                if (RoomIntersectsAnyOther(newRoom, doorAt.Owner))
                {
                    continue;
                }

                //Place New Room
                return newRoom;
            }

            return null;
        }

        public Room GenerateHallway(RoomDoor from)
        {
            //Get Direction
            bool MakeLHallway = random.NextDouble() < 0.1; //10% chance
            bool Horizontal = from.DoorDirection == RoomDoorJigsaw.Right || from.DoorDirection == RoomDoorJigsaw.Left;

            int HallwayHeight = 5;
            int HallwayBaseLength = 15;

            int trysLeft = GenerationRetrysBeforeFail;

            //
            while (trysLeft > 0)
            {
                trysLeft--;

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

                //Testing for future collision
                Room checkingTemp = new ExplicitRectangleRoom(pos+offsetPos, doorHolder, 0, realWidth*2, realHeight*2);
                if (RoomIntersectsAnyOther(checkingTemp, from.Owner))
                {
                    if (random.NextDouble() < 0.2) //20% chance to fail hallway
                    {
                        continue;
                    }

                    //Force L
                    MakeLHallway = true;
                    checkingTemp = null; //garbage collection
                }

                //Create the new room
                Room newRoom = new ExplicitRectangleRoom(pos+offsetPos, doorHolder, 0, realWidth, realHeight);
                newRoom.IsHallway = true;
                newRoom.AllowGeneratingPotentialDoors = false;

                //End Position
                Vector2 endPos = (Horizontal) ? pos + new Vector2(realWidth, 0) : pos + new Vector2(0, realHeight);
                RoomDoorJigsaw doorEnd = from.DoorDirection;

                //

                //
                if (MakeLHallway)
                {

                    Room testHallway = new ExplicitRectangleRoom(pos + offsetPos, doorHolder, 0, realWidth, realHeight);
                    testHallway.IsHallway = true;
                    testHallway.AllowGeneratingPotentialDoors = false;

                    int LtrysLeft = GenerationRetrysBeforeFail;
                    while (LtrysLeft > 0)
                    {
                        LtrysLeft--;

                        //Decide a Direction to create in (always perpendicular)
                        RoomDoorJigsaw testDoorEnd = (random.NextDouble() < 0.5) ? from.DirectionClockWise : from.DirectionCounterClockWise;

                        //
                        //
                        int Lextra = random.NextRange(-2, 5);
                        int LhallwayWidth = HallwayBaseLength + extra;

                        bool LHorizontal = !Horizontal;

                        int hallwayLength = LhallwayWidth;
                        int hallwayDepth = HallwayHeight; // 5

                        //Get Scale
                        Vector2 LdimentionScale = new Vector2(1, 1);
                        if (testDoorEnd == RoomDoorJigsaw.Left) LdimentionScale.X = -1;
                        if (testDoorEnd == RoomDoorJigsaw.Top) LdimentionScale.Y = -1;

                        int realLength = hallwayLength * (int)LdimentionScale.X;

                        Rectangle box;
                        if (LHorizontal)
                        {
                            //Move the end position back to inside s.t the hallway's original length doesn't increase.
                            //-ve hallwaylengths already start like this so i'ts fine
                            int newStartY = (int)endPos.Y;
                            if (dimentionScale.Y == 1)
                            {
                                newStartY -= (int)(dimentionScale.Y) * hallwayDepth;
                            } else
                            {
                                newStartY += 1;
                            }

                            box = new Rectangle((int) endPos.X, newStartY, realLength, hallwayDepth);

                            endPos.X += realLength;
                            endPos.Y = newStartY;
                        }
                        else
                        {
                            //Move the end position back to inside s.t the hallway's original length doesn't increase.
                            //-ve hallwaylengths start like this
                            int newStartX = (int)endPos.X;
                            if (dimentionScale.X == 1)
                            {
                                newStartX -= (int)(dimentionScale.X) * hallwayDepth;
                            }
                            else
                            {
                                newStartX += 1;
                            }

                            box = new Rectangle(newStartX, (int)endPos.Y, hallwayDepth, realLength);

                            endPos.Y += realLength;
                            endPos.X = newStartX;
                        }

                        //Add Box
                        testHallway.AddBoxFromRealPosition(box);

                        //Check Fails
                        if (RoomIntersectsAnyOther(testHallway, from.Owner))
                        {
                            continue;
                        }

                        //Override
                        newRoom = testHallway;


                        //Update End Positions
                        doorEnd = testDoorEnd;
                        break;
                    }

                    //Could Not Make L, Whatever.
                }

                //
                //Check If Room Collides With Anything
                //If Fail, Regenerate the Hallway
                if (RoomIntersectsAnyOther(newRoom, from.Owner)) {
                    continue;
                }



                //
                //Add Extra Doors If Want



                //Place Explicit Door At End Position



                var endDoor = new RoomDoor(endPos, doorEnd, RoomType.GenericRoom, newRoom);
                doorHolder.Add(endDoor);

                return newRoom;
            }

            //Fail - No Hallway Is Generated.
            return null;
        }
        public Room GenerateBossArena(RoomDoor doorAt)
        {
            throw new NotImplementedException();
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
