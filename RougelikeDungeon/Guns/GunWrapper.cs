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
        Gun gun;
        int ShotsLeft;

        DateTime lastShot;
        DateTime reloadStarted;
        DateTime pulloutStarted;

        //Known
        public bool Reloading { get => (DateTime.Now - reloadStarted).Milliseconds <= Gun.ReloadMilliseconds; }

        public bool EmptyClip { get => ShotsLeft <= 0; }

        public bool PullingOut { get => (DateTime.Now - pulloutStarted).Milliseconds <= Gun.PullOutMilliseconds; }

        public bool Shootable
        {
            get
            {
                bool GunExists = Gun != null;
                bool ShotCooldownFinished = (DateTime.Now - lastShot).Milliseconds >= Gun.MillisecondsBetweenShots;

                return GunExists && !EmptyClip && !Reloading && !PullingOut && ShotCooldownFinished;
            }
        }

        //Get
        public Gun Gun
        {
            get => gun;
        }

        public GunWrapper() { }

        public GunWrapper(Gun gun)
        {
            this.gun = gun;
        }

        //------------------------------------------------


        //------------------------------------------------

        public void BeginReload()
        {
            //Start Reload
            reloadStarted = DateTime.UtcNow;
        }

        //
        public void ResetMagazine()
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

            //Update
            ShotsLeft -= bulletsToShoot;
            lastShot = DateTime.UtcNow;
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
