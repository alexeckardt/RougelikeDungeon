using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World.Generation.Rooms.Layout
{
    internal class RandomRectangleRoom : Room
    {

        private readonly int BaseWidth = 20;
        private readonly int BaseHeight = 20;
        private readonly int WidthVariance = 5;
        private readonly int HeightVariance = 5;

        public RandomRectangleRoom(Vector2 origin, DoorHolder doorHolder, int doors, int baseW, int baseH) : base(origin, doorHolder) 
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
            bool Landscape = random.NextBool();
            
            //Calc
            int w = BaseWidth + random.NextMaxRangeRandomSign(WidthVariance);
            int h = BaseHeight + random.NextMaxRangeRandomSign(HeightVariance);

            //Switch If Want To
            int ww = (Landscape) ? w : h;
            int hh = (Landscape) ? h : w;

            //from center
            int x = (int)TileOrigin.X - ww / 2;
            int y = (int)TileOrigin.Y - hh / 2;

            Rectangle box = new Rectangle(x, y, ww, hh);

            //Create
            AddBox(box);

            //Generate Doors and Such
            base.GenerateShape();
        }
    }
}
