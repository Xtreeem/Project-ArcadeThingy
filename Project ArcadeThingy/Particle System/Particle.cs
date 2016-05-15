using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class Particle
    {
        private Texture2D mTexture;
        private Vector2 mPos;
        private Vector2 mVel;
        private float mAngle;
        private float mAngleVel;
        private Color mColor;
        private float mScale;
        private float mDepth;
        private bool mFadeOut;
        private float mFadeOutEffect;
        private float mLifeTime;
        public double TimeToLive { get; private set; }

        public Particle(Texture2D _Texture, Vector2 _Pos, Vector2 _Vel, float _Angle, float _AngleVel, Color _Color, float _Scale, float _TimeToLive, float _Depth, bool _FadeOut)
        {
            mLifeTime = _TimeToLive;
            mTexture = _Texture;
            mPos = _Pos;
            mVel = _Vel;
            mAngle = _Angle;
            mAngleVel = _AngleVel;
            mColor = _Color;
            mScale = _Scale;
            TimeToLive = _TimeToLive;
            mDepth = _Depth;
            mFadeOut = _FadeOut;
            mFadeOutEffect = 1;

        }

        public void Update(GameTime _GT)
        {
            TimeToLive -= _GT.ElapsedGameTime.TotalSeconds;
            mPos += Vector2.Multiply(mVel, (float)_GT.ElapsedGameTime.TotalSeconds);
            mAngle += mAngleVel;
            mFadeOutEffect = (float)(TimeToLive / mLifeTime);
        }

        public void Draw(SpriteBatch _SB)
        {
            Rectangle source = new Rectangle(0, 0, mTexture.Width, mTexture.Height);
            Vector2 origin = new Vector2(mTexture.Width / 2, mTexture.Height / 2);
            _SB.Draw(mTexture, mPos, source, mColor * mFadeOutEffect, mAngle, origin, mScale, SpriteEffects.None, mDepth);
        }
    }
}