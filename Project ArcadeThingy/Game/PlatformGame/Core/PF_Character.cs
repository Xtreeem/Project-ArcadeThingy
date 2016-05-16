using System;
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

    abstract class PF_Character : PF_GameObj
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

        //Wall-Jumping/Sticking
        private bool mCanWallJump { get { return (mWallJumpPlatform != null); } }
        private double mWallStickTimer;
        private double mWallStickTimerMax = 2.5;
        private CollisionDirection mWallJumpDirection;
        private Vector2 mWallJumpPreviousVelocity;
        private PF_Platform_Base mWallJumpPlatform;
        private bool mWallStickPossible = true;
        private double mWallStickGraceTimer;
        private double mWallStickGraceTimerMax = 0.05;
        private Vector2 mWallStickGoingInVelocity;


        //Sideways Movemvent
        private float mMovementGroundAcceleration = 600.0f;
        private float mMovementAirAcceleration = 300.0f;
        private float mMovementTurningMultiplier = 3.0f;
        private Vector2 mWallJumpStrengthInitial = new Vector2(350.0f, -550.0f);
        private float mMovementGroundDeceleration = 1000.0f;

        protected Vector2 MovementSpeedCap = new Vector2(450, 750);



        //Misc
        public PF_Controller Controller { get; protected set; }
        public Platform_CharacterState State { get; protected set; } = Platform_CharacterState.Airbound;
        public PF_Platform_Base mPlatform = null;
        public float mBounceOnCharacterHeadPower = 550;

        public PF_Character(PF_Controller _Controller, World _World)
        {
            mWorld = _World;
            Controller = _Controller;
            Controller.SetPawn(this);
        }

        public override void Update(GameTime _GT)
        {
            Controller.Update(_GT);
            DidIFallOfCheck();
            DidISlideOffCheck();
            SnapToPlatform();
            SnapToWall(_GT);
            UpdateTimers(_GT);
        }

        public override void Draw(SpriteBatch _SB)
        {
            //_SB.DrawString(ContentManager.Font, mBody.Position.X.ToString().TruncateLongString(4) + " , " + mBody.Position.Y.ToString().TruncateLongString(4), mBody.Position + new Vector2(-50, -100), Color.Red);
            //mTexture.Draw(_SB, mBody.GetDrawRectangle(), Color.White);
            mTexture.Draw(_SB, mBody.GetDrawRectangle(), mBody.IsUserDataNull ? Color.Red : Color.White);
            if (mPlatform != null)
                _SB.DrawLine(mBody.Position, mPlatform.Body.Position, 1.0f, Color.Red);

            if (mWallJumpPlatform != null)
                _SB.DrawLine(mBody.Position, mWallJumpPlatform.Body.Position, 1.0f, Color.Green);


            base.Draw(_SB);
        }

        protected virtual void UpdateTimers(GameTime _GT)
        {
            if (mJumpCooldownTimer > 0)
                mJumpCooldownTimer -= _GT.ElapsedGameTime.TotalSeconds;
            if (mJumpGraceTimer > 0)
                mJumpGraceTimer -= _GT.ElapsedGameTime.TotalSeconds;
            if (mWallStickGraceTimer > 0)
                mWallStickGraceTimer -= _GT.ElapsedGameTime.TotalSeconds;
        }

        protected void SetCharacterState(Platform_CharacterState _NewState)
        {
            State = _NewState;
        }

        #region Inputs

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
                    HandleInput_None(_GT);
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

        protected virtual void HandleInput_None(GameTime _GT)
        {
            if (mBody.LinearVelocity.X == 0) return;
            if (State == Platform_CharacterState.Airbound) return;

            if (Math.Abs(mBody.LinearVelocity.X) < Math.Abs(mMovementGroundDeceleration * _GT.ElapsedGameTime.TotalSeconds))
            {
                mBody.LinearVelocity = new Vector2(0, mBody.LinearVelocity.Y); return;
            }
            if (mBody.LinearVelocity.X > 0)
                mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X - mMovementGroundDeceleration * (float)_GT.ElapsedGameTime.TotalSeconds, mBody.LinearVelocity.Y);
            else
                mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X + mMovementGroundDeceleration * (float)_GT.ElapsedGameTime.TotalSeconds, mBody.LinearVelocity.Y);
        }

        #endregion

        #region Jumping

        /// <summary>
        /// Will make ask the character to attempt to jump.
        /// </summary>
        /// <param name="_Override">If true overrides all the possible reasons a character might not be allowed to jump</param>
        public virtual bool TryToJump(GameTime _GT, bool _Override = false)
        {
            if (!CanJump && !Controller.WasIJumpingLastFrame && !_Override) return false;
            if (mWallJumpPlatform != null && mPlatform == null) return WallJump();
            if (mJumpTimer < 0 && Controller.WasIJumpingLastFrame && !_Override) return false;
            mJumpTimer -= _GT.ElapsedGameTime.TotalSeconds;

            if (Controller.WasIJumpingLastFrame && mPlatform == null)
            {
                //Console.WriteLine("Jump - Continouse");
                mBody.ApplyLinearVelocity(new Vector2(0, -mJumpStrengthContinuous) * (float)_GT.ElapsedGameTime.TotalSeconds, -MovementSpeedCap);
            }
            else if (mJumpCooldownTimer <= 0)
            {
                if (mBody.LinearVelocity.Y > 0 && State == Platform_CharacterState.Airbound)
                {
                    //Console.WriteLine("Jump - Secondary - Downwards");
                    mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X, -mJumpStrengthSecondary);
                    AudioManager.PlayEffect(SoundEffectName.Movement_Jump);

                }
                else if (State == Platform_CharacterState.Airbound)
                {
                    //Console.WriteLine("Jump - Secondary - Upwards");
                    mBody.ApplyLinearVelocity(new Vector2(0, -mJumpStrengthSecondary), -MovementSpeedCap);
                    AudioManager.PlayEffect(SoundEffectName.Movement_Jump);
                }
                else
                {
                    //Console.WriteLine("Jump - Initial");
                    mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X, -mJumpStrengthInitial);
                    AudioManager.PlayEffect(SoundEffectName.Movement_Jump);
                }

                if (State == Platform_CharacterState.Grounded)
                    mJumpGraceTimer = mJumpGrace;
                mJumpCooldownTimer = mJumpCooldown;
                mJumpTimer = mJumpTimerMax;
                mWallStickPossible = true;
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

        protected virtual bool WallJump()
        {
            UnstickFromWall();
            if (mWallJumpDirection == CollisionDirection.Right)
                mBody.LinearVelocity = mWallJumpStrengthInitial;
            else
                mBody.LinearVelocity = new Vector2(-mWallJumpStrengthInitial.X, mWallJumpStrengthInitial.Y);
            mWallStickTimer = 0;
            mWallStickPossible = true;
            AudioManager.PlayEffect(SoundEffectName.Movement_Jump);

            return true;
        }
        protected virtual void Landed()
        {
            if (State == Platform_CharacterState.Grounded) return;
            //Reset Jump Variables
            mNumberOfJumpsRemaining = mNumberOfJumps;
            mJumpTimer = 0;
            mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X, 0);
            mWallStickPossible = true;
            SetCharacterState(Platform_CharacterState.Grounded);
        }
        #endregion

        #region WallSticking

        protected virtual void StickToWall(PF_Platform_Base _Wall)
        {
            if (!mWallStickPossible) return;
            mWallJumpPlatform = _Wall;
            mWallStickTimer = 0;
            mWallStickPossible = false;
            mBody.LinearVelocity = new Vector2(0, 0.0f);
            mWallStickGraceTimer = mWallStickGraceTimerMax;

        }

        protected virtual void SnapToWall(GameTime _GT)
        {
            if (mWallJumpPlatform == null) return;
            if (mWallStickTimer >= mWallStickTimerMax) { UnstickFromWall(); return; }
            else mWallStickTimer += _GT.ElapsedGameTime.TotalSeconds;

            if (mWallStickGraceTimer <= 0)
                mNumberOfJumpsRemaining = mNumberOfJumps;

            if (mWallJumpDirection == CollisionDirection.Right)
                mBody.Position = new Vector2(mWallJumpPlatform.Body.Position.X + mBody.Radius + mWallJumpPlatform.Body.Size.X / 2, mBody.Position.Y);
            else
                mBody.Position = new Vector2(mWallJumpPlatform.Body.Position.X - mBody.Radius - mWallJumpPlatform.Body.Size.X / 2, mBody.Position.Y);
            mBody.GravityScale = MathHelper.Clamp((float)(mWallStickTimer / mWallStickTimerMax), 0.0f, 1.0f);

            switch (mWallJumpDirection)
            {
                case CollisionDirection.Right:
                    if (!Controller.LeftInputKeyPressed)
                        UnstickFromWall();
                    break;
                case CollisionDirection.Left:
                    if (!Controller.RightInputKeyPressed)
                        UnstickFromWall();
                    break;
                default:
                    break;
            }
        }

        protected virtual void UnstickFromWall()
        {
            mBody.GravityScale = 1;
            mWallJumpPlatform = null;
            if (mWallStickGraceTimer > 0)
            {
                mWallStickPossible = true;
                mBody.LinearVelocity = new Vector2(-mWallStickGoingInVelocity.X / 2, mWallStickGoingInVelocity.Y);
            }
        }

        protected bool DidISlideOffCheck()
        {
            if (mWallJumpPlatform == null) return true;
            if (mWallJumpPlatform.Body.Position.Y - mWallJumpPlatform.Body.Size.Y / 2 > mBody.Position.Y)
                UnstickFromWall();
            if (mWallJumpPlatform == null) return true;
            if (mWallJumpPlatform.Body.Position.Y + mWallJumpPlatform.Body.Size.Y / 2 < mBody.Position.Y)
                UnstickFromWall();

            return false;
        }
        #endregion


        protected bool DidIFallOfCheck()
        {
            if (mPlatform == null) return true;
            if (mPlatform.Body.Position.X - mPlatform.Body.Size.X / 2 > mBody.Position.X)
                mPlatform = null;
            if (mPlatform == null) return true;
            if (mPlatform.Body.Position.X + mPlatform.Body.Size.X / 2 < mBody.Position.X)
                mPlatform = null;

            return false;
        }
        private void SnapToPlatform()
        {
            if (mPlatform == null) return;
            mBody.Position = new Vector2(mBody.Position.X, mPlatform.Body.Position.Y - mBody.Radius - mPlatform.Body.Size.Y / 2);
        }




        public override bool OnCollision(Fixture _Me, Fixture _Other, Contact _C)
        {
            if (_Other.Body.UserData is PF_Platform_Base && mJumpGraceTimer <= 0)
            {
                return CollisionWithPlatform(_Other.Body.UserData as PF_Platform_Base, _C);
            }
            else if (_Other.Body.UserData is PF_Character)
            {
                return CollisionWithCharacter(_Other.Body.UserData as PF_Character, _C);
            }

            return true;
        }

        protected virtual bool CollisionWithPlatform(PF_Platform_Base _Platform, Contact _C)
        {
            switch (_C.Direction())
            {
                case CollisionDirection.Right:
                    mWallJumpDirection = CollisionDirection.Right;
                    mWallStickGoingInVelocity = mBody.LinearVelocity;
                    StickToWall(_Platform);
                    break;
                case CollisionDirection.Left:
                    mWallJumpDirection = CollisionDirection.Left;
                    mWallStickGoingInVelocity = mBody.LinearVelocity;
                    StickToWall(_Platform);
                    break;
                case CollisionDirection.Bottom:
                    break;
                case CollisionDirection.Top:
                    Landed();
                    mPlatform = (_Platform);
                    break;
                default:
                    break;
            }
            return true;
        }
        protected virtual bool CollisionWithCharacter(PF_Character _OtherGuy, Contact _C)
        {
            switch (Utilities.CircleCollisionDirection(mBody, _OtherGuy.Body))
            {
                case CollisionDirection.Right:
                    break;
                case CollisionDirection.Left:
                    break;
                case CollisionDirection.Bottom:
                    if (Controller.CoinCount > 0)
                    {
                        int CoinsDropped = Controller.CoinCount / 10;
                        DropCoins(CoinsDropped, 350, 350, 1.5, false);
                        Controller.RemoveCoins(CoinsDropped);
                    }
                    break;
                case CollisionDirection.Top:
                    mBody.LinearVelocity = new Vector2(mBody.LinearVelocity.X, 0);
                    mBody.ApplyLinearVelocity(new Vector2(0, -mBounceOnCharacterHeadPower));
                    break;
                default:
                    break;
            }
            return true;
        }



        public void DEBUG(Vector2 _VectorOne, Vector2 VectorTwo, bool _Bool)
        {
            DropCoins(10, 450, 250, 1.5, false);
        }
    }
}
