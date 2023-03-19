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

        List<Rectangle> Parts;

        public RoomShape()
        {
            Parts = new List<Rectangle>();
        }

        public void AddShape(Rectangle rec)
        {
            Parts.Add(rec);
        }

        public bool Collide(RoomShape other, int buffer)
        {
            foreach (Rectangle part in other.Parts)
            {
                if (_Collide(part, buffer))
                {
                    return true;
                }
            }

            return false;
        }

        private bool _Collide(Rectangle part, int buffer)
        {
            foreach (Rectangle part2 in Parts)
            {

                bool containsPart = part.X - buffer <= part2.X
                                && part2.X + part2.Width <= part.X + part.Width + buffer 
                                && part.Y - buffer <= part2.Y 
                                && part2.Y + part2.Height <= part.Y + part.Height + buffer;

                if (containsPart)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Collide(Vector2 tilePos)
        {
            foreach (Rectangle part2 in Parts)
            {
                if (part2.Contains(tilePos))
                {
                    return true;
                }
            }
            return false;
        }
        
        public bool Collide(RoomDoor tilePos) => Collide(tilePos.Position);


        public void Draw(SpriteBatch spriteBatch, int tileSize)
        {
            //Draw Behind All
            foreach (Rectangle part in Parts)
            {
                spriteBatch.Draw(GameConstants.Instance.Pixel, new Vector2(part.X, part.Y) * tileSize, null, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, .9989f);
                spriteBatch.Draw(GameConstants.Instance.Pixel, new Vector2(part.X, part.Y) * tileSize, null, Color.DarkGray * 0.2f, 0f, Vector2.Zero, new Vector2(part.Width, part.Height) * tileSize, SpriteEffects.None, .999f);
            }
        }
    }
}
