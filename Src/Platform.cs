using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeliverBullets {
    class Platform : Collider {

        public Rectangle CollisionBox { get; set; }
        Texture2D texture;

        public Platform(Point position, int width, Texture2D texture, Collision collision) {
            CollisionBox = new Rectangle(position.X, position.Y, width, 20);
            collision.bodies.Add(this);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, CollisionBox, Color.White);
        }
    }

    class MovingPlatform : Collider {

        public Rectangle CollisionBox { get; set; }
        Texture2D texture;
        public Point position1;
        public Point position2;
        
        Point targetPosition;
        public Vector2 velocity = Vector2.Zero;

        public MovingPlatform(Point position1, Point position2, int width, Texture2D texture, Collision collision) {
            this.position1 = position1;
            this.position2 = position2;
            CollisionBox = new Rectangle(position1.X, position1.Y, width, 20);
            collision.bodies.Add(this);
            targetPosition = position2;
        }

        public void Update(float delta) {
            if(CollisionBox.Location == targetPosition)
                targetPosition = targetPosition == position1 ? position2 : position1;
            velocity = Helper.MoveVector(CollisionBox.Location.ToVector2(), targetPosition.ToVector2(), 100 * delta);
            CollisionBox = new Rectangle(CollisionBox.Location + velocity.ToPoint(), CollisionBox.Size);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, CollisionBox, Color.White);
        }
    }
}