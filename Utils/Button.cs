using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Utils {
    class Button : GuiElement {

        public Texture2D texture;
        public Texture2D ogTexture;
        public Texture2D hoverTexture;
        public Texture2D pressTexture;

        Point position;
        public Point Position { 
            get { return position; }
            set {
                position = value;
                if(label != null) {
                    label.Position = value;
                    label.Center();
                }
            }
         }
        public Point Size { get; set; }

        bool visible;
        public bool Visible { 
            get { return visible; }
            set {
                visible = value;
                if(label != null)
                    label.Visible = value;
            }
        }

        bool previousPressed = false;
        bool currentPressed = false;
        public Label label;


        public Button(Texture2D texture, Point position, Point size, SpriteFont font = null, Color? textColor = null, string text = "", bool visible = true) {
            this.texture = texture;
            this.ogTexture = texture;
            this.Position = position;
            this.Size = size;
            this.Visible = visible;
            if (font != null && textColor != null) {
                label = new Label(position, size, font, text, (Color)textColor);
                label.Center();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (Visible)
                spriteBatch.Draw(texture, new Rectangle(Position, Size), Color.White);

            if(label != null)
                label.Draw(spriteBatch);
        }

        public bool Hovered(Point mousePos) {
            var rect = new Rectangle(Position, Size);
            return rect.Contains(mousePos) && Visible;
        }

        public bool Pressed(Point mousePos) {
            return Hovered(mousePos) && Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

        public bool JustPressed(Point mousePos) {
            previousPressed = currentPressed;
            currentPressed = Pressed(mousePos);
            return currentPressed && !previousPressed;
        }

        public bool JustReleased(Point mousePos) {
            previousPressed = currentPressed;
            currentPressed = Pressed(mousePos);
            return !currentPressed && previousPressed && Hovered(mousePos);
        }

        public void SetTextureByState(Texture2D onHovered, Texture2D onPressed, Point mousePos) {
            if (Pressed(mousePos))
                texture = onPressed;
            else if (Hovered(mousePos))
                texture = onHovered;
            else
                texture = ogTexture;
        }
    }
}