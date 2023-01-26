using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon
{
    internal class GameConstants
    {
        //Singleton
        public static GameConstants instance;
        public static GameConstants Instance
        {
            get
            {
                if (instance == null) instance = new GameConstants();
                return instance;
            }
        }

        //Vars
        public Texture2D Pixel;

        public float ActionDepth = 0.5f;

        public float BulletDepth = 0.2f;



        private GameConstants() { }

        public void LoadContent(ContentManager Content)
        {
            Pixel = Content.Load<Texture2D>("core/pixel");
        }

    }
}
