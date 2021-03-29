using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Utils;

namespace DeliverBullets {
    class GameScene : Scene {

        Player player;
        Collision collision = new Collision();
        
        Texture2D test1;
        Texture2D test2;
        Texture2D test3;
        Texture2D cbTexture;

        Texture2D bg;
        MovingPlatform movingPlatform;

        public GameScene() {
            test1 = Helper.Rect(Global.graphicsDevice, new Point(700, 50), 5, Color.Red, Color.Blue);
            test2 = Helper.Rect(Global.graphicsDevice, new Point(100, 100), 5, Color.Yellow, Color.Blue);
            test3 = Helper.Rect(Global.graphicsDevice, new Point(100, 100), 5, Color.Purple, Color.Blue);
            cbTexture = Helper.Rect(Global.graphicsDevice, new Point(10, 10), 0, Color.Blue, Color.Blue);

            // map = MapLoader.Load(@"Content/Test.map");
            // foreach(var cb in map.collisionRects)
            //     colSys.staticBodies.Add(new CollisionBox(cb));

            player = new Player(collision);
            var platformTexture = Helper.Rect(Global.graphicsDevice, new Point(10, 10), 0, Color.Red, Color.Red);
            movingPlatform = new MovingPlatform(new Point(200, 600), new Point(500, 300), 200, platformTexture, collision);
            var platform = new Platform(new Point(0, Global.resolution.Y - 20), Global.resolution.X, platformTexture, collision);
            var p2 = new Platform(new Point(800, 500), 300, platformTexture, collision);

            
            bg = Helper.Rect(Global.graphicsDevice, Global.resolution, 0, Color.Gray, Color.Gray);
        }

        public void Update(float delta) {
            player.Update(delta);
            movingPlatform.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(bg, new Rectangle(Global.cameraPos, Global.resolution), Color.White);
            foreach(var thing in collision.bodies)
                spriteBatch.Draw(cbTexture, thing.CollisionBox, Color.White);
            player.Draw(spriteBatch);
        }
    }
}