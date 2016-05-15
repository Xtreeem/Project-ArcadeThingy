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
    public enum Platform_CharacterState
    {
        Grounded,
        Airbound,
        Stunned
    }

    abstract class Platform_Character : Platform_GameObj
    {
        //Jumping
        public bool CanJump { get { return mNumberOfJumpsRemaining > 0; } }
        protected int mNumberOfJumpsRemaining = 1;
        protected int mNumberOfJumps = 3;
        protected double mJumpTimerMax = 0.25;
        protected double mJumpTimer = 0.0;
        protected float mJumpStrengthInitial = 350.0f;
        protected float mJumpStrengthContinuous = 1000.1f;
        protected float mJumpStrengthSecondary = 300.1f;
        private double mJumpCooldown = 0.35;
        private double mJumpCooldownTimer = 0;
        private double mJumpGraceTimer = 0;
        private double mJumpGrace = 0.05;

        private bool mSideJump { get { return (mWallJumpTimer <= 0); } }
        private double mWallJumpTimer;
        private double mWallJumpTimerMax = 0.2;


        //Sideways Movemvent
        private float mMovementGroundAcceleration = 600.0f;
        private float mMovementAirAcceleration = 300.0f;
        private float mMovementTurningMultiplier = 3.0f;

        protected Vector2 MovementSpeedCap = new Vector2(450, 750);



        //Misc
        public Platform_Controller Controller { get; protected set; }
        public Platform_CharacterState State { get; protected set; } = Platform_CharacterState.Airbound;
        public BasePlatform mPlatform = null;




        public Platform_Character(Platform_Controller _Controller)
        {
            Controller = _Controller;
            Controller.SetPawn(this);
        }

        public override void Update(GameTime _GT)
        {
            Controller.Update(_GT);
            DidIFallOfCheck();
            SnapToPlatform();
            UpdateTimers(_GT);
        }



        public override void Draw(SpriteBatch _SB)
        {
            _SB.DrawString(ContentManager.Font, mNumberOfJumpsRemaining.ToString() + " - " + mBody.LinearVelocity.X.ToString().TruncateLongString(5).PadRight(5, '_') + " , " + mBody.LinearVelocity.Y.ToString().TruncateLongString(5).PadRight(5, '_'), mBody.Position + new Vector2(-50, -100), Color.Red);
            mTexture.Draw(_SB, mBody.GetDrawRectangle(), Color.White);
            if (mPlatform != null)
                _SB.DrawLine(mBody.Position, mPlatform.Body.Body.Position.UnitToPixels(), 1.0f, Color.Red);
            base.Draw(_SB);
        }

        protected virtual void UpdateTimers(GameTime _GT)
        {
            if (mJumpCooldownTimer > 0)
                mJumpCooldownTimer -= _GT.ElapsedGameTime.TotalSeconds;
            if (mJumpGraceTimer > 0)
                mJumpGraceTimer -= _GT.ElapsedGameTime.TotalSeconds;
        }

        internal bool HandleInput(GameTime _GT, MovementInput _Input, bool _Override = false)
        {
            switch (_Input)
            {
                case MovementInput.Left:
                    return HandleInput_SideWays(_GT, false);
                    break;
                case MovementInput.Right:
                    return HandleInput_SideWays(_GT, true);
                    break;
                case MovementInput.Jump:
                    return TryToJump(_GT, _Override);
                    break;
                case MovementInput.None:
                    break;
            }
            return true;
        }

        protected virtual bool HandleInput_SideWays(GameTime _GT, bool _Right, bool _Override = false)
        {
            switch (State)
            {
                case Platform_CharacterState.Stunned:
                    return false;
                case Platform_CharacterState.Airbound:
                    switch (_Right)
                    {
                        case true:
                            if (mBody.LinearVelocity.X > 0)
                                mBody.ApplyLinearVelocity(new Vector2(mMovementAirAcceleration * (float)_GT.ElapsedGameTime.TotalSeconds, 0), new Vector2(MovementSpeedCap.X, 0));
                            else
                                mBody.ApplyLinearVelocity(new Vector2(mMovementAirAcceleration * mMovementTurningMultiplier * (float)_GT.ElapsedGameTime.TotalSeconds, 0), new Vector2(MovementSpeedCap.X, 0));
                            break;
                        case false:
                            if (mBody.LinearVelocity.X < 0)
                                mBody.ApplyLinearVelocity(new Vector2(-mMovementAirAcceleration * (float)_GT.ElapsedGameTime.TotalSeconds, 0), new Vector2(-MovementSpeedCap.X, 0));
                            else
                                mBody.ApplyLinearVelocity(new Vector2(-mMovementAirAcceleration * mMovementTurningMultiplier * (float)_GT.ElapsedGameTime.TotalSeconds, 0), new Vector2(-MovementSpeedCap.X, 0));
                            break;
                    }
                    break;
                default:
                    switch (_Right)
                    {
                        case true:
                            if (mBody.LinearVelocity.X > 0)
                                mBody.ApplyLinearVelocity(new Vector2(mMovementGroundAcceleration * (float)_GT.ElapsedGameTime.TotalSeconds, 0), new Vector2(MovementSpeedCap.X, 0));
                            else
                                mBody.ApplyLinearVelocity(new Vector2(mMovementGroundAcceleration * mMovementTurningMultiplier * (float)_GT.ElapsedGameTime.TotalSeconds, 0), new Vector2(MovementSpeedCap.X, 0));
                            break;
                        case false:
                            if (mBody.LinearVelocity.X < 0)
                                mBody.ApplyLinearVelocity(new Vector2(-mMovementGroundAcceleration * (float)_GT.ElapsedGameTime.TotalSeconds, 0), new Vector2(-MovementSpeedCap.X, 0));
                            else
                                mBody.ApplyLinearVelocity(new Vector2(-mMovementGroundAcceleration * mMovementTurningMultiplier * (float)_GT.ElapsedGameTime.TotalSeconds, 0), new Vector2(-MovementSpeedCap.X, 0));
                            break;
                    }
                    break;
            }
            return true;
        }

        /// <summary>
        /// Will make ask the character to attempt to jump.
        /// </summary>
        /// <param name="_Override">If true overrides all the possible reasons a character might not be allowed to jump</param>
        public virtual bool TryToJump(GameTime _GT, bool _Override = false)
        {
            if (!CanJump && !Controller.WasIJumpingLastFrame && !_Override) return false;
            if (mJumpTimer < 0 && Controller.WasIJumpingLastFrame && !_Override) return false;
            mJumpTimer -= _GT.ElapsedGameTime.TotalSeconds;

            if (Controller.WasIJumpingLastFrame)
            {
                Console.WriteLine("Jump - Continouse");
                mBody.ApplyLinearVelocity(new Vector2(0, -mJumpStrengthContinuous) * (float)_GT.ElapsedGameTime.TotalSeconds, -MovementSpeedCap);
            }
            else if (mJumpCooldownTimer <= 0)
            {
                if (mBody.LinearVelocity.Y > 0 && State == Platform_CharacterState.Airbound)
                {
                    Console.WriteLine("Jump - Secondary - Downwards");
                    mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X, -mJumpStrengthSecondary);

                }
                else if (State == Platform_CharacterState.Airbound)
                {
                    Console.WriteLine("Jump - Secondary - Upwards");
                    mBody.ApplyLinearVelocity(new Vector2(0, -mJumpStrengthSecondary), -MovementSpeedCap);
                }
                else
                {
                    Console.WriteLine("Jump - Initial");
                    mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X, -mJumpStrengthInitial);
                }

                if (State == Platform_CharacterState.Grounded)
                    mJumpGraceTimer = mJumpGrace;
                mJumpCooldownTimer = mJumpCooldown;
                mJumpTimer = mJumpTimerMax;
                --mNumberOfJumpsRemaining;
            }
            else
            {
                Console.WriteLine("Jump on Cooldown");
                return false;
            }
            SetCharacterState(Platform_CharacterState.Airbound);
            mPlatform = null;
            return true;
        }

        protected virtual void Landed()
        {
            if (State == Platform_CharacterState.Grounded) return;
            //Reset Jump Variables
            mNumberOfJumpsRemaining = mNumberOfJumps;
            mJumpTimer = 0;
            Console.WriteLine("Landed");
            mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X, 0);

            SetCharacterState(Platform_CharacterState.Grounded);
        }

        protected void SetCharacterState(Platform_CharacterState _NewState)
        {
            State = _NewState;
        }

        public override bool OnCollision(Fixture _Me, Fixture _Other, Contact _C)
        {
            if (_Other.UserData is BasePlatform && mJumpGraceTimer <= 0)
            {
                if (_C.Direction() == CollisionDirection.Top)
                {
                    Landed();
                    mPlatform = (_Other.UserData as BasePlatform);
                }
            }
            return true;
        }

        protected bool DidIFallOfCheck()
        {
            if (mPlatform == null) return true;
            if (mPlatform.Body.Body.Position.UnitToPixels().X - mPlatform.Body.Size.X / 2 > mBody.Position.X)
                mPlatform = null;
            if (mPlatform == null) return true;
            if (mPlatform.Body.Body.Position.UnitToPixels().X + mPlatform.Body.Size.X / 2 < mBody.Position.X)
                mPlatform = null;

            return false;
        }
        private void SnapToPlatform()
        {
            if (mPlatform == null) return;
            mBody.Position = new Vector2(mBody.Position.X, mPlatform.Body.Body.Position.Y.UnitToPixels() - mBody.Radius - mPlatform.Body.Size.Y/2);
        }


        public void DEBUG(Vector2 _VectorOne, Vector2 VectorTwo, bool _Bool)
        {
            mBody.ApplyLinearVelocity(_VectorOne, VectorTwo);
        }






    }
}
