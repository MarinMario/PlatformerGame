using Microsoft.Xna.Framework.Graphics;

namespace Utils {
    interface Scene {
        void Update(float delta);
        void Draw(SpriteBatch spriteBatch);
    }
}