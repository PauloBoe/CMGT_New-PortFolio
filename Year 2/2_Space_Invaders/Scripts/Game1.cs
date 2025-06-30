using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvadersRemake
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D background;

        // The player.
        private Player player;


        // The player laser.
        private Laser playerLaser;

        // The alien manager
        private AlienManager alienManager;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // Initialize player.
            player = new Player(new Vector2(10, 0), true);

            // Initialize player laser data.
            playerLaser = new Laser(new Vector2(0, -10), false);

            //Initialize the alien manager
            alienManager = new AlienManager(_graphics.GraphicsDevice.Viewport);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Load background asset.
            background = Content.Load<Texture2D>("background");
            // Load player asset.
            player.AddTexture(Content.Load<Texture2D>("player"));
            // Load playerLaser
            playerLaser.AddTexture(Content.Load<Texture2D>("laser2"));

            // Set player position.
            player.Position = new Vector2(
                x: _graphics.GraphicsDevice.Viewport.Width / 2,
                y: _graphics.GraphicsDevice.Viewport.Height
                );

            // Load alien texture. 
            GameObject alienPreFab = new GameObject(new Vector2(10, 15), true);
            alienPreFab.AddTexture(Content.Load<Texture2D>("alien1"));
            alienManager.CreateAliens(alienPreFab);

            // Load alien laser texture
            Laser laserPrefab = new Laser(new Vector2(0, 10), false);
            laserPrefab.AddTexture(Content.Load<Texture2D>("laser1"));
            alienManager.CreateLasers(laserPrefab);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            // Check for collisions 
            CollisionDetection();


            // Update the Aliens through the alien manager.
            alienManager.Update(gameTime);


            // Get Keyboard state
            KeyboardState keyboardState = Keyboard.GetState();

            player.Update(gameTime, keyboardState, _graphics.GraphicsDevice.Viewport);
            playerLaser.Update(gameTime, keyboardState, player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            // Draw the background.
            _spriteBatch.Draw(background, Vector2.Zero, Color.White);

            // Draw player.
            player.Draw(_spriteBatch);

            // Draw player laser.
            playerLaser.Draw(_spriteBatch);

            // Draw the aliens.
            alienManager.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // Method CollisionDetection - a simple implementation.
        // TODO: Refactor this with the Composit and Event patterns. Will be course subject for year 3
        private void CollisionDetection()
        {
            // Handle player laser collitions.
            if (playerLaser.Active)
            {
                if (alienManager.AlienCollisionDetection(playerLaser.PositionPoint) )
                {
                    playerLaser.Hit();
                }
            }

            // Check if player is hit
            if (player.Active)
            {
                if (alienManager.LaserCollisionDetection(player.ColliderBox))
                {
                    player.Hit();
                }
            }
        }
    }
}
