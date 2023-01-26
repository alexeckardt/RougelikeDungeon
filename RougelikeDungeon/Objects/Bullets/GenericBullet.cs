using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RougelikeDungeon.Guns.Bullets;
using RougelikeDungeon.Objects.Collision;

namespace RougelikeDungeon.Objects.Bullets
{
    internal class GenericBullet : Bullet
    {
        public GenericBullet() { }

        public GenericBullet(Vector2 Position)
        {
            this.Position = Position;
            this.Tint = GameConstants.Instance.EnemyBulletColor;
        }

        public override void Initalize() 
        {
            base.Initalize();
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>("bullet/basicbullet");
            OverlaySprite = content.Load<Texture2D>("bullet/basicbullet_overlay");

            Collider = new CollisionBox();
            Collider.Position = Position;
            Collider.Size = new Vector2(Sprite.Width, Sprite.Height);
        }

        public override void Update(GameObjects objects, GameTime gameTime)
        {
            base.Update(objects, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
