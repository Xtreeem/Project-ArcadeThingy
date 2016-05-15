using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    abstract class PF_Controller
    {
        protected PF_Character mPawn;
        public bool LeftInputKeyPressed { get; protected set; }
        public bool RightInputKeyPressed { get; protected set; }


        public bool WasIJumpingLastFrame { get; protected set; }

        public void Set_Pawn(PF_Character _Input)
        {
            mPawn = _Input;
        }

        internal abstract void Update(GameTime _GT);
        internal void SetPawn(PF_Character _Pawn) { mPawn = _Pawn; }
    }
}
