using Microsoft.Xna.Framework.Input;

namespace Utils {
    static class Input {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public static bool IsKeyPressed(Keys key, bool oneShot)
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            
            if(!oneShot) return currentKeyState.IsKeyDown(key);
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }
    }
}