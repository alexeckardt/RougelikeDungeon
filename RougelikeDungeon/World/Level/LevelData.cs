using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Collision;
using RougelikeDungeon.World.Chunks;
using RougelikeDungeon.World.Generation;
using RougelikeDungeon.World.Tiles;
using System;
using System.Collections.Generic;

namespace RougelikeDungeon.World.Level
{
    internal class LevelData : ILevelCollisions, ILevelInstanceContainer, ILevelDataInstanceExposure, ILevelGeneration, ILevelCollisionHandler
    {
        ChunkHandler chunkHandler;
        ObjectHandler objects;
        CollisionHandler collisionHandler;
        LevelTileSets TileSets;
        LevelGenerator generator;

        const int TileSize = 8;
        const int ChunkSize = 16;

        const int ChunksLoadHorizontal = 5;
        const int ChunksLoadVertical = 3;

        bool HoldingSpace = false;


        public SpriteBatch spriteBatch = null;

        public static LevelData inst;
        public static LevelData Instance
        {
            get
            {
                inst ??= new LevelData(null);
                return inst;
            }
        }
        public static LevelData OverrideInstance
        {
            get
            {
                inst = new LevelData(null);
                return inst;
            }
        }

        //-----------------------------------------------------------------------

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

        private LevelData(SpriteBatch spriteBatch)
        {
            chunkHandler = new ChunkHandler(ChunkSize, TileSize);

            objects = new ObjectHandler();
            collisionHandler = new();
            generator = new(0);

            spriteBatch = null;

            TileSets = new();
        }

        //
        // Tile Set
        //

        public float AddTileSet(float atDepth, Texture2D texture)
        {
            var newSet = new TileSet(texture, TileSize);
            TileSets.Add(atDepth, newSet);

            //Passback Key
            return atDepth;
        }

        //
        // Positioning
        //

        public Vector2 PositionToTilePosition(Vector2 Position) => Position / TileSize;

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

            //Reload
            bool spaceHeld = Keyboard.GetState().IsKeyDown(Keys.Space);

            if (spaceHeld && !HoldingSpace)
            {
                if (!generator.Done)
                {
                    generator.GenerationStep();
                } else
                {
                    generator.RegenerateRooms();
                }
            }

            HoldingSpace = spaceHeld;
        }

