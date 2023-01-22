using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Guns.Bullets
{
    internal class BaseBullet : IBullet
    {
        public BaseBullet() { }

        public float Speed => 10f;

        public float Range => 100f;

        public float DamageAmount => 2f;

        public float PenatrationForce => 10f;

        public IBullet BulletSpawnOnDeath => null;

        public IBullet Core => this;
    }
}
