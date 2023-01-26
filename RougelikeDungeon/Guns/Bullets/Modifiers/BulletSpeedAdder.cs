using RougelikeDungeon.Guns.Bullets.Decorations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Guns.Bullets.Modifiers
{
    internal class BulletSpeedAdder : IBulletModifiers
    {
        private float SpeedAmount = 0f;

        public BulletSpeedAdder(float addAmount, IBaseBulletSpec core)
        {
            SpeedAmount = addAmount;
            base.core = core;
        }

        //
        // Modifing Attributes
        //

        public override float Speed => base.core.Speed + this.SpeedAmount;

        public override float Range => base.core.Range;

        public override float DamageAmount => base.core.DamageAmount;

        public override float PenatrationForce => base.core.PenatrationForce;

        public override IBaseBulletSpec BulletSpawnOnDeath => base.core.BulletSpawnOnDeath;

        public override IBaseBulletSpec Core => base.core.Core;
    }
}
