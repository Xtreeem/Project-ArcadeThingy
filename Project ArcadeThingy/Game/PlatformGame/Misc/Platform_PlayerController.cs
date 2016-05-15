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


        public int Index { get { return mIndex; } }
        private int mIndex;
        public Platform_PlayerController(int _Index)
        {
        }

        public void Draw(SpriteBatch _SB)
        {

        }

        internal override void Update(GameTime _GT)
        {
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
                mPawn.TryToJump(_GT);
        }
    }
}
