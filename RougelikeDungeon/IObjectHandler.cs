using Microsoft.Xna.Framework;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon
{
    internal interface IObjectHandler
    {
        public void AddObject(GameObject newObject);
        public void RemoveObject(GameObject newObject);

        //Collisions
        public Solid CheckSolidCollision(ICollideable input);
        public Solid CheckSolidCollision(ICollideable input, Vector2 boxOffsetPosition);
        public Solid CheckSolidPosition(Vector2 input);
        public void AddCollideableReference(ICollideable reference);

        public ICollideable CheckCollision(ICollideable input);
        public ICollideable CheckCollisionWith(ICollideable input, Type type);
    }
}
