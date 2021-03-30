using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Src {
    public class GameLoop : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Utils.Window window;

        List<Point> points1 = new List<Point>();
        List<Point> points2 = new List<Point>();

        public GameLoop() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 360;
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.graphicsDevice = GraphicsDevice;
            Global.currentScene = new MenuScene();
            window = new Utils.Window(
                Global.resolution,
                Utils.WindowAspect.Keep,
                new RenderTarget2D(GraphicsDevice, Global.resolution.X, Global.resolution.Y)
            );
        }

        protected override void Update(GameTime gameTime) {
            Global.currentScene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            window.UpdateSize(GraphicsDevice);
            window.resolution = Global.resolution;
            window.cameraPosition = new Point(-1) * Global.cameraPos;
            Global.mousePos = window.MousePosition();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            window.Draw(_spriteBatch, GraphicsDevice, () => Global.currentScene.Draw(_spriteBatch));


            base.Draw(gameTime);
        }
    }
}
