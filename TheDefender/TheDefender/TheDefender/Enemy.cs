using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDefender
{
    class Enemy : Sprite
    {
        public static int maxWidth { get; set; }
        public static int maxHeight { get; set; }

        public Enemy(Texture2D texture, Vector2 origin, SpriteBatch spriteBatch, Vector2 initialPosition)
            : base(texture, origin, spriteBatch, initialPosition, 0f)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (this.Position.Y < maxHeight)
            {
                this.Position += Vector2.UnitY * 1.0f;
            }
            else
            {
                this.reset();
            }

            base.Update(gameTime);
        }

        public void OnHit()
        {
            this.reset();
        }

        void reset()
        {
            Random random = new Random();

            this.Position = new Vector2(32.0f + random.Next(maxWidth - 32), -1.0f - random.Next(maxHeight));
        }
    }
}
