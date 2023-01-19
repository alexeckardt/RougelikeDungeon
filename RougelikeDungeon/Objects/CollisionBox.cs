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
        Rectangle BoundingBox;

        public CollisionBox() 
        {
            BoundingBox = new Rectangle();
        }

        public float Left
        {
            get { return BoundingBox.Left; }
        }

        public float Top
        {
            get { return BoundingBox.Top; }
        }

        public float Right
        {
            get { return BoundingBox.Right; }
        }

        public float Bottom
        {
            get { return BoundingBox.Bottom; }
        }

        public bool Intersects(CollisionBox other) => this.BoundingBox.Intersects(other.BoundingBox);
    }
}
