using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Utils {
    class PanelList : GuiElement {

        public Texture2D texture;
        Button prevPage;
        Button nextPage;
        Texture2D pageButtonNormal;
        Texture2D pageButtonHover;
        Texture2D pageButtonPress;
    

        Point position;
        public Point Position { 
            get { return position; }
            set {
                position = value;
                foreach(var thing in content)
                    thing.Position = position;
                if (prevPage != null && nextPage != null) {
                    prevPage.Position =  position + new Point(0, Size.Y);
                    nextPage.Position = Size / new Point(2, 1);
                }
            }

        }
        public Point Size { get; set; }
        public bool Visible { get; set; }

        public List<Panel> content = new List<Panel>();
        int pageCount = 0;


        public PanelList(GraphicsDevice graphicsDevice, Texture2D texture, Point position, Point size, bool visible = true) {
            this.texture = texture;
            this.Position = position;
            this.Size = size;
            this.Visible = visible;

            var buttonSize = new Point(size.X / 2, 50);
            pageButtonNormal = Shapes.ColorRect(graphicsDevice, buttonSize.X, buttonSize.Y, Color.Gray);
            pageButtonHover = Shapes.ColorRect(graphicsDevice, buttonSize.X, buttonSize.Y, 
                new Color(Color.Gray.ToVector3() - Vector3.One * 0.1f));
            pageButtonPress = Shapes.ColorRect(graphicsDevice, buttonSize.X, buttonSize.Y, 
                new Color(Color.Gray.ToVector3() - Vector3.One * 0.2f));
            
            prevPage = new Button(pageButtonNormal, position + new Point(0, size.Y), buttonSize);
            nextPage = new Button(pageButtonNormal, position + new Point(size.X / 2, size.Y), buttonSize);
        }

        public void Update(Point mousePos) {
            nextPage.SetTextureByState(pageButtonHover, pageButtonPress, mousePos);
            prevPage.SetTextureByState(pageButtonHover, pageButtonPress, mousePos);
            PageUpdate(mousePos);
            foreach(var thing in content)
                thing.Update(AlignItems.Horizontally, 2);
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (!Visible)
                return;
            
            if (texture != null)
                spriteBatch.Draw(texture, new Rectangle(Position, Size), Color.White);

            foreach(var thing in content)
                thing.Draw(spriteBatch);

            nextPage.Draw(spriteBatch);
            prevPage.Draw(spriteBatch);
        }

        public void PageUpdate(Point mousePos) {
            if (nextPage.JustPressed(mousePos)) {
                pageCount += 1;
                if (pageCount > content.Count - 1)
                    pageCount = 0;
            }
            if (prevPage.JustPressed(mousePos)) {
                pageCount -= 1;
                if (pageCount < 0)
                    pageCount = content.Count - 1;
            }

            for(var i = 0; i < content.Count; i++) {
                content[i].Visible = i == pageCount && Visible;
            }
        }
    }
}