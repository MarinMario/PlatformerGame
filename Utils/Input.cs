using Microsoft.Xna.Framework.Input;

namespace Utils {
    static class Input {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;
        static MouseState currentMouseState;
        static MouseState previousMouseState;

        public static bool IsKeyPressed(Keys key, bool oneShot = false) {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            
            if(!oneShot) return currentKeyState.IsKeyDown(key);
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        public static bool IsMouseClicked(bool oneShot = false) {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            
            if(!oneShot) return currentMouseState.LeftButton == ButtonState.Pressed;
            return currentMouseState.LeftButton == ButtonState.Pressed 
                && previousMouseState.LeftButton == ButtonState.Released;
        }
    }
}