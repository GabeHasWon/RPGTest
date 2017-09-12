using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RPGTest.ID;
using RPGTest.Monsters;
using RPGTest.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest
{
    public class Player
    {
        public Vector2 position = new Vector2(12.2f, 8.8f);
        public Monster monster = Monster.GetMonster(4, MonsterID.Draftlion);
        public Rectangle bounds = new Rectangle();
        private bool moving = false;
        private Keys lastKey = Keys.None;

        public Player()
        {
            bounds = new Rectangle(new Point(0, 0), new Point(20, 20));
        }

        public void Update()
        {
            bounds.Location = ((position + Main.worldOffset) * 30).ToPoint();
            moving = false;
            if (Main.currentBattle == null)
            {
                Move();
                if (KeyDown(Keys.S) || KeyDown(Keys.W) || KeyDown(Keys.A) || KeyDown(Keys.D))
                    moving = true;
            }
            TileCollide();
        }

        public void Move()
        {
            if (KeyDown(Keys.S))
            {
                position.Y += 0.05f;
                Main.worldOffset.Y -= 0.05f;
                lastKey = Keys.S;
            }
            if (KeyDown(Keys.W))
            {
                position.Y -= 0.05f;
                Main.worldOffset.Y += 0.05f;
                lastKey = Keys.W;
            }
            if (KeyDown(Keys.A))
            {
                position.X -= 0.05f;
                Main.worldOffset.X += 0.05f;
                lastKey = Keys.A;
            }
            if (KeyDown(Keys.D))
            {
                position.X += 0.05f;
                Main.worldOffset.X -= 0.05f;
                lastKey = Keys.D;
            }
        }

        public void TileCollide()
        {
            for (int i = 0; i < Tile.tiles.Length; ++i)
            {
                if (Tile.tiles[i].active)
                {
                    if (Tile.tiles[i].type == 2 && Main.rand.Next(300) == 0 && Main.currentBattle == null && bounds.Intersects(Tile.tiles[i].bounds) && moving)
                    {
                        int level = Areas.level - (Main.rand.Next(Areas.range) * Main.rand.Next(2) == 0 ? 1 : -1);
                        Main.currentBattle = new Battle(monster, Monster.GetMonster(level, Areas.encounters[Main.rand.Next(Areas.encounters.Count)]));
                    }
                    if (Tile.blockSolid[Tile.tiles[i].type] && Main.currentBattle == null && bounds.Intersects(Tile.tiles[i].bounds))
                    {
                        if (lastKey == Keys.W)
                        {
                            Main.worldOffset.Y -= 0.05f;
                            position.Y += 0.05f;
                        }
                        if (lastKey == Keys.S)
                        {
                            Main.worldOffset.Y += 0.05f;
                            position.Y -= 0.05f;
                        }
                        if (lastKey == Keys.A)
                        {
                            Main.worldOffset.X -= 0.05f;
                            position.X += 0.05f;
                        }
                        if (lastKey == Keys.D)
                        {
                            Main.worldOffset.X += 0.05f;
                            position.X -= 0.05f;
                        }
                    }
                }
            }
        }

        public bool KeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
    }
}
