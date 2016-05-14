using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    public class MovingObj : GameObj
    {
        protected Vector2 mMaxVelocity;
        public MovingObj(Vector2 _Size, Vector2 _Position,ref World _World) : base(_Size, _Position, ref _World)
        {
            for (int i = 0; i < mBody.Body.FixtureList.Count; ++i)
                mBody.Body.FixtureList[i].UserData = this;
        }

        public void SetPos()
        {
            mBody.Body.Position = new Vector2(100, 100).PixelsToUnits();
        }

        public override void Update(GameTime _GT)
        {
            UpdateAABB();

        }

        protected void UpdateAABB()
        {
            mQuadBox.Set_Position((int)mBody.Body.Position.X.UnitToPixels(), (int)mBody.Body.Position.Y.UnitToPixels());
            mQuadBox.Rotation = mBody.Body.Rotation;
        }

        public virtual void AddVelocity(Vector2 _Input)
        {
            Vector2 tFinalSpeed = mBody.Body.LinearVelocity.UnitToPixels();
            tFinalSpeed.X = MathHelper.Clamp(tFinalSpeed.X + _Input.X, -mMaxVelocity.X, mMaxVelocity.X);
            tFinalSpeed.Y = MathHelper.Clamp(tFinalSpeed.Y + _Input.Y, -mMaxVelocity.Y, mMaxVelocity.Y);
            mBody.Body.LinearVelocity = tFinalSpeed.PixelsToUnits();

        }
    }
}
