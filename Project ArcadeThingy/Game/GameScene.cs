using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class GameScene : Scene
    {
        PlatformGame TestGame;
        GameTimer mTimer;
        double mTotalTime = 60.0 * 5;
        Texture2D mUI;

        public GameScene()
        {
            TestGame = new PlatformGame(new Rectangle(0, 0, SceneManager.Width, SceneManager.Height));
            mTimer = new GameTimer();
            mUI = ContentManager.Platformer_UI;
        }

        public override bool HandleTransition(GameTime _GT)
        {
            if (base.HandleTransition(_GT))
            {
                if (mState == SceneState.Active)
                    mTimer.Restart(mTotalTime);
                return true;
            }
            else return false;
        }

        public override void Update(GameTime _GT)
        {
            TestGame.Update(_GT);
            mTimer.Update(_GT);
        }

        public override void Draw(SpriteBatch _SB)
        {
            TestGame.Draw(_SB);
            _SB.Draw(mUI, new Vector2(400, 150), Color.White);
            mTimer.Draw(_SB, ContentManager.Font, new Vector2(400, 150), Color.Black);
        }
    }
}