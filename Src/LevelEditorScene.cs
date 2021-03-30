using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Utils;

namespace Src {
    enum LevelObject { Platform };
    class LevelEditorScene : Scene {
        
        Texture2D bg;
        PanelList objectMenu;
        Panel panel;
        Button hideMenuButton;
        Button saveLevelButton;
        Button button1;
        Button button2;
        Button button3;

        List<(LevelObject type, Rectangle transform)> levelObjects = new List<(LevelObject, Rectangle)>();
        Texture2D platformTexture;

        public LevelEditorScene() {
            bg = Helper.Rect(Global.graphicsDevice, new Point(10, 10), 0, Color.LightBlue, Color.LightBlue);
            platformTexture = Helper.ColorRect(Global.graphicsDevice, 10, 10, Color.Blue);

            var omSize = new Point(64, Global.resolution.Y - 32);
            var omPos = new Point(Global.resolution.X - omSize.X, 16);
            var omTexture = Helper.ColorRect(Global.graphicsDevice, omSize.X, omSize.Y, Color.Gray);
            objectMenu = new PanelList(Global.graphicsDevice, omTexture, omPos, omSize);

            var hmbTexture = Helper.ColorRect(Global.graphicsDevice, omSize.X, 16, Color.Gray);
            hideMenuButton = new Button(hmbTexture, new Point(omPos.X, 0), new Point(omSize.X, 16));

            var bTexture = Helper.ColorRect(Global.graphicsDevice, 32, 32, Color.Blue);
            button1 = new Button(bTexture, Point.Zero, new Point(32, 32));
            button2 = new Button(bTexture, Point.Zero, new Point(32, 32));
            button3 = new Button(bTexture, Point.Zero, new Point(32, 32));
            
            panel = new Panel(Global.graphicsDevice, null, omPos, omSize);
            panel.content.Add(button1);
            panel.content.Add(button2);
            panel.content.Add(button3);
            
            panel.Align(AlignItems.Vertically, 2);
            objectMenu.content.Add(panel);

            saveLevelButton = new Button(hmbTexture, Point.Zero, new Point(100, 50));

        }

        public void Update(float delta) {
            objectMenu.Update(Global.mousePos);
            if(hideMenuButton.JustReleased(Global.mousePos))
                objectMenu.Visible = !objectMenu.Visible;
            
            if(Input.IsMouseClicked(true) && Global.mousePos.X < objectMenu.Position.X - 32)
                levelObjects.Add((LevelObject.Platform, new Rectangle(Global.mousePos, new Point(200, 20))));
            
            if(saveLevelButton.JustPressed(Global.mousePos))
                SaveLevel();
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(bg, new Rectangle(Point.Zero, Global.resolution), Color.White);
            objectMenu.Draw(spriteBatch);
            hideMenuButton.Draw(spriteBatch);
            saveLevelButton.Draw(spriteBatch);
            foreach(var thing in levelObjects)
                spriteBatch.Draw(platformTexture, thing.transform, Color.White);
        }

        public void SaveLevel() {
            var stringList = new List<string>();
            foreach(var thing in levelObjects)
                stringList.Add($"{(int)thing.type} {thing.transform.X} {thing.transform.Y} {thing.transform.Width} {thing.transform.Height}");
            System.IO.File.WriteAllLines(@"Content\Test.level", stringList);
        }
    }
}