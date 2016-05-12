using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    class PlayerController : SideScrollerController
    {


        public int Index { get { return mIndex; } }
        private int mIndex;
        public PlayerController(int _Index)
        {
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
                    mPawn.TryJump(_GT);
                }
                else
                {
                    //Console.WriteLine("Setting was jumping to false");
                    mPawn.mWasJumpingLastFrame = false;
                }
            }




        }

        public void Draw(SpriteBatch _SB)
        {

        }
    }
}
