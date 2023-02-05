using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World
{
    internal class ChunkHandler
    {
        Dictionary<Vector2, Chunk> ChunkMap;
        List<Vector2> ActiveChunkId;


        int ChunkSize;
        int TileSize;

        public ChunkHandler(int chunkWidth, int tileSize)
        {
            ChunkMap = new();
            ActiveChunkId = new();

            ChunkSize = chunkWidth;
            TileSize = tileSize;
        }

        public Vector2 GetChunkId(Vector2 TilePosition) => (TilePosition / ChunkSize).Floored();
        public Vector2 GetInChunkPosition(Vector2 TilePosition) => (new Vector2(TilePosition.X % ChunkSize, TilePosition.Y % ChunkSize));
        public Chunk GetChunk(Vector2 ChunkId)
        {
            //Singleton Esk
            if (ChunkMap.ContainsKey(ChunkId))
                return ChunkMap[ChunkId];

            //Create a new Chunk
            Chunk myChunk = new Chunk(ChunkId, TileSize, ChunkSize * TileSize);
            ChunkMap[ChunkId] = myChunk;

            return myChunk;
        }

        //
        //

        public void ActivateAllChunks() { } //bad

        public void ActivateAllChunks(Vector2 FromCenter, Vector2 SurroundingBox)
        {
            //Reset
            ActiveChunkId.Clear();

            //Offset
            var w = (int) SurroundingBox.X / 2;
            var h = (int) SurroundingBox.Y / 2;

            //Enable
            for (int i = 0; i < SurroundingBox.X; i++)
            {
                for (int j = 0; j < SurroundingBox.Y; j++)
                {
                    ActiveChunkId.Add(FromCenter + new Vector2(i-w, j-h));
                }
            }
        }

        //
        // Drawing
        //
        public void DrawActiveChunks(SpriteBatch spriteBatch)
        {
            foreach (Vector2 chunkId in ActiveChunkId)
            {
                Chunk chunkDrawing = GetChunk(chunkId);

                chunkDrawing.DrawBorder(spriteBatch);
                //chunkDrawing.DrawTiles();
            }
        }

        //
        // Generation
        //

        public void PlaceSolid(Vector2 TilePosition, Vector2 TileSize)
        {
            //Place in a chunk
            int tileWLeft = (int) TileSize.X;
            int tileHLeft = (int) TileSize.Y;

            while (tileWLeft > 0 || tileHLeft > 0)
            {
                Vector2 ChunkPosition = GetInChunkPosition(TilePosition);

                int placementWidth = Math.Min(ChunkSize - (int)ChunkPosition.X, tileWLeft);
                tileWLeft -= placementWidth;

                int placementHeight = Math.Min(ChunkSize - (int)ChunkPosition.Y, tileHLeft);
                tileHLeft -= placementHeight;

                var tilePlacementSize = new Vector2(placementWidth, placementHeight);

                Chunk chunkPlacingIn = GetChunk(GetChunkId(TilePosition));
                chunkPlacingIn.PlaceSolid(ChunkPosition, tilePlacementSize);

                //Itterate
                TilePosition += tilePlacementSize;
            }
        }
    }
}
