using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Utils {
    class Button {

        public Texture2D texture;
        public Point position;
        public Point size;

        public Button(Texture2D texture, Point position, Point size) {
            this.texture = texture;
            this.position = position;
            this.size = size;
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(texture, new Rectangle(position, size), Color.White);
        }

        public bool Hovered() {
            var rect = new Rectangle(position, size);
            return rect.Contains(Mouse.GetState().Position);
        }
        public bool Pressed() {
            return Hovered() && Mouse.GetState().LeftButton == ButtonState.Pressed;
        }
    }
}