using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Utils {
    static class Helper {
        public static Texture2D Rect(GraphicsDevice gd, Point size, int borderSize, Color color, Color borderColor) {
            var texture = new Texture2D(gd, size.X, size.Y);
            var data = new Color[size.X * size.Y];

            for(var y = 0; y < size.Y; y++)
                for(var x = 0; x < size.X; x++)
                    if(x >= size.X - borderSize || x < borderSize 
                    || y >= size.Y - borderSize || y < borderSize)
                        data[y * size.X + x] = borderColor;
                    else
                        data[y * size.X + x] = color;
            
            texture.SetData(data);
            return texture;
        }

        public static Texture2D ColorRect(GraphicsDevice graphicsDevice, int width, int height, Color color) {
            var q = 40;
            var color2 = new Color(color.R + q, color.G + q, color.B + q);
            return Rect(graphicsDevice, new Point(width, height), (int)Math.Sqrt(width + height) / 2, color2, color);
        }

        public static float MoveVal(float n, float target, float amount) {
            if(n < target && target - n > 0)
                return amount;
            if(n > target && n - target > 0)
                return -amount;
            return 0;
        }

        public static Vector2 MoveVector(Vector2 vector, Vector2 target, float amount) {
            var x = MoveVal(vector.X, target.X, amount);
            var y = MoveVal(vector.Y, target.Y, amount);
            return new Vector2(x, y);
        }
    }
}