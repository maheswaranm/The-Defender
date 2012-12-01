using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDefender
{
    class MissileManager
    {
        Texture2D missileTexture;
        Vector2 missileOrigin;
        SpriteBatch spriteBatch;
        List<Missile> missiles;
        Single fireCounter;
        EnemyManager enemyManager;
        Scoreboard scoreBoard;

        public MissileManager(Texture2D missileTexture, Vector2 missileOrigin, SpriteBatch spriteBatch, EnemyManager enemyManager, Scoreboard scoreboard)
        {
            this.missileTexture = missileTexture;
            this.missileOrigin = missileOrigin;
            this.spriteBatch = spriteBatch;
            this.enemyManager = enemyManager;
            this.scoreBoard = scoreboard;

            missiles = new List<Missile>(64);

            for (Int32 i = 0; i < 64; i++)
            {
                missiles.Add(new Missile(this.missileTexture, this.missileOrigin, this.spriteBatch));
            }

            fireCounter = 0.0f;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Missile i in missiles)
            {
                if (i.IsActive)
                {
                    i.Update(gameTime);
                }
            }

            if (fireCounter > 0.0f)
            {
                fireCounter -= (Single)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                if (fireCounter < 0.0f)
                {
                    fireCounter = 0.0f;
                }
            }

            // Collisions... 
            foreach (Missile i in missiles)
            {
                if (i.IsActive)
                {
                    foreach (Enemy e in this.enemyManager.Enemies)
                    {
                        if (i.BoundingRectangle.Intersects(e.BoundingRectangle))
                        {
                            e.OnHit();
                            scoreBoard.Score += 20;
                        }
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Missile i in missiles)
            {
                if (i.IsActive)
                {
                    i.Draw(gameTime);
                }
            }
        }

        public void Fire(float angle, Vector2 position)
        {
            if (this.fireCounter == 0.0f)
            {
                foreach (Missile i in missiles)
                {
                    if (!i.IsActive)
                    {
                        i.Fire(angle, position);

                        this.fireCounter = 200.0f;

                        break;
                    }
                }
            }
        }
    }
}
