using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public struct AnimationDesc
    {
        public bool VerticleAnimation { get; private set; }
        public bool BoomerangAnimation { get; private set; }
        public double TimePerFrame { get; private set; }
        public Vector2 FirstFrameLocation { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public Vector2 Origin { get; private set; }
        public int FramesPerAnimation { get; private set; }

        public AnimationDesc(double _TimePerFrame, Vector2 _FirstFrameLocation, int _FrameWidth, int _FrameHeight, int _FramesPerAnimation, bool _BoomerangAnimation = false, bool _VerticleAnimation = false)
        {
            BoomerangAnimation = _BoomerangAnimation;
            VerticleAnimation = _VerticleAnimation;
            TimePerFrame = _TimePerFrame;
            FirstFrameLocation = _FirstFrameLocation;
            FrameWidth = _FrameWidth;
            FrameHeight = _FrameHeight;
            FramesPerAnimation = _FramesPerAnimation;
            Origin = new Vector2(FrameWidth / 2, FrameHeight / 2);
        }
    }

    public class AnimatedTexture
    {
        private List<AnimationDesc> mAnimations = new List<AnimationDesc>();
        private AnimationDesc mCurrentAnimation;

        private bool mReturning = false;
        private double mFrameTimer = 0;
        private int mFrameIndex = 0;
        private Rectangle mSourceRec;
        private Texture2D mSourceSheet;
        public double AnimationSpeedScale { get; set; }
        public AnimatedTexture()
        {
            mSourceSheet = ContentManager.MarioSheet;
            mAnimations.Add(new AnimationDesc(0.07, new Vector2(16, 0), 16, 16, 3, true, false));
            SwapAnimation(0);
            SetupRectangle();
        }

        public void Update(GameTime _GT)
        {
            Animate(_GT);
        }
        public void Draw(SpriteBatch _SB, Rectangle _Destination, Color _Color, float _Rotation = 0.0f, SpriteEffects _Effect = SpriteEffects.None, float _LayerDepth = 0.5f)
        {
            _SB.Draw(mSourceSheet, _Destination, mSourceRec, _Color, _Rotation, mCurrentAnimation.Origin, _Effect, _LayerDepth);
        }

        public void Set_FrameSize(int _Width, int _Height)
        {
            mSourceRec.Width = _Width;
            mSourceRec.Height = _Height;
        }

        private void SetupRectangle()
        {
            mSourceRec = new Rectangle((int)mCurrentAnimation.FirstFrameLocation.X, (int)mCurrentAnimation.FirstFrameLocation.Y, mCurrentAnimation.FrameWidth, mCurrentAnimation.FrameHeight);
        }


        private void Animate(GameTime _GT)
        {
            mFrameTimer += _GT.ElapsedGameTime.TotalSeconds;
            if (mFrameTimer < mCurrentAnimation.TimePerFrame - (mCurrentAnimation.TimePerFrame * AnimationSpeedScale)) return;
            else
                mFrameTimer = 0;

            if (mReturning)
            {
                if(--mFrameIndex < 0)
                {
                    mReturning = false;
                    mFrameIndex = 1;
                }
            }
            else
            if (++mFrameIndex >= mCurrentAnimation.FramesPerAnimation)
                if (mCurrentAnimation.BoomerangAnimation)
                {
                    mFrameIndex -= 2;
                    mReturning = true;
                }
                else mFrameIndex = 0;



            if (mCurrentAnimation.VerticleAnimation)
                mSourceRec.Y = (int)(mCurrentAnimation.FirstFrameLocation.Y + mFrameIndex * mCurrentAnimation.FrameHeight);
            else
                mSourceRec.X = (int)(mCurrentAnimation.FirstFrameLocation.X + mFrameIndex * mCurrentAnimation.FrameWidth);
        }

        public void SwapAnimation(int _AnimationIndex, int _StartingFrame = 0)
        {
            if (_AnimationIndex > mAnimations.Count) throw new Exception("Invalid AnimationIndex");
            mCurrentAnimation = mAnimations[_AnimationIndex];
            mFrameTimer = 0;
            mFrameIndex = _StartingFrame;
            if (mCurrentAnimation.VerticleAnimation)
            {
                mSourceRec.Y = (int)(mCurrentAnimation.FirstFrameLocation.Y + mFrameIndex * mCurrentAnimation.FrameHeight);
                mSourceRec.X = (int)mCurrentAnimation.FirstFrameLocation.X;
            }
            else
            {
                mSourceRec.Y = (int)mCurrentAnimation.FirstFrameLocation.Y;
                mSourceRec.X = (int)(mCurrentAnimation.FirstFrameLocation.X + mFrameIndex * mCurrentAnimation.FrameWidth);
            }

        }
    }
}
