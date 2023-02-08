using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Collision;
using RougelikeDungeon.World.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RougelikeDungeon.World
{
    internal class ObjectHandler : IObjectHandler
    {
        //
        private List<GameObject> Objects;

        //Adding & Removing
        private Queue<GameObject> ToAdd;
        private Queue<GameObject> ToRemove;

        public ObjectHandler()
        {
            Objects = new();

            ToAdd = new();
            ToRemove = new();
        }

        //Object Addition

        public void AddObject(GameObject newObject)
        {
            ToAdd.Enqueue(newObject);

            //Add to Chunk If Solid
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

        //
        // Instance
        //

        public void LoadObjects(ContentManager Content)
        {

            //Load All
            foreach (GameObject obj in Objects)
            {
                obj.Initalize();
                obj.LoadContent(Content);
            }
        }

        public void UpdateObjects(ContentManager Content, GameTime time, ILevelDataInstanceExposure level)
        {
            //
            AddEnqueuedObjects(Content);

            //
            foreach (GameObject obj in Objects)
            {
                obj.Update(level, time);
            }

            //Delete All
            ClearRemovedObjects();
        }

        public void DrawObjects(SpriteBatch spriteBatch)
        {
            foreach (GameObject obj in Objects)
            {
                obj.Draw(spriteBatch);
            }
        }
    }
}
