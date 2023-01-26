using Microsoft.Xna.Framework.Content;
using RougelikeDungeon.Guns;
using RougelikeDungeon.Guns.Guns;
using RougelikeDungeon.Objects.Guns;
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

        public bool RequiredLoadContent = true;

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
            RequiredLoadContent = true;

            AddGun(new DefaultPistol());
        }

        public void AddGun(Gun newGun)
        {
            GunWrapper newWrapper = new GunWrapper(newGun);
            Guns.Add(newWrapper);
        }
        
        public void AddGun(GunWrapper newWrapper)
        {
            Guns.Add(newWrapper);
            RequiredLoadContent = true;
        }

        public void DeleteGun(int i)
        {
            Guns.RemoveAt(i);
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

        public void LoadContent(ContentManager Content)
        {
            //
            foreach (GunWrapper gun in this.Guns) {
                gun.LoadContent(Content);
                RequiredLoadContent = false;
            }
        }
    }
}
