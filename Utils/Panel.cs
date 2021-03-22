using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Utils {
    class Panel : GuiElement {

        public Texture2D texture;
        public Point Position { get; set; }
        public Point Size { get; set; }

        bool visible;
        public bool Visible { 
            get { return visible; }
            set {
                visible = value;
                foreach(var thing in content)
                    thing.Visible = value;
            }
        }

        public List<GuiElement> content = new List<GuiElement>();

        public Panel(GraphicsDevice graphicsDevice, Texture2D texture, Point position, Point size, bool visible = true) {
            this.texture = texture;
            this.Position = position;
            this.Size = size;
            this.Visible = visible;
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (!Visible)
                return;
            
            if (texture != null)
                spriteBatch.Draw(texture, new Rectangle(Position, Size), Color.White);

            foreach(var thing in content)
                thing.Draw(spriteBatch);
        }

        public void UpdateVertically(float separationSpace, Point mousePos) {
            var columnSize = 0f;
            foreach(var thing in content)
                columnSize += thing.Size.Y * separationSpace;
                
            for(var i = 0; i < content.Count; i++) {
                var offsetX = (Size.X - content[i].Size.X) / 2;

                var thing = i != 0 ? (content[i].Size.Y + content[i - 1].Size.Y) / 2 : 1;
                var offsetY = (int)(thing * separationSpace) * i + Size.Y / 2 - (int)columnSize / 2;
                content[i].Position = Position + new Point(offsetX, offsetY);
            }
        }
    }
}