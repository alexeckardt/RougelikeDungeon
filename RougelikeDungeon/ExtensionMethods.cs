using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon
{
    public static class ExtensionMethods
    {
        public static Vector2 Normalized(this Vector2 v)
        {
            if (v == Vector2.Zero) return Vector2.Zero;

            float num = 1f / MathF.Sqrt(v.X * v.X + v.Y * v.Y);
            return v * num;
        }

        public static Vector2 Floored(this Vector2 v)
        {
            return new Vector2((int)v.X, (int)v.Y);
        }

        public static Vector2 FlooredNegatives(this Vector2 v)
        {
            Vector2 copy = new Vector2(v.X, v.Y);
            int sX = Math.Sign(v.X);
            int sY = Math.Sign(v.Y);

            if (sX < 0) copy.X -= 1;
            if (sY < 0) copy.Y -= 1;

            return new Vector2((int) Math.Abs(copy.X) * sX, (int) Math.Abs(copy.Y) * sY);
        }

        public static Vector2 Rounded(this Vector2 v)
        {
            return new Vector2((float) Math.Round(v.X), (float)Math.Round(v.Y));
        }

        public static Vector2 Signed(this Vector2 v)
        {
            float x = v.X;
            float y = v.Y;

            if (x < 0) x = -1;
            if (x > 0) x = 1;
            
            if (y < 0) y = -1;
            if (y > 0) y = 1;

            return new Vector2(x, y);
        }
        
        public static float Angle(this Vector2 v)
        {
            return (float) (Math.Atan2(-v.Y, -v.X));
        }

    }
}
