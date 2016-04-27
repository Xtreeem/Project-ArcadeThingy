using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    class GameObj
    {
        public Vector2 mPos { get; protected set; } = Vector2.Zero;
        public Texture2D mTex { get; protected set; } = null;
        public AABBRectangle mBounds { get; protected set; } = null;
        public Rectangle mDestRec { get; protected set; }
        public Rectangle? mSourceRec { get; protected set; } = null;
        public Vector2 mOrigin { get; protected set; } = Vector2.Zero;
        public float mRot { get; protected set; } = 0.0f;
        public Color mColor { get; protected set; } = Color.White;
        public SpriteEffects mSpriteEffect { get; protected set; } = SpriteEffects.None;
        public float mLayerDepth { get; protected set; } = 0.5f;
        public bool mHasCollision { get; protected set; } = true;

        public GameObj(Vector2 _Pos, Texture2D _Tex, bool _HasCollision = true) : this(_Pos, _Tex, new Vector2(_Tex.Width, _Tex.Height), _HasCollision) { }

        public GameObj(Vector2 _Pos, Texture2D _Tex, Vector2 _Size, bool _HasCollision = true)
        {
            mPos = _Pos;
            mTex = _Tex;
            mHasCollision = _HasCollision;
            Set_Size((int)_Size.X, (int)_Size.Y);
        }

        public void Set_Size(int _Height, int _Width)
        {
            mDestRec = new Rectangle((int)mPos.X, (int)mPos.Y, _Width, _Height);
            if (mHasCollision)
                mBounds = new AABBRectangle(mDestRec, mRot);
        }

        protected void Set_Rotation(float _Rot)
        {
            mRot = _Rot;
            if (mBounds != null)
                mBounds.Rotation = _Rot;
        }

        public void Draw(SpriteBatch _SB)
        {
            _SB.Draw(mTex, mDestRec, mSourceRec, mColor, mRot, mOrigin, mSpriteEffect, mLayerDepth);
        }

    }
}
