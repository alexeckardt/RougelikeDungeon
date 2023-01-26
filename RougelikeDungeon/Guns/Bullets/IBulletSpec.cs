using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougelikeDungeon.Objects;

namespace RougelikeDungeon.Guns.Bullets
{
    internal interface IBulletSpec
    {
        public float Speed { get; }

        public float Range { get; }

        public float DamageAmount { get; }

        public float PenatrationForce { get; } //each hit enemy will lower this, wall will set to 0, at 0 it will break

        public IBulletSpec BulletSpawnOnDeath { get; }

        public IBulletSpec Core { get; }

        public GameObject GenerateInstance();
    }
}
