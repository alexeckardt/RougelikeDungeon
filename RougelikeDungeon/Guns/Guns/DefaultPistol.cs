using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.Guns;
using RougelikeDungeon.Guns.Bullets;
using RougelikeDungeon.Guns.Bullets.Decorations;
using RougelikeDungeon.Objects.Bullets;
using RougelikeDungeon.Objects.Guns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Guns.Guns
{
    internal class DefaultPistol : Gun
    {

        private Texture2D sprite;
        private IBulletSpec bullet = new BaseBulletSpec();
        private Vector2 spriteOffset;
        private Vector2 absoluteBulletSpawnOffset;

        public DefaultPistol() { }

        //

        public IBulletSpec Bullet => bullet;

        public int BulletsPerShot => 1;

        public int ClipSize => 8;

        public GunFireStyle Style => GunFireStyle.SemiAuto;

        public float MillisecondsBetweenShots => 300;

        public float ReloadMilliseconds => 1000;

        public int ShotsLeftForCritical => 2;

        public float CriticalShotDamageMultiplier => 1.5f;

        public float PullOutMilliseconds => 200;

        public Texture2D Sprite => sprite;

        public GunRarity Rarity => GunRarity.Common;

        public Vector2 BulletSpawnOffset => absoluteBulletSpawnOffset;

        //
        //
        //

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("guns/defPistol");
            spriteOffset = new Vector2(0, 3);
            absoluteBulletSpawnOffset = new Vector2(Sprite.Width, 2);
        }

        public void SetBulletSpec(IBulletSpec newSpec)
        {
            bullet = newSpec;
        }
    }
}
