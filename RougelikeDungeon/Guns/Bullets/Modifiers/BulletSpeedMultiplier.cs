using RougelikeDungeon.Guns.Bullets.Decorations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Guns.Bullets.Modifiers
{
    internal class BulletSpeedMultiplier : IBulletModifiers
    {
        private float SpeedMultiplier = 1f;

        public BulletSpeedMultiplier(float multiAmount, IBulletSpec core)
        {
            SpeedMultiplier = multiAmount;
            base.core = core;
        }

        //
        // Modifing Attributes
        //

        public override float Speed => base.core.Speed * this.SpeedMultiplier;

        public override float Range => base.core.Range;

        public override float DamageAmount => base.core.DamageAmount;

        public override float PenatrationForce => base.core.PenatrationForce;

        public override IBulletSpec BulletSpawnOnDeath => base.core.BulletSpawnOnDeath;

        public override IBulletSpec Core => base.core.Core;
    }
}
