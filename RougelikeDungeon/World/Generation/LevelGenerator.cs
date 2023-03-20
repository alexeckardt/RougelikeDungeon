using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.World.Generation.Rooms;
using RougelikeDungeon.World.Generation.Rooms.Door;
using RougelikeDungeon.World.Generation.Rooms.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation
{
    internal class LevelGenerator
    {
        int Seed;
        RoomGenerator Rooms;
        DoorHolder KnownRoomDoors;
        DebugTiles DebugTiles;
        Random random;

        public bool Step = false;
        public bool Done = false;

        public int MaxStepDepth = 105;
        private int stepDepth = 0;

        const float DoorEatDistance = 250;

        public LevelGenerator(int seed)
        {
            Seed = seed;
            KnownRoomDoors = new();
            random = new();
            DebugTiles = new();

            Rooms = new(KnownRoomDoors);
        }

        public void SetSeed(int seed)
        {
            Seed = seed;
        }

        public void RegenerateRooms()
        {
            KnownRoomDoors = new();
            DebugTiles = new();
            Rooms = new(KnownRoomDoors);

            Done = false;
            stepDepth = 0;

            GenerateRooms();
        }

        public void GenerateRooms()
        {

            //Start
            GenerateStartingRoom();


        }

        public void GenerationStep()
        {
            stepDepth++;

            //Basic Generation
            if (stepDepth <= MaxStepDepth)
            {
                //Check For Places to Create Rooms
                if (KnownRoomDoors.IsEmpty)
                {
                    //Generate New Doors
                    var doorMade = CreateDoor();
                    if (doorMade)
                    {
                        //Exit Early, For Step
                        return;
                    }
                    else
                    {
                        //Finished
                        stepDepth = MaxStepDepth;
                        return;
                    }
                }

                //Choose Door To Generate From
                var Door = KnownRoomDoors.PopRandom();

                //Eat Door
                var distFromCenter = Door.Position.Length();
                var chance = Math.Pow(distFromCenter / DoorEatDistance, 3);
                var d = random.NextDouble();

                if (d < chance)
                {
                    //Door Gets Popped.
                    return;
                }

                //Create New Room
                var newRoom = Rooms.GenerateRoomAtDoor(Door);

                //Add Room If Able
                if (newRoom != null)
                {
                    Rooms.Add(newRoom);
                }
                else
                {
                    //Room failed to be placed. It's okay, because we popped the door so this won't happen again.
                }
            } else
            {
                //End
                Rooms.CleanupDeadRooms();
                Done = true;
            }
        }

        private bool CreateDoor()
        {
            int trys = 0;
            int maxtrys = 200;

            while (trys < maxtrys)
            {
                //Choose Random
                trys++;
                int ind = random.Next(Rooms.Count);

                Room roomTesting = Rooms.GetIndex(ind);
                if (roomTesting.PotentialDoorsLeft > 0)
                {
                    var doorPos = roomTesting.ConvertPotentialDoor();

                    if (doorPos != null)
                    {
                        KnownRoomDoors.Add(doorPos);
                        return true;
                    }
                }
            }

            return false;
        }

        public void GenerateStartingRoom()
        {
            /*
            var r = new Room(Vector2.Zero);

            int w = 22 + random.NextMaxRangeRandomSign(5);
            int h = 24 + random.NextMaxRangeRandomSign(5);

            bool b = random.NextBool();
            int rw = b ? w : h;
            int rh = b ? h : w;

            r.AddBox(new Rectangle(-rw/2, -rh/2, rw, rh));
            Rooms.Add(r);
            */

            Rooms.Add(new RandomRectangleRoom(Vector2.Zero, KnownRoomDoors, 2, 22, 24));
        }

        public void Draw(SpriteBatch spriteBatch, int tileSize)
        {
            Rooms.Draw(spriteBatch, tileSize);

            KnownRoomDoors.Draw(spriteBatch, tileSize);
            DebugTiles.Draw(spriteBatch, tileSize);
        }
    }
}
