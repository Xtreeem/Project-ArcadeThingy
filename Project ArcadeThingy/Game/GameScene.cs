using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class GameScene : Scene
    {
        PlatformGame TestGame;
        GameTimer mTimer;
        double mTotalTime = 60.0 * 5;
        Vector2 mTimerPosition;
        Rectangle mInfoBounds;

        public GameScene()
        {
            mTransitionOnTime = 0.5f;
            TestGame = new PlatformGame();
            mTimer = new GameTimer();
            mTimerPosition = TestGame.mObjects[0].Body.Position;
            mInfoBounds = TestGame.mObjects[0].Body.GetDrawRectangle();
            mTimer.OnFinished = delegate { GameOver(TestGame.PlayerOneCoins > TestGame.PlayerTwoCoins); };
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

        void GameOver(bool _PlayerOneWon)
        {
            SceneManager.AddScene(new GameOverScene(_PlayerOneWon));
            Close();
        }

        public override void Update(GameTime _GT)
        {
            TestGame.Update(_GT);
            mTimer.Update(_GT);

            if (TestGame.PlayerOneCoins == PlatformGame.MAX_PLAYER_COINS
                ||
                TestGame.PlayerTwoCoins == PlatformGame.MAX_PLAYER_COINS)
                GameOver(TestGame.PlayerOneCoins > TestGame.PlayerTwoCoins);

        }

        public override void Draw(SpriteBatch _SB)
        {
            TestGame.Draw(_SB);
            mTimer.Draw(_SB, ContentManager.Font, mTimerPosition, Color.Black);
            _SB.DrawString(ContentManager.Font, TestGame.PlayerOneCoins.ToString(), mTimerPosition - new Vector2(mInfoBounds.Size.ToVector2().X / 2, 0), Color.Blue, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            _SB.DrawString(ContentManager.Font, TestGame.PlayerTwoCoins.ToString(), mTimerPosition + new Vector2(mInfoBounds.Size.ToVector2().X / 2 - ContentManager.Font.MeasureString(TestGame.PlayerTwoCoins.ToString()).X, 0), Color.Red, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}