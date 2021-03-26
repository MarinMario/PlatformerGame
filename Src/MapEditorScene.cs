using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Utils;

namespace DeliverBullets {
    class MapEditorScene : Scene {
        
        Texture2D bg;
        PanelList objectMenu;
        Panel panel;
        Button hideMenuButton;
        List<(Point pos, Texture2D texture)> stuff = new List<(Point pos, Texture2D texture)>();

        Texture2D texture1;
        Texture2D texture2;
        Texture2D texture3;

        Texture2D selectedTexture;

        Button button1;
        Button button2;
        Button button3;

        public MapEditorScene() {
            bg = Shapes.ColorRect(Global.graphicsDevice, Global.resolution.X, Global.resolution.Y, Color.Gray);

            var omSize = new Point(64, Global.resolution.Y - 32);
            var omPos = new Point(Global.resolution.X - omSize.X, 16);
            var omTexture = Shapes.ColorRect(Global.graphicsDevice, omSize.X, omSize.Y, Color.Gray);
            objectMenu = new PanelList(Global.graphicsDevice, omTexture, omPos, omSize);

            var hmbTexture = Shapes.ColorRect(Global.graphicsDevice, omSize.X, 16, Color.Gray);
            hideMenuButton = new Button(hmbTexture, new Point(omPos.X, 0), new Point(omSize.X, 16));

            texture1 = Shapes.ColorRect(Global.graphicsDevice, 32, 32, Color.Purple);
            texture2 = Shapes.ColorRect(Global.graphicsDevice, 32, 32, Color.Blue);
            texture3 = Shapes.ColorRect(Global.graphicsDevice, 32, 32, Color.Red);
            selectedTexture = texture1;

            var bTexture = Shapes.ColorRect(Global.graphicsDevice, 32, 32, Color.Blue);
            button1 = new Button(bTexture, Point.Zero, new Point(32, 32));
            button2 = new Button(bTexture, Point.Zero, new Point(32, 32));
            button3 = new Button(bTexture, Point.Zero, new Point(32, 32));
            
            panel = new Panel(Global.graphicsDevice, null, omPos, omSize);
            panel.content.Add(button1);
            panel.content.Add(button2);
            panel.content.Add(button3);
            
            panel.Align(AlignItems.Vertically, 2);
            objectMenu.content.Add(panel);

        }

        public void Update(float delta) {
            objectMenu.Update(Global.mousePos);
            if(hideMenuButton.JustReleased(Global.mousePos))
                objectMenu.Visible = !objectMenu.Visible;
            
            if(Mouse.GetState().LeftButton == ButtonState.Pressed && Global.mousePos.X < objectMenu.Position.X - 32)
                stuff.Add((ConvertToGrid(Global.mousePos), selectedTexture));
            

            if(button1.JustReleased(Global.mousePos))
                selectedTexture = texture1;
            if(button2.JustReleased(Global.mousePos))
                selectedTexture = texture2;
            if(button3.JustReleased(Global.mousePos))
                selectedTexture = texture3;
        }

        Point ConvertToGrid(Point pos) {
            return new Point(32) * (pos / new Point(32));
        }

        public void Draw(SpriteBatch spriteBatch) {
            // spriteBatch.Draw(bg, Vector2.Zero, Color.White);
            objectMenu.Draw(spriteBatch);
            hideMenuButton.Draw(spriteBatch);
            foreach(var thing in stuff)
                spriteBatch.Draw(thing.texture, thing.pos.ToVector2(), Color.White);
        }

    }
}