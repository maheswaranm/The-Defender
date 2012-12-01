using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TheDefender
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TheDefenderGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        EnemyManager enemyManager;
        MissileManager missileManager;

        Scoreboard scoreBoard;

        Player player;
        Rectangle wallBounds;

        Boolean showTitleScreen = true;

        public TheDefenderGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Logger.init();
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            startTheGame();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>mmmn
        /// 
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (showTitleScreen)
            {
                checkIfKeyPressed();
            }
            else
            {
                this.player.Update(gameTime);
                this.enemyManager.Update(gameTime, wallBounds);
                this.missileManager.Update(gameTime);

                updateGame();
            }

            base.Update(gameTime);
        }

        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            if (showTitleScreen)
            {
                showTheTitleScreen();
            }
            else
            {

                drawTheWall();

                this.player.Draw(gameTime);
                this.enemyManager.Draw(gameTime);
                this.missileManager.Draw(gameTime);

                this.scoreBoard.Draw(this.spriteBatch);
            }
            this.spriteBatch.End(); 

            base.Draw(gameTime);
        }

        
        private void startTheGame()
        {
            scoreBoard = new Scoreboard();
            scoreBoard.Font = Content.Load<SpriteFont>("gameFont");
            
            int maxWidth = graphics.GraphicsDevice.Viewport.Width;
            int maxHeight = graphics.GraphicsDevice.Viewport.Height;

            enemyManager = new EnemyManager(this.Content.Load<Texture2D>("Enemy"), new Vector2(16), this.spriteBatch, maxWidth, maxHeight, scoreBoard);
            missileManager = new MissileManager(this.Content.Load<Texture2D>("Missile"), new Vector2(2.0f, 4.0f), this.spriteBatch, enemyManager, scoreBoard);


            //Setup player
            Texture2D playerTexture = Content.Load<Texture2D>("player");
            int playerX = graphics.GraphicsDevice.Viewport.Width / 2;
            int playerY = graphics.GraphicsDevice.Viewport.Height - 50;
            player = new Player(playerTexture, new Vector2(playerX,playerY), this.spriteBatch, missileManager);

            Logger.log(Log_Type.INFO, "max width "+playerX*2);
            Logger.log(Log_Type.INFO, "max height " + (playerY + 50));
        }

        private void drawTheWall()
        {
            Texture2D wallTexture = Content.Load<Texture2D>("wall");
            int wallX = 0;
            int wallY = graphics.GraphicsDevice.Viewport.Height - wallTexture.Height;
            spriteBatch.Draw(wallTexture, new Vector2(wallX, wallY), Color.Black);

            wallBounds = new Rectangle(wallX, wallY, wallTexture.Width, wallTexture.Height);
        }

        private void updateGame()
        {
            int currentLevel = scoreBoard.diffLevel;
            int newlevel = scoreBoard.Score / 500 + 1;

            if (newlevel != currentLevel)
            {
                enemyManager.makeEnemies(newlevel * 5);
                scoreBoard.diffLevel = newlevel;
            }
        }

        private void showTheTitleScreen()
        {

            int wallX = (graphics.GraphicsDevice.Viewport.Width / 2);
            int wallY = (graphics.GraphicsDevice.Viewport.Height / 2);

            SpriteFont font = Content.Load<SpriteFont>("gameFont");
            this.spriteBatch.DrawString(font, "The Defender", new Vector2(wallX, wallY), Color.White);
            this.spriteBatch.DrawString(font, "Left - rotate gun left", new Vector2(wallX, wallY + 20), Color.White);
            this.spriteBatch.DrawString(font, "Right - rotate gun right", new Vector2(wallX, wallY + 40), Color.White);
            this.spriteBatch.DrawString(font, "Space - shoot", new Vector2(wallX, wallY + 60), Color.White);


            this.spriteBatch.DrawString(font, "Press Enter To Continue", new Vector2(wallX-40, wallY + 80), Color.White);

        }

        private void checkIfKeyPressed()
        {
            KeyboardState keysDown = Keyboard.GetState();
            if (keysDown.IsKeyDown(Keys.Enter))
                showTitleScreen = false;

        }


    }
}
