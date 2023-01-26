using Microsoft.Xna.Framework;
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

        public Color EnemyBulletColor = new Color(0xff0000);

        public Color CommonRarityColor = new Color(0xffffffff);

        public Color UnCommonRarityColor = new Color(0x20ff00);

        public Color RareRarityColor = new Color(0x00f5ff);

        public Color EpicRarityColor = new Color(0xff00ff);

        public Color LegendaryRarityColor = new Color(0xffff00);



        private GameConstants() { }

        public void LoadContent(ContentManager Content)
        {
            Pixel = Content.Load<Texture2D>("core/pixel");
        }

    }
}
