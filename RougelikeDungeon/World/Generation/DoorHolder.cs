using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        List<RoomDoor> LegacyDoorPosses = new();
        private readonly Random random = new();

        public bool IsEmpty { get => DoorPosses.Count == 0; }

        public DoorHolder() { }

        //Add
        public void Add(RoomDoor newDoor)
        {
            DoorPosses.Add(newDoor);
            LegacyDoorPosses.Add(newDoor);
        }

        public RoomDoor PopRandom()
        {
            //Pass
            int ind = random.Next(DoorPosses.Count);

            //
            RoomDoor pos = DoorPosses[ind];
            DoorPosses.RemoveAt(ind);

            return pos;

        }

        public RoomDoor GetRandom() => DoorPosses[random.Next(DoorPosses.Count)];

        public void Draw(SpriteBatch spriteBatch, int tileSize)
        {
            Color col = Color.PaleVioletRed;

            foreach (RoomDoor door in LegacyDoorPosses) {
                Vector2 doorPosition = door.Position;

                spriteBatch.Draw(GameConstants.Instance.Pixel, doorPosition * tileSize, null, col, 0f, Vector2.Zero, tileSize, SpriteEffects.None, .996f);
            }
        }
    }
}
