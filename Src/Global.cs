using Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace DeliverBullets {
    static class Global {
        public static GraphicsDevice graphicsDevice;
        public static Point mousePos;
        public static Scene currentScene;
        public static Point resolution = new Point(1280, 720);
    }
}