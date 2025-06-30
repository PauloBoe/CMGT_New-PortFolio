using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvadersRemake
{
    class Laser: GameObject
    {
        public Laser(): base() { }
        public Laser(Laser obj): base(obj)  { }
        public Laser(Vector2 speed, bool active) : base(speed, active) { }


        public void Update(GameTime gameTime, KeyboardState keyboardState, Player player)
        {
            // Update laser position. If laser is active
            if (active)
            {
                if (Position.Y > 0)
                {
                    Position += speed;
                }
                else
                {
                   active = false;
                }
            }

            // Check space key for laser fire action
            if (keyboardState.IsKeyDown(Keys.Space))
            {
               active = true;
               Position = player.Position;
            }
        }
    }
}
