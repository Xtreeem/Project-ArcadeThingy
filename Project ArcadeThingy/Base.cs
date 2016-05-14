
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class Base
    {
        Texture2D mTexture;
        Vector2 mPosition;
        Color mColor;
        float mRot;
        Vector2 mOrigin;
        float mScale;
        float mLayerDepth;

        public bool IsDestroyed { get { return mLife == 0; } }
        int mLife;

        public Base(Vector2 _Position)
        {
            mPosition = _Position;
            mTexture = ContentManager.Castle;
            mColor = Color.White;
            mRot = 0.0f;
            mOrigin = new Vector2(mTexture.Width / 2, mTexture.Height / 2);
            mScale = 1.0f;
            mLayerDepth = 1.0f;
            mPosition.Y -= mOrigin.Y;
            mLife = 100;
        }

        public void DoDamage(int _Damage)
        {
            mLife = MathHelper.Clamp(mLife - _Damage, 0, mLife);
        }

        public void Update(GameTime _GT)
        {

        }

        public void Draw(SpriteBatch _SB)
        {
            _SB.Draw(mTexture, mPosition, null, mColor, mRot, mOrigin, mScale, SpriteEffects.None, mLayerDepth);
        }
    }
}