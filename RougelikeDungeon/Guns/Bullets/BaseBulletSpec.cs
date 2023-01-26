using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Guns.Bullets
{
    internal class BaseBulletSpec : IBaseBulletSpec
    {
        public BaseBulletSpec() { }

        public float Speed => 10f;

        public float Range => 100f;

        public float DamageAmount => 2f;

        public float PenatrationForce => 10f;

        public IBaseBulletSpec BulletSpawnOnDeath => null;

        public IBaseBulletSpec Core => this;
    }
}
