using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Utils {
   class AnimatedSprite
   {
        public Texture2D texture;
        public Point spriteAmount;
        public float speed;
        Point cellSize;
        public List<Texture2D> frames = new List<Texture2D>();
        public AnimatedSprite(GraphicsDevice graphicsDevice, Texture2D texture, Point spriteAmount, float speed)
        {
            this.texture = texture;
            this.spriteAmount = spriteAmount;
            this.speed = speed;
            cellSize = texture.Bounds.Size / spriteAmount;

            //split texture into multiple textures that will be animated
            var data = new Color[texture.Bounds.Height * texture.Bounds.Width];
            texture.GetData(data);
            for(var y = 0; y < texture.Bounds.Height; y += cellSize.Y)
                for(var x = 0; x < texture.Bounds.Width; x += cellSize.X)
                {
                    var d = new Color[cellSize.Y * cellSize.X];
                    for(var y2 = 0; y2 < cellSize.Y; y2++)
                        for(var x2 = 0; x2 < cellSize.X; x2++)
                            d[y2 * cellSize.X + x2] = data[y * texture.Bounds.Width + x + y2 * texture.Bounds.Width + x2];
                    
                    var t = new Texture2D(graphicsDevice, cellSize.X, cellSize.Y);
                    t.SetData(d);
                    frames.Add(t);
                }
        }

        public int currentFrame = 0;
        float changeFrameTimer = 0f;
        public void Animate(float delta)
        {
            changeFrameTimer += delta;
            if(changeFrameTimer > speed)
            {
                changeFrameTimer = 0;
                currentFrame += 1;
                if(currentFrame == frames.Count)
                    currentFrame = 0;
            }
        }
   }
}