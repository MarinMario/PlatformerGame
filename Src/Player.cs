using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeliverBullets {
    class Player {

        public CollisionBox collisionBox = new CollisionBox(new Rectangle(100, 50, 32, 32));
        Texture2D texture;
        Collision collision;
        int speed = 400;
        int gravity = 10;
        int jumpForce = 7;
        int jumpTimes = 0;
        int maxJumpTimes = 1;
        Vector2 velocity = Vector2.Zero;
        Vector2 maxVelocity = new Vector2(10, 10);

        public Player(Collision collision) {
            this.collision = collision;
            collision.bodies.Add(collisionBox);
            texture = Utils.Shapes.Rect(Global.graphicsDevice, collisionBox.rect.Size, 5, Color.Red, Color.Gold);
        }
        
        public void Update(float delta) {
            PlatformerMovement(delta);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, collisionBox.rect, Color.White);
        }

        void PlatformerMovement(float delta) {
            var directionX = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                directionX = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                directionX = 1;
            velocity.X = directionX * speed * delta;

            velocity.Y += gravity * delta;
            if(jumpTimes < maxJumpTimes)
                if(Input.IsKeyPressed(Keys.J, true)) {
                    velocity.Y = -jumpForce;
                    jumpTimes += 1;
                }

            velocity.X = MathHelper.Clamp(velocity.X, -maxVelocity.X, maxVelocity.X);
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxVelocity.Y, maxVelocity.Y);
            
            var testRect = collisionBox.rect;
            testRect.Location += velocity.ToPoint();
            var c = collision.CheckCollision(testRect, 15);

            if(c.y == 1 && velocity.Y > 0)
                jumpTimes = 0;

            var collisionX = false;
            foreach(var rect in c.collidingRects)
                if (rect != collisionBox.rect)
                    if(rect.Height > 20) {
                        collisionX = c.x != 0;
                        if (c.x == -1 && velocity.X < 0 || c.x == 1 && velocity.X > 0)
                            velocity.X = 0;
                        if (c.y == -1 && velocity.Y < 0 || c.y == 1 && velocity.Y > 0)
                            velocity.Y = 0;
                    } else if (c.y == 1 && velocity.Y > 0) {
                        collisionX = false;
                        velocity.Y = 0;
                    }

            
                if(collisionX && c.y == 0) {
                    jumpTimes = 0;
                    maxVelocity.Y = 1;
                    jumpForce = 0;
                } else {
                    maxVelocity.Y = 10;
                    jumpForce = 7;
                }

            collisionBox.rect.Location += velocity.ToPoint();
            
        }

        void TopDownMovement(float delta) {
            var direction = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                direction.X = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                direction.X = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                direction.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                direction.Y = 1;

            if (direction != Vector2.Zero)
                direction.Normalize();
            
            var velocity = direction * speed * delta;
            var testRect = collisionBox.rect;
            testRect.Location += velocity.ToPoint();
            var c = collision.CheckCollision(testRect, 15);
            
            if (c.x == -1 && velocity.X < 0 || c.x == 1 && velocity.X > 0)
                velocity.X = 0;
            if (c.y == -1 && velocity.Y < 0 || c.y == 1 && velocity.Y > 0)
                velocity.Y = 0;

            collisionBox.rect.Location += velocity.ToPoint();
        }
    }
}