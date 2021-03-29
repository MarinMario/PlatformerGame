using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Utils {
    interface Scene {
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }

    interface GuiElement {
        void Draw(SpriteBatch spriteBatch);
        Point Position { get; set; }
        Point Size { get; set; }
        bool Visible { get; set; }
    }

    interface Collider {
        Rectangle CollisionBox { get; set; }
    }
}