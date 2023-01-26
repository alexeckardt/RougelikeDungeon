using RougelikeDungeon.Guns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects.PlayerObjects
{
    internal class PlayerGuns
    {
        private List<GunWrapper> Guns;

        private int GunSelected;

        public GunWrapper CurrentGun 
        { 
            get 
            {
                return Guns[GunSelected];
            }
        }

        public PlayerGuns()
        {
            Guns = new List<GunWrapper>();
            GunSelected = 0;

            //AddGun();
        }

        public void AddGun(GunWrapper newGun)
        {
            Guns.Add(newGun);
        }

        public void DeleteGun(GunWrapper newGun)
        {
            Guns.Remove(newGun);
        }

        public void DeleteCurrentGun()
        {
            Guns.Remove(CurrentGun);
        }


    }
}
