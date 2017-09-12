using Microsoft.Xna.Framework;
using RPGTest.ID;
using RPGTest.UI;
using System.Collections.Generic;

namespace RPGTest.World
{
    public class Areas
    {
        public static List<int> encounters = new List<int>();
        public static int level = 5;
        public static int range = 5;
        public static TextUI name = new TextUI("Debug");

        public static void SetArea(int ID)
        {
            if (ID == 0)
                SetLevelDebug();
        }

        public static void SetLevelDebug()
        {
            encounters = new List<int>();
            encounters.AddMultiple(MonsterID.Leaflet, MonsterID.Pebblo, MonsterID.Fumgi);
            level = 6;
            range = 2;
            for (int i = -3; i < 20; i++)
            {
                for (int k = -3; k < 20; ++k)
                {
                    Point position = new Point(i, k);
                    Tile.AddTile(position, 3, false);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; ++k)
                {
                    Point position = new Point(i, k);
                    Tile.AddTile(position, 2, false);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int k = 0; k < 1; ++k)
                {
                    Point position = new Point(i + 5, k + 2);
                    Tile.AddTile(position, 2, false);
                }
            }
            Tile.AddTile(new Point(2, 2), 0, false);
            Tile.AddTile(new Point(4, 1), 0, false);
            for (int k = 0; k < 3; ++k)
            {
                for (int i = 0; i < 10; ++i)
                {
                    Tile.AddTile(new Point(-3 + k, i), 0, false);
                }
            }
            for (int k = 0; k < 3; ++k)
            {
                for (int i = 0; i < 10; ++i)
                {
                    Tile.AddTile(new Point(i, -k), 0, false);
                }
            }
        }
    }
}
