using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RougelikeDungeon.Objects.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RougelikeDungeon.World.Level;

namespace RougelikeDungeon.Objects
{
    internal interface IGameObject
    {
        public void Initalize();

        public void LoadContent(ContentManager content);

        public void Update(ILevelDataInstanceExposure level, GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch);

        public ICollideable Collider { get; set; }
        public bool IsActive { get; }
    }
}
