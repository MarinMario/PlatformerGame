using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Utils {
    static class Shapes {
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
    }
}