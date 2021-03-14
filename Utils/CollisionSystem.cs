using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;


namespace Utils {
    class CollisionSystem {

        public List<CollisionBox> staticBodies = new List<CollisionBox>();
        public List<CollisionBox> movingBodies= new List<CollisionBox>();
        public int collisionMargin = 10;

        public CollisionSystem() {
        }

        public void Update() {
            // static body - moving body collision
            for(var mb = 0; mb < movingBodies.Count; mb++)
                for(var sb = 0; sb < staticBodies.Count; sb++) {
                    var collision = movingBodies[mb].Collides(staticBodies[sb], collisionMargin);
                    var mbo = movingBodies[mb];
                    var sbo = staticBodies[sb];
                    switch (collision) {
                        case CollisionDir.Up:
                            mbo.rect.Location = new Point(mbo.rect.Location.X, sbo.rect.Bottom);
                            break;
                        case CollisionDir.Down:
                            mbo.rect.Location = new Point(mbo.rect.Location.X, sbo.rect.Top - mbo.rect.Height);
                            break;
                        case CollisionDir.Left:
                             mbo.rect.Location = new Point(sbo.rect.Right, mbo.rect.Location.Y);
                            break;
                        case CollisionDir.Right:
                             mbo.rect.Location = new Point(sbo.rect.Left - mbo.rect.Width, mbo.rect.Location.Y);
                            break;
                    }
                }
        }


        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) {
            foreach(var body in movingBodies)
                body.Draw(spriteBatch, graphicsDevice);
            foreach(var body in staticBodies)
                body.Draw(spriteBatch, graphicsDevice);
        }
    }
}