        public void DrawObjects(SpriteBatch spriteBatch)
        {
            objects.DrawObjects(spriteBatch);

            //Update Chunk Instances
            chunkHandler.DrawActiveChunks(spriteBatch, TileSets);
            generator.Draw(spriteBatch, 8); //debug

            //
            spriteBatch.Draw(GameConstants.Instance.Pixel, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, .0f);
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

        public void GenerateWorld(ContentManager Content)
        {
            //Setup Tiles
            var floorTiles = AddTileSet(1, Content.Load<Texture2D>("tiles/basefloortiles"));
            var wallTiles = AddTileSet(0, Content.Load<Texture2D>("tiles/basetiles"));

            //Generate Rooms
            generator.GenerateRooms();

            GenerateSolids(generator);

            AutoTile(wallTiles);
        }

        public void GenerateSolids(LevelGenerator generator)
        {
            //TODO: Do it
        }

        public void GenerateRectangle(int tileX, int tileY, int tileW, int tileH)
        {
            chunkHandler.PlaceSolid(new Vector2(tileX, tileY), new Vector2(tileW, tileH));
        }

        public void PlaceTile(int tileX, int tileY, int tileId, float tileDepth)
        {
            TileData tileData = new TileData(tileId, tileDepth);

            chunkHandler.PlaceTile(tileX, tileY, tileData);
        }

        public void AutoTile(float wallTiles)
        {
            var list = ChunkHandler.ChunkIdList;
            Random rnd = new Random();

            foreach (Vector2 chunkId in list)
            {
                Chunk c = ChunkHandler.GetChunk(chunkId);
                Vector2 pos = c.TilePosition;

                //Loop Over Chunk
                for (int i = 0; i < ChunkSize; i++)
                {
                    for (int j = 0; j < ChunkSize; j++)
                    {
                        //Add
                        int tileAdd = rnd.Next(2);

                        //Decide
                        int tileId = (int) TileSurroundingIndex(chunkId, new Vector2(i, j)) + tileAdd*(int)TileTypeBaseId.Count;


                        if (tileId != 0)
                        {
                            PlaceTile(i + (int) pos.X, j + (int) pos.Y, tileId, wallTiles);
                        }
                    }
                }
            }
        }

        //only handels offsets of (-15, 15).
        private bool CheckSolidAtPosition(Vector2 ChunkPosition, Vector2 ChunkId)
        {
            int xoff = 0;
            int yoff = 0;

            if (ChunkPosition.X < 0)
                xoff = -1;
            else if (ChunkPosition.X >= ChunkSize)
                xoff = 1;

            if (ChunkPosition.Y < 0)
                yoff = -1;
            else if (ChunkPosition.Y >= ChunkSize)
                yoff = 1;

            ChunkId += new Vector2(xoff, yoff);
            ChunkPosition += new Vector2(xoff, yoff) * -ChunkSize; //offset

            Chunk c = ChunkHandler.GetChunk(ChunkId);
            return c.solidTileHere[(int) ChunkPosition.X, (int) ChunkPosition.Y];

        }

        public enum TileTypeBaseId
        {
            Empty = 0,
            Filled = 1,
            TopLeftCorner = 2,
            TopEdge = 3,
            TopRightCorner = 4,
            RightEdge = 5,
            BottomRightCorner = 6,
            BottomEdge = 7,
            BottomLeftCorner = 8,
            LeftEdge = 9,

            BottomRightCrease = 10,
            BottomLeftCrease = 11,
            TopRightCrease = 12,
            TopLeftCrease = 13,

            Count = 14
        }

        public TileTypeBaseId TileSurroundingIndex(Vector2 chunkId, Vector2 tilePosition)
        {
            //
            // ___          ___         XX_         XXX
            // ___ -> 0     XX_ -> 4    XX_ -> 9    XXX -> 11
            // ___          XX_         XX_         _XX
            //
            // XX_          ___         XXX         XXX
            // XX_ -> 6     XXX -> 3    XXX -> 1    XXX
            // ___          XXX         XXX         XX_ -> 10
            //
            // XXX          ___         XX_
            // XXX -> 7     _XX -> 2    XXX -> 12
            // ___          _XX         XXX
            //
            // _XX          _XX         _XX
            // _XX -> 8     _XX -> 5    XXX -> 13
            // ___          _XX         XXX
            //

            
            Vector2 pos = tilePosition;

            bool center = CheckSolidAtPosition(pos, chunkId);

            //Cannot Tile
            if (!center)
                return TileTypeBaseId.Empty;

            //Get the others
            // (if chunk doesn't exist a border chunk will be created -- it's empty, and isn't in the list.
                 
            //Edges
            bool up = CheckSolidAtPosition(pos + new Vector2(0, -1), chunkId);
            bool left = CheckSolidAtPosition(pos + new Vector2(-1, 0), chunkId);
            bool right = CheckSolidAtPosition(pos + new Vector2(1, 0), chunkId);
            bool down = CheckSolidAtPosition(pos + new Vector2(0, 1), chunkId);

            //Corners
            bool upleft = CheckSolidAtPosition(pos + new Vector2(-1, -1), chunkId);
            bool upRight = CheckSolidAtPosition(pos + new Vector2(1, -1), chunkId);
            bool downLeft = CheckSolidAtPosition(pos + new Vector2(-1, 1), chunkId);
            bool downRight = CheckSolidAtPosition(pos + new Vector2(1, 1), chunkId);

            //Surrouneded Tiles
            if (left && right && up && down)
            {
                if (!upleft && upRight && downLeft && downRight) return TileTypeBaseId.TopLeftCrease;
                if (upleft && !upRight && downLeft && downRight) return TileTypeBaseId.TopRightCrease;
                if (upleft && upRight && !downLeft && downRight) return TileTypeBaseId.BottomLeftCrease;
                if (upleft && upRight && downLeft && !downRight) return TileTypeBaseId.BottomRightCrease;

                return TileTypeBaseId.Filled;
            }

            //Edges
            if (left && right)
            {
                if (up) return TileTypeBaseId.BottomEdge;

                if (down) return TileTypeBaseId.TopEdge;
            }

            //Edges
            if (up && down)
            {
                if (left) return TileTypeBaseId.RightEdge;
                if (right) return TileTypeBaseId.LeftEdge;
            }

            //Corners
            if (right && down && downRight)
                return TileTypeBaseId.TopLeftCorner;

            if (left && down && downLeft)
                return TileTypeBaseId.TopRightCorner;

            if (right && up && upRight)
                return TileTypeBaseId.BottomLeftCorner;

            if (left && up && upleft)
                return TileTypeBaseId.BottomRightCorner;


            //Weird Combonation, Don't Tile
            return TileTypeBaseId.Filled;
        }
    }
}
