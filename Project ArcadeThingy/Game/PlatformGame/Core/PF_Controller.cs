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
        public int CoinCount { get; private set; } = 0;
        protected PF_Character mPawn;
        public bool LeftInputKeyPressed { get; protected set; }
        public bool RightInputKeyPressed { get; protected set; }
        public bool JumpInputKeyPressed { get; protected set; }

        public void AddCoins(int _NrOfCoins) { CoinCount = MathHelper.Clamp(CoinCount + _NrOfCoins, 0, PlatformGame.MAX_PLAYER_COINS); }
        public void RemoveCoins(int _NrOfCoins) { CoinCount = MathHelper.Clamp(CoinCount - _NrOfCoins, 0, PlatformGame.MAX_PLAYER_COINS); }
        public bool WasIJumpingLastFrame { get; protected set; }

        public void Set_Pawn(PF_Character _Input)
        {
            mPawn = _Input;
        }

        internal abstract void Update(GameTime _GT);
        internal void SetPawn(PF_Character _Pawn) { mPawn = _Pawn; }
    }
}
