using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class GameOverScene : MenuScene
    {
        Texture2D mTexture;
        Vector2 mOrigin;
        Rectangle mRect;
        string mText;

        public GameOverScene(bool _PlayerOneWon) : base()
        {
            Set_Desc(new EntryDesc(ContentManager.Font, new Vector2(SceneManager.Width / 2, SceneManager.Height / 2)));
            AddEntry("Play again", PlayAgain);
            AddEntry("Exit To Menu", ExitToMenu);

            mTransitionOffTime = 0.0f;
            mTransitionOnTime = 0.0f;
            mTexture = ContentManager.Platformer_UI;

            mText = "Congratulations\nPlayer ";
            if (_PlayerOneWon)
                mText += "One!";
            else
                mText += "Two!";

            Vector2 size = ContentManager.Font.MeasureString(mText);
            mRect = new Rectangle(SceneManager.Width / 2, SceneManager.Height / 2, (int)(SceneManager.Width * 0.75f), (int)(SceneManager.Height * 0.75f));
            mOrigin = new Vector2(mRect.Width / 2, mRect.Height / 2);
            mRect.X -= (int)mOrigin.X;
            mRect.Y -= (int)mOrigin.Y;
        }

        private void PlayAgain()
        {
            SceneManager.AddScene(new GameScene());
            Close();
        }

        private void ExitToMenu()
        {
            SceneManager.AddScene(new MainMenuScene());
            Close();
        }

        public override void Draw(SpriteBatch _SB)
        {
            _SB.Draw(mTexture, mRect, null, Color.White * mTransitionStatus, 0.0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            _SB.DrawString(ContentManager.Font, mText, new Vector2(mRect.X + mOrigin.X, mRect.Y + mOrigin.Y / 2), Color.Black * mTransitionStatus, 0.0f, ContentManager.Font.MeasureString(mText) / 2, 1.0f, SpriteEffects.None, 1.0f);
            base.Draw(_SB);
        }
    }
}