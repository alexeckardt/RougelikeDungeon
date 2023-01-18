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
    }
}
