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


        internal int Index { get { return mIndex; } }
        private int mIndex;
        internal PF_PlayerController(int _Index)
        {
        }

        internal void Draw(SpriteBatch _SB)
        {

        }

        internal override void Update(GameTime _GT)
        {
            LeftInputKeyPressed = InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad1);
            RightInputKeyPressed = InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad3);
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                /*WasIJumpingLastFrame = */mPawn.HandleInput(_GT, MovementInput.Jump);
                WasIJumpingLastFrame = true;
            }
            else
                WasIJumpingLastFrame = false;
            if (LeftInputKeyPressed)
                mPawn.HandleInput(_GT, MovementInput.Left);
            if (RightInputKeyPressed)
                mPawn.HandleInput(_GT, MovementInput.Right);
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad5))
                mPawn.DEBUG(new Vector2(0, -170), new Vector2(0, -750), true);
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad2))
                mPawn.DEBUG(new Vector2(0, 170), new Vector2(0, 750), true);
            if (!LeftInputKeyPressed && !RightInputKeyPressed)
                mPawn.HandleInput(_GT, MovementInput.None);

            if (InputManager.IsKeyClicked(Microsoft.Xna.Framework.Input.Keys.D1))
                AudioManager.PlayEffect(SoundEffectName.Movement_Jump);

            if (InputManager.IsKeyClicked(Microsoft.Xna.Framework.Input.Keys.D2))
                AudioManager.PlayEffect(SoundEffectName.Pickup_Coin);

            if (InputManager.IsKeyClicked(Microsoft.Xna.Framework.Input.Keys.D3))
                AudioManager.PlayEffect(SoundEffectName.Pickup_PowerUp);


        }
    }
}
