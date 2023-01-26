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

        private int Slot0 = 0;
        private int Slot1 = 0;

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

        public void SwitchGun()
        {
            if (GunSelected != Slot0)
            {
                SwitchGun(Slot0);
            } else
            {
                SwitchGun(Slot1);
            }
        }

        public void SwitchGun(int position)
        {
            //Switch
            GunSelected = position;

            //Update
            if (CurrentGun.EmptyClip)
            {
                CurrentGun.BeginPullout();
            }
        }
    }
}
