using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Packets;
using System.Collections.Generic;

namespace RougelikeDungeon.Objects
{
    internal class GameObject
    {
        //Sprite Data
        protected Texture2D Sprite;
        protected Vector2 SpriteOffset;

        //Visual
        public Vector2 Position;
        public Color Tint;
        public float Scale = 1.0f;
        public float Rotation = 0f;

        //Game Object
        public int Depth = 0;
        public bool Active = true;

        public GameObject() { }

        public virtual void Initalize() { }

        public virtual void LoadContent(ContentManager content) { }

        public virtual void Update(List<GameObject> objects, GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }


        private void SpriteCenteredOrigin()
        {
            if (Sprite == null) return;
            SpriteOffset = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }
    }
}
