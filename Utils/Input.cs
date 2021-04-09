using Microsoft.Xna.Framework.Input;

namespace Utils {
    enum MouseButton { Left, Right }
    static class Input {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;
        static MouseState currentMouseState;
        static MouseState previousMouseState;

        public static KeyboardState GetKeyboardState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }

        public static MouseState GetMouseState()
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            return currentMouseState;
        }

        public static bool IsKeyPressed(Keys key, bool oneShot = false) {
            if(!oneShot) return currentKeyState.IsKeyDown(key);
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        public static bool IsMouseClicked(MouseButton button, bool oneShot = false) {
            if(button == MouseButton.Left)
                if(!oneShot) return currentMouseState.LeftButton == ButtonState.Pressed;
                else 
                    return currentMouseState.LeftButton == ButtonState.Pressed 
                    && previousMouseState.LeftButton == ButtonState.Released;
            if(button == MouseButton.Right)
                if(!oneShot) return currentMouseState.RightButton == ButtonState.Pressed;
                else return currentMouseState.RightButton == ButtonState.Pressed 
                    && previousMouseState.RightButton == ButtonState.Released;
            
            return false;
        }

        public static bool IsMouseJustReleased()
        {
            return currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}