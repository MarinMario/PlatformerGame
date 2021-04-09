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
    enum ObjectName { Platform, Wall, MovingPlatform };

    class LevelObject
    {
        public ObjectName name;
        public Point position;
        public Point size;
        public Point? targetPosition;
        
        public LevelObject(ObjectName name, Point position, Point size, Point? targetPosition = null)
        {
            this.name = name; this.position = position; this.size = size; this.targetPosition = targetPosition;
        }
    }
    
    class LevelEditorScene : Scene {

        Button closeMenuButton;
        Button saveLevelButton;
        Button playButton;
        PanelList menu;
        Texture2D buttonTexture;
        Texture2D buttonHover;
        Texture2D buttonPress;
        Point bannedEditingArea = Point.Zero;
        Button selectedButton = null;
        List<LevelObject> levelObjects = new List<LevelObject>();
        (Texture2D texture, Point size, bool visible) placingTexture;
        
        Point movingPlatformTargetSize = new Point(50);
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
            var page1 = new Panel(Global.graphicsDevice, null, Point.Zero, menuSize, AlignItems.Vertically, 0);
            foreach(var levelObject in Enum.GetValues(typeof(ObjectName)))
                page1.AddElement(new Button(buttonTexture, Point.Zero, buttonSize, Global.font, borderColor, levelObject.ToString()));
            var page2 = new Panel(Global.graphicsDevice, null, Point.Zero, menuSize, AlignItems.Vertically, 0);
            
            menu.AddPanel(page1);
            menu.AddPanel(page2);

            //button for saving the level
            var saveLevelButtonPos = new Point(Global.cameraPos.X, 0);
            saveLevelButton = new Button(buttonTexture, saveLevelButtonPos, buttonSize, Global.font, borderColor, "Save");

            //play button
            var playButtonPos = new Point(saveLevelButtonPos.X + buttonSize.X, 0);
            playButton = new Button(buttonTexture, playButtonPos, buttonSize, Global.font, borderColor, "Play");

            //set editing area
            bannedEditingArea = new Point(Global.resolution.X - buttonSize.X, buttonSize.Y);

            placingTexture.texture = Helper.Rect(Global.graphicsDevice, new Point(1), 0, color, color);

        }

        public void Update(float delta) 
        {
            menu.Update(Global.mousePos);
            if(closeMenuButton.JustReleased(Global.mousePos))
                menu.Visible = !menu.Visible;
            
            closeMenuButton.SetTextureByState(buttonHover, buttonPress, Global.mousePos);
            saveLevelButton.SetTextureByState(buttonHover, buttonPress, Global.mousePos);
            playButton.SetTextureByState(buttonHover, buttonPress, Global.mousePos);

            if(saveLevelButton.JustReleased(Global.mousePos))
                SaveLevel();
            
            if(playButton.JustReleased(Global.mousePos))
                Global.currentScene = new GameScene();


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


            EditorLogic();
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            foreach(var thing in levelObjects)
            {
                if(thing.targetPosition != null)
                {   
                    //draw line between targetPosition and position
                    var pos1 = thing.position + thing.size / new Point(2);
                    var pos2 = (Point)thing.targetPosition + movingPlatformTargetSize / new Point(2);
                    Helper.DrawLine(spriteBatch, placingTexture.texture, pos1, pos2, 10);
                    
                    //draw square for targetPosition
                    spriteBatch.Draw(placingTexture.texture, new Rectangle((Point)thing.targetPosition, movingPlatformTargetSize), Color.White);
                }
                //draw object
                spriteBatch.Draw(placingTexture.texture, new Rectangle(thing.position, thing.size), Color.White);
            }
            if(placingTexture.visible)
                spriteBatch.Draw(placingTexture.texture, new Rectangle(clickPosition, placingTexture.size), Color.White);
            closeMenuButton.Draw(spriteBatch);
            saveLevelButton.Draw(spriteBatch);
            playButton.Draw(spriteBatch);
            menu.Draw(spriteBatch);
        }

        Point clickPosition = Point.Zero;
        ObjectName? newObject = null;
        LevelObject selectedObject = null;
        Point? selectedTargetPoint = null;

        //this function handles shit like adding, moving, removing objects
        void EditorLogic()
        {   
            var mouseState = Input.GetMouseState();

            //add new object when mouse is clicked
            if(Input.IsMouseClicked(MouseButton.Left, true)
            && Global.mousePos.X < bannedEditingArea.X 
            && Global.mousePos.Y > bannedEditingArea.Y)
            {
                foreach(var thing in levelObjects)
                {
                    if(new Rectangle(thing.position, thing.size).Contains(Global.mousePos))
                    {
                        selectedObject = thing;
                        break;
                    }
                    else if(thing.targetPosition != null)
                        if(new Rectangle((Point)thing.targetPosition, movingPlatformTargetSize).Contains(Global.mousePos))
                        {
                            selectedObject = thing;
                            selectedTargetPoint = thing.targetPosition;
                        }
                }
                
            
                if(selectedObject == null && selectedTargetPoint == null && selectedButton != null)
                {
                    var obj = Enum.Parse<ObjectName>(selectedButton.label.text);
                    newObject = obj;
                    clickPosition = Global.mousePos;
                    placingTexture.visible = true;
                }
            }

            var size = Global.mousePos - clickPosition;
            if(size.X < 15) size.X = 15;
            if(size.Y < 15) size.Y = 15;

            if(mouseState.LeftButton == ButtonState.Released)
            {
                selectedObject = null;
                selectedTargetPoint = null;
                if(newObject != null)
                {
                    if(newObject == ObjectName.MovingPlatform)
                        levelObjects.Add(new LevelObject((ObjectName)newObject, clickPosition, size, clickPosition + new Point(size.X + 20, 0)));
                    else
                        levelObjects.Add(new LevelObject((ObjectName)newObject, clickPosition, size));
                    newObject = null;
                    placingTexture.visible = false;
                }
            }

            //move object when you click on it
            if(selectedObject != null)
            {
                if(selectedTargetPoint != null)
                    selectedObject.targetPosition = Global.mousePos - new Point(25);
                else
                    selectedObject.position = Global.mousePos - selectedObject.size / new Point(2);
            }

            //remove object when clicking right mouse button on it
            if(Input.IsMouseClicked(MouseButton.Right, true))
                for(var i = levelObjects.Count - 1; i >= 0; i--)
                    if(new Rectangle(levelObjects[i].position, levelObjects[i].size).Contains(Global.mousePos))
                    {
                        levelObjects.RemoveAt(i);
                        break;
                    }

            if(placingTexture.visible)
                placingTexture.size = size;
        }

        void SaveLevel()
        {
            List<string> data = new List<string>();
            foreach(var thing in levelObjects)
                if(thing.name != ObjectName.MovingPlatform)
                    data.Add($"{thing.name.ToString()} {thing.position.X} {thing.position.Y} {thing.size.X} {thing.size.Y}");
                else
                    data.Add(
                    thing.name.ToString() + " " +
                    thing.position.X.ToString() + " " + thing.position.Y.ToString() + " " +
                    thing.size.X.ToString() + " " + thing.size.Y.ToString() + " " +
                    ((Point)thing.targetPosition).X.ToString() + " " + ((Point)thing.targetPosition).Y.ToString());

            System.IO.File.WriteAllLines(@"Content\Test.level", data);
        }

        public static List<LevelObject> LoadLevel()
        {
            var toReturn = new List<LevelObject>();
            var file = System.IO.File.ReadAllLines(@"Content\Test.level");
            foreach(var line in file)
            {
                var keys = line.Split(' ');
                var name = Enum.Parse<ObjectName>(keys[0]);
                var pos = new Point(int.Parse(keys[1]), int.Parse(keys[2]));
                var size = new Point(int.Parse(keys[3]), int.Parse(keys[4]));
                var targetPos = keys.Length > 5 ? (Point?)(new Point(int.Parse(keys[5]), int.Parse(keys[6]))) : null;
                toReturn.Add(new LevelObject(name, pos, size, targetPos));
            }
            return toReturn;
        }
    }
}