using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheDefender
{
    class Player : Sprite
    {
        MissileManager missileManager;

        public Player(Texture2D texture, Vector2 position, SpriteBatch theSpriteBatch, MissileManager missileManager)
            : base(texture, position, theSpriteBatch, position, 0.0f )
        {
            this.missileManager = missileManager;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyBoardState = Keyboard.GetState();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float RotationAngle = 0;
            RotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            RotationAngle = RotationAngle % circle;
            
            
            if(keyBoardState.IsKeyDown(Keys.Left))
            {
                if(this.rotation >= -1.35 )
                    this.rotation -= RotationAngle;
            }
            if (keyBoardState.IsKeyDown(Keys.Right))
            {
                if (this.rotation <= 1.35)
                    this.rotation += RotationAngle;
            }

            if (keyBoardState.IsKeyDown(Keys.Space))
            {
                if(Math.Abs(this.rotation) <= 1.35)
                    this.missileManager.Fire(this.rotation, this.Position);
            }

            base.Update(gameTime);
        }
    }
}
