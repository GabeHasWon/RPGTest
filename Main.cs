using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGTest.ID;
using RPGTest.Monsters;
using RPGTest.UI;
using RPGTest.World;
using System;

namespace RPGTest
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Battle currentBattle;
        public int frames = 0;
        public static Point screenSize = new Point(800, 480);
        public TextUI text = new TextUI();
        public Player player = new Player();
        public static Random rand = new Random();
        public static Vector2 worldOffset = new Vector2(0, 0);

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "My Crappy Pokemon Knockoff Game";
            Tile.SetTiles();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Loader.Load(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (screenSize != Window.ClientBounds.Size)
                screenSize = Window.ClientBounds.Size;
            frames++;
            Areas.SetArea(0);
            player.Update();
            if (currentBattle != null)
                currentBattle.Update();
            CheckValidBattle();
        }

        public void CheckValidBattle()
        {
            if (currentBattle != null)
            {
                if (currentBattle.finished) currentBattle = null;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (currentBattle != null)
                GraphicsDevice.Clear(Color.CornflowerBlue);
            else
                GraphicsDevice.Clear(Color.Green);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.PointClamp);
            if (currentBattle != null)
            {
                currentBattle.Draw();
            }
            if (currentBattle == null)
            {
                Tile.DrawAll();
                spriteBatch.Draw(Loader.playerSprites[0], (player.position + worldOffset) * 30, null, Color.White, 0f, new Vector2(), 3f, SpriteEffects.None, 1f);
                spriteBatch.Draw(Loader.uiSprites[5], new Vector2(30, 0), null, Color.White, 0f, new Vector2(), 3f, SpriteEffects.None, 1f);
                Areas.name.Draw(new Vector2(46, 40), 3f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
