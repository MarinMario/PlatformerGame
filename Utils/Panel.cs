using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Utils {

    enum AlignItems { Horizontally, Vertically, None }

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

        public void Align(AlignItems alignItems, int separationSpace) {
            var elemnSize = (x: 0, y: 0);
            for(var i = 0; i < content.Count; i++) {
                elemnSize.x += content[i].Size.X + separationSpace;
                elemnSize.y += content[i].Size.Y + separationSpace;
            }
            
            switch (alignItems) {
                case AlignItems.Vertically:
                    for(var i = 0; i < content.Count; i++) {
                        var lastElemPos = i != 0 ? content[i - 1].Position.Y + content[i -1].Size.Y - Position.Y : 0;
                        var offsetY = lastElemPos + separationSpace;

                        content[i].Position = Position + new Point(0, offsetY);
                    }

                    for(var i = 0; i < content.Count; i++) {
                        var offsetX = (Size.X - content[i].Size.X) / 2;
                        var offsetY = (Size.Y - elemnSize.y) / 2;
                        content[i].Position += new Point(offsetX, offsetY);
                     }
                    break;
                case AlignItems.Horizontally:
                    for(var i = 0; i < content.Count; i++) {
                        var lastElemPos = i != 0 ? content[i - 1].Position.X + content[i - 1].Size.X - Position.X : 0;
                        var offsetX = lastElemPos + separationSpace;

                        content[i].Position = Position + new Point(offsetX, 0);
                    }

                    for(var i = 0; i < content.Count; i++) {
                        var offsetY = (Size.Y - content[i].Size.Y) / 2;
                        var offsetX = (Size.X - elemnSize.x) / 2;
                        content[i].Position += new Point(offsetX, offsetY);
                     }
                    break;
                case AlignItems.None:
                    break;
            }

        }
    }
}