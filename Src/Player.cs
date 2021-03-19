using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DeliverBullets {
    class Player : Scene {

        public CollisionBox collisionBox = new CollisionBox(new Rectangle(0, 100, 100, 100));
        Texture2D texture;

        int speed = 500;

        public Player() {
            texture = Utils.Shapes.Rect(Global.graphicsDevice, collisionBox.rect.Size, 5, Color.Red, Color.Gold);
        }

        public void Update(float delta) {
            var direction = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                direction.X = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                direction.X = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                direction.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                direction.Y = 1;

            if (direction != Vector2.Zero) {
                direction.Normalize();
            }
            var velocity = direction * speed * delta;
            collisionBox.rect.Location += velocity.ToPoint();
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, collisionBox.rect, Color.White);
        }
    }
}