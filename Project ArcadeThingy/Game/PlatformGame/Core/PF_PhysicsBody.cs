using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    public class PF_PhysicsBody
    {
        public bool IsCircle { get; private set; }

        private Vector2 mSize;
        private float mRadius;
        public Vector2 Size { get { return mSize; } }
        public float Height { get { return mSize.Y; } }
        public float Width { get { return mSize.X; } }
        public float Radius { get { return mRadius; } }
        public float GravityScale { get { return mBody.GravityScale; } set { mBody.GravityScale = value; } }

        private Body mBody;
        public Vector2 Position { get { return mBody.Position.UnitToPixels(); } set { mBody.Position = value.PixelsToUnits(); } }
        public Vector2 LinearVelocity { get { return mBody.LinearVelocity.UnitToPixels(); } set { mBody.LinearVelocity = value.PixelsToUnits(); } }
        public BodyType BodyType { get { return mBody.BodyType; } set { mBody.BodyType = value; } }

        public bool CollisionEnabled { get { return mBody.Enabled; } set { mBody.Enabled = value; } }
        public Category CollidesWith { set { mBody.CollidesWith = value; } }
        public Category CollisionCategories { set { mBody.CollisionCategories = value; } }

        public bool IsUserDataNull { get { return mBody.UserData == null; } }

        public PF_PhysicsBody(World _World, Vector2 _Position, Vector2 _Size, float _density, bool _Circle = false, object _Owner = null)
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
            if (_F1.UserData == mBody.UserData) return (mBody.UserData as PF_GameObj).OnCollision(_F1, _F2, _C);
            if (_F2.UserData == mBody.UserData) return (mBody.UserData as PF_GameObj).OnCollision(_F2, _F1, _C);
            return true;
        }

        public void ApplyLinearVelocity(Vector2 _Input)
        {
            mBody.LinearVelocity = mBody.LinearVelocity + _Input.PixelsToUnits();
        }

        public void ApplyLinearVelocity(Vector2 _Input, Vector2 _Cap)
        {
            Vector2 tVelocity = Vector2.Zero;
            if (_Input.X != 0)
            {
                if (_Input.X < 0)
                {
                    if (LinearVelocity.X > _Cap.X)
                        tVelocity.X = (LinearVelocity.X + _Input.X > _Cap.X) ? _Input.X : (_Cap.X - LinearVelocity.X);
                }
                else
                {
                    if (LinearVelocity.X < _Cap.X)
                        tVelocity.X = (_Input.X + LinearVelocity.X < _Cap.X) ? _Input.X : (_Cap.X - LinearVelocity.X);
                }
            }
            if (_Input.Y != 0)
                if (_Input.Y != 0)
                {
                    if (_Input.Y < 0)
                    {
                        if (LinearVelocity.Y > _Cap.Y)
                            tVelocity.Y = (LinearVelocity.Y + _Input.Y > _Cap.Y) ? _Input.Y : (_Cap.Y - LinearVelocity.Y);
                    }
                    else
                    {
                        if (LinearVelocity.Y < _Cap.Y)
                            tVelocity.Y = (_Input.Y + LinearVelocity.Y < _Cap.Y) ? _Input.Y : (_Cap.Y - LinearVelocity.Y);
                    }
                }
            if (tVelocity == Vector2.Zero) return;

            Vector2 debug = new Vector2(LinearVelocity.X + tVelocity.X, LinearVelocity.Y + tVelocity.Y).PixelsToUnits();

            mBody.LinearVelocity = debug;
        }


        public void ApplyLinearForce(Vector2 _Force, Vector2 _Position)
        {
            mBody.ApplyLinearImpulse(_Force.PixelsToUnits(), _Position.PixelsToUnits());
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
