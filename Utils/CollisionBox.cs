using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Utils {
    class CollisionBox {
        public Rectangle rect = new Rectangle();
        Texture2D texture;
        public Color color = Color.Blue;

        public CollisionBox(Rectangle rect) {
            this.rect = rect;
        }

        public (int, int) Collides(CollisionBox collisionBox, int collisionMargin) {
            bool CheckRange(int a, int b) {
                var t = a - b;
                if (t > -collisionMargin && t < collisionMargin)
                    return true;
                return false;
            }

            if (rect.Intersects(collisionBox.rect)) {
                if (CheckRange(rect.Top, collisionBox.rect.Bottom))
                    return (0, -1);
                if (CheckRange(rect.Bottom, collisionBox.rect.Top))
                    return (0, 1);
                if (CheckRange(rect.Left, collisionBox.rect.Right))
                    return (-1, 0);
                if (CheckRange(rect.Right, collisionBox.rect.Left))
                    return (1, 0);
            }
            return (0, 0);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) {
            if (texture == null)
                texture = Shapes.Rect(graphicsDevice, new Point(10, 10), 0, Color.White, Color.White);
            
            spriteBatch.Draw(texture, rect, color);
        }
    }
}