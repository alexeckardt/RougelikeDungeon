﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougelikeDungeon.Objects.Collision;
using RougelikeDungeon.World.Level;

namespace RougelikeDungeon.Objects
{
    internal class GenericSolid : Solid
    {
        public GenericSolid(Vector2 StartPosition)
        {
            Collider = new CollisionBox(StartPosition, 8, 8);
            Position = StartPosition; //Move to Correct Position
        }

        public GenericSolid(Vector2 StartPosition, Vector2 TilesScale)
        {
            Collider = new CollisionBox(StartPosition, TilesScale.X*8, TilesScale.Y*8);
            Position = Collider.Position; //Move to Correct Position

        }

        public override void Initalize()
        {
            base.Initalize();
            Collider.Enable(this);
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = GameConstants.Instance.Pixel;
            base.LoadContent(content);
        }

        public override void Update(ILevelDataInstanceExposure level, GameTime gameTime)
        {
            base.Update(level, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Collider.Draw(spriteBatch);
            //base.Draw(spriteBatch);
        }
    }
}
