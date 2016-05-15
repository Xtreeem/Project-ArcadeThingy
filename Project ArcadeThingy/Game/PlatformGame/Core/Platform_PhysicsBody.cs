using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    class Platform_PhysicsBody
    {
        public bool IsCircle { get; private set; }

        private Vector2 mSize;
        private float mRadius;
        public Vector2 Size { get { return mSize; } }
        public float Height { get { return mSize.Y; } }
        public float Width { get { return mSize.X; } }
        public float Radius { get { return mRadius; } }

        private Body mBody;
        public Vector2 Position { get { return mBody.Position.UnitToPixels(); } set { mBody.Position = value.PixelsToUnits(); } }
        public Vector2 LinearVelocity { get { return mBody.LinearVelocity.UnitToPixels(); } set { mBody.LinearVelocity = value.PixelsToUnits(); } }
        public BodyType BodyType { get { return mBody.BodyType; } set { mBody.BodyType = value; } }


        public Platform_PhysicsBody(World _World, Vector2 _Position, Vector2 _Size, float _density, bool _Circle = false, object _Owner = null)
        {
            IsCircle = _Circle;
            mSize = _Size;
            if (IsCircle)
            {
                mRadius = mSize.X / 2;
                mBody = BodyFactory.CreateCircle(_World, mRadius.PixelToUnit(), _density, _Owner);
            }
            else
            {
                mRadius = 0;
                mBody = BodyFactory.CreateRectangle(_World, Width.PixelToUnit(), Height.PixelToUnit(), _density, _Owner);
            }
            mBody.Position = _Position.PixelsToUnits();
            mBody.OnCollision += mOnCollision;
            mBody.UserData = _Owner;
        }

        private bool mOnCollision(Fixture _F1, Fixture _F2, Contact _C)
        {
            if (mBody.UserData == null) return true;
            if (_F1.UserData == mBody.UserData) return (mBody.UserData as Platform_GameObj).OnCollision(_F1, _F2, _C);
            if (_F2.UserData == mBody.UserData) return (mBody.UserData as Platform_GameObj).OnCollision(_F2, _F1, _C);
            return true;
        }

        public void ApplyLinearVelocity(Vector2 _Input)
        {
            mBody.LinearVelocity = mBody.LinearVelocity + _Input.PixelsToUnits();
        }

        public Rectangle GetDrawRectangle()
        {
            if (IsCircle)
                return new Rectangle((int)Position.X, (int)(Position.Y), (int)Radius * 2, (int)Radius * 2);
            else
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Width, (int)Height);
        }

    }
}
