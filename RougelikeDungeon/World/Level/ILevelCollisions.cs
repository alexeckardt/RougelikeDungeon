using RougelikeDungeon.Objects.Collision;
using RougelikeDungeon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace RougelikeDungeon.World.Level
{
    internal interface ILevelCollisions
    {
        public Solid CheckSolidCollision(ICollideable input);
        public Solid CheckSolidCollision(ICollideable input, Vector2 boxOffsetPosition);
        public Solid CheckSolidPosition(Vector2 input);
        public IGameObject CheckCollision(ICollideable input);
        public IGameObject CheckCollisionWith(ICollideable input, Type type);
    }
}
