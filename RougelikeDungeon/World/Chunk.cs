﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World
{
    internal class Chunk
    {
        HashSet<GameObject> chunkObjects;
        bool[,] solidTileHere; //Used for Tile Generation, Discarded After Use
        int[,] tileIds;

        Vector2 ChunkId;
        int ChunkRealWidth;

        bool ChunkLoaded = false;
        public bool IsLoaded { get => ChunkLoaded; }

        public Chunk(Vector2 chunkId, int tilePerAxis, int chunkRealWorldWidth)
        {
            ChunkId = chunkId.Floored();
            chunkObjects = new();

            solidTileHere = new bool[tilePerAxis, tilePerAxis];
            tileIds = new int[tilePerAxis, tilePerAxis];
            ChunkRealWidth = chunkRealWorldWidth;
        }

        public Vector2 RealPosition(Vector2 InChunkPosition) => InChunkPosition + ChunkId * ChunkRealWidth;

        //
        //
        //

        public void PlaceSolid(Vector2 InChunkPosition, Vector2 TileWidth)
        {
            //TODO: Optimize, don't allow overlap

            GameObject solid = new GenericSolid(RealPosition(InChunkPosition), TileWidth);
            chunkObjects.Add(solid);

            //Update
            for (int i = (int) InChunkPosition.X; i < InChunkPosition.X + TileWidth.X; i++)
            {
                for (int j = (int)InChunkPosition.Y; j < InChunkPosition.Y + TileWidth.Y; j++)
                {
                    solidTileHere[i, j] = true;
                }
            }
        }

        //
        // Loading Chunk Instances
        //

        public void LoadChunkInstances(ContentManager content)
        {
            //Load All
            foreach (GameObject obj in chunkObjects)
            {
                if (obj.Loaded)
                {
                    continue;
                }

                obj.Initalize();
                obj.LoadContent(content);
            }

            ChunkLoaded = true;
        }

        public void UpdateChunkInstances(ContentManager content, GameTime time)
        {
            //Load All
            foreach (GameObject obj in chunkObjects)
            {
                if (!obj.Loaded)
                {
                    obj.LoadContent(content);
                }

                obj.Update((ObjectHandler) ObjectHandler.Instance, time);
            }
        }

        //
        // Draw
        //

        public void DrawBorder(SpriteBatch spriteBatch)
        {
            //Draw Border
            spriteBatch.Draw(GameConstants.Instance.Pixel, ChunkId*ChunkRealWidth, null, Color.Yellow, 0f, Vector2.Zero, new Vector2(ChunkRealWidth, 1), SpriteEffects.None, .999f);
            spriteBatch.Draw(GameConstants.Instance.Pixel, ChunkId*ChunkRealWidth, null, Color.Yellow, 0f, Vector2.Zero, new Vector2(1, ChunkRealWidth), SpriteEffects.None, .999f);
            spriteBatch.Draw(GameConstants.Instance.Pixel, (ChunkId + new Vector2(0, 1))*ChunkRealWidth, null, Color.Yellow, 0f, Vector2.Zero, new Vector2(ChunkRealWidth, 1), SpriteEffects.None, .999f);
            spriteBatch.Draw(GameConstants.Instance.Pixel, (ChunkId + new Vector2(1, 0))*ChunkRealWidth, null, Color.Yellow, 0f, Vector2.Zero, new Vector2(1, ChunkRealWidth), SpriteEffects.None, .999f);
        }

        public void Draw(SpriteBatch spriteBatch, TileMap tileset)
        {
            DrawChunkInstances(spriteBatch);

            //Draw Tiles
        }

        public void DrawChunkInstances(SpriteBatch spriteBatch)
        {
            //Load All
            foreach (GameObject obj in chunkObjects)
            {
                if (obj.Loaded)
                {
                    obj.Draw(spriteBatch);
                }
            }
        }

    }
}
