using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace RougelikeDungeon.Objects.Collision
{
    internal interface ICollideable
    {
        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }

        public Vector2 Offset { get; set; }

        public bool Intersects(CollisionBox other);

        public bool Contains(Vector2 point);

        public void Draw(SpriteBatch spriteBatch);

        public float Left { get; }
        public float Top { get; }
        public float Right { get; }
        public float Bottom { get; }
    }
}
