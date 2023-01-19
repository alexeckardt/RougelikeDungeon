﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Objects.Collision
{
    internal class CollisionBox : ICollideable
    {
        private float X;
        private float Y;
        private float Width;
        private float Height;

        private float OffsetX = 0;
        private float OffsetY = 0;

        public float Left => X - OffsetX;
        public float Top => Y - OffsetY;
        public float Right => X + Width - OffsetX;
        public float Bottom => Y + Height - OffsetY;

        public Vector2 Position
        {
            get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2 Size
        {
            get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public Vector2 Offset
        {
            get => new Vector2(OffsetX, OffsetY);
            set
            {
                OffsetX = value.X;
                OffsetY = value.Y;
            }
        }

        public Vector2 Center => new Vector2(X + Width / 2, Y + Height / 2);

        //Constructors

        public CollisionBox()
        {
        }

        public CollisionBox(float width, float height)
        {
            Width = width;
            Height = height;
        }
        
        public CollisionBox(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        
        public CollisionBox(float x, float y, float width, float height, float xoff, float yoff)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            OffsetX = xoff;
            OffsetY = yoff;
        }


        //Methods

        public bool Intersects(CollisionBox other)
        {
            if (other.Left < Right && Left < other.Right && other.Top < Bottom)
            {
                return Top < other.Bottom;
            }

            return false;
        }

        public bool Contains(Vector2 point)
        {
            if (X <= point.X && point.X <= X + Width && Y <= point.Y)
            {
                return point.Y <= Y + Height;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Behind All
            spriteBatch.Draw(GlobalTextures.Instance.Pixel, new Vector2(Left, Top), null, Color.Red, 0f, Vector2.Zero, Size, SpriteEffects.None, 1f);
        }
    }
}