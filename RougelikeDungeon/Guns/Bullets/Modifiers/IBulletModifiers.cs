using RougelikeDungeon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Guns.Bullets.Decorations
{
    internal abstract class IBulletModifiers : IBulletSpec
    {

        //Some of these Modifers are flags only, Will be used on bullet creation

        //Actual Core
        protected IBulletSpec core;

        //Defining That These Need Done
        public abstract float Speed { get; }

        public abstract float Range { get; }

        public abstract float DamageAmount { get; }

        public abstract float PenatrationForce { get; }

        public abstract IBulletSpec BulletSpawnOnDeath { get; }

        public abstract IBulletSpec Core { get; }

        public GameObject GenerateInstance()
        {
            return core.GenerateInstance();
        }
    }
}
