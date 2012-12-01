using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TheDefender
{
    abstract class Sprite
    {
        Texture2D texture;
        Vector2 position;
        Rectangle boundingRectangle;
        SpriteBatch spriteBatch;
        public float rotation { get; set; }

        public Sprite(Texture2D texture, Vector2 position, SpriteBatch theSpriteBatch, Vector2 initialPosition, float rotation)
        {
            // TODO: Complete member initialization
            this.texture = texture;
            this.position = initialPosition;
            this.spriteBatch = theSpriteBatch;
            this.rotation = rotation;

            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        protected Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;

                boundingRectangle = new Rectangle((Int32)(position.X), (Int32)(position.Y ), texture.Width, texture.Height);
            }
        }

        public Rectangle BoundingRectangle
        {
            get { return boundingRectangle; }
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {
            Vector2 textureOrigin = new Vector2();
            textureOrigin.X = texture.Width / 2;
            textureOrigin.Y = texture.Height / 2;


            this.spriteBatch.Draw(texture, position, null, Color.White, rotation, textureOrigin, 1.0f, SpriteEffects.None, 0f);
        }


        
    }
}
