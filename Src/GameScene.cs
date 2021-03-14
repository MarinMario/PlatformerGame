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
        CollisionBox testRect2 = new CollisionBox(new Rectangle(400, 210, 100, 100));
        CollisionBox testRect3 = new CollisionBox(new Rectangle(290, 100, 100, 100));

        public GameScene() {
            collision.movingBodies.Add(player.collisionBox);
            collision.staticBodies.Add(testRect);
            collision.staticBodies.Add(testRect2);
            collision.staticBodies.Add(testRect3);
        }

        public void Update(float delta) {
            collision.Update();
            player.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch) {
            player.Draw(spriteBatch);
            collision.Draw(spriteBatch, GameLoop.graphicsDevice);
        }
    }
}