using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RPGTest.ID;

namespace RPGTest
{
    public class Loader
    {
        public static Texture2D[] monsterSprites = new Texture2D[MonsterID.Count];
        public static Texture2D[] uiSprites = new Texture2D[8];
        public static Texture2D[] animSprites = new Texture2D[10];
        public static Texture2D[] tileSprites = new Texture2D[5];
        public static Texture2D[] playerSprites = new Texture2D[1];
        public static SpriteFont font;
        public static Effect monsterShaders;

        public static void Load(ContentManager content)
        {
            for (int i = 0; i < MonsterID.Count; ++i)
                monsterSprites[i] = content.Load<Texture2D>("Monsters/Monster_" + i);
            for (int i = 0; i < 1; ++i)
                animSprites[i] = content.Load<Texture2D>("Effects/Effect_" + i);
            for (int i = 0; i < 4; ++i)
                tileSprites[i] = content.Load<Texture2D>("World/Tile_" + i);
            uiSprites[0] = content.Load<Texture2D>("UI/HPBar");
            uiSprites[1] = content.Load<Texture2D>("UI/HealthBar");
            uiSprites[2] = content.Load<Texture2D>("UI/GroundGrass");
            uiSprites[3] = content.Load<Texture2D>("UI/Font");
            uiSprites[4] = content.Load<Texture2D>("UI/TextBar");
            uiSprites[5] = content.Load<Texture2D>("UI/GrassBar");

            font = content.Load<SpriteFont>("BattleFont");

            monsterShaders = content.Load<Effect>("Effects/DefDown");

            playerSprites[0] = content.Load<Texture2D>("Player/Player_Male");
        }
    }
}