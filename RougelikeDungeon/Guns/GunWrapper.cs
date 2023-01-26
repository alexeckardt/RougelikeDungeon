using Microsoft.Xna.Framework;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Bullets;
using RougelikeDungeon.Objects.Guns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Guns
{
    internal class GunWrapper
    {
        //
        Gun Gun;

        int ShotsLeft;

        public bool Shootable
        {
            get
            {
                bool MagEmpty = ShotsLeft <= 0;
                bool GunExists = Gun != null;

                return GunExists && !MagEmpty;
            }
        }

        public GunWrapper() { }

        public GunWrapper(Gun gun)
        {
            Gun = gun;
        }

        public void ReloadMag()
        {
            ShotsLeft = Gun.ClipSize;
        }

        public void Shoot(GameObjects objects, Vector2 SpawnPosition, Vector2 ShootDirection)
        {
            int bulletsToShoot = Math.Min(ShotsLeft, Gun.BulletsPerShot);

            for (var i = 0; i < bulletsToShoot; i++)
            {
                Bullet newBullet = MakeBulletInstance();

                //Set Properties
                newBullet.Position = SpawnPosition;
                newBullet.SetFirePosition(ShootDirection);

                //Add Instance To World
                objects.Add(newBullet);
            }
        }

        public Bullet MakeBulletInstance()
        {
            var spec = Gun.GetBulletSpec();
            var inst = (Bullet) spec.Core.GenerateInstance();

            //Set From Wrapper
            inst.Speed = spec.Speed;
            inst.Range = spec.Range;
            inst.PenatrationForce = spec.PenatrationForce;
            inst.DamageAmount = spec.DamageAmount;

            return inst;
        }
    }
}
