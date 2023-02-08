﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Bullets;
using RougelikeDungeon.Objects.EnemyObjects;
using RougelikeDungeon.Objects.PlayerObjects;
using RougelikeDungeon.Utilities;
using RougelikeDungeon.World.Level;
using System.Collections.Generic;

namespace RougelikeDungeon
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player player;
        Camera camera;

        LevelData level;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //Set Resolution
            Resolution.Init(ref _graphics);
            Resolution.SetVirtualResolution(320, 180); //Camera Screen Width

            int scale = 5;

            Resolution.SetResolution(320*scale, 180*scale, false); //Window Size
        }

        protected override void Initialize()
        {
            camera = Camera.Instance;

            level = LevelData.OverrideInstance;

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

            //Update
            Input.Instance.Update();

            if (player.Guns != null)
            {
                if (player.Guns.RequiredLoadContent)
                {
                    player.Guns.LoadContent(this.Content);
                }
            }

            //Update Chunks
            level.DecideChunksActive(camera.GetPosition());

            //Update Objects
            UpdateObjects(gameTime);

            //Update Camera
            camera.Update(player.Position, gameTime);

            //Update Game
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Clear
            GraphicsDevice.Clear(Color.Black);

            //Set Resoli
            Resolution.BeginDraw();

            //Draw to Sprite Batch
            this._spriteBatch.Begin(
                    SpriteSortMode.BackToFront, 
                    BlendState.AlphaBlend, 
                    SamplerState.PointClamp, 
                    DepthStencilState.None, 
                    RasterizerState.CullNone, 
                    null, 
                    //Resolution.getTransformationMatrix());
                    camera.GetTransformMatrix());

                //Draw
                DrawObjects();

            //End
            this._spriteBatch.End();

            //Pass SpriteBatch Into GUI, do Resolution.getTransformMatrix() for last input

            //Draw
            base.Draw(gameTime);
        }

        public void LoadLevel()
        {
            player = new Player(Vector2.Zero);
            level.AddObject(player);

            //Generate world in chunks
            level.GenerateWorld();

            //Activate necessary chunks
            level.DecideChunksActive(player.Position);

            LoadObjects();
        }

        public void LoadObjects()
        {
            GameConstants.Instance.LoadContent(this.Content);
            level.LoadObjects(this.Content);
        }

        public void UpdateObjects(GameTime time)
        {
            level.UpdateObjects(this.Content, time);
        }

        public void DrawObjects()
        {
            level.DrawObjects(this._spriteBatch);
        }
    }
}