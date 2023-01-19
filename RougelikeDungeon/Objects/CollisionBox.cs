using Microsoft.Xna.Framework;
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
        public float W;
        public float H;

        public float Left => X;
        public float Top => Y;
        public float Right => X + W;
        public float Bottom => Y + H;

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
            get => new Vector2(W, H);
            set
            {
                W = value.X;
                H = value.Y;
            }
        }

        public Vector2 Center => new Vector2(X + W / 2, Y + H / 2);

        //Constructors

        public CollisionBox() 
        {
        }

        public CollisionBox(float x, float y, float w, float h)
        {
            this.X = x;
            this.Y = y;
            this.W = w;
            this.H = h;
        }

        //Equalities

        public static bool operator ==(CollisionBox a, CollisionBox b)
        {
            if (a.X == b.X && a.Y == b.Y && a.W == b.H)
            {
                return a.H == b.H;
            }

            return false;
        }

        public static bool operator !=(CollisionBox a, CollisionBox b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            if (obj is CollisionBox)
            {
                return this == (CollisionBox)obj;
            }

            return false;
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
            if (X <= point.X && point.X <= X + W && Y <= point.Y)
            {
                return point.Y <= Y + H;
            }

            return false;
        }

        public override int GetHashCode()
        {
            //Stolen from Microsoft.XNA.Rectangle Hashcode
            return (((17 * 23 + X.GetHashCode()) * 23 + Y.GetHashCode()) * 23 + W.GetHashCode()) * 23 + H.GetHashCode();
        }
    }
}
