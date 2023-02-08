using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.Objects;
using RougelikeDungeon.World.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Chunks
{
    internal class Chunk : IActiveChunk
    {
        HashSet<IGameObject> chunkObjects;
        bool[,] solidTileHere; //Used for Tile Generation, Discarded After Use
        int[,] tileIds;

        Vector2 ChunkId;
        int ChunkRealWidth;

        bool ChunkLoaded = false;
        public bool IsLoaded { get => ChunkLoaded; }

        public int TileWidth;
        public int TilesPerAxis;

        public Chunk(Vector2 chunkId, int tilePerAxis, int chunkRealWorldWidth)
        {
            ChunkId = chunkId.Floored();
            chunkObjects = new();

            solidTileHere = new bool[tilePerAxis, tilePerAxis];
            tileIds = new int[tilePerAxis, tilePerAxis];
            ChunkRealWidth = chunkRealWorldWidth;

            TilesPerAxis = tilePerAxis;
            TileWidth = chunkRealWorldWidth / tilePerAxis;
        }

        public Vector2 RealPosition(Vector2 InChunkPosition) => InChunkPosition*TileWidth + ChunkId*ChunkRealWidth;

        //
        //
        //

        public void PlaceSolid(Vector2 InChunkPosition, Vector2 TileWidth)
        {
            //TODO: Optimize, don't allow overlap

            GameObject solid = new GenericSolid(RealPosition(InChunkPosition), TileWidth);
            chunkObjects.Add(solid);

            //Update
            for (int i = (int)InChunkPosition.X; i < InChunkPosition.X + TileWidth.X; i++)
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

                obj.Update(LevelData.Instance, time);
            }
        }

        //
        // Draw
        //

        public void DrawBorder(SpriteBatch spriteBatch)
        {
            //Draw Border
            float borderDepth = 1f;
            spriteBatch.Draw(GameConstants.Instance.Pixel, ChunkId * ChunkRealWidth, null, Color.Yellow, 0f, Vector2.Zero, new Vector2(ChunkRealWidth, 1), SpriteEffects.None, borderDepth);
            spriteBatch.Draw(GameConstants.Instance.Pixel, ChunkId * ChunkRealWidth, null, Color.Yellow, 0f, Vector2.Zero, new Vector2(1, ChunkRealWidth), SpriteEffects.None, borderDepth);
            spriteBatch.Draw(GameConstants.Instance.Pixel, (ChunkId + new Vector2(0, 1)) * ChunkRealWidth, null, Color.Yellow, 0f, Vector2.Zero, new Vector2(ChunkRealWidth, 1), SpriteEffects.None, borderDepth);
            spriteBatch.Draw(GameConstants.Instance.Pixel, (ChunkId + new Vector2(1, 0)) * ChunkRealWidth, null, Color.Yellow, 0f, Vector2.Zero, new Vector2(1, ChunkRealWidth), SpriteEffects.None, borderDepth);
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

        //
        //
        //

        public HashSet<IGameObject> ChunkObjects { get => chunkObjects; }

    }
}
