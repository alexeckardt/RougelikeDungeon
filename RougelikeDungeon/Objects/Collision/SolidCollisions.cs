using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects.Collision
{
    internal class SolidCollisions
    {
        private List<Solid> SolidObjects;

        public SolidCollisions() {
            this.SolidObjects = new List<Solid>();
        }

        //Add (Allow Passback)
        public Solid Add(Solid newSolid)
        {
            SolidObjects.Add(newSolid);
            return newSolid;
        }

        public void Remove(Solid solid)
        {
            SolidObjects.Remove(solid);
        }

        //
        // Collision Functions
        //
        //Solid Collision Check

        public Solid CheckSolidCollision(ICollideable input) => CheckSolidCollision(input, Vector2.Zero);

        public Solid CheckSolidCollision(ICollideable input, Vector2 CheckPositionOffset)
        {
            if (!input.IsEnabled()) return null;

            //Move TestBox As Described
            if (CheckPositionOffset != Vector2.Zero)
                input.Position += CheckPositionOffset;

            Solid foundSolid = null;

            //Test for Collision (Bad, O(n))
            foreach (Solid solid in SolidObjects)
            {
                if (!solid.Active) continue;
                
                if (solid.Collider.Intersects(input))
                {
                    foundSolid = solid;
                    break;
                }
            }

            //Return Collider
            if (CheckPositionOffset != Vector2.Zero)
                input.Position -= CheckPositionOffset;

            //Return
            return foundSolid;
        }

        public Solid CheckSolidCollision(Vector2 input)
        {
            foreach (Solid solid in SolidObjects)
            {
                if (solid.Active)
                {
                    if (solid.Collider.Contains(input))
                        return solid;
                }
            }

            return null;
        }

    }
}
