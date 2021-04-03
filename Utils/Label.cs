using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Utils {
    class Label : GuiElement {

        public Point Position { get; set; }
        public Point Size { get; set; }
        public bool Visible { get; set; }
        public SpriteFont font;
        public Color color;
        public string text;
        Point updatedPosition;

        public Label(Point position, Point size, SpriteFont font, string text, Color color, bool Visible = true) {
            this.Position = position; 
            this.updatedPosition = position;
            this.Size = size; 
            this.font = font; 
            this.text = text;
            this.color = color;
            this.Visible = Visible;
        }

        public void Draw(SpriteBatch spriteBatch) {
            if(Visible)
                spriteBatch.DrawString(font, text, updatedPosition.ToVector2(), color);
        }

        public void Center() {
            var textSize =  font.MeasureString(text) / 2;
            updatedPosition = Position + Size / new Point(2) - textSize.ToPoint();
        }
    }
}