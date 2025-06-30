using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceInvadersRemake
{
    class Player : GameObject
    {
        public Player() : base() { }
        public Player(Player obj) : base(obj) { }
        public Player(Vector2 speed, bool active) : base(speed, active) { }
        public void Update(GameTime gameTime, KeyboardState keyboardState, Viewport viewport)
        {
            // Test right arrow button for player movement.
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                // Test if player is at the right edge of the screen
                if ((Position.X + (this.Width / 2)) < viewport.Width)
                {
                    Position += speed;
                }
            }

            // Test left arrow button for player movement.
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if ((Position.X + (this.Width / 2)) > 0)
                {
                    Position -= speed;
                }
            }

        }
    }
}
