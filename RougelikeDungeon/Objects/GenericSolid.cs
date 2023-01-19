using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using RougelikeDungeon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects
{
    internal class GenericSolid : Solid
    {
        public GenericSolid(Vector2 StartPosition)
        {
            Position = StartPosition;
            CollisionBox = new CollisionBox(8, 8);
            CollisionBox.Position = StartPosition;
        }

        public override void Initalize()
        {
            base.Initalize();
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = GlobalTextures.Instance.Pixel;
            base.LoadContent(content);
        }

        public override void Update(List<GameObject> objects, GameTime gameTime)
        {
            base.Update(objects, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
