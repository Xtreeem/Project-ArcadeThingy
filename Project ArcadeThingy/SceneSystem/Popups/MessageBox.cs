using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_ArcadeThingy
{
    abstract class MessageBox : MenuScene
    {
        protected Rectangle mBounds;
        protected Texture2D mTexture;

        public MessageBox(Texture2D _Texture, EntryDesc _Desc, Vector2 _Size) : base(_Desc)
        {
            mBounds = new Rectangle((int)(_Desc.StartPosition.X - _Size.X / 2), (int)(_Desc.StartPosition.Y - _Size.Y / 2), (int)_Size.X, (int)_Size.Y);
            mTexture = _Texture;
        }

        public override bool HandleTransition(GameTime _GT)
        {
            mBounds.X = (int)MathHelper.Lerp(Desc.StartPosition.X, Desc.EndPosition.X, mTransitionStatus) - mBounds.Width / 2;
            mBounds.Y = (int)MathHelper.Lerp(Desc.StartPosition.Y, Desc.EndPosition.Y, mTransitionStatus) - mBounds.Height / 2;

            return base.HandleTransition(_GT);
        }

        public override void Draw(SpriteBatch _SB)
        {
            SceneManager.FadeToBlack(_SB, mTransitionStatus / 2.0f);
            _SB.Draw(mTexture, mBounds, Color.White * mTransitionStatus);

            base.Draw(_SB);
        }
    }
}