using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvadersRemake
{
    class GameObject
    {
        // Class attributes.
        // The texture for this GameObject.
        // For now, the GameObject contains only one texture asset.
        // TODO: Add multiple textures capability later on. (for animation
        protected Texture2D texture;
        // The origin of the texture.
        private Vector2 origin;
        // The position of the GameObject.
        private Vector2 position;
        // The speedfor this GameObject.
        protected Vector2 speed;
        // The active flag for this GameObject. Active for drawing.
        protected bool active;
        // Simple implementation for collider detection.
        //The ColliderBox for this GameObject
        private Rectangle colliderBox;


        // Class construtors.
        // The default constructor
        public GameObject()
        {
            texture = null;
            colliderBox = new Rectangle(0, 0, 0, 0);
            origin = Vector2.Zero;
            position = Vector2.Zero;
            speed = Vector2.Zero;
            active = false;
        }
        // The copy constructor
        public GameObject(GameObject obj)
        {
            texture = obj.texture;
            colliderBox = obj.colliderBox;

            origin = obj.origin;
            position = obj.position;
            speed = obj.speed;
            active = obj.active;
        }
        // The initializing constructor
        public GameObject(Vector2 speed, bool active)
        {
            texture = null;
            colliderBox = new Rectangle(0, 0, 0, 0);
            origin = Vector2.Zero;
            position = Vector2.Zero;
            this.speed = speed;
            this.active = active;
        }
        // Class properties implemented with accessors (get and set)
        // Property Position. For now read/write. TODO: refactor this later.
        public Vector2 Position
        {
            get { return position;  }
            set { 
                position = value;
                colliderBox.X = (int)(position.X - origin.X);
                colliderBox.Y = (int)(position.Y - origin.Y);
            
            }
        }
        // Property PositionPoint, point type position (read-only)
        public Point PositionPoint
        {
            get { return new Point((int)position.X, (int)position.Y); }
        }

        // Property Speed (read-only)
        public Vector2 Speed
        {
            get { return speed; }
        }
        // Property Active (read/write)
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        // Property Width (read-only)
        public int Width
        {
            get
            {
                if (texture!= null)
                {
                    return texture.Width;   
                }
                else
                {
                    return 0;
                }
            }
        }
        // Property Height (read-only)
        public int Height
        {
            get
            {
                if (texture != null)
                {
                    return texture.Height;
                }
                else
                {
                    return 0;
                }
            }
        }

        // Property ColliderBox (read-only) 
        public Rectangle ColliderBox
        {
            get { return colliderBox; }
        }

        //Method Hit - to signal the GameObject that it's hit.
        // Child classes can implement there spescific hit behaviour, by overriding this method.
        public void Hit()
        {
            active = false;
        }

        // Class methods.
        // Method AddTexture - adding a texture to this GameObject.
        // For now , only one texture will be supported. TODO: Add multiple texture support
        public void AddTexture(Texture2D texture)
        {
            this.texture = texture;
            origin = new Vector2(texture.Width / 2, texture.Height);
            colliderBox = new Rectangle(
                x: (int)(position.X - origin.X),
                y: (int)(position.Y - origin.Y),
                width: texture.Width,
                height: texture.Height);
        }

        // Method Draw - General draw functionality for all visual objects in the game.
        public void Draw(SpriteBatch spritebatch)
        {
            if (active)
            {
                spritebatch.Draw(
                    texture: texture,
                    position: position,
                    sourceRectangle: texture.Bounds,
                    color: Color.White,
                    rotation: 0.0f,
                    origin: origin,
                    scale: 1.0f,
                    effects: SpriteEffects.None,
                    layerDepth: 1.0f
                    ); 
            }
        }
    }
}
