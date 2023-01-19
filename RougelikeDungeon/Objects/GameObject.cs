using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Packets;
using System;
using System.Collections.Generic;

namespace RougelikeDungeon.Objects
{
    internal class GameObject
    {
        //Sprite Data
        protected Texture2D Sprite;
        protected Vector2 SpriteOffset; //generally should be constant

        //Visual
        public Vector2 Position = Vector2.Zero;
        public Color Tint = Color.White;
        public float Scale = 1.0f;
        public float Rotation = 0f;
        
        //Game Object
        public float Depth = 0.5f;
        public bool Active = true;
        public CollisionBox CollisionBox;

        public GameObject() { }

        public virtual void Initalize() { }

        public virtual void LoadContent(ContentManager content) 
        {
            //Default Collision Box : Cover Whole Sprite
            if (CollisionBox == null)
            {
                CollisionBox = new CollisionBox();

                if (Sprite != null)
                {
                    CollisionBox.Position = Position - SpriteOffset;
                    CollisionBox.Size = new Vector2(Sprite.Width, Sprite.Height);
                }
            }
        }

        public virtual void Update(List<GameObject> objects, GameTime gameTime) 
        {
            //Update My Collision Box's Position
            CollisionBox.Position = Position - SpriteOffset;
        }

        public virtual void Draw(SpriteBatch spriteBatch) {

            //Default Behaviour
            if (Sprite != null && Active)
            {
                spriteBatch.Draw(Sprite, Position, null, Tint, Rotation, SpriteOffset, Scale, SpriteEffects.None, Depth);
            }
        }

        private void SpriteCenteredOrigin()
        {
            if (Sprite == null) return;
            SpriteOffset = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }
    }
}
