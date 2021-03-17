using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DeliverBullets
{
    public class GameLoop : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static GraphicsDevice graphicsDevice;

        GameScene gameScene;
        Utils.Window window;

        Texture2D testTexture;
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
            graphicsDevice = GraphicsDevice;
            gameScene = new GameScene();
            window = new Utils.Window(new Point(1280, 720), Utils.WindowAspect.Keep, new RenderTarget2D(GraphicsDevice, 1280, 720));
            testTexture = Utils.Shapes.Rect(GraphicsDevice, new Point(10), 1, Color.White, Color.LightBlue);
        }

        protected override void Update(GameTime gameTime) {
            // gameScene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            window.UpdateSize(graphicsDevice);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                points1.Add(window.MousePosition());
                // points2.Add(Mouse.GetState().Position);
            }

            base.Update(gameTime);
        }

        void Draw2() {
            _spriteBatch.Draw(testTexture, new Rectangle(0, 0, 1280, 720), Color.Red);
            foreach(var p1 in points1)
                _spriteBatch.Draw(testTexture, p1.ToVector2(), Color.Green);
            foreach(var p2 in points2)
                _spriteBatch.Draw(testTexture, p2.ToVector2(), Color.Blue);
        }

        protected override void Draw(GameTime gameTime) {
            window.Draw(_spriteBatch, GraphicsDevice, Draw2);


            base.Draw(gameTime);
        }
    }
}
