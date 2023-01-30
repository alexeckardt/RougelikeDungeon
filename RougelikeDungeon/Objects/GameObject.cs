using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Objects.Collision;
using System;
using System.Collections.Generic;

namespace RougelikeDungeon.Objects
{
    internal class GameObject : IGameObject
    {
        //Sprite Data
        protected Texture2D Sprite;
        protected Vector2 SpriteOffset = Vector2.Zero; //generally should be constant

        //Visual
        public Vector2 SpawnPosition = Vector2.Zero;
        public Vector2 Position = Vector2.Zero;
        public Vector2 LastPosition = Vector2.Zero;

        public Color Tint = Color.White;
        public float Scale = 1.0f;
        public float Rotation = 0f;
        
        //Game Object
        public float Depth = 0.5f;
        public bool Active = true;
        public bool MarkedToRemove = false;

        //Collision
        public ICollideable Collider;

        public GameObject() { }

        public virtual void Initalize() 
        {
            SpawnPosition = Position;
        }

        public virtual void LoadContent(ContentManager content) 
        {
            //Default Collision Box : Cover Whole Sprite
            if (Collider == null)
            {
                Collider = new CollisionBox();

                if (Sprite != null)
                {
                    Collider.Position = Position;
                    Collider.Size = new Vector2(Sprite.Width, Sprite.Height);
                }
            }
        }

        public virtual void Update(ObjectHandler objects, GameTime gameTime) 
        {
            //Update My Collision Box's Position
            Collider.Position = Position;

            //
            LastPosition = Position;
        }

        public virtual void Draw(SpriteBatch spriteBatch) {

            //Default Behaviour
            if (Sprite != null && Active)
            {
                spriteBatch.Draw(Sprite, Position, null, Tint, Rotation, SpriteOffset, Scale, SpriteEffects.None, Depth);
            }
        }

        protected void SetSpriteCenteredOrigin()
        {
            if (Sprite == null) return;
            SpriteOffset = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
        }

        //Set To Destroy
        public virtual void DestroySelf(ObjectHandler objects)
        {
            if (MarkedToRemove) return;

            //Remove
            objects.RemoveObject(this);
            MarkedToRemove = true;
        }

        //Clean Up Resources -- Called upon Remoevall
        public virtual void CleanUp() { }
    }
}
