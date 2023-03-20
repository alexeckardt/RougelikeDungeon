using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation.Rooms
{
    internal class RoomShapePart
    {
        Rectangle Rect;
        Room Owner;

        public RoomShapePart(Rectangle part, Room owner)
        {
            this.Rect = AbsoluteifyRectangle(part);
            this.Owner = owner;
        }

        public int X { get => Rect.X + (int)Owner.TileOrigin.X; }
        public int Y { get => Rect.Y + (int)Owner.TileOrigin.Y; }
        public int Width { get => Rect.Width; }
        public int Height { get => Rect.Height; }

        public int Left { get => X; }
        public int Right { get => X + Width; }
        public int Top { get => Y; }
        public int Bottom { get => Y + Height; }

        //Force all rectangles to have positive dimentionds
        private Rectangle AbsoluteifyRectangle(Rectangle input)
        {
            if (input.Width >= 0 && input.Height >= 0)
            {
                return input;
            }

            int w = Math.Abs(input.Width);
            int h = Math.Abs(input.Height);
            int x = input.X;
            int y = input.Y;

            if (input.Width < 0)
            {
                x += input.Width; //subtract cause -ve
            }
            if (input.Height < 0)
            {
                y += input.Height; //subtract cause -ve
            }

            return new Rectangle(x, y, w, h);
        }


        public bool ContainsLocal(Vector2 tilePosition)
        {
            return Rect.Contains(tilePosition);
        }

        public bool Intersects(RoomShapePart other) => this.Intersects(other, 0);

        public bool Intersects(RoomShapePart other, int buffer)
        {
            if (other.Left < Right+buffer && Left-buffer < other.Right && other.Top < Bottom + buffer)
            {
                return Top-buffer < other.Bottom;
            }

            return false;
        }
    }
}
