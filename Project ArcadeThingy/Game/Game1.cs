using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch SB;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Window.Position = new Point(-7, -40);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SB = new SpriteBatch(GraphicsDevice);
            ContentManager.Load(Content);
            SceneManager.Initialize(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            SceneManager.AddScene(new MainMenuScene());
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            if (!SceneManager.Update(gameTime))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SB.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, null);
            SceneManager.Draw(SB);
            SB.End();
            base.Draw(gameTime);
        }
    }
}