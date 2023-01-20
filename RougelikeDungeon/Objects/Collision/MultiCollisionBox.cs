using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects.Collision
{
    internal class MultiCollisions : ICollideable
    {
        readonly List<ICollideable> Collideables;

        private float masterX;
        private float masterY;
        private float masterWidth;
        private float masterHeight;
        private float masterXoffset;
        private float masterYoffset;

        public Vector2 Position 
        { 
            get => new Vector2(masterX, masterY);
            set
            {
                masterX = value.X;
                masterY = value.Y;
            }
        }

        public Vector2 Size { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Offset
        {
            get => new Vector2(masterXoffset, masterYoffset);
            set
            {
                masterX = value.X;
                masterY = value.Y;
            }
        }

        public float Left => throw new NotImplementedException();

        public float Top => throw new NotImplementedException();

        public float Right => throw new NotImplementedException();

        public float Bottom => throw new NotImplementedException();


        public MultiCollisions() 
        { 
            Collideables = new List<ICollideable>();
        }

        public bool Contains(Vector2 point)
        {
            foreach (ICollideable collision in Collideables)
            {
                if (collision.Contains(point))
                    return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ICollideable collision in Collideables)
            {
                collision.Draw(spriteBatch);
            }
        }

        public bool Intersects(CollisionBox other)
        {
            foreach (ICollideable collision in Collideables)
            {
                if (collision.Intersects(other))
                    return true;
            }

            return false;
        }

        //Adding Colliders (Allow To Keep Reference)
        public ICollideable Add(ICollideable collider)
        {
            Collideables.Add(collider);
            return collider;
        }

        //Remove
        public void Remove(ICollideable collider)
        {
            Collideables.Remove(collider);
        }
    }
}
