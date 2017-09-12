using Microsoft.Xna.Framework;
using RPGTest.ID;
using RPGTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest.Monsters
{
    public class Monster
    {
        public MonsterType type;
        public MonsterType typeTwo;
        public MonsterType weakType;
        public float health = 10;
        public int statMaxHealth = 2;
        public int statHealth = 1;
        public int statAtk = 1;
        public int statDef = 1;
        public int statSpDef = 1;
        public int statSpAtk = 1;
        public int statSpeed = 1;
        public int statLuck = 1;
        public int effectiveDef = 1;
        public int effectiveAtk = 1;
        public Move[] moves = new Move[4];
        public int id = 0;
        public int level = 0;
        public Vector2 drawPos = new Vector2(0);
        public string name = "";
        public Vector2 scale = new Vector2(1f);
        public Color drawColor = Color.White;
        public int exp = 0;
        public int maxExp = 30;
        public int expGiven = 23;
        public List<Tuple<Move, int>> moveSet = new List<Tuple<Move, int>>();

        public static Monster GetMonster(int Level, int Type)
        {
            Monster mon = new Monster();
            mon.level = Level;
            mon.id = Type;
            mon.SetTypes();
            mon.SetMoves();
            mon.SetStats();
            mon.expGiven += mon.level * 4;
            return mon;
        }

        public void SetStats()
        {
            if (id == MonsterID.Pebblo)
            {
                statHealth = 55 + (int)(level * 1.4f);
                statAtk = 50 + (int)(level * 1.2f);
                statDef = 40 + (int)(level * 1.6f);
                statSpAtk = 15 + (int)(level * 01.1f);
                statSpDef = 20 + (int)(level * 1.1f);
                statSpeed = 38;
                statLuck = 50;
                name = "Pebblo";
                expGiven = 45 + (level * 37);
                maxExp = 250 + (int)(level * 57.2f);
            }
            if (id == MonsterID.Fumgi)
            {
                statHealth = 48 + (int)(level * 1.4f);
                statAtk = 50 + (int)(level * 1.3f);
                statDef = 20 + (int)(level * 1.6f);
                statSpAtk = 55 + (int)(level * 1.1f);
                statSpDef = 35 + (int)(level * 1.15f);
                statSpeed = 48 + (int)(level * 1.1f);
                statLuck = 50;
                name = "Fumgi";
                expGiven = 67 + (level * 56);
            }
            if (id == MonsterID.Tiddle)
            {
                statHealth = 35 + (int)(level * 1.2f);
                statAtk = 40 + (int)(level * 1.4f);
                statDef = 30 + (int)(level * 1.1f);
                statSpAtk = 25 + (int)(level * 1.1f);
                statSpDef = 35 + (int)(level * 1.1f);
                statSpeed = 60 + (int)(level * 1.6f);
                statLuck = 50;
                name = "Tiddle";
                expGiven = 18 + (level * 32);
            }
            if (id == MonsterID.Crepavin)
            {
                statHealth = 60 + (int)(level * 1.3f);
                statAtk = 35 + (int)(level * 1.3f);
                statDef = 30 + (int)(level * 1.6f);
                statSpAtk = 55 + (int)(level * 1.4f);
                statSpDef = 45 + (int)(level * 1.2f);
                statSpeed = 20 + (int)(level * 1.05f);
                statLuck = 50;
                name = "Crepavin";
                expGiven = 56 + (level * 57);
            }
            if (id == MonsterID.Leaflet)
            {
                statHealth = 38 + (int)(level * 1.1f);
                statAtk = 58 + (int)(level * 1.6f);
                statDef = 35 + (int)(level * 1.2f);
                statSpAtk = 65 + (int)(level * 1.6f);
                statSpDef = 40 + (int)(level * 1.1f);
                statSpeed = 50 + (int)(level * 1.3f);
                statLuck = 50;
                name = "Leaflet";
                expGiven = 43 + (level * 78);
                maxExp = 224 + (int)(level * 32f);
            }
            if (id == MonsterID.Boldur)
            {
                statHealth = 80 + (int)(level * 1.4f);
                statAtk = 95 + (int)(level * 1.2f);
                statDef = 70 + (int)(level * 1.6f);
                statSpAtk = 25 + (int)(level * 01.1f);
                statSpDef = 40 + (int)(level * 1.1f);
                statSpeed = 58;
                statLuck = 75;
                name = "Boldur";
                expGiven = 167 + (level * 145);
                maxExp = 788 + (int)(level * 101.6f);
            }
            if (id == MonsterID.Gardle)
            {
                statHealth = 50 + (int)(level * 1.1f);
                statAtk = 50 + (int)(level * 1.1f);
                statDef = 50 + (int)(level * 1.1f);
                statSpAtk = 50 + (int)(level * 1.1f);
                statSpDef = 50 + (int)(level * 1.1f);
                statSpeed = 50 + (int)(level * 1.1f);
                statLuck = 50;
                name = "Gardle";
                expGiven = 24 + (level * 30);
            }
            if (id == MonsterID.Mammound)
            {
                statHealth = 98 + (int)(level * 1.6f);
                statAtk = 21 + (int)(level * 1.2f);
                statDef = 67 + (int)(level * 1.5f);
                statSpAtk = 16 + (int)(level * 1.2f);
                statSpDef = 48 + (int)(level * 1.4f);
                statSpeed = 13 + (int)(level * 1.1f);
                statLuck = 100;
                name = "Mammound";
                expGiven = 108 + (level * 34);
            }
            if (id == MonsterID.Draftlion)
            {
                statHealth = 45 + (int)(level * 1.3f);
                statAtk = 57 + (int)(level * 1.4f);
                statDef = 23 + (int)(level * 1.1f);
                statSpAtk = 52 + (int)(level * 1.3f);
                statSpDef = 34 + (int)(level * 1.2f);
                statSpeed = 78 + (int)(level * 1.4f);
                statLuck = 50;
                name = "Draftlion";
                expGiven = 46 + (level * 24);
            }
            health = statHealth;
        }

        public void SetTypes()
        {
            if (id == MonsterID.Pebblo)
            {
                type = RockType.instance;
            }
            else if (id == MonsterID.Fumgi)
            {
                type = GrassType.instance;
            }
            else if (id == MonsterID.Tiddle)
            {
                type = WaterType.instance;
                typeTwo = BugType.instance;
            }
            else if (id == MonsterID.Crepavin)
            {
                type = WaterType.instance;
                typeTwo = GrassType.instance;
            }
            else if (id == MonsterID.Leaflet)
            {
                type = GrassType.instance;
            }
            else if (id == MonsterID.Boldur)
            {
                type = RockType.instance;
                type = GroundType.instance;
            }
            else if (id == MonsterID.Gardle)
            {
                type = BugType.instance;
            }
            else if (id == MonsterID.Mammound)
            {
                type = NormalType.instance;
            }
            else if (id == MonsterID.Draftlion)
            {
                type = FlyingType.instance;
                typeTwo = GrassType.instance;
            }
        }

        public void SetMoves()
        {
            if (id == MonsterID.Pebblo)
            {
                moves[0] = Move.GetMove(MoveID.Pound);
                moves[1] = Move.GetMove(MoveID.Guard);
                if (level >= 8)
                    moves[2] = Move.GetMove(MoveID.MudShot);
                moveSet.AddMultiple(new Tuple<Move, int>(Move.GetMove(MoveID.Pound), 1), new Tuple<Move, int>(Move.GetMove(MoveID.Guard), 1), new Tuple<Move, int>(Move.GetMove(MoveID.MudShot), 8));
            }
            if (id == MonsterID.Fumgi)
            {
                moves[0] = Move.GetMove(MoveID.Absorb);
                moveSet.Add(new Tuple<Move, int>(Move.GetMove(MoveID.Absorb), 1));
            }
            if (id == MonsterID.Tiddle)
            {
                moves[0] = Move.GetMove(MoveID.BugBite);
                if (level > 9)
                    moves[1] = Move.GetMove(MoveID.MudShot);
                moveSet.AddMultiple(new Tuple<Move, int>(Move.GetMove(MoveID.BugBite), 1), new Tuple<Move, int>(Move.GetMove(MoveID.MudShot), 10));
            }
            if (id == MonsterID.Crepavin)
            {
                moves[0] = Move.GetMove(MoveID.Pound);
                if (level > 4)
                    moves[1] = Move.GetMove(MoveID.Growth);
                moveSet.AddMultiple(new Tuple<Move, int>(Move.GetMove(MoveID.Pound), 1), new Tuple<Move, int>(Move.GetMove(MoveID.Growth), 5));
            }
            if (id == MonsterID.Leaflet)
            {
                moves[0] = Move.GetMove(MoveID.Absorb);
                moves[1] = Move.GetMove(MoveID.Growth);
                moveSet.AddMultiple(new Tuple<Move, int>(Move.GetMove(MoveID.Absorb), 1), new Tuple<Move, int>(Move.GetMove(MoveID.Growth), 1));
            }
            if (id == MonsterID.Boldur)
            {
                moves[0] = Move.GetMove(MoveID.Pound);
                moves[1] = Move.GetMove(MoveID.Guard);
                moves[2] = Move.GetMove(MoveID.MudShot);
                moveSet.AddMultiple(new Tuple<Move, int>(Move.GetMove(MoveID.Pound), 1), new Tuple<Move, int>(Move.GetMove(MoveID.Guard), 1), new Tuple<Move, int>(Move.GetMove(MoveID.MudShot), 1));
            }
            if (id == MonsterID.Gardle)
            {
                moves[0] = Move.GetMove(MoveID.Guard);
                moveSet.Add(new Tuple<Move, int>(Move.GetMove(MoveID.Guard), 1));
            }
            if (id == MonsterID.Mammound)
            {
                moves[0] = Move.GetMove(MoveID.Pound);
                moveSet.Add(new Tuple<Move, int>(Move.GetMove(MoveID.Pound), 1));
            }
            if (id == MonsterID.Draftlion)
            {
                moves[0] = Move.GetMove(MoveID.Absorb);
                moves[1] = Move.GetMove(MoveID.Growth);
                if (level >= 15)
                moves[2] = Move.GetMove(MoveID.SkySwipe);
                moveSet.AddMultiple(new Tuple<Move, int>(Move.GetMove(MoveID.Absorb), 1), new Tuple<Move, int>(Move.GetMove(MoveID.Growth), 6), new Tuple<Move, int>(Move.GetMove(MoveID.SkySwipe), 15));
            }
        }
    }
}
