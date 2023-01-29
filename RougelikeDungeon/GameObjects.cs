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
        private Queue<GameObject> ToRemove;

        public GameObjects()
        {
            this.Objects  = new List<GameObject>();
            Collisions = new SolidCollisions();

            ToAdd = new Queue<GameObject>();
            ToRemove = new Queue<GameObject>();
        }

        //Object Addition

        public void AddObjects(GameObject newObject)
        {
            ToAdd.Enqueue(newObject);

            //Store in a Seperate Copy to Speed Up Collisions
            if (newObject is Solid)
                Collisions.Add((Solid)newObject);
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

        //Remove
        public void RemoveObjects(GameObject obj)
        {
            ToRemove.Enqueue(obj);
        }

        public void ClearRemovedObjects()
        {
            //Add Everything I Want to Add
            while (ToRemove.Count > 0)
            {
                var obj = ToRemove.Dequeue();
                obj.CleanUp();

                //Remove The Object
                Objects.Remove(obj);
            }
        }
    }
}
