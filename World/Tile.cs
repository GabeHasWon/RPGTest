using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGTest.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.World
{
    public class Tile
    {
        public static Tile[] tiles = new Tile[2000];
        public Point pos = new Point();
        public int type = 0;
        public bool solid = false;
        public bool active = false;
        public static bool[] drawBelow = new bool[] { false, false, false, true };
        public static bool[] blockSolid = new bool[] { true, false, false, false };
        public Rectangle bounds = new Rectangle(0, 0, 0, 0);

        public static int AddTile(Point pos, int type, bool solid = false)
        {
            int index = 0;
            for (int i = 0; i < tiles.Length; ++i)
            {
                if (!tiles[i].active)
                {
                    tiles[i].type = type;
                    tiles[i].solid = solid;
                    tiles[i].pos = (pos.ToVector2() * 120).ToPoint();
                    tiles[i].active = true;
                    tiles[i].bounds = new Rectangle(tiles[i].pos, new Point(120));
                    break;
                }
            }
            return index;
        }

        public static void SetTiles()
        {
            for (int i = 0; i < tiles.Length; ++i)
            {
                tiles[i] = new Tile();
                tiles[i].type = 0;
                tiles[i].solid = false;
                tiles[i].pos = new Point(0);
                tiles[i].active = false;
            }
        }

        public static void DrawAll()
        {
            for (int i = 0; i < tiles.Length; ++i)
            {
                if (tiles[i].active && drawBelow[tiles[i].type])
                {
                    Main.spriteBatch.Draw(Loader.tileSprites[tiles[i].type], tiles[i].pos.ToVector2() + (Main.worldOffset * 30), null, Color.White, 0f, new Vector2(), 3f, SpriteEffects.None, 1f);
                }
            }
            for (int i = 0; i < tiles.Length; ++i)
            {
                if (tiles[i].active && !drawBelow[tiles[i].type])
                {
                    Main.spriteBatch.Draw(Loader.tileSprites[tiles[i].type], tiles[i].pos.ToVector2() + (Main.worldOffset * 30), null, Color.White, 0f, new Vector2(), 3f, SpriteEffects.None, 1f);
                    tiles[i].bounds.Location = (tiles[i].pos.ToVector2() + (Main.worldOffset * 30)).ToPoint();
                }
            }
        }
    }
}
