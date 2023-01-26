using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Collision;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RougelikeDungeon
{
    internal class GameObjects
    {
        private List<GameObject> Objects;
        private SolidCollisions Collisions;

        private Queue<GameObject> ToAdd;

        public GameObjects()
        {
            this.Objects  = new List<GameObject>();
            Collisions = new SolidCollisions();
            ToAdd = new Queue<GameObject>();
        }

        //Object Addition

        public void Add(GameObject newObject)
        {
            ToAdd.Enqueue(newObject);

            //Store in a Seperate Copy to Speed Up Collisions
            if (newObject is Solid)
                Collisions.Add((Solid) newObject);
        }

        //
        public void Remove(GameObject objectToDelete)
        {
            throw new NotImplementedException();
        }

        public void AddEnqueuedObjects(ContentManager Content)
        {
            //Add Everything I Want to Add
            while (ToAdd.Count > 0)
            {
                //
                var obj = ToAdd.Dequeue();

                //Setup
                obj.Initalize();
                obj.LoadContent(Content);

                //
                Objects.Add(obj);
            }
        }

        //Passback
        public List<GameObject> AsList() => Objects;

        public Solid CheckSolidCollision(CollisionBox input) => Collisions.CheckSolidCollision(input);
        public Solid CheckSolidCollision(CollisionBox input, Vector2 boxOffsetPosition) => Collisions.CheckSolidCollision(input, boxOffsetPosition);
        public Solid CheckSolidPosition(Vector2 input) => Collisions.CheckSolidCollision(input);
    }
}
