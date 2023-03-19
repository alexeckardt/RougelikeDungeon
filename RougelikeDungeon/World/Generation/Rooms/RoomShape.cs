using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.World.Generation.Rooms.Door;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation.Rooms
{
    internal class RoomShape
    {
        Room Owner;
        List<RoomShapePart> Parts;

        public RoomShape(Room owner)
        {
            Parts = new();
            Owner = owner;
        }

        public void AddShape(Rectangle rec)
        {
            var part = new RoomShapePart(rec, Owner);
            Parts.Add(part);
        }

        public bool Collide(RoomShape other, int buffer)
        {
            foreach (var part in other.Parts)
            {
                if (_Collide(part, buffer))
                {
                    return true;
                }
            }

            return false;
        }

        private bool _Collide(RoomShapePart part, int buffer)
        {
            foreach (var part2 in Parts)
            {

                bool hasIntersection = part.Intersects(part2);
                if (hasIntersection)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CollideLocal(RoomDoor tilePos) => CollideLocal(tilePos.Position);

        public bool CollideLocal(Vector2 tilePos)
        {
            foreach (var part2 in Parts)
            {
                if (part2.ContainsLocal(tilePos))
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, int tileSize)
        {
            //Draw Behind All
            foreach (var part in Parts)
            {
                spriteBatch.Draw(GameConstants.Instance.Pixel, new Vector2(part.X, part.Y) * tileSize, null, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, .9989f);
                spriteBatch.Draw(GameConstants.Instance.Pixel, new Vector2(part.X, part.Y) * tileSize, null, Color.DarkGray * 0.2f, 0f, Vector2.Zero, new Vector2(part.Width, part.Height) * tileSize, SpriteEffects.None, .999f);
            }
        }
    }
}
