using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Bullets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Guns.Bullets
{
    internal class BaseBulletSpec : IBulletSpec
    {
        public BaseBulletSpec() { }

        public float Speed => 10f;

        public float Range => 100f;

        public float DamageAmount => 2f;

        public float PenatrationForce => 10f;

        public IBulletSpec BulletSpawnOnDeath => null;

        public IBulletSpec Core => this;

        public GameObject GenerateInstance()
        {
            return new GenericBullet();
        }
    }
}
