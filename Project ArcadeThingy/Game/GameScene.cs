using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class GameScene : Scene
    {
        PlatformGame TestGame;
        GameTimer mTimer;
        double mTotalTime = 2;//60.0 * 5;
        Vector2 mTimerPosition;

        public GameScene()
        {
            mTransitionOnTime = 0.5f;
            TestGame = new PlatformGame(new Rectangle(0, 0, SceneManager.Width, SceneManager.Height));
            mTimer = new GameTimer();
            mTimerPosition = TestGame.mObjects[0].Body.Position;
            mTimer.OnFinished = delegate { SceneManager.AddScene(new GameOverScene(true)); Close(); };
        }

        public override bool HandleTransition(GameTime _GT)
        {
            if (base.HandleTransition(_GT))
            {
                if (mState == SceneState.Active)
                {
                    mTimer.Restart(mTotalTime);

                }
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
            mTimer.Draw(_SB, ContentManager.Font, mTimerPosition, Color.Black);
        }
    }
}