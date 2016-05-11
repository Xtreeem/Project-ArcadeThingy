using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    class PlayerController : Controller
    {
        //Jumping variables
        private double mMaxJumpTime = 0.1;
        private float mJumpStrengthInitial = 0.9f;
        private float mJumpStrengthContinous = 0.1f;
        private double mJumpTimer = 0;
        private bool mCanIJump = true;
        private bool mWasJumpingLastFrame = false;

        public int Index { get { return mIndex; } }
        private int mIndex;
        public PlayerController(int _Index)
        {
        }

        private void Landed()
        {
            mCanIJump = true;
            mJumpTimer = mMaxJumpTime;
        }

        private void TryJump(GameTime _GT)
        {
            if ((!mCanIJump) && !mWasJumpingLastFrame) return;
            if (mJumpTimer > mMaxJumpTime) return;

            mJumpTimer += _GT.ElapsedGameTime.TotalSeconds;
            if (mWasJumpingLastFrame)
                mPawn.HandleMovementInput(MovementInput.Up, -mJumpStrengthContinous);
            else
                mPawn.HandleMovementInput(MovementInput.Up, -mJumpStrengthInitial);
            Console.WriteLine("Pring - " + mJumpTimer);
            mCanIJump = false;
            mWasJumpingLastFrame = true;
        }

        public void Update(GameTime _GT)
        {
            if (mPawn != null)
            {
                Vector2 FinalVelocity = Vector2.Zero;
                if (InputManager.PlayerOneButtonMoveLeft && !InputManager.PlayerOneButtonMoveRight)
                {
                    mPawn.HandleMovementInput(MovementInput.Left, 10);
                }
                else if (InputManager.PlayerOneButtonMoveRight && !InputManager.PlayerOneButtonMoveLeft)
                {
                    mPawn.HandleMovementInput(MovementInput.Right, 10);
                }
                else
                    mPawn.HandleMovementInput(MovementInput.None, 10);
                if (InputManager.PlayerOneButtonMoveJump)
                {
                    TryJump(_GT);
                }
                else
                    mWasJumpingLastFrame = false;
            }




        }

        public void Draw(SpriteBatch _SB)
        {

        }
    }
}
