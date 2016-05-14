using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    class MaxiGame
    {
        public Rectangle Bounds { get { return mBounds; } }
        protected Rectangle mBounds;

        protected List<GameObj> mGameObjects = new List<GameObj>();

        Spline mTopSpline = new Spline();
        Spline mMidSpline = new Spline();
        Spline mBotSpline = new Spline();

        Texture2D mBackground;

        Base mPlayerOneBase;
        Base mPlayerTwoBase;

        public MaxiGame(Rectangle _Bounds)
        {
            mBounds = _Bounds;

            mTopSpline.AddRange(FileUtils.GetSpline(FileUtils.Spline_Top));
            mMidSpline.AddRange(FileUtils.GetSpline(FileUtils.Spline_Mid));
            mBotSpline.AddRange(FileUtils.GetSpline(FileUtils.Spline_Bot));

            mPlayerOneBase = new Base(mMidSpline.mBasePositions[0]);
            mPlayerTwoBase = new Base(mMidSpline.mBasePositions[mMidSpline.mBasePositions.Count - 1]);

            mBackground = ContentManager.LaneBackground;
        }

        public void Update(GameTime _GT)
        {
            for (int i = 0; i < mGameObjects.Count; ++i)
                mGameObjects[i].Update(_GT);
        }

        public void Draw(SpriteBatch _SB)
        {
            _SB.Draw(mBackground, mBounds, Color.White);

            mTopSpline.DrawSpline(_SB, ContentManager.LaneTexture, Color.White, 1.0f, 3);
            mMidSpline.DrawSpline(_SB, ContentManager.LaneTexture, Color.White, 1.0f, 3);
            mBotSpline.DrawSpline(_SB, ContentManager.LaneTexture, Color.White, 1.0f, 3);

            mPlayerOneBase.Draw(_SB);
            mPlayerTwoBase.Draw(_SB);

            for (int i = 0; i < mGameObjects.Count; ++i)
                mGameObjects[i].Draw(_SB);
        }
    }
}