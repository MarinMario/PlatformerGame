using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Utils {
    class Collision
    {
        
        public List<Collider> bodies = new List<Collider>();

        public (int x, int y, List<Collider> collidingBodies) CheckCollision(Rectangle rect, int margin)
        {
            bool CheckRange(int a, int b) {
                var t = a - b;
                if (t > -margin && t < margin)
                    return true;
                return false;
            }

            var collision = (x: 0, y: 0, collidingBodies: new List<Collider>{});
            foreach (var collidingBody in bodies)
                if (rect != collidingBody.CollisionBox && rect.Intersects(collidingBody.CollisionBox)) 
                {
                    if (CheckRange(rect.Left, collidingBody.CollisionBox.Right))
                        collision.x = -1;
                    if (CheckRange(rect.Right, collidingBody.CollisionBox.Left))
                        collision.x = 1;
                    if (CheckRange(rect.Top, collidingBody.CollisionBox.Bottom))
                        collision.y = -1;
                    if (CheckRange(rect.Bottom, collidingBody.CollisionBox.Top))
                        collision.y = 1;
                    collision.collidingBodies.Add(collidingBody);
                }
            return collision;
        }
    }
}