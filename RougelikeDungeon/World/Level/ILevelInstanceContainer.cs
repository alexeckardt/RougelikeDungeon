using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RougelikeDungeon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RougelikeDungeon.World.Level
{
    internal interface ILevelInstanceContainer
    {
        public void AddObject(GameObject obj);
        public void RemoveObject(GameObject obj);
        public void LoadObjects(ContentManager content);
        public void UpdateObjects(ContentManager content, GameTime time);
        public void DrawObjects(SpriteBatch spriteBatch);

    }
}
