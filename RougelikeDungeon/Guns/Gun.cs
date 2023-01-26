using RougelikeDungeon.Guns;
using RougelikeDungeon.Guns.Bullets;
using RougelikeDungeon.Guns.Bullets.Decorations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects.Guns
{
    internal class Gun
    {
        // Bullet Information

        private IBulletSpec Bullet;

        public int BulletsPerShot;

        public int ClipSize;

        // Shooting

        public GunFireStyle Style;

        public float MillisecondsBetweenShots;

        public float ReloadMilliseconds;

        // Cool

        public int ShotsLeftForCritical;

        public float CriticalShotDamageMultiplier;

        //
        public IBulletSpec GetBulletSpec() => Bullet;

        //Decorator = Bad
        public void SetBulletSpec(IBulletSpec newSpec) {
            Bullet = newSpec;
        }
    }
}
