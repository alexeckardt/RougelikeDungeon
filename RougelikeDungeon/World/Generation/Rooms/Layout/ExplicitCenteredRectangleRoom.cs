﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation.Rooms.Layout
{
    internal class ExplicitCenteredRectangleRoom : Room
    {

        private readonly int BaseWidth = 20;
        private readonly int BaseHeight = 20;

        public ExplicitCenteredRectangleRoom(Vector2 origin, DoorHolder doorHolder, int doors, int baseW, int baseH) : base(origin, doorHolder) 
        {
            DoorsToSpawnWith = doors;
            BaseWidth = baseW;
            BaseHeight = baseH;

            //Generate
            GenerateShape();
        }

        //Generate
        public override void GenerateShape()
        {
            //Calc
            int w = BaseWidth;
            int h = BaseHeight;

            //from center
            int x = (int)TileOrigin.X - w / 2;
            int y = (int)TileOrigin.Y - h / 2;

            Rectangle box = new Rectangle(x, y, w, h);

            //Create
            AddBox(box);

            //Generate Doors and Such
            base.GenerateShape();
        }
    }
}
