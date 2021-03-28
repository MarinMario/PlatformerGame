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

        LoadedMap map;
        Texture2D bg;

        public GameScene() {
            test1 = Shapes.Rect(Global.graphicsDevice, new Point(700, 50), 5, Color.Red, Color.Blue);
            test2 = Shapes.Rect(Global.graphicsDevice, new Point(100, 100), 5, Color.Yellow, Color.Blue);
            test3 = Shapes.Rect(Global.graphicsDevice, new Point(100, 100), 5, Color.Purple, Color.Blue);
            cbTexture = Shapes.Rect(Global.graphicsDevice, new Point(10, 10), 0, Color.Blue, Color.Blue);

            // map = MapLoader.Load(@"Content/Test.map");
            // foreach(var cb in map.collisionRects)
            //     colSys.staticBodies.Add(new CollisionBox(cb));

            player = new Player(collision);

            collision.bodies.Add(new CollisionBox(new Rectangle(50, 200, 300, 20)));
            collision.bodies.Add(new CollisionBox(new Rectangle(400, 400, 200, 20)));
            collision.bodies.Add(new CollisionBox(new Rectangle(100, 550, 700, 20)));
            collision.bodies.Add(new CollisionBox(new Rectangle(1000, 300, 20, 300)));
            collision.bodies.Add(new CollisionBox(new Rectangle(-Global.resolution.X * 2, 0, 30, Global.resolution.Y)));
            collision.bodies.Add(new CollisionBox(new Rectangle(-Global.resolution.X, -50, Global.resolution.X, 100)));
            collision.bodies.Add(new CollisionBox(new Rectangle(Global.resolution.X * 3 - 30, 0, 30, Global.resolution.Y)));
            collision.bodies.Add(new CollisionBox(new Rectangle(0, Global.resolution.Y - 30, Global.resolution.X * 2, 30)));
            
            bg = Shapes.Rect(Global.graphicsDevice, Global.resolution, 0, Color.Gray, Color.Gray);
        }

        public void Update(float delta) {
            player.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(bg, new Rectangle(Global.cameraPos, Global.resolution), Color.White);
            foreach(var thing in collision.bodies)
                spriteBatch.Draw(cbTexture, thing.rect, Color.White);
            player.Draw(spriteBatch);
        }
    }
}