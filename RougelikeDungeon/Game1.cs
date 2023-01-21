﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Packets;
using RougelikeDungeon.Utilities;
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
            Resolution.Init(ref _graphics);
            Resolution.SetVirtualResolution(320, 180); //Camera Screen Width

            Resolution.SetResolution(960, 540, false); //Window Size
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
                    Resolution.getTransformationMatrix());

                //Draw
                DrawObjects();

            //End
            this._spriteBatch.End();

            //Pass SpriteBatch Into GUI

            //Draw
            base.Draw(gameTime);
        }

        public void LoadLevel()
        {
            objects.Add(new Player(new Vector2(30, 30)));

            
            
            objects.Add(new GenericSolid(new Vector2(12, 12), new Vector2(4, -1)));

            LoadObjects();
        }

        public void LoadObjects()
        {
            GlobalTextures.Instance.LoadContent(this.Content);

            foreach (GameObject obj in objects.AsList()) {
                obj.Initalize();
                obj.LoadContent(this.Content);
            }
        }

        public void UpdateObjects(GameTime time)
        {
            foreach (GameObject obj in objects.AsList())
            {
                obj.Update(objects, time);
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