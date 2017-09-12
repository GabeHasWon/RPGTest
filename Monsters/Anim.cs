using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGTest.Monsters
{
    public class Anim
    {
        public Vector2 drawPos = new Vector2();
        public Point frame = new Point();
        public Point size = new Point();
        public int type = 0;
        public bool finished = false;
        private int[] counters = new int[2];
        public bool draw = false;

        public Anim(int type, Point size)
        {
            this.type = type;
            this.size = size;
            finished = false;
        }
        
        public void Animate()
        {
            if (type == 0)
            {
                counters[0]++;
                if (counters[0] % 5 == 0)
                    frame.X += size.X;
                if (counters[0] >= 25)
                {
                    finished = true;
                    frame.X = 0;
                    counters[0] = 0;
                }
            }
        }

        public void Draw(bool player, Vector2 pos)
        {
            Animate();
            if (draw)
            {
                Main.spriteBatch.Draw(Loader.animSprites[type], pos + drawPos, new Rectangle(frame, size), Color.White, 0f, new Vector2(), new Vector2(player ? 8f : 5f), SpriteEffects.None, 1f);
            }
        }
    }
}
