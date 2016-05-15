using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    class Platform_PlayerController : Platform_Controller
    {


        internal int Index { get { return mIndex; } }
        private int mIndex;
        internal Platform_PlayerController(int _Index)
        {
        }

        internal void Draw(SpriteBatch _SB)
        {

        }

        internal override void Update(GameTime _GT)
        {
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                /*WasIJumpingLastFrame = */mPawn.HandleInput(_GT, MovementInput.Jump);
                WasIJumpingLastFrame = true;
            }
            else
                WasIJumpingLastFrame = false;
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad1))
                mPawn.HandleInput(_GT, MovementInput.Left);
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad3))
                mPawn.HandleInput(_GT, MovementInput.Right);
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad5))
                mPawn.DEBUG(new Vector2(0, -170), new Vector2(0, -750), true);
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.NumPad2))
                mPawn.DEBUG(new Vector2(0, 170), new Vector2(0, 750), true);
        }
    }
}
