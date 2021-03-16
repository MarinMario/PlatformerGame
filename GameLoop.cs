using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DeliverBullets
{
    public class GameLoop : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static GraphicsDevice graphicsDevice;

        GameScene gameScene;
        Utils.Window window;

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
        }

        protected override void Update(GameTime gameTime) {
            gameScene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            window.Draw(_spriteBatch, GraphicsDevice, () => gameScene.Draw(_spriteBatch));

            base.Draw(gameTime);
        }
    }
}
