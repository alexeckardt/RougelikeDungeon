using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Guns;
using RougelikeDungeon.Objects.Collision;
using RougelikeDungeon.Objects.Guns;
using RougelikeDungeon.Objects.PlayerObjects;
using RougelikeDungeon.Utilities;
using RougelikeDungeon.World;
using RougelikeDungeon.World.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects.EnemyObjects
{
    internal class Enemy : GameObject
    {
        //
        public Vector2 Velocity;
        public float CollisionStep = 0.25f;

        //Empty Constructor
        public Enemy() { }

        //Regular Creation
        public Enemy(Vector2 StartPosition)
        {
            Position = StartPosition;
            Velocity = Vector2.Zero;
        }

        //
        //
        //

        public override void Initalize()
        {
            base.Initalize();
        }

        public override void LoadContent(ContentManager content)
        {
            //ForNow
            Sprite = content.Load<Texture2D>("player/player");

            SpriteOffset = new Vector2(Sprite.Width / 2, 5);

            Collider = new CollisionBox();
            Collider.Position = Position;
            Collider.Size = new Vector2(Sprite.Width, Sprite.Height);
            Collider.Offset = SpriteOffset;
            Collider.Enable(this);

            base.LoadContent(content);
        }

        public override void Update(ILevelDataInstanceExposure level, GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Collision
            Vector2 MoveVel = DoCollision(Velocity * time, level);

            //Add To Position
            Position += MoveVel;

            //Update Base
            base.Update(level, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw My Position
            spriteBatch.Draw(GameConstants.Instance.Pixel, Position, Color.Aqua);

            //Collision Box
            Collider.Draw(spriteBatch);

            //Draw Me
            base.Draw(spriteBatch);
        }

        private Vector2 DoCollision(Vector2 MoveVel, ILevelDataInstanceExposure level)
        {
            Solid collider = level.CheckSolidCollision((CollisionBox) Collider, MoveVel);
            if (collider != null) //Collision Hit
            {
                Vector2 stepVelocity = MoveVel.Signed() * CollisionStep;
                Vector2 stepVelocityY = new Vector2(0, stepVelocity.Y);
                Vector2 stepVelocityX = new Vector2(stepVelocity.X, 0);

                //Move Y
                if (stepVelocity.Y != 0)
                {
                    for (float i = CollisionStep; i <= 1f; i += CollisionStep)
                    {
                        var subStepCollidedSolid = level.CheckSolidCollision((CollisionBox)Collider, stepVelocityY);
                        bool freeMove = subStepCollidedSolid == null;

                        if (freeMove)
                        {
                            Position += stepVelocityY;
                            Collider.Position += stepVelocityY;
                        }
                        else
                        {
                            break;
                        }
                    }

                    Velocity.Y = 0;
                    MoveVel.Y = 0;
                }

                //Move X
                if (stepVelocity.X != 0)
                {
                    for (float i = CollisionStep; i <= 1f; i += CollisionStep)
                    {
                        var subStepCollidedSolid = level.CheckSolidCollision((CollisionBox)Collider, stepVelocityX);
                        bool freeMove = subStepCollidedSolid == null;

                        if (freeMove)
                        {
                            Position += stepVelocityX;
                            Collider.Position += stepVelocityX;
                        }
                        else
                        {
                            break;
                        }
                    }

                    Velocity.X = 0;
                    MoveVel.X = 0;
                }

                //These are done seperately to avoid corner clipping
            }

            return MoveVel;
        }
    }
}
