using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class MovingObj : GameObj
    {
        public bool AffectedByGravity { get; protected set; } = false;
        public bool Grounded { get; protected set; } = false;
        protected float mGravityScale = 1.0f;
        protected Vector2 mVelocity;
        private float mMaxPositiveYVelocity = 500;
        private float mMaxNegativeYVelocity = -500;

        public MovingObj(Vector2 _Pos, Texture2D _Tex, bool _AffectedByGravity = false, bool _HasCollision = true) : base(_Pos, _Tex, _HasCollision)
        {
            AffectedByGravity = _AffectedByGravity;
        }

        public void Update(GameTime _GT)
        {
            if (InputManager.PlayerOneButtonMoveJump)
                mVelocity.Y = -500; 
            GravitationalPull(_GT);
            Move(_GT);
            Update_Rectangles();
        }

        private void Move(GameTime _GT)
        {
            mPos += (mVelocity * (float)_GT.ElapsedGameTime.TotalSeconds);
        }

        public void Set_Grounded(bool _Input)
        {
            if (_Input)
                mVelocity.Y = 0;
            Grounded = _Input;
        }

        protected void Set_Position(Vector2 _Pos)
        {
            mPos = _Pos;
            Update_Rectangles();
        }

        protected void Update_Rectangles()
        {
            if (mHasCollision)
                mBounds.Set_Position((int)mPos.X, (int)mPos.Y);
            //mDestRec = new Rectangle((int)mPos.X, (int)mPos.Y, mDestRec.Width, mDestRec.Height);
            mDestRec.X = (int)mPos.X;
            mDestRec.Y = (int)mPos.Y;
        }

        protected void GravitationalPull(GameTime _GT)
        {
            if (!AffectedByGravity || Grounded) return;
            float t = (float)((Utilities.Gravity * mGravityScale) * _GT.ElapsedGameTime.TotalSeconds);
            mVelocity.Y = MathHelper.Clamp(mVelocity.Y + t, mMaxNegativeYVelocity, mMaxPositiveYVelocity);
        }

        public void StopMove(bool _X, bool _Y)
        {
            if (_X)
                mVelocity.X = 0;
            if (_Y)
                mVelocity.Y = 0;
        }

    }
}
