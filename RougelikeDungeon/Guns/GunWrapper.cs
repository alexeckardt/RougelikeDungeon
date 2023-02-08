using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RougelikeDungeon.Guns.Guns;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Bullets;
using RougelikeDungeon.Objects.Guns;
using RougelikeDungeon.World.Level;
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

        DateTime lastShot = DateTime.UtcNow;
        DateTime reloadStarted = DateTime.UtcNow;
        DateTime pulloutStarted = DateTime.UtcNow;

        //Known
        public bool Reloading 
        { 
            get 
            {
                var f = (DateTime.UtcNow - reloadStarted);
                return f.TotalMilliseconds <= Gun.ReloadMilliseconds;
            }
        }

        public bool EmptyClip { get => ShotsLeft <= 0; }

        public bool PullingOut
        {
            get
            {
                var f = (DateTime.UtcNow - pulloutStarted);
                return f.TotalMilliseconds <= Gun.PullOutMilliseconds;
            }
        }

        public bool Shootable()
        {
            bool GunExists = Gun != null;
            var timeSinceShot = (DateTime.UtcNow - lastShot);
            bool shotCoolingDown = timeSinceShot.TotalMilliseconds <= Gun.MillisecondsBetweenShots;

            return GunExists && !EmptyClip && !Reloading && !PullingOut && !shotCoolingDown;
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
            ResetMagazine();
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

        //--------------------------------------------------

        public void BeginPullout()
        {
            pulloutStarted = DateTime.UtcNow;
        }

        public void Shoot(ILevelInstanceContainer level, Vector2 SpawnPosition, Vector2 ShootDirection)
        {
            int bulletsToShoot = Math.Min(ShotsLeft, Gun.BulletsPerShot);

            for (var i = 0; i < bulletsToShoot; i++)
            {
                Bullet newBullet = MakeBulletInstance();

                //Set Properties
                newBullet.Position = SpawnPosition;
                newBullet.SetFirePosition(ShootDirection);

                //Add Instance To World
                level.AddObject(newBullet);
            }

            //Update
            //ShotsLeft -= bulletsToShoot;
            lastShot = DateTime.UtcNow;
        }

        public Bullet MakeBulletInstance()
        {
            var spec = Gun.Bullet;
            var inst = (Bullet) spec.Core.GenerateInstance();

            //Set From Wrapper
            inst.Speed = spec.Speed;
            inst.Range = spec.Range;
            inst.PenatrationForce = spec.PenatrationForce;
            inst.DamageAmount = spec.DamageAmount;

            return inst;
        }

        public void LoadContent(ContentManager content)
        {
            gun.LoadContent(content);
        }
    }
}
