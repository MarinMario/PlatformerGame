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
        public Texture2D pageButtonNormal;
        public Texture2D pageButtonHover;
        public Texture2D pageButtonPress;
    

        Point position;
        public Point Position { 
            get { return position; }
            set {
                position = value;
                foreach(var thing in content)
                    thing.Position = position;
                if (prevPage != null && nextPage != null) {
                    prevPage.Position = position + new Point(0, Size.Y);
                    nextPage.Position = position + new Point(Size.X - nextPage.Size.X, Size.Y);
                }
            }

        }
        public Point Size { get; set; }
        public bool Visible { get; set; }

        public List<Panel> content = new List<Panel>();
        int pageCount = 0;


        public PanelList(GraphicsDevice graphicsDevice, Texture2D texture, Point position, Point size, Texture2D btn, Point btnSize, bool visible = true) {
            this.texture = texture;
            this.Position = position;
            this.Size = size;
            this.Visible = visible;
            pageButtonNormal = pageButtonHover = pageButtonPress = btn;
            prevPage = new Button(pageButtonNormal, position + new Point(0, size.Y), btnSize);
            nextPage = new Button(pageButtonNormal, position + new Point(size.X - btnSize.X, size.Y), btnSize);
        }

        public void Update(Point mousePos) {
            nextPage.SetTextureByState(pageButtonHover, pageButtonPress, mousePos);
            prevPage.SetTextureByState(pageButtonHover, pageButtonPress, mousePos);
            PageUpdate(mousePos);
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

        void PageUpdate(Point mousePos) {
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

        public void AddPanel(Panel panel) {
            content.Add(panel);
            panel.Position = Position;
        }

        public void RemovePanel(Panel panel) {
            content.Remove(panel);
        }
    }
}