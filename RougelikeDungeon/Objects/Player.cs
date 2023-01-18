using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Packets;
using RougelikeDungeon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects
{
    internal class Player
    {

        public Vector2 Position;
        public Vector2 Velocity;
        public Color Tint;

        public float MoveSpeed = 1.0f;

        public Texture2D PlayerSprite;

        public Player()
        {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;

            Tint = Color.White;
        }

        public void LoadContent(ContentManager content)
        {
            PlayerSprite = content.Load<Texture2D>("player/player");
        }

        public void Update(GameTime gameTime)
        {
            //Get Time
            var time = gameTime.ElapsedGameTime.TotalSeconds;

            //Update
            var left = (float) Input.Instance.IsKeyDownInt(Keys.Left);
            var right = (float) Input.Instance.IsKeyDownInt(Keys.Right);
            var up = (float) Input.Instance.IsKeyDownInt(Keys.Up);
            var down = (float) Input.Instance.IsKeyDownInt(Keys.Down);

            //Get an Input Direction
            var inputDirection = new Vector2(right - left, down - up);

            //Normalize Input
            if (inputDirection.X != 0 && inputDirection.Y != 0)
            {
                inputDirection.Normalize();
            }

            //Move
            Position += inputDirection*(MoveSpeed);
        }

        //Get the Sprite info
        public SpriteInfo GetSpriteInfo() => new SpriteInfo(PlayerSprite, new Vector2((float) Math.Floor(Position.X), (float) Math.Floor(Position.Y)), Tint);
    }
}
