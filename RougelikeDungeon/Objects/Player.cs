﻿using Microsoft.Xna.Framework;
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
    internal class Player : GameObject
    {
        public Vector2 Velocity;
        public float MoveSpeed = 1.0f;


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
            base.Initalize();
        }

        public override void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>("player/player");
            base.LoadContent(content);
        }

        public override void Update(List<GameObject> objects, GameTime gameTime)
        {
            var time = gameTime.ElapsedGameTime.TotalSeconds;
            var inputDirection = GetKeyboardInputDirection();

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

        //
        private Vector2 GetKeyboardInputDirection()
        {
            Input input = Input.Instance;

            //Update
            var left = input.IsKeyDownInt(Keys.Left);
            var right = input.IsKeyDownInt(Keys.Right);
            var up = input.IsKeyDownInt(Keys.Up);
            var down = input.IsKeyDownInt(Keys.Down);

            //Get an Input Direction
            return new Vector2(right - left, down - up);
        }
    }
}
