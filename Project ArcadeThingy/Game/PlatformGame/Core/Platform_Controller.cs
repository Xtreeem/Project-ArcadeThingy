using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    abstract class Platform_Controller
    {
        protected Platform_Character mPawn;

        public bool WasIJumpingLastFrame { get; protected set; }

        public void Set_Pawn(Platform_Character _Input)
        {
            mPawn = _Input;
        }

        internal abstract void Update(GameTime _GT);
        internal void SetPawn(Platform_Character _Pawn) { mPawn = _Pawn; }
    }
}
