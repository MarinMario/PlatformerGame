using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DeliverBullets {
    class Platform {

        public CollisionBox collisionBox;

        public Platform(Rectangle rectangle, Collision collision) {
            collisionBox = new CollisionBox(rectangle);
            collision.bodies.Add(collisionBox);
        }
    }
}