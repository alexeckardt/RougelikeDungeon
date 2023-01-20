using Microsoft.Xna.Framework;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Collision;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon
{
    internal class GameObjects
    {
        private List<GameObject> Objects;
        private SolidCollisions Collisions;

        public GameObjects()
        {
           this.Objects  = new List<GameObject>();
           Collisions = new SolidCollisions();
        }

        //Object Addition

        public void Add(GameObject newObject)
        {
            Objects.Add(newObject);

            //Store in a Seperate Copy to Speed Up Collisions
            if (newObject is Solid)
                Collisions.Add((Solid) newObject);
        }

        //
        public void Remove(GameObject objectToDelete)
        {
            throw new NotImplementedException();
        }

        //Passback
        public List<GameObject> AsList() => Objects;

        public Solid CheckSolidCollision(CollisionBox input) => Collisions.CheckSolidCollision(input);
        public Solid CheckSolidCollision(CollisionBox input, Vector2 boxOffsetPosition) => Collisions.CheckSolidCollision(input, boxOffsetPosition);
        public Solid CheckSolidPosition(Vector2 input) => Collisions.CheckSolidCollision(input);
    }
}
