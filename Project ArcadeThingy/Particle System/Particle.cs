using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.mLifeTime = _TimeToLive;
            this.mTexture = _Texture;
            this.mPos = _Pos;
            this.mVel = _Vel;
            this.mAngle = _Angle;
            this.mAngleVel = _AngleVel;
            this.mColor = _Color;
            this.mScale = _Scale;
            this.TimeToLive = _TimeToLive;
            this.mDepth = _Depth;
            this.mFadeOut = _FadeOut;
            this.mFadeOutEffect = 1;

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
