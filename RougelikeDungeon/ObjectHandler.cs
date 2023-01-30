using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Collision;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RougelikeDungeon
{
    internal class ObjectHandler : IObjectHandler
    {
        //
        //Singleton Instantiation
        //
        private static ObjectHandler inst;
        public static IObjectHandler Instance
        {
            get
            {
                inst ??= new ObjectHandler();
                return inst;
            }
        }

        //
        private List<GameObject> Objects;

        //Adding & Removing

        private Queue<GameObject> ToAdd;
        private Queue<GameObject> ToRemove;

        //Copy References
        private SolidCollisions SolidCollisions;
        private HashSet<ICollideable> Collidables;

        private ObjectHandler()
        {
            this.Objects = new();
            SolidCollisions = new();
            Collidables = new();

            ToAdd = new();
            ToRemove = new();
        }

        //Object Addition

        public void AddObject(GameObject newObject)
        {
            ToAdd.Enqueue(newObject);

            //Store in a Seperate Copy to Speed Up Collisions
            if (newObject is Solid)
                SolidCollisions.Add((Solid)newObject);
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

        public Solid CheckSolidCollision(ICollideable input) => SolidCollisions.CheckSolidCollision(input);
        public Solid CheckSolidCollision(ICollideable input, Vector2 boxOffsetPosition) => SolidCollisions.CheckSolidCollision(input, boxOffsetPosition);
        public Solid CheckSolidPosition(Vector2 input) => SolidCollisions.CheckSolidCollision(input);

        //Remove
        public void RemoveObject(GameObject obj)
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

        public void AddCollideableReference(ICollideable reference)
        {
            Collidables.Add(reference);
        }

        public ICollideable CheckCollision(ICollideable input)
        {
            //To Remove
            Stack<ICollideable> toRemove = new();

            //Look
            foreach (ICollideable hitbox in Collidables)
            {
                //Check
                if (hitbox.Intersects(input))
                {
                    return hitbox;
                }
            }

            return null;
        }

        public ICollideable CheckCollisionWith(ICollideable input, Type type)
        {
            //To Remove
            Stack<ICollideable> toRemove = new();

            //Look
            foreach (ICollideable hitbox in Collidables)
            {
                //Check if It's what i'm searching for
                if (hitbox.CreatorType.IsAssignableFrom(type))
                {
                    //Check
                    if (hitbox.Intersects(input)) //maybe switch these to increase performace? idk
                    {
                        return hitbox;
                    }
                }
            }

            return null;
        }
    }
}
