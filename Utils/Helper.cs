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
            return Rect(graphicsDevice, new Point(width, height), 0, color, color);
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

        public static void DrawLine(SpriteBatch sb, Texture2D texture, Point start, Point end, int width)
        {
            var edge = (end - start).ToVector2();
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y , edge.X);


            sb.Draw(texture,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    width), //width of line, change this to make thicker line
                null,
                Color.White, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}