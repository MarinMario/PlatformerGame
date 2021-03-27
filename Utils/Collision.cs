using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Utils {
    class CollisionBox {
        public Rectangle rect;
        public CollisionBox(Rectangle rect) {
            this.rect = rect;
        }
    }
    class Collision {
        
        public List<CollisionBox> bodies = new List<CollisionBox>();

        public (int x, int y, List<Rectangle> collidingRects) CheckCollision(Rectangle rect, int margin) {
            bool CheckRange(int a, int b) {
                var t = a - b;
                if (t > -margin && t < margin)
                    return true;
                return false;
            }

            var collision = (x: 0, y: 0, collidingRects: new List<Rectangle>{});
            foreach (var collisionBox in bodies)
                if (rect != collisionBox.rect && rect.Intersects(collisionBox.rect)) {
                    if (CheckRange(rect.Left, collisionBox.rect.Right))
                        collision.x = -1;
                    if (CheckRange(rect.Right, collisionBox.rect.Left))
                        collision.x = 1;
                    if (CheckRange(rect.Top, collisionBox.rect.Bottom))
                        collision.y = -1;
                    if (CheckRange(rect.Bottom, collisionBox.rect.Top))
                        collision.y = 1;
                    collision.collidingRects.Add(collisionBox.rect);
                }
            return collision;
        }
    }
}