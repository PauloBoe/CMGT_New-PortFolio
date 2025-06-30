using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvadersRemake
{
    class AlienManager
    {
        Viewport viewport;
        // The aliens.
        private List<GameObject> aliens;
        // Data model for spawning
        private int numberOfAliens = 30;
        private int numberOfRows = 3;
        private float horizontalMargin = 0.1f;  // % of viewport Width.
        private float verticalMargin = 0.2f;    // % of viewport Height.
        private float verticalSpawnArea = 0.5f; // % of viewport Height.

        // Data model for alien movement.
        private double lastHorizontalTimeStamp = 0.0;
        private double lastVerticalTimeStamp = 0.0;
        private int stepInterval = 500; // in mms
        private int numberOfSteps = 5;
        private int horizontalStep = 0;

        // The alien lasers.
        private Stack<Laser> lasers;
        private Queue<Laser> activeLasers;

        private int numberOfLasers = 5;
        private double laserShotTimeStamp = 0.0;
        private Random laserRandomizer;

        public AlienManager(Viewport viewport)
        {
            this.viewport = viewport;

            aliens = new List<GameObject>();
            lasers = new Stack<Laser>();
            activeLasers = new Queue<Laser>();
            laserRandomizer = new Random();
        }

        public void CreateAliens(GameObject preFab)
        {
            int numberOfComlums = numberOfAliens / numberOfRows;
            for (int i = 0; i < numberOfAliens; i++)
            {
                GameObject nextAlien = new GameObject(preFab);
                nextAlien.Position = new Vector2(
                    x: (viewport.Width * horizontalMargin) + (i % numberOfComlums) * (viewport.Width * (1.0f - 2 * horizontalMargin) / numberOfComlums),
                    y: (viewport.Height * verticalMargin) + (i / numberOfComlums) * (viewport.Height * verticalSpawnArea / numberOfRows)
                    );
                aliens.Add(nextAlien);

            }
        }

        public void CreateLasers(Laser prefab) 
        { 
            // Create lasers by copying the prefab and add them to the stack
            for(int i = 0; i < numberOfAliens; i++)
            {
                 lasers.Push(new Laser(prefab));
            }
        }
        public void Update(GameTime gameTime)
        {
            // update alen position
            double now = gameTime.TotalGameTime.TotalMilliseconds;

            if (now - lastHorizontalTimeStamp > stepInterval)
            {
                foreach (GameObject alien in aliens)
                {
                    Vector2 position = alien.Position;
                    if (horizontalStep < numberOfSteps)
                    {
                        position.X += alien.Speed.X;
                    }
                    else
                    {
                        position.X -= alien.Speed.X;
                    }
                    alien.Position = position;
                }
                horizontalStep = (horizontalStep + 1) % (2 * numberOfSteps);
                lastHorizontalTimeStamp = now;
            }
            if (now - lastVerticalTimeStamp > numberOfSteps * stepInterval)
            {
                foreach (GameObject alien in aliens)
                {
                    Vector2 position = alien.Position;
                    position.Y += alien.Speed.Y;
                    alien.Position = position;
                }
                lastVerticalTimeStamp = now;
            }

            // Handling live tieme of the lasers
            if(activeLasers.Count > 0)
            {
                if (activeLasers.Peek().Position.Y > viewport.Height)
                {
                    Laser laser = activeLasers.Dequeue();
                    laser.Active = false;
                    lasers.Push(laser);
                }
            }

            // Handling laser movement
            foreach(Laser laser in activeLasers)
            {
                laser.Position += laser.Speed;
            }

            // Handling random alien laser triggers
            if (now - laserShotTimeStamp > stepInterval)
            {
                int randomIndex = laserRandomizer.Next(aliens.Count);

                if (lasers.Count > 0)
                {
                    Laser laser = lasers.Pop();
                    laser.Position = aliens[randomIndex].Position;
                    laser.Active = true;
                    activeLasers.Enqueue(laser);
                }
                laserShotTimeStamp = now;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the aliens
            foreach (GameObject alien in aliens)
            {
                alien.Draw(spriteBatch);
            }

            // Draw the alien lasers
            foreach(Laser laser in activeLasers)
            {
                laser.Draw(spriteBatch);
            }
        }

        // Method AlienCollisionDetection - a simple implementation.    
        // TODO: Refactor this with the Composit and Event patterns. Will be course subject for year 3.
        public bool AlienCollisionDetection(Point point)
        {
            bool collision = false;
            // TODO: Checkif this collision detection algorithm can be more efficient.
            // Ceck collision with all of the aliens.
            foreach(GameObject alien in aliens)
            {
                if (alien.Active & alien.ColliderBox.Contains(point))
                {
                    collision = true;
                    alien.Hit();
                    break;
                }
            }

            return collision;
        }

        // Method LaserCollisionDetection - a simple implementation. 
        // TODO: Refactor this with the Composit and Event patterns. Will be course subject for year 3.
        public bool LaserCollisionDetection(Rectangle target)
        {
            bool collision = false;
            // Check all the alien laser for collision with target
            foreach(Laser laser in activeLasers)
            {
                if (target.Contains(laser.PositionPoint))
                {
                    collision = true;
                    laser.Hit();
                    break;
                }
            }

            return collision;
        } 
    }
}
