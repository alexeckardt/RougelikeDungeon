﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RougelikeDungeon.Guns.Bullets;
using RougelikeDungeon.Objects.Collision;
using System.ComponentModel.DataAnnotations;
using RougelikeDungeon.Objects.PlayerObjects;
using RougelikeDungeon.Objects.EnemyObjects;
using RougelikeDungeon.World;
using RougelikeDungeon.World.Level;

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
            Collider.Size = new Vector2(Sprite.Width / 2, Sprite.Height / 2);
            Collider.Offset = new Vector2(2, 2);
            Collider.Enable(this);

            SetSpriteCenteredOrigin();
        }

        public override void Update(ILevelDataInstanceExposure level, GameTime gameTime)
        {
            var flaggedToDestroy = false;
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Out Of Range
            if ((Position - SpawnPosition).Length() >= Range)
            {
                //Destroy Self
                flaggedToDestroy = true;
            }

            //Hit Solid
            var collidingSolid = level.CheckSolidCollision((CollisionBox)Collider);
            if (!flaggedToDestroy && collidingSolid != null)
            {
                flaggedToDestroy = true;
            }

            //Check Collision Of Enemy
            var collidedWith = level.CheckCollisionWith(Collider, typeof(Enemy));
            if (collidedWith != null)
            {
                flaggedToDestroy = true;
            }

            //Exit
            if (flaggedToDestroy)
            {
                DestroySelf(level);
                return;
            }

            //Move
            Position += MovementDirection * Speed * time;

            //Update
            base.Update(level, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
