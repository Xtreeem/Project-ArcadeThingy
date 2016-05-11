using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch SB;

        MiniGame TestGame;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {

            this.Window.Position = new Point(-7, -40);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {


            SB = new SpriteBatch(GraphicsDevice);
            ContentManager.Load(Content);
            TestGame = new MiniGame(new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            TestGame.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SB.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, null);
            TestGame.Draw(SB);
            //SB.DrawLine(Tester.LastBounds.Center, TestPlat.LastBounds.Center, 1.0f, Color.Red);
            SB.End();
            base.Draw(gameTime);
        }
    }
}
