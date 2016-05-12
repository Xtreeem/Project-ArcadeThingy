using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    abstract class SideScrollerController
    {
        protected SideScrollingCharacter mPawn;

        public void Set_Pawn(SideScrollingCharacter _Input)
        {
            mPawn = _Input;
        }
    }
}
