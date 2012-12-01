using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDefender
{
    class Missile : Sprite
    {
        Boolean isActive;

        public Boolean IsActive
        {
            get { return isActive; }
        }

        public Missile(Texture2D texture, Vector2 origin, SpriteBatch spriteBatch)
            : base(texture, origin, spriteBatch, Vector2.Zero, 0.0f)
        {
            isActive = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.Position.Y > -32.0f)
            {
                Move(-4.0f);
            }
            else
            {
                this.isActive = false;
            }

            base.Update(gameTime);
        }

        public void Fire(float angle, Vector2 position)
        {
            Logger.log(Log_Type.INFO, "now shooting at " + angle);

            this.rotation = angle;
            this.Position = position;

            this.isActive = true;
        }

        private void Move(float moveDistance)
        {
            Vector2 movement = new Vector2();

            float angleOfMovement = MathHelper.ToRadians(90)+this.rotation;

            movement.X = moveDistance * (float)Math.Cos(angleOfMovement);
            movement.Y = moveDistance * (float)Math.Sin(angleOfMovement);

            this.Position += movement;
        }

    }
}
