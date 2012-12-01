using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDefender
{
    class EnemyManager
    {
        Texture2D enemyTexture;
        Vector2 enemyOrigin;
        SpriteBatch spriteBatch;
        List<Enemy> enemies;
        Scoreboard scoreBoard;

        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        public EnemyManager(Texture2D enemyTexture, Vector2 enemyOrigin, SpriteBatch spriteBatch, int maxWidth, int maxHeight, Scoreboard scoreboard)
        {
            this.enemyTexture = enemyTexture;
            this.enemyOrigin = enemyOrigin;
            this.spriteBatch = spriteBatch;
            Enemy.maxWidth = maxWidth;
            Enemy.maxHeight = maxHeight;
            this.scoreBoard = scoreboard;

            enemies = new List<Enemy>();
        }

        public void Update(GameTime gameTime, Rectangle theWall)
        {
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime);

                if (e.BoundingRectangle.Intersects(theWall))
                {
                    e.OnHit();
                    scoreBoard.Score -= 10;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Enemy e in enemies)
            {
                e.Draw(gameTime);
            }
        }

        public void makeEnemies(Int32 enemyCount)
        {
            Random random = new Random();

            Int32 additionCount = enemyCount - enemies.Count;

            for (Int32 i = 0; i < additionCount; i++)
            {
                enemies.Add(new Enemy(this.enemyTexture, this.enemyOrigin, this.spriteBatch, new Vector2(32.0f + random.Next(Enemy.maxWidth - 32), -32.0f - random.Next(Enemy.maxHeight))));
            }
        }
    }
}
