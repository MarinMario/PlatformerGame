using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Utils;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Src {
    enum LevelObject { Platform, MovingPlatform, Wall, None };
    class LevelEditorScene : Scene {

        Button closeMenuButton;
        Button saveLevelButton;
        PanelList menu;
        Panel page1;
        Texture2D buttonTexture;
        Texture2D buttonHover;
        Texture2D buttonPress;
        Point bannedEditingArea = Point.Zero;
        Button selectedButton = null;
        public LevelEditorScene() 
        {
            //style
            var color = new Color(255, 226, 104);
            var borderColor = new Color(54, 69, 71);
            var borderSize = 10;
            var borderColorHover = new Color(35, 45, 46);
            var colorPress = new Color(255, 176, 55);

            var buttonSize = new Point(200, 50);
            buttonTexture = Helper.Rect(Global.graphicsDevice, buttonSize, borderSize, color, borderColor);
            buttonHover = Helper.Rect(Global.graphicsDevice, buttonSize, borderSize, color, borderColorHover);
            buttonPress = Helper.Rect(Global.graphicsDevice, buttonSize, borderSize, colorPress, borderColorHover);

            //close menu button
            var closeMenuButtonPos = new Point(Global.cameraPos.X + Global.resolution.X - buttonSize.X, 0);
            closeMenuButton = new Button(buttonTexture, closeMenuButtonPos, buttonSize, Global.font, borderColor, "Hide");
            
            //menu
            var menuSize = new Point(buttonSize.X, Global.resolution.Y - buttonSize.Y * 2);
            var menuTexture = Helper.Rect(Global.graphicsDevice, menuSize, borderSize, color, borderColor);
            var menuPos = new Point(Global.cameraPos.X + Global.resolution.X - menuSize.X, buttonSize.Y);
            var menuButtonSize = new Point(buttonSize.X / 2, buttonSize.Y);
            var menuButtonTexture = Helper.Rect(Global.graphicsDevice, menuButtonSize, borderSize, color, borderColor);
            menu = new PanelList(Global.graphicsDevice, menuTexture, menuPos, menuSize, menuButtonTexture, menuButtonSize);
            menu.pageButtonHover = Helper.Rect(Global.graphicsDevice, menuButtonSize, borderSize, color, borderColorHover);
            menu.pageButtonPress = Helper.Rect(Global.graphicsDevice, menuButtonSize, borderSize, colorPress, borderColorHover);

            //first page in menu
            page1 = new Panel(Global.graphicsDevice, null, Point.Zero, menuSize, AlignItems.Vertically, 0);
            page1.AddElement(new Button(buttonTexture, Point.Zero, buttonSize, Global.font, borderColor, LevelObject.Wall.ToString()));
            page1.AddElement(new Button(buttonTexture, Point.Zero, buttonSize, Global.font, borderColor, LevelObject.Platform.ToString()));
            page1.AddElement(new Button(buttonTexture, Point.Zero, buttonSize, Global.font, borderColor, LevelObject.MovingPlatform.ToString()));
            menu.AddPanel(page1);

            //button for saving the level
            var saveLevelButtonPos = new Point(Global.cameraPos.X, 0);
            saveLevelButton = new Button(buttonTexture, saveLevelButtonPos, buttonSize, Global.font, borderColor, "Save");

            //set editing area
            bannedEditingArea = buttonSize;
        }

        public void Update(float delta) 
        {
            menu.Update(Global.mousePos);
            if(closeMenuButton.JustReleased(Global.mousePos))
                menu.Visible = !menu.Visible;
            
            closeMenuButton.SetTextureByState(buttonHover, buttonPress, Global.mousePos);
            saveLevelButton.SetTextureByState(buttonHover, buttonPress, Global.mousePos);


            //this assigns to selectedButton what button was last selected and also sets the texture for the selectedButton
            foreach(var page in menu.content)
                foreach(var thing in page.content)
                    if(thing is Button) 
                    {
                        var btn = (Button)thing;
                        btn.SetTextureByState(buttonHover, buttonPress, Global.mousePos);
                        if(btn.JustReleased(Global.mousePos))
                        {
                            if(selectedButton != null)
                                selectedButton.ogTexture = buttonTexture;
                            btn.ogTexture = buttonPress;
                            selectedButton = btn;
                        }
                    }
            
            if(Input.IsMouseClicked(true) && Global.mousePos.X < bannedEditingArea.X && Global.mousePos.Y > bannedEditingArea.Y)
            {
                
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            closeMenuButton.Draw(spriteBatch);
            saveLevelButton.Draw(spriteBatch);
            menu.Draw(spriteBatch);
        }
    }
}