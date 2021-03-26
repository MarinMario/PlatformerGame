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
        public Point Position { get; set; }
        public Point Size { get; set; }
        public bool Visible { get; set; }

        bool previousPressed = false;
        bool currentPressed = false;


        public Button(Texture2D texture, Point position, Point size, bool visible = true) {
            this.texture = texture;
            this.ogTexture = texture;
            this.Position = position;
            this.Size = size;
            this.Visible = visible;
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (Visible)
                spriteBatch.Draw(texture, new Rectangle(Position, Size), Color.White);
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