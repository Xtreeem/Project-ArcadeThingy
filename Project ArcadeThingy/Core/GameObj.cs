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
        public Vector2 Pos { get { return mPos; } }
        protected Vector2 mPos = Vector2.Zero;
        protected Texture2D mTex = null;
        public AABBRectangle Bounds { get { return mBounds; } }
        protected AABBRectangle mBounds = null;

        protected Rectangle mDestRec;
        protected Rectangle? mSourceRec = null;
        protected Vector2 mOrigin = Vector2.Zero;
        protected float mRot = 0.0f;
        protected Color mColor = Color.White;
        protected SpriteEffects mSpriteEffect = SpriteEffects.None;
        protected float mLayerDepth = 0.5f;
        public bool HasCollision { get { return (mBounds != null); } }
        protected bool mHasCollision = true;

        public GameObj(Vector2 _Pos, Texture2D _Tex, bool _HasCollision = true) : this(_Pos, _Tex, new Vector2(_Tex.Width, _Tex.Height), _HasCollision) { }

        public GameObj(Vector2 _Pos, Texture2D _Tex, Vector2 _Size, bool _HasCollision = true)
        {
            mPos = _Pos;
            mTex = _Tex;
            mHasCollision = _HasCollision;
            mDestRec = new Rectangle((int)_Pos.X, (int)_Pos.Y, (int)_Size.X, (int)_Size.Y);
            Set_Size((int)_Size.X, (int)_Size.Y);
        }

        public void Set_Size(int _Height, int _Width)
        {
            mDestRec.Height = _Height;
            mDestRec.Width = _Width ;
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
