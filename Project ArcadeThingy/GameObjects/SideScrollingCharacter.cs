using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics.Contacts;

namespace Project_ArcadeThingy
{
    class SideScrollingCharacter : Character
    {
        AnimatedTexture TestAnimation;

        public SideScrollingCharacter(Vector2 _Size, Vector2 _Position, ref World _World, Controller _Controller) : base(_Size, _Position, ref _World, _Controller)
        {
            mBody = new MovementPhysicsObject(ref _World, this, _Size, _Position);
            TestAnimation = new AnimatedTexture();
            (mBody as MovementPhysicsObject).mWheel.OnCollision += mOnCollisionWheel;
            mBody.Body.OnCollision += mOnCollisionBody;

        }
        public override void Update(GameTime _GT)
        {
            TestAnimation.Update(_GT);
            //mBody.Body.Position = new Vector2(mBody.Body.Position.X, mBody.Body.Position.Y + (3.0f).PixelToUnit());
            base.Update(_GT);

        }


        public override void AddVelocity(Vector2 _Input)
        {
            Vector2 tFinalSpeed = mBody.Body.LinearVelocity.UnitToPixels();
            tFinalSpeed.X = MathHelper.Clamp(tFinalSpeed.X + _Input.X, -mMaxVelocity.X, mMaxVelocity.X);
            mBody.Body.LinearVelocity = tFinalSpeed.PixelsToUnits();
        }

        public override void HandleMovementInput(MovementInput _Input, float _intensity)
        {
            switch (_Input)
            {
                case MovementInput.Left:
                    (mBody as MovementPhysicsObject).Accelerate(true);
                    break;
                case MovementInput.Right:
                    (mBody as MovementPhysicsObject).Accelerate(false);
                    break;
                case MovementInput.Up:
                    mBody.Body.ApplyLinearImpulse(new Vector2(0, _intensity));
                    break;
                case MovementInput.None:
                    (mBody as MovementPhysicsObject).DecreaseSidewaysSpeed();
                    break;
                default:
                    break;
            }
        }

        private bool mOnCollisionWheel(Fixture _FOne, Fixture _FTwo, Contact _Contact)
        {
            Console.WriteLine("Collision - Wheel");
            if (_FOne.Body.UserData == this)
                HandleCollisionWheel(_FOne, _FTwo, _Contact);
            else if (_FTwo.UserData == this)
                HandleCollisionWheel(_FTwo, _FOne, _Contact);


            return true;
        }
        private bool mOnCollisionBody(Fixture _FOne, Fixture _FTwo, Contact _Contact)
        {
            Console.WriteLine("Collision - Body");
            return true;
        }

        private bool HandleCollisionWheel(Fixture _Me, Fixture _Other, Contact _Contact)
        {
            return true;
        }



        public override void Draw(SpriteBatch _SB)
        {
            Rectangle Rec = (mBody as MovementPhysicsObject).GetDrawRectangle();
            TestAnimation.Draw(_SB, Rec, Color.White);            //(mBody as MovementPhysicsObject).Draw(_SB);
        }
    }
}
