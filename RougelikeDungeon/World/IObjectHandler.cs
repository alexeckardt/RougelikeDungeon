using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.Objects;
using RougelikeDungeon.Objects.Collision;
using RougelikeDungeon.World.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.World
{
    internal interface IObjectHandler
    {
        public void AddObject(GameObject newObject);
        public void RemoveObject(GameObject newObject);

        public void LoadObjects(ContentManager Content);
        public void UpdateObjects(ContentManager Content, GameTime time, ILevelDataInstanceExposure level);
        public void DrawObjects(SpriteBatch spriteBatch);
    }
}
