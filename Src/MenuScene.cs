using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Src {
    class MenuScene : Scene {

        Texture2D buttonTexture;
        Texture2D buttonTexture2;
        Texture2D buttonTexture3;

        Panel panel;
        Button gameScene;
        Button mapEditorScene;
        
        Texture2D bg;

        public MenuScene() {
            buttonTexture = Helper.ColorRect(Global.graphicsDevice, 64, 32, Color.Green);
            buttonTexture2 = Helper.ColorRect(Global.graphicsDevice, 64, 32, Color.Orange);
            buttonTexture3 = Helper.ColorRect(Global.graphicsDevice, 64, 32, Color.Red);
            gameScene = new Button(buttonTexture, Point.Zero, new Point(64, 32), Global.font, Color.White, "Start");
            mapEditorScene = new Button(buttonTexture, Point.Zero, new Point(64, 32), Global.font, Color.White, "Level Editor");

            panel = new Panel(Global.graphicsDevice, null, Global.resolution / new Point(2), Point.Zero, AlignItems.Vertically);
            panel.AddElement(gameScene);
            panel.AddElement(mapEditorScene);

            bg = Helper.ColorRect(Global.graphicsDevice, Global.resolution.X, Global.resolution.Y, Color.Purple);
        }

        public void Update(float delta) {
            gameScene.SetTextureByState(buttonTexture2, buttonTexture3, Global.mousePos);
            mapEditorScene.SetTextureByState(buttonTexture2, buttonTexture3, Global.mousePos);
            if (gameScene.JustReleased(Global.mousePos))
                Global.currentScene = new GameScene();
            if (mapEditorScene.JustReleased(Global.mousePos))
                Global.currentScene = new LevelEditorScene();       
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(bg, Vector2.Zero, Color.White);
            panel.Draw(spriteBatch);
        }
    }
}