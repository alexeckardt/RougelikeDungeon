using RougelikeDungeon.Objects;
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

        public GameObjects()
        {
           this.Objects  = new List<GameObject>();
        }

        public void Add(GameObject newObject)
        {
            Objects.Add(newObject);
        }

        public void Remove(GameObject objectToDelete)
        {
            throw new NotImplementedException();
        }

        //Passback
        public List<GameObject> AsList() => Objects;
    }
}
