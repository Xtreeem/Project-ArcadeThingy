using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    abstract class Platform_Character : Platform_GameObj
    {
        //Jumping
        public bool CanJump { get { return mNumberOfJumpsRemaining > 0; } }
        protected int mNumberOfJumpsRemaining = 1;
        protected int mNumberOfJumps = 1;
        protected double mJumpTimerMax = 0.1;
        protected double mJumpTimer = 0.0;
        public bool WasIJumpingLastFrame { get; protected set; } = false;
        protected float mJumpStrengthInitial = 500.0f;
        protected float mJumpStrengthContinuous = 100.1f;

        //Misc
        public Platform_Controller Controller { get; protected set; }

        public Platform_Character(Platform_Controller _Controller)
        {
            Controller = _Controller;
            Controller.SetPawn(this);
        }

        public override void Update(GameTime _GT)
        {
            Controller.Update(_GT);
        }

        /// <summary>
        /// Will make ask the character to attempt to jump.
        /// </summary>
        /// <param name="_Override">If true overrides all the possible reasons a character might not be allowed to jump</param>
        public virtual void TryToJump(GameTime _GT, bool _Override = false)
        {
            if (!CanJump && !WasIJumpingLastFrame && !_Override) { WasIJumpingLastFrame = false; return; }
            if (mJumpTimer > mJumpTimerMax && !_Override) { WasIJumpingLastFrame = false; return; }
            mJumpTimer += _GT.ElapsedGameTime.TotalSeconds;

            if (WasIJumpingLastFrame) mBody.ApplyLinearVelocity(new Vector2(0, -mJumpStrengthContinuous));
            else mBody.ApplyLinearVelocity(new Vector2(0, -mJumpStrengthInitial));
            --mNumberOfJumpsRemaining;
            WasIJumpingLastFrame = true;
        }

        protected virtual void Landed()
        {
            //Reset Jump Variables
            mNumberOfJumpsRemaining = mNumberOfJumps;
            mJumpTimer = 0;
            WasIJumpingLastFrame = false;

            mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X, 0);
        }

        public override bool OnCollision(Fixture _Me, Fixture _Other, Contact _C)
        {
            if (_Other.UserData is BasePlatform)
            {
                if (_C.Direction() == CollisionDirection.Top)
                    Landed();
            }





            return true;
        }








    }
}
