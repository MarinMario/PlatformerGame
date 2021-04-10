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
        AnimatedSprite sprite;
        Texture2D t2;

        public GameScene() {
            var t = Helper.ColorRect(Global.graphicsDevice, 9, 9, new Color(255, 226, 104));
            t2 = Helper.Rect(Global.graphicsDevice, new Point(90, 90), 10, Color.Red, Color.Blue);

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
            sprite = new AnimatedSprite(Global.graphicsDevice, t2, new Point(3, 3), 0.3f);
        }

        public void Update(float delta) {
            sprite.Animate(delta);
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
            for(var i = 0; i < sprite.frames.Count; i++)
                spriteBatch.Draw(sprite.frames[i], new Vector2(i * (sprite.frames[i].Bounds.Size.X + 1), 100), Color.White);
            spriteBatch.Draw(t2, new Vector2(200, 200), Color.White);
            spriteBatch.Draw(sprite.frames[sprite.currentFrame], new Rectangle(300, 200, 90, 90), Color.White);
        }
    }
}