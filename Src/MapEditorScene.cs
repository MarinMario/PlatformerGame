using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Utils;

namespace DeliverBullets {
    class MapEditorScene : Scene {

        PanelList panelList;
        Panel panel3;

        Button selectTexture;

        public MapEditorScene() {
            var panelSize = new Point(Global.resolution.X - 300, 500);
            var panelPos = new Point(100, 100);
            var panelTexture = Shapes.ColorRect(Global.graphicsDevice, 300, Global.resolution.Y, Color.Gray);
            var buttonTexture = Shapes.ColorRect(Global.graphicsDevice, 100, 100, Color.Purple);
            
            var panel = new Panel(Global.graphicsDevice, null, panelPos, panelSize);
            panel.content.Add(new Button(buttonTexture, new Point(100, 100), new Point(100, 100)));
            panel.content.Add(new Button(buttonTexture, Point.Zero, new Point(120, 150)));
            panel.content.Add(new Button(buttonTexture, new Point(500, 300), new Point(100, 100)));
            panel.content.Add(new Button(buttonTexture, Point.Zero, new Point(100, 100)));
            var panel2 = new Panel(Global.graphicsDevice, null, panelPos, panelSize);
            panel2.content.Add(new Button(buttonTexture, Point.Zero, new Point(100, 100)));
            panel2.content.Add(new Button(buttonTexture, Point.Zero, new Point(150, 200)));
            panel3 = new Panel(Global.graphicsDevice, null, panelPos, new Point(200, 100));
            panel3.content.Add(new Button(buttonTexture, Point.Zero, new Point(100, 100)));
            panel3.content.Add(new Button(buttonTexture, new Point(400, 30), new Point(150, 100)));
            panel3.content.Add(new Button(buttonTexture, Point.Zero, new Point(120, 100)));
            panel2.content.Add(panel3);
    

            panelList = new PanelList(Global.graphicsDevice, panelTexture, panelPos, panelSize);
            panelList.content.Add(panel2);
            panelList.content.Add(panel);
            // panelList.content.Add(panel3);

            selectTexture = new Button(
                Shapes.Rect(Global.graphicsDevice, new Point(200, 200), 10, Color.Blue, Color.Red), 
                new Point(200, 200), new Point(200, 200));
        }

        public void Update(float delta) {
            panel3.Update(AlignItems.Vertically, 10);
            panelList.Update(Global.mousePos);
        }

        public void Draw(SpriteBatch spriteBatch) {
            panelList.Draw(spriteBatch);
            // selectTexture.Draw(spriteBatch);
        }

    }
}