using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class GameScene : Scene
    {
        PlatformGame TestGame;
        public GameScene()
        {
            TestGame = new PlatformGame(new Rectangle(0, 0, SceneManager.Width, SceneManager.Height));
        }

        public override void Update(GameTime _GT)
        {
            TestGame.Update(_GT);
        }

        public override void Draw(SpriteBatch _SB)
        {
            TestGame.Draw(_SB);
        }
    }
}
