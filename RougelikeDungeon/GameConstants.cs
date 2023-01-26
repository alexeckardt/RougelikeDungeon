using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RougelikeDungeon.Guns;
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
                instance ??= new GameConstants();
                return instance;
            }
        }

        //Vars
        public Texture2D Pixel;

        public float ActionDepth = 0.5f;

        public float BulletDepth = 0.2f;

        public Color EnemyBulletColor = new(0xff0000);

        public Dictionary<GunRarity, Color> RarityColorMap;

        public Color GetRarityColour(GunRarity rarity) => RarityColorMap.GetValueOrDefault(rarity);

        //

        private GameConstants() {

            //Setup Colour Map
            RarityColorMap = new();
            RarityColorMap.Add(GunRarity.Common, new(0xffffffff));
            RarityColorMap.Add(GunRarity.UnCommon, new(0x20ff00ff));
            RarityColorMap.Add(GunRarity.Rare, new(0x00f5ffff));
            RarityColorMap.Add(GunRarity.Epic, new(0xff00ffff));
            RarityColorMap.Add(GunRarity.Legendary, new(0xffff00ff));
        }

        public void LoadContent(ContentManager Content)
        {
            Pixel = Content.Load<Texture2D>("core/pixel");


        }

    }
}
