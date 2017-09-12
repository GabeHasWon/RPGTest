using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RPGTest.Monsters;
using RPGTest.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest
{
    public class Battle
    {
        public Anim animation = null;
        public bool animPlace = false;
        public Monster playerMonster;
        public TextUI namePlayer;
        public TextUI levelPlayer;
        public Monster enemyMonster;
        public TextUI nameEnemy;
        public TextUI levelEnemy;
        public bool? decideAttack = true;
        public Move playerAttack;
        public Move enemyAttack;
        public bool enemyAttempted = false;
        public bool playerAttempted = false;
        public TextUI currentText = new TextUI();
        public TextUI lastText = new TextUI();
        public int currentShader = -1;
        public int totalRounds = -1;
        public TextUI moves;
        public TextUI movesTwo;
        public bool finished = false;
        public int deadCounter = 0;

        public Battle(Monster player, Monster enemy)
        {
            playerMonster = player;
            enemyMonster = enemy;
            playerMonster.effectiveDef = playerMonster.statDef;
            enemyMonster.effectiveDef = enemyMonster.statDef;
            playerMonster.effectiveAtk = playerMonster.statAtk;
            enemyMonster.effectiveAtk = enemyMonster.statAtk;
            currentShader = -1;
            namePlayer = new TextUI(player.name);
            nameEnemy = new TextUI(enemy.name);
            levelPlayer = new TextUI(player.level.ToString());
            levelEnemy = new TextUI(enemy.level.ToString());
            moves = new TextUI(player.moves[0].ToString() + " " + 
                (player.moves[1] != null ? player.moves[1].ToString() : "") + " ");
            movesTwo = new TextUI((player.moves[2] != null ? player.moves[2].ToString() : "") + " " +
                (player.moves[3] != null ? player.moves[3].ToString() : ""));
        }

        public void Draw()
        {
            Texture2D tex = Loader.uiSprites[2];
            Main.spriteBatch.Draw(tex, new Vector2(Main.screenSize.X - 340, 56), null, Color.White, 0f, new Vector2(), 7f, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(tex, new Vector2(10, Main.screenSize.Y - 240), null, Color.White, 0f, new Vector2(), 8f, SpriteEffects.None, 1f);
            tex = Loader.uiSprites[0];
            Main.spriteBatch.Draw(tex, new Vector2(26, 24), new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0f, new Vector2(), 3f, SpriteEffects.None, 1f);
            Main.spriteBatch.Draw(tex, new Vector2(Main.screenSize.X - 142 * 3, Main.screenSize.Y - 66 * 3), new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0f, new Vector2(), 3f, SpriteEffects.None, 1f);
            namePlayer.Draw(new Vector2(Main.screenSize.X - 138 * 3, Main.screenSize.Y - 62 * 3), namePlayer.text.Length < 8 ? 3f : 3f - (namePlayer.text.Length * 0.11f));
            nameEnemy.Draw(new Vector2(40), nameEnemy.text.Length < 8 ? 3f : 3f - (nameEnemy.text.Length * 0.11f));
            levelPlayer.Draw(new Vector2(Main.screenSize.X - 100, Main.screenSize.Y - 180), 3f);
            levelEnemy.Draw(new Vector2(340, 40), 3f);
            tex = Loader.uiSprites[1];
            DrawHealthBar(tex);
            DrawShaded(ref tex);
            animation?.Draw(animPlace, animPlace ? new Vector2(Main.screenSize.X - 300, 30) : new Vector2(40, Main.screenSize.Y - 200));
            tex = Loader.uiSprites[4];
            Main.spriteBatch.Draw(tex, new Vector2(0, Main.screenSize.Y - 100), Color.White);
            if (decideAttack == true)
            {
                moves.Draw(new Vector2(20, Main.screenSize.Y - 80), 3f);
                movesTwo.Draw(new Vector2(20, Main.screenSize.Y - 40), 3f);
            }
            else
            {
                currentText.Draw(new Vector2(20, Main.screenSize.Y - 80), 2f);
                lastText.Draw(new Vector2(20, Main.screenSize.Y - 40), 2f);
            }
        }

        public void DrawHealthBar(Texture2D tex)
        {
            float reps = enemyMonster.health / enemyMonster.statHealth;
            for (float i = 0; i < reps; i += 0.0062f)
            {
                Main.spriteBatch.Draw(tex, new Vector2(264 + (i * 160), 84), Color.Green);
            }
            for (float i = 0; i < playerMonster.health / playerMonster.statHealth; i += 0.0062f)
            {
                Main.spriteBatch.Draw(tex, new Vector2(Main.screenSize.X - 186 + (i * 160), Main.screenSize.Y - 136), Color.Green);
            }
        }

        public void DrawShaded(ref Texture2D tex)
        {
            if (currentShader >= 0)
                Loader.monsterShaders.CurrentTechnique.Passes[currentShader].Apply();
            tex = Loader.monsterSprites[playerMonster.id];
            Main.spriteBatch.Draw(tex, new Vector2(60, Main.screenSize.Y - (tex.Height * 12f)) + playerMonster.drawPos, new Rectangle(tex.Width / 2 + 1, 0, tex.Width / 2, tex.Height), playerMonster.drawColor, 0f, new Vector2(0), new Vector2(8f) + playerMonster.scale, SpriteEffects.None, 1f);
            if (currentShader >= 0)
                Loader.monsterShaders.CurrentTechnique.Passes[currentShader].Apply();
            tex = Loader.monsterSprites[enemyMonster.id];
            Main.spriteBatch.Draw(tex, new Vector2(Main.screenSize.X - ((tex.Width / 2) * 9f), 60) + enemyMonster.drawPos, new Rectangle(0, 0, tex.Width / 2, tex.Height), enemyMonster.drawColor, 0f, new Vector2(0), new Vector2(5f) + enemyMonster.scale, SpriteEffects.None, 1f);
        }

        public void Update()
        {
            if (playerMonster.health < 1)
            {
                decideAttack = null;
                lastText.text = "";
                currentText.text = playerMonster.name + " has fainted.";
                deadCounter++;
                if (deadCounter > 100)
                {
                    finished = true;
                    playerMonster.health = playerMonster.statHealth;
                }
            }

            if (enemyMonster.health < 1)
            {
                decideAttack = null;
                lastText.text = "";
                if (deadCounter < 100)
                    currentText.text = enemyMonster.name + " has fainted.";
                deadCounter++;
                if (deadCounter == 100)
                {
                    playerMonster.exp += enemyMonster.expGiven;
                    currentText.text = playerMonster.name + " has gained " + enemyMonster.expGiven + " experience!";
                }
                if (deadCounter == 200 && playerMonster.exp > playerMonster.maxExp)
                {
                    playerMonster.level++;
                    playerMonster.exp -= playerMonster.maxExp;
                    levelPlayer.text = playerMonster.level.ToString();
                    currentText.text = playerMonster.name + " has leveled up to level " + playerMonster.level;
                    playerMonster.SetStats();
                    foreach (var item in playerMonster.moveSet)
                    {
                        if (item.Item2 == playerMonster.level && !playerMonster.moves.Any(x => x != null && x.name == item.Item1.name))
                        {
                            if (playerMonster.moves[1] == null)
                                playerMonster.moves[1] = item.Item1;
                            else if (playerMonster.moves[2] == null)
                                playerMonster.moves[2] = item.Item1;
                            else if (playerMonster.moves[3] == null)
                                playerMonster.moves[3] = item.Item1;
                        }
                    }
                }
                if (deadCounter >= 300)
                {
                    finished = true;
                }
            }

            if (decideAttack == true)
            {
                if (KeyDown(Keys.A))
                {
                    playerAttack = playerMonster.moves[0];
                    decideAttack = false;
                }
                if (KeyDown(Keys.S) && playerMonster.moves[1] != null)
                {
                    playerAttack = playerMonster.moves[1];
                    decideAttack = false;
                }
                if (KeyDown(Keys.D) && playerMonster.moves[2] != null)
                {
                    playerAttack = playerMonster.moves[2];
                    decideAttack = false;
                }
                if (decideAttack == false)
                {
                    totalRounds++;
                    playerAttack.finished = false;
                    GetEnemyAttack();
                    enemyAttack.finished = false;
                    enemyAttempted = playerAttempted = false;
                }
            }
            else if (decideAttack == false)
            {
                if (enemyMonster.statSpeed < playerMonster.statSpeed)
                {
                    if (!playerAttack.finished)
                        PlayerAttack();
                    else if (!enemyAttack.finished)
                        EnemyAttack();
                }
                else
                {
                    if (!enemyAttack.finished)
                        EnemyAttack();
                    else if (!playerAttack.finished)
                        PlayerAttack();
                }
                if (enemyAttack.finished && playerAttack.finished)
                {
                    enemyMonster.drawPos = playerMonster.drawPos = new Vector2(0);
                    decideAttack = true;
                }
            }

            if (playerMonster.health > playerMonster.statHealth)
                playerMonster.health = playerMonster.statHealth;
            if (enemyMonster.health > enemyMonster.statHealth)
                enemyMonster.health = enemyMonster.statHealth;
        }

        public void PlayerAttack()
        {
            playerAttack.effect.Invoke(playerMonster, enemyMonster, true);
            currentShader = playerAttack.shader;
            bool? effective = null;
            if (!playerAttempted)
            {
                string text = playerMonster.name + " used " + playerAttack.name + "!";
                if (playerAttack.damaging)
                {
                    if (playerAttack.type.Effective(enemyMonster.type))
                        text += " It's super effective! ";
                    if (playerAttack.type.Ineffective(enemyMonster.type))
                        text += " It's not very effective.";
                }
                SetText(text);
            }
            if (playerAttack.damaging && !playerAttempted)
            {
                effective = DoDamageEnemy();
                playerAttempted = true;
            }
        }

        public bool? DoDamageEnemy()
        {
            float damage = CalcDamage(playerAttack, enemyMonster, playerMonster);
            if (damage < 2)
                damage = 1;
            enemyMonster.health -= damage;
            return GetEffectivity(enemyMonster, playerAttack); 
        }

        public void EnemyAttack()
        {
            enemyAttack.effect.Invoke(enemyMonster, playerMonster, false);
            currentShader = enemyAttack.shader;
            bool? effective = null;
            if (!enemyAttempted)
            {
                string text = enemyMonster.name + " used " + enemyAttack.name + "!";
                if (enemyAttack.damaging)
                {
                    if (enemyAttack.type.Effective(playerMonster.type))
                        text += " It's super effective!";
                    if (enemyAttack.type.Ineffective(playerMonster.type))
                        text += " It's not very effective.";
                }
                SetText(text);
            }
            if (enemyAttack.damaging && !enemyAttempted)
            {
                effective = DoDamagePlayer();
                enemyAttempted = true;
            }
        }

        public bool? DoDamagePlayer()
        {
            float damage = CalcDamage(enemyAttack, playerMonster, enemyMonster);
            if (damage < 2)
                damage = 1;
            playerMonster.health -= damage;
            return GetEffectivity(playerMonster, enemyAttack);
        }

        public bool? GetEffectivity(Monster mon, Move atk)
        {
            bool? effective = null;
            if (atk.type.Effective(mon.type))
                effective = true;
            else if (atk.type.Ineffective(mon.type))
                effective = false;
            if (mon.typeTwo != null)
            {
                if (atk.type.Effective(mon.typeTwo))
                {
                    if (effective == false) effective = null;
                    if (effective == null) effective = true;
                }
                else if (atk.type.Ineffective(mon.typeTwo))
                {
                    if (effective == true) effective = null;
                    if (effective == null) effective = false;
                }
            }
            return effective;
        }

        public void GetEnemyAttack()
        {
            Move[] choices = enemyMonster.moves;
            int totalMoves = 0;
            for (int i = 0; i < choices.Length; ++i)
                if (choices[i] != null) totalMoves++;
            if (totalMoves == 1)
            {
                enemyAttack = choices[0];
            }
            else if (totalMoves == 2)
            {
                if (totalRounds < 2)
                {
                    if (!choices[0].damaging)
                        enemyAttack = choices[0];
                    else if (!choices[1].damaging)
                        enemyAttack = choices[1];
                    else
                    {
                        if (choices[0].baseDamage > choices[1].baseDamage)
                            enemyAttack = choices[0];
                        else
                            enemyAttack = choices[1];
                    }
                }
                else
                {
                    if (choices[0].baseDamage > choices[1].baseDamage)
                        enemyAttack = choices[0];
                    else
                        enemyAttack = choices[1];
                }
            }
            else if (totalMoves == 3)
            {
                if (playerMonster.health > 10)
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        if (choices[i].damaging)
                        {
                            if (enemyAttack == null)
                                enemyAttack = choices[i];
                            else
                            {
                                float damageDone = CalcDamage(choices[i], playerMonster, enemyMonster);
                                float currDamage = CalcDamage(enemyAttack, playerMonster, enemyMonster);
                                if (damageDone > currDamage)
                                {
                                    enemyAttack = choices[i];
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; ++i)
                    {
                        if (choices[i].damaging)
                        {
                            if (enemyAttack != null)
                                enemyAttack = choices[i];
                            else
                            {
                                if (enemyAttack.baseDamage < choices[i].baseDamage)
                                {
                                    enemyAttack = choices[i];
                                }
                            }
                        }
                    }
                }
            }
        }

        public void SetText(string text)
        {
            lastText.text = currentText.text;
            currentText.text = text;
        }

        public float CalcDamage(Move attack, Monster enemy, Monster user)
        {
            int atk = user.effectiveAtk;
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
            float damageEffectivity = 1f; //0f, .25f, .5f, 2f, 4f
            if (enemy.type.StrongTo(attack.type))
                damageEffectivity /= 2;
            if (enemy.type.WeakTo(attack.type))
                damageEffectivity *= 2;
            if (enemy.typeTwo != null)
            {
                if (enemy.typeTwo.StrongTo(attack.type))
                    damageEffectivity /= 2;
                if (enemy.typeTwo.WeakTo(attack.type))
                    damageEffectivity *= 2;
            }
            float mod = 1f *
                (attack.type.Effective(enemy.type) ? 1.5f : 1f) *
                (attack.type.Ineffective(enemy.type) ? 0.5f : 1f) *
                damageEffectivity;
            return mod;
        }

        public bool KeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }
    }
}
