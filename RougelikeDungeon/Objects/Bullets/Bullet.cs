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
    internal class Bullet : GameObject
    {

        protected Texture2D OverlaySprite;

        public override void Initalize() 
        {
            base.Initalize();
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        public override void Update(GameObjects objects, GameTime gameTime)
        {
            base.Update(objects, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var depth = GameConstants.Instance.BulletDepth;

            if (Sprite != null)
                spriteBatch.Draw(Sprite, Position, null, Color.White, Rotation, SpriteOffset, Scale, SpriteEffects.None, depth);

            if (Sprite != null)
                spriteBatch.Draw(OverlaySprite, Position, null, Tint, Rotation, SpriteOffset, Scale, SpriteEffects.None, depth);
        }
    }
}
