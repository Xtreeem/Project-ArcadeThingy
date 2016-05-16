using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    class PF_PlayerController : PF_Controller
    {


        public int Index { get { return mIndex; } }
        private int mIndex;
        internal PF_PlayerController(int _Index)
        {
            mIndex = _Index;
        }

        internal void Draw(SpriteBatch _SB)
        {

        }

        internal override void Update(GameTime _GT)
        {
            if (mIndex == 1)
            {
                LeftInputKeyPressed = InputManager.PlayerOneButtonMoveLeft || InputManager.PlayerOneJoystickLeft;
                RightInputKeyPressed = InputManager.PlayerOneButtonMoveRight || InputManager.PlayerOneJoystickRight;
                JumpInputKeyPressed = InputManager.PlayerOneButtonMoveJump;
            }
            else
            {
                LeftInputKeyPressed = InputManager.PlayerTwoButtonMoveLeft || InputManager.PlayerTwoJoystickLeft;
                RightInputKeyPressed = InputManager.PlayerTwoButtonMoveRight || InputManager.PlayerTwoJoystickRight;
                JumpInputKeyPressed = InputManager.PlayerTwoButtonMoveJump;
            }
            if (JumpInputKeyPressed)
            {
                mPawn.HandleInput(_GT, MovementInput.Jump);
                WasIJumpingLastFrame = true;
            }
            else
                WasIJumpingLastFrame = false;
            if (LeftInputKeyPressed)
                mPawn.HandleInput(_GT, MovementInput.Left);
            if (RightInputKeyPressed)
                mPawn.HandleInput(_GT, MovementInput.Right);
            if (!LeftInputKeyPressed && !RightInputKeyPressed)
                mPawn.HandleInput(_GT, MovementInput.None);
        }
    }
}
