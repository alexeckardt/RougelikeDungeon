using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
    internal interface Gun
    {
        // Bullet Information

        public IBulletSpec Bullet { get; }

        public int BulletsPerShot { get; }

        public int ClipSize { get; }

        // Shooting

        public GunFireStyle Style { get; }

        public float MillisecondsBetweenShots { get; }

        public float ReloadMilliseconds { get; }

        // Cool

        public int ShotsLeftForCritical { get; }

        public float CriticalShotDamageMultiplier { get; }

        // Other Stats

        public float PullOutMilliseconds { get; }

        //Visual
        public Texture2D Sprite { get; }

        public Vector2 SpriteOffset { get; }

        public GunRarity Rarity { get; }

        public Vector2 BulletSpawnOffset { get; }


        //Decorator = Bad
        public void SetBulletSpec(IBulletSpec newSpec);

        public void LoadContent(ContentManager content);
    }
}
