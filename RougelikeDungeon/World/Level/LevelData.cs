using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Collision;
using RougelikeDungeon.World.Chunks;
using System;

namespace RougelikeDungeon.World.Level
{
    internal class LevelData : ILevelCollisions, ILevelInstanceContainer, ILevelDataInstanceExposure, ILevelGeneration, ILevelCollisionHandler
    {
        TileMap TileHandler;

        ChunkHandler chunkHandler;
        ObjectHandler objects;
        CollisionHandler collisionHandler;

        const int TileSize = 8;
        const int ChunkSize = 16;

        const int ChunksLoadHorizontal = 5;
        const int ChunksLoadVertical = 3;

        public static LevelData inst;
        public static LevelData Instance
        {
            get
            {
                inst ??= new LevelData();
                return inst;
            }
        }
        public static LevelData OverrideInstance
        {
            get
            {
                inst = new LevelData();
                return inst;
            }
        }

        public ChunkHandler ChunkHandler
        {
            get => chunkHandler;
        }

        public ObjectHandler Objects
        {
            get => objects;
        }

        //
        //
        //

        private LevelData()
        {
            TileHandler = new TileMap(TileSize);
            chunkHandler = new ChunkHandler(ChunkSize, TileSize);

            objects = new ObjectHandler();
            collisionHandler = new();
        }

        public Vector2 PositionToTilePosition(Vector2 Position) => Position / TileSize;

        //Generation Scripts
        public void GenerateRectangle(int tileX, int tileY, int tileW, int tileH)
        {
            chunkHandler.PlaceSolid(new Vector2(tileX, tileY), new Vector2(tileW, tileH));
        }

        public void DecideChunksActive(Vector2 CameraPosition)
        {
            var chunkPlayerIn = chunkHandler.GetChunkId(PositionToTilePosition(CameraPosition));

            //Update All Active Chunks
            chunkHandler.ActivateAllChunks(chunkPlayerIn, new Vector2(ChunksLoadHorizontal, ChunksLoadVertical));
        }

        //
        // Instance Handeling
        //

        public void AddObject(GameObject obj)
        {
            objects.AddObject(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            objects.RemoveObject(obj);
        }

        public void LoadObjects(ContentManager content)
        {
            objects.LoadObjects(content);

            //Load Chunks
            chunkHandler.LoadActiveChunks(content);
        }

        public void UpdateObjects(ContentManager content, GameTime time)
        {
            objects.UpdateObjects(content, time, this);

            //Update Chunk Instances
            chunkHandler.UpdateActiveChunks(content, time);
        }

        public void DrawObjects(SpriteBatch spriteBatch)
        {
            objects.DrawObjects(spriteBatch);

            //Update Chunk Instances
            chunkHandler.DrawActiveChunks(spriteBatch);
        }

        //
        // Collision
        //

        public Solid CheckSolidCollision(ICollideable input) => collisionHandler.CheckSolidCollision(input, this);

        public Solid CheckSolidCollision(ICollideable input, Vector2 boxOffsetPosition) => collisionHandler.CheckSolidCollision(input, boxOffsetPosition, this);

        public Solid CheckSolidPosition(Vector2 input) => collisionHandler.CheckSolidPosition(input, this);

        public IGameObject CheckCollision(ICollideable input) => collisionHandler.CheckCollision(input, this);

        public IGameObject CheckCollisionWith(ICollideable input, Type type) => collisionHandler.CheckCollisionWith(input, type, this);


        //
        // Generation
        //

        public void GenerateWorld()
        {
            //TODO: Do it
            GenerateRectangle(8, 8, 12, 12);
        }
    }
}
