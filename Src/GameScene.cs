using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Utils;

namespace Src {
    class GameScene : Scene {

        Player player;
        Collision collision = new Collision();
        

        Texture2D bg;
        List<MovingPlatform> movingPlatforms = new List<MovingPlatform>();
        List<Platform> platforms = new List<Platform>();
        List<Wall> walls = new List<Wall>();

        public GameScene() {
            var t = Helper.ColorRect(Global.graphicsDevice, 1, 1, new Color(255, 226, 104));

            var level = LevelEditorScene.LoadLevel();
            foreach(var thing in level)
            {
                if(thing.name == ObjectName.MovingPlatform)
                    movingPlatforms.Add(new MovingPlatform(thing.position, (Point)thing.targetPosition, thing.size, t, collision));
                else if(thing.name == ObjectName.Platform)
                    platforms.Add(new Platform(thing.position, thing.size, t, collision));
                else
                    walls.Add(new Wall(new Rectangle(thing.position, thing.size), t, collision));
            }

            player = new Player(collision);
            
            bg = Helper.Rect(Global.graphicsDevice, Global.resolution, 0, Color.Gray, Color.Gray);
        }

        public void Update(float delta) {
            player.Update(delta);
            foreach(var thing in movingPlatforms)
                thing.Update(delta);
        }

        public void Draw(SpriteBatch spriteBatch) {
            // spriteBatch.Draw(bg, new Rectangle(Global.cameraPos, Global.resolution), Color.White);
            foreach(var thing in platforms)
                thing.Draw(spriteBatch);
            foreach(var thing in movingPlatforms)
                thing.Draw(spriteBatch);
            foreach(var thing in walls)
                thing.Draw(spriteBatch);

            player.Draw(spriteBatch);
        }
    }
}