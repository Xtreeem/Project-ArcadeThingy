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
    public enum SideCharacterState
    {
        Idle = 0,
        Running = 1,
        Airbound = 2,
        Dying = 3
    }

    class SideScrollingCharacter : MovingObj
    {
        private AnimatedTexture mMainTexture;
        protected SideCharacterState mCurrentState;
        protected SideCharacterState mOldState;

        protected SideScrollerController mController;

        //Jumping variables
        private double mMaxJumpTime = 0.1;
        private float mJumpStrengthInitial = 0.7f;
        private float mJumpStrengthContinous = 0.1f;
        private double mJumpTimer = 0;
        private double mJumpGraceTimer = 0.1;
        private double mJumpGraceMaxTimer = 0.05;
        private double mJumpCooldown = 1.1;
        private double mJumpCooldownTimer = 0;


        public bool mCanIJump { get; private set; } = true;
        public bool mWasJumpingLastFrame { get; set; } = false;

        private BasePlatform mGroundPlatform = null;

        public SideScrollingCharacter(Vector2 _Size, Vector2 _Position, ref World _World, SideScrollerController _Controller) : base(_Size, _Position, ref _World)
        {
            mController = _Controller;
            mController.Set_Pawn(this);

            mBody = new MovementPhysicsObject(ref _World, this, _Size, _Position);
            mMainTexture = new AnimatedTexture();
            (mBody as MovementPhysicsObject).mWheel.OnCollision += mOnCollisionWheel;
            mBody.Body.OnCollision += mOnCollisionBody;
            SetupAnimations();
        }
        public override void Update(GameTime _GT)
        {
            mMainTexture.Update(_GT);
            //mBody.Body.Position = new Vector2(mBody.Body.Position.X, mBody.Body.Position.Y + (3.0f).PixelToUnit());
            base.Update(_GT);
            mOldState = mCurrentState;
            if (mJumpGraceTimer > 0)
                mJumpGraceTimer -= _GT.ElapsedGameTime.TotalSeconds;
            if (mJumpCooldownTimer > 0)
                mJumpCooldownTimer -= _GT.ElapsedGameTime.TotalSeconds;
            SnapToGround();
            //mGroundPlatform = null;

        }

        private void SnapToGround()
        {
            if (mGroundPlatform == null) return;
            Vector2 tNewPos = mGroundPlatform.Body.Body.Position.UnitToPixels();
            tNewPos.X = mBody.Body.Position.X.UnitToPixels();
            tNewPos.Y += mGroundPlatform.Body.Size.Y / 2;
            tNewPos.Y -= mBody.Size.Y;
            mBody.Body.Position = tNewPos.PixelsToUnits();

        }

        protected void SwapState(SideCharacterState _Input)
        {
            mCurrentState = _Input;
            //Console.WriteLine("Swapped State to : " + _Input.ToString());
        }

        protected virtual void SetupAnimations()
        {
            mMainTexture.AddAnimation(CreateIdleAnimation());
            mMainTexture.AddAnimation(CreateRunningAnimation());
            mMainTexture.AddAnimation(CreateAirboundAnimation());
            mMainTexture.AddAnimation(CreateDyingAnimation());
            mMainTexture.Initialize(0);


        }
        protected virtual AnimationDesc CreateAirboundAnimation()
        {
            return new AnimationDesc(0, new Vector2(80, 0), 16, 16, 1);
        }
        protected virtual AnimationDesc CreateIdleAnimation()
        {
            return new AnimationDesc(0, new Vector2(0, 0), 16, 16, 1);
        }
        protected virtual AnimationDesc CreateRunningAnimation()
        {
            return new AnimationDesc(0.1, new Vector2(16, 0), 16, 16, 3, true);
        }
        protected virtual AnimationDesc CreateTurnAnimation()
        {
            return new AnimationDesc(0, new Vector2(64, 0), 16, 16, 1);
        }
        protected virtual AnimationDesc CreateDyingAnimation()
        {
            return new AnimationDesc(0, new Vector2(0, 0), 16, 16, 1);
        }

        public override void AddVelocity(Vector2 _Input)
        {
            Vector2 tFinalSpeed = mBody.Body.LinearVelocity.UnitToPixels();
            tFinalSpeed.X = MathHelper.Clamp(tFinalSpeed.X + _Input.X, -mMaxVelocity.X, mMaxVelocity.X);
            mBody.Body.LinearVelocity = tFinalSpeed.PixelsToUnits();
        }

        public void HandleMovementInput(MovementInput _Input, float _intensity)
        {
            switch (_Input)
            {
                case MovementInput.Left:
                    mEffect = SpriteEffects.FlipHorizontally;
                    if (mCurrentState != SideCharacterState.Airbound)
                    {
                        (mBody as MovementPhysicsObject).Accelerate(true);
                        StateCheck();
                    }
                    break;
                case MovementInput.Right:
                    mEffect = SpriteEffects.None;
                    {
                        if (mCurrentState != SideCharacterState.Airbound)
                            (mBody as MovementPhysicsObject).Accelerate(false);
                        StateCheck();
                    }
                    break;
                case MovementInput.Up:
                    mBody.Body.ApplyLinearImpulse(new Vector2(0, _intensity));
                    SwapState(SideCharacterState.Airbound);
                    CheckAnimation();
                    break;
                case MovementInput.None:
                    (mBody as MovementPhysicsObject).DecreaseSidewaysSpeed();
                    StateCheck();
                    break;
                default:
                    break;
            }
        }

        private void StateCheck()
        {
            if (mCurrentState == SideCharacterState.Airbound) return;
            //Console.WriteLine(mBody.Body.LinearVelocity.X.UnitToPixels().ToString());
            if (Math.Abs(mBody.Body.LinearVelocity.X.UnitToPixels()) > 40)
            {
                SwapState(SideCharacterState.Running);
                if (mBody.Body.LinearVelocity.X.UnitToPixels() > 0)
                    mEffect = SpriteEffects.None;
                else
                    mEffect = SpriteEffects.FlipHorizontally;
            }
            else
                SwapState(SideCharacterState.Idle);

            CheckAnimation();
        }

        private void CheckAnimation()
        {
            if (mCurrentState != mOldState)
                switch (mCurrentState)
                {
                    case SideCharacterState.Idle:
                        mMainTexture.SwapAnimation(0);
                        //Console.WriteLine("Swapped animation to : " + mCurrentState.ToString());
                        break;
                    case SideCharacterState.Running:
                        mMainTexture.SwapAnimation(1);
                        //Console.WriteLine("Swapped animation to : " + mCurrentState.ToString());
                        break;
                    case SideCharacterState.Airbound:
                        mMainTexture.SwapAnimation(2);
                        //Console.WriteLine("Swapped animation to : " + mCurrentState.ToString());
                        break;
                    case SideCharacterState.Dying:
                        mMainTexture.SwapAnimation(3);
                        //Console.WriteLine("Swapped animation to : " + mCurrentState.ToString());
                        break;
                    default:
                        break;
                }
        }


        private bool mOnCollisionWheel(Fixture _FOne, Fixture _FTwo, Contact _Contact)
        {
            //Console.WriteLine("Collision - Wheel");
            if (_FOne.Body.UserData == this)
                HandleCollisionWheel(_FOne, _FTwo, _Contact);
            else if (_FTwo.UserData == this)
                HandleCollisionWheel(_FTwo, _FOne, _Contact);


            return true;
        }
        private bool mOnCollisionBody(Fixture _FOne, Fixture _FTwo, Contact _Contact)
        {
            //Console.WriteLine("Collision - Body");
            return true;
        }

        private bool HandleCollisionWheel(Fixture _Me, Fixture _Other, Contact _Contact)
        {
            if (_Other.Body.UserData is BasePlatform)
                if (mJumpGraceTimer <= 0)
                {
                    mGroundPlatform = (_Other.Body.UserData as BasePlatform);
                    Landed();
                }


            return true;
        }

        private void Landed()
        {
            if (mCurrentState == SideCharacterState.Airbound) mCurrentState = SideCharacterState.Idle;
            StateCheck();
            mCanIJump = true;
            mJumpTimer = 0;
            mBody.Body.LinearVelocity = new Vector2(mBody.Body.LinearVelocity.X, 0);
        }

        public override void Draw(SpriteBatch _SB)
        {
            Rectangle Rec = (mBody as MovementPhysicsObject).GetDrawRectangle();
            mMainTexture.Draw(_SB, Rec, Color.White, 0, mEffect);            //(mBody as MovementPhysicsObject).Draw(_SB);
            _SB.DrawString(ContentManager.Font, mCurrentState.ToString(), new Vector2(Rec.X, Rec.Y), Color.Red);
            if (mGroundPlatform != null)
                _SB.DrawLine(mGroundPlatform.Body.Body.Position.UnitToPixels(), mBody.Body.Position.UnitToPixels(), 0.5f, Color.Red);
        }

        public void TryJump(GameTime _GT)
        {
            Console.WriteLine(mCanIJump + " - " + mWasJumpingLastFrame + " " + mJumpTimer + " / " + mMaxJumpTime);
            if ((!mCanIJump) && !mWasJumpingLastFrame) return;
            if (mJumpTimer > mMaxJumpTime) return;
            if (mJumpCooldownTimer > 0) return;
            if (!mWasJumpingLastFrame)
                mJumpGraceTimer = mJumpGraceMaxTimer;
            mJumpTimer += _GT.ElapsedGameTime.TotalSeconds;
            if (mWasJumpingLastFrame)
            {
                Console.WriteLine("Continous jump");
                HandleMovementInput(MovementInput.Up, -mJumpStrengthContinous);
            }
            else
                HandleMovementInput(MovementInput.Up, -mJumpStrengthInitial);
            Console.WriteLine("Pring - " + mJumpTimer);
            mCanIJump = false;
            mWasJumpingLastFrame = true;
            mGroundPlatform = null;
        }

    }
}
