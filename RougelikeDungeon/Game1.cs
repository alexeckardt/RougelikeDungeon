using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Packets;
using System.Collections.Generic;

namespace RougelikeDungeon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        GameObjects objects;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //Set Resolution
            _graphics.PreferredBackBufferWidth = 320;
            _graphics.PreferredBackBufferHeight = 180;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            objects = new GameObjects();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load Game Objects
            LoadLevel();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update Objects
            UpdateObjects(gameTime);

            //Update Game
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Clear
            GraphicsDevice.Clear(Color.Black);

            //Draw to Sprite Batch
            this._spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                DrawObjects();
            this._spriteBatch.End();

            //Draw
            base.Draw(gameTime);
        }

        public void LoadLevel()
        {
            objects.Add(new Player(new Vector2(3, 3)));

            LoadObjects();
        }

        public void LoadObjects()
        {
            foreach (GameObject obj in objects.AsList()) {
                obj.Initalize();
                obj.LoadContent(this.Content);
            }
        }

        public void UpdateObjects(GameTime time)
        {
            foreach (GameObject obj in objects.AsList())
            {
                obj.Update(objects.AsList(), time);
            }
        }

        public void DrawObjects()
        {
            foreach (GameObject obj in objects.AsList())
            {
                obj.Draw(this._spriteBatch);
            }
        }
    }
}