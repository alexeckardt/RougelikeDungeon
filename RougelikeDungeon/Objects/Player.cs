using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Packets;
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
            var left = Input.Instance.IsKeyDown(Keys.Left);
            var right = Input.Instance.IsKeyDown(Keys.Right);
            var up = Input.Instance.IsKeyDown(Keys.Up);
            var down = Input.Instance.IsKeyDown(Keys.Down);

        }

        //Get the Sprite info
        public SpriteInfo GetSpriteInfo() => new SpriteInfo(PlayerSprite, Position, Tint);
    }
}
