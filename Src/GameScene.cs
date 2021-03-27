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
        CollisionBox testRect = new CollisionBox(new Rectangle(400, 100, 100, 100));
        CollisionBox testRect2 = new CollisionBox(new Rectangle(400, 310, 100, 100));
        CollisionBox testRect3 = new CollisionBox(new Rectangle(290, 100, 100, 100));
        
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
            collision.bodies.Add(player.collisionBox);
            // collision.bodies.Add(new CollisionBox(new Rectangle(300, 300, 150, 150)));
            // collision.bodies.Add(new CollisionBox(new Rectangle(450, 300, 150, 150)));
            // collision.bodies.Add(new CollisionBox(new Rectangle(450, 450, 150, 150)));
            // collision.bodies.Add(new CollisionBox(new Rectangle(600, 450, 200,  15)));

            collision.bodies.Add(new CollisionBox(new Rectangle(50, 200, 300, 20)));
            collision.bodies.Add(new CollisionBox(new Rectangle(400, 400, 200, 20)));
            collision.bodies.Add(new CollisionBox(new Rectangle(100, 550, 700, 20)));
            collision.bodies.Add(new CollisionBox(new Rectangle(1000, 300, 20, 300)));
            collision.bodies.Add(new CollisionBox(new Rectangle(0, 0, 30, Global.resolution.Y)));
            collision.bodies.Add(new CollisionBox(new Rectangle(0, 0, Global.resolution.X, 30)));
            collision.bodies.Add(new CollisionBox(new Rectangle(Global.resolution.X - 30, 0, 30, Global.resolution.Y)));
            collision.bodies.Add(new CollisionBox(new Rectangle(0, Global.resolution.Y - 30, Global.resolution.X, 30)));
            
            bg = Shapes.ColorRect(Global.graphicsDevice, Global.resolution.X, Global.resolution.Y, Color.Gray);
        }

        public void Update(float delta) {
            player.Update(delta);
            // player.UpdateTopDown(delta);
            // colSys.Update();
            
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(bg, new Rectangle(Point.Zero, Global.resolution), Color.White);
            // foreach (var t in map.textures)
            //     switch (t.Item1) {
            //         case 1:
            //             spriteBatch.Draw(test1, t.Item2.ToVector2(), Color.White);
            //             break;
            //         case 2:
            //             spriteBatch.Draw(test2, t.Item2.ToVector2(), Color.White);
            //             break;
            //         case 3:
            //             spriteBatch.Draw(test3, t.Item2.ToVector2(), Color.White);
            //             break;
            //     }
            foreach(var thing in collision.bodies)
                spriteBatch.Draw(cbTexture, thing.rect, Color.White);
            player.Draw(spriteBatch);
        }
    }
}