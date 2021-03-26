using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DeliverBullets {
    class Player : Scene {

        public CollisionBox collisionBox = new CollisionBox(new Rectangle(100, 50, 32, 32));
        Texture2D texture;
        Collision collision;
        int speed = 300;
        int gravity = 10;
        int jumpForce = 7;
        int jumpTimes = 0;
        int maxJumpTimes = 1;
        Vector2 velocity = Vector2.Zero;
        Vector2 maxVelocity = new Vector2(10, 10);

        public Player(Collision collision) {
            this.collision = collision;
            texture = Utils.Shapes.Rect(Global.graphicsDevice, collisionBox.rect.Size, 5, Color.Red, Color.Gold);
        }

        public void Update(float delta) {
            var directionX = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                directionX = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                directionX = 1;
            velocity.X = directionX * speed * delta;

            velocity.Y += gravity * delta;
            if(Utils.Input.IsKeyPressed(Keys.Space, true) && jumpTimes < maxJumpTimes) {
                velocity.Y = -jumpForce;
                jumpTimes += 1;
            }

            velocity.X = MathHelper.Clamp(velocity.X, -maxVelocity.X, maxVelocity.X);
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxVelocity.Y, maxVelocity.Y);
            
            var testRect = collisionBox.rect;
            testRect.Location += velocity.ToPoint();
            var c = collision.CheckCollision(testRect, 15);

            if(c.x != 0)
                velocity.X = 0;
            if(c.y != 0) velocity.Y = 0;

            if(c.x != 0 && c.y == 0) {
                jumpTimes = 0;
                maxVelocity.Y = 1;
                jumpForce = 0;
            } else {
                maxVelocity.Y = 10;
                jumpForce = 7;
            }
            if(c.y == 1)
                jumpTimes = 0;

            if(collisionBox.rect.Location.X > Global.resolution.X)
                collisionBox.rect.Location = new Point(0, collisionBox.rect.Location.Y);
            if(collisionBox.rect.Location.X < 0)
                collisionBox.rect.Location = new Point(Global.resolution.X, collisionBox.rect.Location.Y);
            if(collisionBox.rect.Location.Y > Global.resolution.Y)
                collisionBox.rect.Location = new Point(collisionBox.rect.Location.X, 0);
            if(collisionBox.rect.Location.Y < 0)
                collisionBox.rect.Location = new Point(collisionBox.rect.Location.X, Global.resolution.Y);
            
            collisionBox.rect.Location += velocity.ToPoint();
            
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, collisionBox.rect, Color.White);
        }
    }
}