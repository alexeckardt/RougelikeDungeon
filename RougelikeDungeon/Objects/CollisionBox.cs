using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects
{
    internal class CollisionBox
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public float Left => X;
        public float Top => Y;
        public float Right => X + Width;
        public float Bottom => Y + Height;

        public Vector2 Position
        {
            get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public Vector2 Size
        {
            get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public Vector2 Center => new Vector2(X + Width / 2, Y + Height / 2);

        //Constructors

        public CollisionBox() 
        {
        }

        public CollisionBox(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
        
        public CollisionBox(float width, float height)
        {
            this.X = 0;
            this.Y = 0;
            this.Width = width;
            this.Height = height;
        }

        //Methods

        public bool Intersects(CollisionBox other)
        {
            if (other.Left < Right && Left < other.Right && other.Top < Bottom)
            {
                return Top < other.Bottom;
            }

            return false;
        }

        public bool Contains(Vector2 point)
        {
            if (X <= point.X && point.X <= X + Width && Y <= point.Y)
            {
                return point.Y <= Y + Height;
            }

            return false;
        }

        public override int GetHashCode()
        {
            //Stolen from Microsoft.XNA.Rectangle Hashcode
            return (((17 * 23 + X.GetHashCode()) * 23 + Y.GetHashCode()) * 23 + Width.GetHashCode()) * 23 + Height.GetHashCode();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Behind All
            spriteBatch.Draw(GlobalTextures.Instance.Pixel, Position, null, Color.Red, 0f, Vector2.Zero, Size, SpriteEffects.None, 1f);
        }
    }
}
