using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheDefender
{
    class Scoreboard
    {
        private Vector2 scorePos = new Vector2(20, 10);
        private Vector2 levelPos = new Vector2(20, 30);
 
        public SpriteFont Font { get; set; }
 
        public int Score { get; set; }

        public int diffLevel { get; set; }

        public Scoreboard()
        {
            this.Score = 0;
            this.diffLevel = 2;
        }
 
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font,"Score: " + Score.ToString(),scorePos,Color.White);
            spriteBatch.DrawString(Font, "Difficulty Level: " + diffLevel.ToString(), levelPos, Color.White);
        }
    }
}
