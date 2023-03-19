using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.World.Generation.Rooms.Door;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation.Rooms
{
    internal class Room
    {

        public Vector2 TileOrigin;
        public readonly RoomShape Shape;
        public readonly HashSet<RoomDoor> PotentialDoors;
        private readonly DoorHolder DoorsReference;
        protected readonly Random random;

        public bool IsHallway = false;

        int DoorWidth = 3;
        int PotentialDoorCount = 0;
        protected int DoorsToSpawnWith = 0;

        int maxDoorSameWallSeperation {  get => DoorWidth * 2; }

        public int PotentialDoorsLeft { get => PotentialDoorCount; }

        public Room(Vector2 origin, DoorHolder doorsReference)
        {
            TileOrigin = origin;
            Shape = new();
            random = new();
            PotentialDoors = new();

            DoorsReference = doorsReference;
        }

        public virtual void GenerateShape()
        {
            //Enqueue Doors
            for (var i = 0; i < DoorsToSpawnWith; i++)
            {
                RoomDoor doorPos = ConvertPotentialDoor();
                DoorsReference.Add(doorPos);
            }
        }

        public void AddBox(Rectangle box)
        {
            //Add the Edges Of The Box
            int forcedCornerSize = 1; //space corner must have
            var space = (DoorWidth + 1) / 2 + forcedCornerSize;

            for (var i = space; i < box.Width - space; i++)
            {
                RoomDoor top = new RoomDoor(new Vector2(box.Left + i, box.Top-1), RoomDoorJigsaw.Top, RoomType.Hallway, this);
                RoomDoor bottom = new RoomDoor(new Vector2(box.Left + i, box.Bottom), RoomDoorJigsaw.Bottom, RoomType.Hallway, this);

                PotentialDoors.Add(top);
                PotentialDoors.Add(bottom);

                PotentialDoorCount += 2;
            }

            for (var i = space; i < box.Height - space; i++)
            {
                RoomDoor left = new RoomDoor(new Vector2(box.Left-1, box.Top + i), RoomDoorJigsaw.Left, RoomType.Hallway, this);
                RoomDoor right = new RoomDoor(new Vector2(box.Right, box.Top + i), RoomDoorJigsaw.Right, RoomType.Hallway, this);

                PotentialDoors.Add(left);
                PotentialDoors.Add(right);

                PotentialDoorCount += 2;
            }

            CleanUpPotentialDoors();

            //Add Box
            Shape.AddShape(box);
        }

        private void CleanUpPotentialDoors()
        {
            Queue<RoomDoor> toDelete = new();

            //
            foreach (var edgePos in PotentialDoors)
            {
                if (Shape.Collide(edgePos))
                {
                    toDelete.Enqueue(edgePos);
                }
            }

            //
            foreach (var toDel in toDelete)
            {
                PotentialDoors.Remove(toDel);
                PotentialDoorCount--;
            }
        }

        public RoomDoor ConvertPotentialDoor()
        {
            //Get Random Door
            var Door = PotentialDoors.ElementAt(random.Next(PotentialDoorCount));

            //Remove from Potentials
            PotentialDoors.Remove(Door);
            PotentialDoorCount--;

            //Remove any Within Distance
            ClearForDoorway(Door);

            //Give Back
            return Door;
        }

        public void ClearForDoorway(RoomDoor Source)
        {
            //O(n)
            foreach (var potDoor in PotentialDoors)
            {
                //On Same Plane
                if (potDoor.X == Source.X || potDoor.Y == Source.Y)
                {
                    //Door Would be Too Close
                    int sep = maxDoorSameWallSeperation;

                    int xdiff = Math.Abs(potDoor.X - Source.X);
                    int ydiff = Math.Abs(potDoor.Y - Source.Y);

                    if ((xdiff == 0 && ydiff < sep) || (ydiff == 0 && xdiff < sep))
                    {
                        PotentialDoors.Remove(potDoor);
                        PotentialDoorCount--;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int tileSize)
        {
            Shape.Draw(spriteBatch, tileSize);
            spriteBatch.Draw(GameConstants.Instance.Pixel, TileOrigin*tileSize, null, Color.Red, 0f, Vector2.Zero, tileSize, SpriteEffects.None, .998f);

            foreach (RoomDoor potentialDoor in PotentialDoors)
            {
                spriteBatch.Draw(GameConstants.Instance.Pixel, potentialDoor.Position * tileSize, null, Color.White*0.1f, 0f, Vector2.Zero, tileSize, SpriteEffects.None, .998f);
            }
        }

        public bool Contains(Vector2 tileCoord)
        {



            return false;
        }

        public bool Collides(Room otherRoom, int buffer = 0)
        {
            return Shape.Collide(otherRoom.Shape, buffer);
        }
    }
}
