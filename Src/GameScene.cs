using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeliverBullets {
    class GameScene : Scene {

        Player player = new Player();
        CollisionSystem collision = new CollisionSystem();
        CollisionBox testRect = new CollisionBox(new Rectangle(400, 100, 100, 100));
        CollisionBox testRect2 = new CollisionBox(new Rectangle(400, 310, 100, 100));
        CollisionBox testRect3 = new CollisionBox(new Rectangle(290, 100, 100, 100));

        Texture2D test;

        public GameScene() {
            collision.movingBodies.Add(player.collisionBox);
            collision.staticBodies.Add(testRect2);
            collision.staticBodies.Add(testRect3);
            collision.staticBodies.Add(testRect);
            test = Shapes.Rect(GameLoop.graphicsDevice, new Point(1280, 720), 30, Color.Gold, Color.Red);
        }

        public void Update(float delta) {
            collision.Update();
            player.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(test, Vector2.Zero, Color.White);
            player.Draw(spriteBatch);
            collision.Draw(spriteBatch, GameLoop.graphicsDevice);
        }
    }
}