using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    abstract class Controller
    {
        protected Character mPawn;

        public void Set_Pawn(Character _Input)
        {
            mPawn = _Input;
        }
    }
}
