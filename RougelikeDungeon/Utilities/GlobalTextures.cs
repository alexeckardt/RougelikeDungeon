using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon.Utilities
{
    internal class GlobalTextures
    {
        //Singleton
        public static GlobalTextures instance;
        public static GlobalTextures Instance
        {
            get
            {
                if (instance == null) instance = new GlobalTextures();
                return instance;
            }
        }

        //Vars
        public Texture2D Pixel;

        private GlobalTextures() { }

        public void LoadContent(ContentManager Content)
        {
            Pixel = Content.Load<Texture2D>("core/pixel");
        }

    }
}
