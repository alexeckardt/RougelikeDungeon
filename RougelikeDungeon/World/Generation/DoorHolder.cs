using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.World.Generation.Rooms;
using RougelikeDungeon.World.Generation.Rooms.Door;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation
{
    internal class DoorHolder
    {

        List<RoomDoor> DoorPosses = new();
        List<RoomDoor> PriorityDoorPosses = new();



        List<RoomDoor> HistoryDoorPosses = new();
        private readonly Random random = new();

        public bool IsEmpty { get => DoorPosses.Count + PriorityDoorPosses.Count == 0; }

        public DoorHolder() { }

        //Add
        public void Add(RoomDoor newDoor)
        {
            if (newDoor == null)
            {
                return;
            }

            HistoryDoorPosses.Add(newDoor);
            if (newDoor.RoomTypeWant == RoomType.Hallway)
            {
                DoorPosses.Add(newDoor);
                return;
            }

            //Special Rooms, Rooms Should Come First
            PriorityDoorPosses.Add(newDoor);
        }

        public RoomDoor PopRandom()
        {

            if (PriorityDoorPosses.Count > 0)
            {
                int priorityInd = random.Next(PriorityDoorPosses.Count);
                RoomDoor priortyDoor = PriorityDoorPosses[priorityInd];
                PriorityDoorPosses.RemoveAt(priorityInd);
                return priortyDoor;
            }

            //Pass
            int ind = random.Next(DoorPosses.Count);
            RoomDoor pos = DoorPosses[ind];
            DoorPosses.RemoveAt(ind);
            return pos;
        }

        public RoomDoor GetRandom() => DoorPosses[random.Next(DoorPosses.Count)];

        public void Draw(SpriteBatch spriteBatch, int tileSize)
        {
            Color col = Color.PaleVioletRed;

            foreach (RoomDoor door in HistoryDoorPosses) {
                Vector2 doorPosition = door.Position;

                spriteBatch.Draw(GameConstants.Instance.Pixel, doorPosition * tileSize, null, col, 0f, Vector2.Zero, tileSize, SpriteEffects.None, .996f);
            }
        }
    }
}
