using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeliverBullets {
    class MenuScene : Scene {

        Texture2D buttonTexture;
        Texture2D buttonTexture2;
        Texture2D buttonTexture3;
        Button testButton;
        
        Texture2D bg;

        public MenuScene() {
            buttonTexture = Shapes.Rect(Global.graphicsDevice, new Point(200, 100), 10, Color.Green, Color.Black);
            buttonTexture2 = Shapes.Rect(Global.graphicsDevice, new Point(200, 100), 10, Color.Yellow, Color.Black);
            buttonTexture3 = Shapes.Rect(Global.graphicsDevice, new Point(200, 100), 10, Color.Red, Color.Black);
            testButton = new Button(buttonTexture, new Point(300, 300), new Point(200, 100));

            bg = Shapes.Rect(Global.graphicsDevice, Global.resolution, 20, Color.Purple, Color.Red);
        }

        public void Update(float delta) {
            testButton.texture = 
                testButton.Pressed(Global.mousePos) 
                    ? buttonTexture3 
                    : testButton.Hovered(Global.mousePos) 
                        ? buttonTexture2 
                        : buttonTexture;
            
            testButton.position = (Global.resolution - testButton.size) / new Point(2);

            if (testButton.Pressed(Global.mousePos))
                Global.currentScene = new GameScene();
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(bg, Vector2.Zero, Color.White);
            testButton.Draw(spriteBatch);
        }
    }
}