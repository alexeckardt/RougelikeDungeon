using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Guns;
using RougelikeDungeon.Objects.Collision;
using RougelikeDungeon.Objects.Guns;
using RougelikeDungeon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects.PlayerObjects
{
    internal class Player : GameObject
    {
        //
        public Vector2 Velocity;
        public float MoveSpeed = 82f;
        public float MoveSlip = 25f;
        public float CollisionStep = 0.25f;

        public PlayerGuns Guns;

        //Empty Constructor
        public Player() { }

        //Regular Creation
        public Player(Vector2 StartPosition)
        {
            Position = StartPosition;
            Velocity = Vector2.Zero;
        }

        //
        //
        //

        public override void Initalize()
        {
            Guns = new PlayerGuns();

            base.Initalize();
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>("player/player");

            SpriteOffset = new Vector2(Sprite.Width/2, 5);


            Collider = new CollisionBox();
            Collider.Position = Position;
            Collider.Size = new Vector2(Sprite.Width, Sprite.Height);
            Collider.Offset = SpriteOffset;

            base.LoadContent(content);
        }

        public override void Update(GameObjects objects, GameTime gameTime)
        {
            var time = (float) gameTime.ElapsedGameTime.TotalSeconds;
            var inputDirection = GetKeyboardInputDirection().Normalized();

            //Move
            Velocity = Vector2.Lerp(Velocity, inputDirection * MoveSpeed, MoveSlip * time);

            //Collision
            Vector2 MoveVel = DoCollision(Velocity * time, objects);

            //Add To Position
            Position += MoveVel;

            TryShoot(objects);

            //Update Base
            base.Update(objects, gameTime);
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

        //
        private Vector2 GetKeyboardInputDirection()
        {
            Input input = Input.Instance;

            //Update
            var left = input.IsKeyDown(Keys.Left) || input.IsKeyDown(Keys.A) ? 1 : 0;
            var right = input.IsKeyDown(Keys.Right) || input.IsKeyDown(Keys.D) ? 1 : 0;
            var up = input.IsKeyDown(Keys.Up) || input.IsKeyDown(Keys.W) ? 1 : 0;
            var down = input.IsKeyDown(Keys.Down) || input.IsKeyDown(Keys.S) ? 1 : 0;

            //Get an Input Direction
            return new Vector2(right - left, down - up);
        }

        private Vector2 DoCollision(Vector2 MoveVel, GameObjects objects)
        {
            Solid collider = objects.CheckSolidCollision((CollisionBox)Collider, MoveVel);
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
                        var subStepCollidedSolid = objects.CheckSolidCollision((CollisionBox)Collider, stepVelocityY);
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
                        var subStepCollidedSolid = objects.CheckSolidCollision((CollisionBox)Collider, stepVelocityX);
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

        public void TryShoot(GameObjects objects)
        {

            var click = Input.Instance.MouseLeftClicked();
            GunWrapper CurrentGunHolder = Guns.CurrentGun;

            //Set Prefire Timer
            if (click)
            {
                
            }

            //
            if (click && CurrentGunHolder.Shootable)
            {
                //Get Information
                Vector2 MousePos = Input.Instance.MousePositionCamera();

                Vector2 ShootDirection = Position - MousePos;


                //Figure Out Where To Spawn Bullet
                Vector2 BulletSpawnPosition = Position + ShootDirection.Normalized()*CurrentGunHolder.Gun.BulletSpawnOffset;

    
                //Shoot Gun, Side Effects
                CurrentGunHolder.Shoot(objects, BulletSpawnPosition, ShootDirection);
            }
        }
    }
}
