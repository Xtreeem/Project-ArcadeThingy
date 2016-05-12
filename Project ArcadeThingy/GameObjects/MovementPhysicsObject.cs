using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Dynamics.Contacts;

namespace Project_ArcadeThingy
{
    class MovementPhysicsObject : Physics_Objetct
    {
        Vector2 mCenterOffSet;
        public Body mWheel;
        RevoluteJoint mMotor;
        float mMaxEngineSpeed = 50;
        float mMotorAcceleration = 0.5f;
        float mMotorFallOffSpeed = 46.25f;

        public override float GetMotorSpeedX
        {
            get
            {
                return mMotor.MotorSpeed.UnitToPixels();
            }
        }

        public MovementPhysicsObject(ref World _World, GameObj _Owner, Vector2 _Size, Vector2 _Position, Texture2D _Tex = null, BodyType _BodyType = BodyType.Static, float _Density = 1) : base(ref _World, _Owner, _Size, _Position, _Tex, _BodyType, _Density)
        {
            if (_Size.X > _Size.Y)
            {
                throw new Exception("Error width > height: can't make character because wheel would stick out of body");
            }

        }

        internal void DecreaseSidewaysSpeed(GameTime _GT)
        {
            if (mMotor.MotorSpeed == 0) return;
            if (mMotor.MotorSpeed > 0)
                if (mMotor.MotorSpeed <= 0.5f) mMotor.MotorSpeed = 0;
                else mMotor.MotorSpeed = MathHelper.Clamp(mMotor.MotorSpeed * ((mMotorFallOffSpeed * (float)_GT.ElapsedGameTime.TotalSeconds)), 0.4f, float.MaxValue);
            if (mMotor.MotorSpeed < 0)
                if (mMotor.MotorSpeed >= -0.5f) mMotor.MotorSpeed = 0;
                else mMotor.MotorSpeed = MathHelper.Clamp(mMotor.MotorSpeed * ((mMotorFallOffSpeed * (float)_GT.ElapsedGameTime.TotalSeconds)), -50, -0.4f);

            Console.WriteLine(GetMotorSpeedX.ToString());
        }

        public override void SetUpPhysics(Vector2 _Position)
        {
            float aspectRatio = Size.X / Size.Y;
            float upperBodyHeight = Size.Y - (Size.X / 2);
            float HminusW = Size.Y - Size.X;
            mBody = BodyFactory.CreateRectangle(mWorld, Size.X.PixelToUnit(), (Size.Y - (Size.X)).PixelToUnit(), mDensity, _Position.PixelsToUnits());
            mBody.BodyType = BodyType.Dynamic;
            mBody.Restitution = 0.3f;
            mBody.Friction = 0.5f;
            //mBody.Position = (_Position - (Vector2.UnitY * (Size.X / 4))).PixelsToUnits();            
            mBody.Position = _Position.PixelsToUnits();
            mBody.Position = new Vector2(_Position.X, (_Position.Y + (HminusW / 2))).PixelsToUnits();
            mCenterOffSet = new Vector2(0, _Position.Y - mBody.Position.Y.UnitToPixels());
            mBody.FixedRotation = true; //Make fixed joint? 
            mBody.UserData = mOwner;


            mWheel = BodyFactory.CreateCircle(mWorld, (Size.X / 2).PixelToUnit(), mDensity);
            //mWheel.Position = mBody.Position + (Vector2.UnitY * (Size.Y - (Size.X + Size.X / 4))).PixelsToUnits();
            mWheel.Position = _Position.PixelsToUnits();
            mWheel.Position = new Vector2(mWheel.Position.X, (_Position.Y + (Size.Y - ((Size.X / 2)))).PixelToUnit());
            mWheel.BodyType = BodyType.Dynamic;
            mWheel.Restitution = 0.3f;
            mWheel.Friction = 0.5f;
            mWheel.UserData = mOwner;


            mMotor = JointFactory.CreateRevoluteJoint(mWorld, mBody, mWheel, Vector2.Zero);
            mMotor.MotorEnabled = true;
            mMotor.MaxMotorTorque = 1000f;
            mMotor.MotorSpeed = 0;

            mWheel.IgnoreCollisionWith(mBody);
            mBody.IgnoreCollisionWith(mWheel);

            mWheel.Friction = float.MaxValue;
        }

        public void Draw(SpriteBatch _SB)
        {
            //_SB.Draw(mTex, new Rectangle((int)mBody.Position.X.UnitToPixels(), (int)mBody.Position.Y.UnitToPixels(), (int)Size.X, (int)(Size.Y - (Size.X / 2))), null, Color.White, mBody.Rotation, mOrigin, SpriteEffects.None, 1.0f);
            //_SB.Draw(mTex, new Rectangle((int)mWheel.Position.X.UnitToPixels(), (int)mWheel.Position.Y.UnitToPixels(), (int)Size.X, (int)(Size.X)), null, Color.Blue, mWheel.Rotation, mOrigin, SpriteEffects.None, 1.0f);
            _SB.Draw(mTex, new Rectangle((int)mBody.Position.X.UnitToPixels(), (int)mBody.Position.Y.UnitToPixels(), (int)Size.X, (int)(Size.Y - (Size.X))), null, Color.White, mBody.Rotation, mOrigin, SpriteEffects.None, 1.0f);
            _SB.Draw(mTex, new Rectangle((int)mWheel.Position.X.UnitToPixels(), (int)mWheel.Position.Y.UnitToPixels(), (int)Size.X, (int)(Size.X)), null, Color.Blue, mWheel.Rotation, mOrigin, SpriteEffects.None, 1.0f);
            _SB.Draw(mTex, new Rectangle((int)Position.X, (int)(Position.Y + Size.X / 2), (int)Size.X, (int)Size.Y), null, new Color(1, 1, 1, 0.5f), mBody.Rotation, mOrigin, SpriteEffects.None, 0f);
        }

        public Rectangle GetDrawRectangle()
        {
            return new Rectangle((int)Position.X, (int)(Position.Y + Size.X / 2), (int)Size.X, (int)Size.Y);
        }

        public void Accelerate(GameTime _GT, bool _Left)
        {
            if (_Left)
            {
                if (mMotor.MotorSpeed > 0)
                    mMotor.MotorSpeed *= (mMotorFallOffSpeed * (float)_GT.ElapsedGameTime.TotalSeconds);
                SetMotorSpeed(GetMotorSpeed - GetMotorAcceleration);
            }
            else
            {
                if (mMotor.MotorSpeed < 0)
                    mMotor.MotorSpeed *= (mMotorFallOffSpeed * (float)_GT.ElapsedGameTime.TotalSeconds);
                SetMotorSpeed(GetMotorSpeed + GetMotorAcceleration);
            }
        }

        public void SetMotorSpeed(float _Input)
        {
            mMotor.MotorSpeed = MathHelper.Clamp(_Input, -mMaxEngineSpeed, mMaxEngineSpeed);
        }
        public float GetMotorSpeed { get { return mMotor.MotorSpeed; } }
        public float GetMotorAcceleration { get { return mMotorAcceleration; } }
        public void SetMotorAcceleration(float _Input)
        {
            mMotorAcceleration = _Input;
        }
        public Vector2 Position
        {
            get
            {
                return ((mBody.Position.UnitToPixels()/*) + Vector2.UnitY * mCenterOffSet*/));
            }
        }
    }
}
