﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World
{
    internal class LevelData
    {
        TileMap TileHandler;
        ChunkHandler Chunks;

        IObjectHandler objects;

        const int TileSize = 8;
        const int ChunkSize = 16;

        const int ChunksLoadHorizontal = 1;
        const int ChunksLoadVertical = 1;

        public LevelData()
        {
            TileHandler = new TileMap(TileSize);
            Chunks = new ChunkHandler(ChunkSize, TileSize);

            objects = ObjectHandler.Instance;
        }

        public Vector2 PositionToTilePosition(Vector2 Position) => Position / TileSize;

        //Generation Scripts
        public void GenerateRectangle(int tileX, int tileY, int tileW, int tileH)
        {
            Chunks.PlaceSolid(new Vector2(tileX, tileY), new Vector2(tileW, tileH));
        }

        public void DecideChunksActive(Vector2 CameraPosition)
        {
            var chunkPlayerIn = Chunks.GetChunkId(PositionToTilePosition(CameraPosition));

            //Update All Active Chunks
            Chunks.ActivateAllChunks(chunkPlayerIn, new Vector2(ChunksLoadHorizontal, ChunksLoadVertical));
        }

        public void DrawActiveChunks(SpriteBatch spriteBatch)
        {
            Chunks.DrawActiveChunks(spriteBatch);
        }

        //
        //
        //

        public void GenerateWorld()
        {
            //TODO: Do it
            GenerateRectangle(8, 8, 12, 12);

        }

    }
}
