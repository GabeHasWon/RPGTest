using Microsoft.Xna.Framework;
using RPGTest.ID;
using RPGTest.Types;
using System;

namespace RPGTest.Monsters
{
    public class Move
    {
        public MonsterType type;
        public Action<Monster, Monster, bool> effect;
        public bool damaging = true;
        public bool finished = true;
        public bool special = false;
        public int baseDamage = 0;
        public string name = "";
        public int shader = -1;

        public static Move GetMove(int type)
        {
            Move move = new Move();
            if (type == MoveID.Pound)
            {
                move.name = "Pound";
                move.type = NormalType.instance;
                move.baseDamage = 40;
                bool moveBack = false;
                move.effect = (Monster user, Monster victim, bool firstUser) =>
                {
                    if (firstUser)
                    {
                        if (!moveBack)
                        {
                            user.drawPos += new Vector2(4, -4);
                            if (user.drawPos.X > 40)
                                moveBack = true;
                        }
                        else
                        {
                            user.drawPos += new Vector2(-4, 4);
                            if (user.drawPos.X < 0)
                            {
                                move.finished = true;
                                moveBack = false;
                            }
                        }
                    }
                    else
                    {
                        if (!moveBack)
                        {
                            user.drawPos += new Vector2(-4, 4);
                            if (user.drawPos.X < -40)
                                moveBack = true;
                        }
                        else
                        {
                            user.drawPos += new Vector2(4, -4);
                            if (user.drawPos.X > -4)
                            {
                                move.finished = true;
                                moveBack = false;
                            }
                        }
                    }
                };
            }
            if (type == MoveID.MudShot)
            {
                move.name = "Mudshot";
                move.type = GroundType.instance;
                move.baseDamage = 20;
                bool moveBack = false;
                move.effect = (Monster user, Monster victim, bool firstUser) =>
                {
                    if (!moveBack)
                    {
                        user.drawPos += new Vector2(5, -3);
                        user.drawColor = Color.SaddleBrown;
                        if (user.drawPos.X > 45)
                            moveBack = true;
                    }
                    if (moveBack)
                    {
                        user.drawPos -= new Vector2(5, -3);
                        if (user.drawPos.X < 5)
                        {
                            move.finished = true;
                            moveBack = false;
                            user.drawColor = Color.White;
                        }
                    }
                };
            }
            if (type == MoveID.Guard)
            {
                move.name = "Guard";
                move.type = RockType.instance;
                move.damaging = false;
                move.shader = 0;
                int timer = 0;
                move.effect = (Monster user, Monster victim, bool firstUser) =>
                {
                    timer++;
                    if (timer == 1)
                    {
                        user.effectiveDef += 6;
                        move.shader = 0;
                    }
                    if (timer > 80)
                    {
                        move.finished = true;
                        move.shader = -1;
                    }
                };
            }
            if (type == MoveID.Absorb)
            {
                move.name = "Absorb";
                move.type = GrassType.instance;
                move.damaging = true;
                move.baseDamage = 30;
                move.special = true;
                int timer = 0;
                move.effect = (Monster user, Monster victim, bool firstUser) =>
                {
                    timer++;
                    if (timer > 80)
                    {
                        move.finished = true;
                        user.health += move.CalcDamage(move, victim, user) / 2;
                    }
                };
            }
            if (type == MoveID.Growth)
            {
                move.name = "Growth";
                move.type = GrassType.instance;
                move.damaging = false;
                move.special = true;
                int timer = 0;
                move.effect = (Monster user, Monster victim, bool firstUser) =>
                {
                    timer++;
                    if (timer % 3 == 0 && timer < 20)
                        user.scale *= 1.1f;
                    if (timer % 3 == 0 && timer > 20 && timer < 40)
                        user.scale *= 0.9f;
                    if (timer > 80)
                    {
                        move.finished = true;
                        user.effectiveAtk += 5;
                        user.scale = new Vector2(1);
                    }
                };
            }
            if (type == MoveID.BugBite)
            {
                move.name = "BugBite";
                move.type = BugType.instance;
                move.damaging = true;
                move.baseDamage = 40;
                int timer = 0;
                move.effect = (Monster user, Monster victim, bool firstUser) =>
                {
                    timer++;
                    if (timer == 1)
                    {
                        Main.currentBattle.animPlace = user == Main.currentBattle.playerMonster;
                        Main.currentBattle.animation = new Anim(0, new Point(26, 23));
                        Main.currentBattle.animation.draw = true;
                    }
                    if (Main.currentBattle.animation.finished)
                    {
                        move.finished = true;
                        Main.currentBattle.animation.draw = false;
                        timer = 0;
                        return;
                    }
                };
            }
            if (type == MoveID.SkySwipe)
            {
                move.name = "Sky Swipe";
                move.type = FlyingType.instance;
                move.baseDamage = 40;
                bool moveBack = false;
                int reps = 0;
                move.effect = (Monster user, Monster victim, bool firstUser) =>
                {
                    reps++;
                    if (!moveBack)
                    {
                        if (reps < 6)
                            user.drawPos += new Vector2(6, 2);
                        else
                            user.drawPos += new Vector2(2, 6);

                        if (reps == 12)
                        {
                            moveBack = true;
                            reps = 0;
                        }
                    }
                    if (moveBack)
                    {
                        if (reps < 6)
                            user.drawPos -= new Vector2(6, 2);
                        else
                            user.drawPos -= new Vector2(2, 6);
                        if (reps == 11)
                        {
                            moveBack = false;
                            move.finished = true;
                            reps = 0;
                        }
                    }
                };
            }
            return move;
        }

        public float CalcDamage(Move attack, Monster enemy, Monster user)
        {
            int atk = user.statAtk;
            if (attack.special) atk = user.statSpAtk;
            int def = enemy.effectiveDef;
            if (attack.special) def = enemy.statSpDef;
            float damage = ((((2 * user.level) / 5) + 2) * attack.baseDamage * (atk / def)) / 50 + 2;
            if (user.type == attack.type)
                damage *= 1.5f;
            damage *= CalcModifiers(enemy, attack);
            return damage;
        }

        public float CalcModifiers(Monster enemy, Move attack)
        {
            float mod = 1f;
            if (enemy.type.WeakTo(attack.type))
                mod *= 1.5f;
            if (enemy.type.StrongTo(attack.type))
                mod *= 0.5f;
            if (attack.type.Ineffective(enemy.type))
                mod *= 0.5f;
            if (enemy.typeTwo != null)
            {
                if (enemy.typeTwo.WeakTo(attack.type))
                    mod *= 1.5f;
                if (enemy.typeTwo.StrongTo(attack.type))
                    mod *= 0.5f;
                if (attack.type.Ineffective(enemy.typeTwo))
                    mod *= 0.5f;
                if (attack.type.Effective(enemy.typeTwo))
                    mod *= 1.5f;
            }
            return mod;
        }

        public override string ToString()
        {
            return name + " - " + type;
        }
    }
}
