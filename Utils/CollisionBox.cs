using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;


namespace Utils {
    
    public enum CollisionDir {
        Up, Down, Left, Right, None
    }

    class CollisionBox {
        public Rectangle rect = new Rectangle();
        Texture2D texture;
        Color color = Color.Blue;

        public CollisionBox(Rectangle rect) {
            this.rect = rect;
        }

        public CollisionDir Collides(CollisionBox collisionBox, int collisionMargin) {
            bool CheckRange(int a, int b) {
                var t = a - b;
                if (t > -collisionMargin && t < collisionMargin)
                    return true;
                return false;
            }

            color = Color.Red;
            collisionBox.color = Color.Red;
            if (rect.Intersects(collisionBox.rect)) {
                if (CheckRange(rect.Top, collisionBox.rect.Bottom))
                    return CollisionDir.Up;
                if (CheckRange(rect.Bottom, collisionBox.rect.Top))
                    return CollisionDir.Down;
                if (CheckRange(rect.Left, collisionBox.rect.Right))
                    return CollisionDir.Left;
                if (CheckRange(rect.Right, collisionBox.rect.Left))
                    return CollisionDir.Right;
            }
            color = Color.Blue;
            collisionBox.color = Color.Blue;
            return CollisionDir.None;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) {
            if (texture == null)
                texture = Shapes.Rect(graphicsDevice, new Point(10, 10), 0, Color.White, Color.White);
            
            spriteBatch.Draw(texture, rect, color);
        }
    }
}