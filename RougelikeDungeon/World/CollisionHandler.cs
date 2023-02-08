using RougelikeDungeon.Objects.Collision;
using RougelikeDungeon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RougelikeDungeon.World.Chunks;
using RougelikeDungeon.World.Level;
using System.Security.Cryptography;

namespace RougelikeDungeon.World
{
    internal class CollisionHandler
    {
        public CollisionHandler() { }

        //Solid Collisions Occur Based On Chunks

        public Solid CheckSolidCollision(ICollideable input, ILevelCollisionHandler level) => CheckSolidCollision(input, Vector2.Zero, level);
        public Solid CheckSolidCollision(ICollideable input, Vector2 CheckPositionOffset, ILevelCollisionHandler level)
        {
            //Get All Active Chunks
            HashSet<IActiveChunk> activeChunks = level.ChunkHandler.ActiveChunks;

            //Move TestBox As Described
            input.Position += CheckPositionOffset;

            foreach (var chunk in activeChunks)
            {
                foreach (var obj in chunk.ChunkObjects)
                {
                    //Ensure Solid
                    if (!obj.IsActive) continue;
                    if (obj is not Solid) continue;

                    //Check Collision
                    if (input.Intersects(obj.Collider))
                    {
                        //Move Back Because We Moved it
                        input.Position -= CheckPositionOffset;
                        return (Solid) obj;
                    }
                }
            }

            //Move Back Because We Moved it
            input.Position -= CheckPositionOffset;
            return null;
        }

        public Solid CheckSolidPosition(Vector2 input, ILevelCollisionHandler level)
        {
            //Get All Active Chunks
            HashSet<IActiveChunk> activeChunks = level.ChunkHandler.ActiveChunks;

            foreach (var chunk in activeChunks)
            {
                foreach (var obj in chunk.ChunkObjects)
                {
                    //Ensure Solid
                    if (!obj.IsActive) continue;
                    if (obj is not Solid) continue;

                    //Check If Inside
                    if (obj.Collider.Contains(input)) return (Solid)obj;
                }
            }
            return null;
        }

        public IGameObject CheckCollision(ICollideable input, ILevelCollisionHandler level)
        {
            var activeChunks = level.ChunkHandler.ActiveChunks;
            var persistantObjects = level.Objects.AsList();

            //Look At Persistant Objects (Not Chunk Objects)
            foreach (IGameObject obj in persistantObjects)
            {
                //Ensure Active
                if (!obj.IsActive) continue;

                if (input.Intersects(obj.Collider)) return obj;

            }

            //Look At Chunk Objects
            foreach (var chunk in activeChunks)
            {
                foreach (IGameObject obj in chunk.ChunkObjects)
                {
                    //Ensure Active
                    if (!obj.IsActive) continue;

                    //Check
                    if (input.Intersects(obj.Collider)) return obj;
                }
            }

            //No Collision
            return null;
        }


        public IGameObject CheckCollisionWith(ICollideable input, Type type, ILevelCollisionHandler level)
        {
            //Get
            var activeChunks = level.ChunkHandler.ActiveChunks;
            var persistantObjects = level.Objects.AsList();

            //Look At Persistant Objects (Not Chunk Objects)
            foreach (IGameObject obj in persistantObjects)
            {
                //Ensure Active & Correct Type
                if (!obj.IsActive) continue;
                if (!obj.Collider.CreatorType.IsAssignableFrom(type)) continue;

                if (input.Intersects(obj.Collider)) return obj;
            }

            //Look At Chunk Objects
            foreach (var chunk in activeChunks)
            {
                foreach (IGameObject obj in chunk.ChunkObjects)
                {
                    //Ensure Active & Correct Type
                    if (!obj.IsActive) continue;
                    if (!obj.Collider.CreatorType.IsAssignableFrom(type)) continue;

                    //Check
                    if (input.Intersects(obj.Collider)) return obj;
                }
            }

            //No Collision
            return null;
        }
    }
}
