using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Utils {

    enum WindowAspect { Keep, Expand }
    class Window {

        public Point resolution;
        public WindowAspect windowAspect;
        public RenderTarget2D renderTarget;

        public Window(Point resolution, WindowAspect windowAspect, RenderTarget2D renderTarget) {
            this.resolution = resolution;
            this.windowAspect = windowAspect;
            this.renderTarget = renderTarget;
        }


        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Action draw) {
            graphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin();
            draw();
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            var size = graphicsDevice.Viewport.Bounds.Size;
            switch (windowAspect) {
                case WindowAspect.Expand:
                    spriteBatch.Draw(renderTarget, new Rectangle(Point.Zero, size), Color.White);
                    break;
                case WindowAspect.Keep:
                    {
                        var resRatio = (float)resolution.X / (float)resolution.Y;
                        var scaledRes = new Point((int)(size.Y * resRatio), (int)(size.X / resRatio));
                        var scaledWindowSize =  
                            size.X > size.Y && size.X > scaledRes.X
                                ? new Point(scaledRes.X, size.Y)
                                : new Point(size.X, scaledRes.Y);
                        var windowPosition = (size - scaledWindowSize) / new Point(2);
                        
                        spriteBatch.Draw(renderTarget, new Rectangle(windowPosition, scaledWindowSize), Color.White);
                    }
                    break;
            }
            spriteBatch.End();
        }
    }
}