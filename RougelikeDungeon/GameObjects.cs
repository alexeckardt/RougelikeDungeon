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
        private List<Solid> SolidObjectsCopy;

        public GameObjects()
        {
           this.Objects  = new List<GameObject>();
           this.SolidObjectsCopy = new List<Solid>();
        }

        //Object Addition

        public void Add(GameObject newObject)
        {
            Objects.Add(newObject);

            //Store in a Seperate Copy to Speed Up Collisions
            if (newObject is Solid)
                SolidObjectsCopy.Add((Solid) newObject);
        }

        //
        public void Remove(GameObject objectToDelete)
        {
            throw new NotImplementedException();
        }

        //Solid Collision Check

        public Solid CheckSolidCollision(CollisionBox input)
        {
            foreach (Solid solid in SolidObjectsCopy)
            {
                if (solid.Active)
                {
                    if (solid.Collider.Intersects(input))
                        return solid;
                }
            }

            return null;
        }

        public Solid CheckSolidCollision(Vector2 input)
        {
            foreach (Solid solid in SolidObjectsCopy)
            {
                if (solid.Active)
                {
                    if (solid.Collider.Contains(input))
                        return solid;
                }
            }

            return null;
        }

        //Passback

        public List<GameObject> AsList() => Objects;
    }
}
