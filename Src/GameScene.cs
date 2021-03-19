using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Utils;

namespace DeliverBullets {
    class GameScene : Scene {

        Player player = new Player();
        CollisionSystem colSys = new CollisionSystem();
        CollisionBox testRect = new CollisionBox(new Rectangle(400, 100, 100, 100));
        CollisionBox testRect2 = new CollisionBox(new Rectangle(400, 310, 100, 100));
        CollisionBox testRect3 = new CollisionBox(new Rectangle(290, 100, 100, 100));

        Texture2D test1;
        Texture2D test2;
        Texture2D test3;

        LoadedMap map;

        public GameScene() {
            colSys.movingBodies.Add(player.collisionBox);

            test1 = Shapes.Rect(Global.graphicsDevice, new Point(100, 100), 5, Color.Red, Color.Blue);
            test2 = Shapes.Rect(Global.graphicsDevice, new Point(100, 100), 5, Color.Yellow, Color.Blue);
            test3 = Shapes.Rect(Global.graphicsDevice, new Point(100, 100), 5, Color.Purple, Color.Blue);

            map = MapLoader.Load(@"Content/Test.map");
            foreach(var cb in map.collisionRects)
                colSys.staticBodies.Add(new CollisionBox(cb));

        }

        public void Update(float delta) {
            colSys.Update();
            player.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (var t in map.textures)
                switch (t.Item1) {
                    case 1:
                        spriteBatch.Draw(test1, t.Item2.ToVector2(), Color.White);
                        break;
                    case 2:
                        spriteBatch.Draw(test2, t.Item2.ToVector2(), Color.White);
                        break;
                    case 3:
                        spriteBatch.Draw(test3, t.Item2.ToVector2(), Color.White);
                        break;
                }
            player.Draw(spriteBatch);
            // colSys.Draw(spriteBatch, Global.graphicsDevice);
        }
    }
}