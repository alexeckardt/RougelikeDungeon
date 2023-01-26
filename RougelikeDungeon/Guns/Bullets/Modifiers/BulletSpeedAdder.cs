using RougelikeDungeon.Guns.Bullets.Decorations;
using RougelikeDungeon.Objects;
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

        public BulletSpeedAdder(float addAmount, IBulletSpec core)
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

        public override IBulletSpec BulletSpawnOnDeath => base.core.BulletSpawnOnDeath;

        public override IBulletSpec Core => base.core.Core;
    }
}
