using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public static class InputManager
    {
        #region PlayerOne
        public static bool PlayerOneJoystickUp { get { return mPlayerOneJoystickUp; } }
        public static bool PlayerOneJoystickDown { get { return mPlayerOneJoystickDown; } }
        public static bool PlayerOneJoystickLeft { get { return mPlayerOneJoystickLeft; } }
        public static bool PlayerOneJoystickRight { get { return mPlayerOneJoystickRight; } }
        private static bool mPlayerOneJoystickUp;
        private static bool mPlayerOneJoystickDown;
        private static bool mPlayerOneJoystickLeft;
        private static bool mPlayerOneJoystickRight;

        public static bool PlayerOneButtonMoveJump { get { return mPlayerOneButtonMoveJump; } }
        public static bool PlayerOneButtonMoveLeft { get { return mPlayerOneButtonMoveLeft; } }
        public static bool PlayerOneButtonMoveRight { get { return mPlayerOneButtonMoveRight; } }
        private static bool mPlayerOneButtonMoveLeft;
        private static bool mPlayerOneButtonMoveRight;
        private static bool mPlayerOneButtonMoveJump;

        public static bool PlayerOneButtonSpawnOne { get { return mPlayerOneButtonSpawnOne; } }
        public static bool PlayerOneButtonSpawnTwo { get { return mPlayerOneButtonSpawnTwo; } }
        public static bool PlayerOneButtonSpawnThree { get { return mPlayerOneButtonSpawnThree; } }
        private static bool mPlayerOneButtonSpawnOne;
        private static bool mPlayerOneButtonSpawnTwo;
        private static bool mPlayerOneButtonSpawnThree;
        #endregion

        #region PlayerTwo
        public static bool PlayerTwoJoystickUp { get { return mPlayerTwoJoystickUp; } }
        public static bool PlayerTwoJoystickDown { get { return mPlayerTwoJoystickDown; } }
        public static bool PlayerTwoJoystickLeft { get { return mPlayerTwoJoystickLeft; } }
        public static bool PlayerTwoJoystickRight { get { return mPlayerTwoJoystickRight; } }
        private static bool mPlayerTwoJoystickUp;
        private static bool mPlayerTwoJoystickDown;
        private static bool mPlayerTwoJoystickLeft;
        private static bool mPlayerTwoJoystickRight;

        public static bool PlayerTwoButtonMoveJump { get { return mPlayerTwoButtonMoveJump; } }
        public static bool PlayerTwoButtonMoveLeft { get { return mPlayerTwoButtonMoveLeft; } }
        public static bool PlayerTwoButtonMoveRight { get { return mPlayerTwoButtonMoveRight; } }
        private static bool mPlayerTwoButtonMoveLeft;
        private static bool mPlayerTwoButtonMoveRight;
        private static bool mPlayerTwoButtonMoveJump;

        public static bool PlayerTwoButtonSpawnOne { get { return mPlayerTwoButtonSpawnOne; } }
        public static bool PlayerTwoButtonSpawnTwo { get { return mPlayerTwoButtonSpawnTwo; } }
        public static bool PlayerTwoButtonSpawnThree { get { return mPlayerTwoButtonSpawnThree; } }
        private static bool mPlayerTwoButtonSpawnOne;
        private static bool mPlayerTwoButtonSpawnTwo;
        private static bool mPlayerTwoButtonSpawnThree;
        #endregion

        private static KeyboardState mOldState, mKeyboardState;

        public static void Update()
        {
            mKeyboardState = Keyboard.GetState();

            mPlayerOneButtonMoveJump = IsKeyPressed(Keys.NumPad2);
            mPlayerOneButtonMoveLeft = IsKeyPressed(Keys.NumPad1);
            mPlayerOneButtonMoveRight = IsKeyPressed(Keys.NumPad3);
            mPlayerOneButtonSpawnOne = IsKeyClicked(Keys.NumPad4);
            mPlayerOneButtonSpawnTwo = IsKeyClicked(Keys.NumPad5);
            mPlayerOneButtonSpawnThree = IsKeyClicked(Keys.NumPad6);
            mPlayerOneJoystickUp = IsKeyPressed(Keys.Up);
            mPlayerOneJoystickDown = IsKeyPressed(Keys.Down);
            mPlayerOneJoystickLeft = IsKeyPressed(Keys.Left);
            mPlayerOneJoystickRight = IsKeyPressed(Keys.Right);

            mPlayerTwoButtonMoveJump = IsKeyPressed(Keys.H);
            mPlayerTwoButtonMoveLeft = IsKeyPressed(Keys.J);
            mPlayerTwoButtonMoveRight = IsKeyPressed(Keys.K);
            mPlayerTwoButtonSpawnOne = IsKeyClicked(Keys.U);
            mPlayerTwoButtonSpawnTwo = IsKeyClicked(Keys.I);
            mPlayerTwoButtonSpawnThree = IsKeyClicked(Keys.O);
            mPlayerTwoJoystickUp = IsKeyPressed(Keys.W);
            mPlayerTwoJoystickDown = IsKeyPressed(Keys.S);
            mPlayerTwoJoystickLeft = IsKeyPressed(Keys.A);
            mPlayerTwoJoystickRight = IsKeyPressed(Keys.D);

            mOldState = mKeyboardState;
        }

        private static bool IsKeyClicked(Keys _Key)
        {
            return (mKeyboardState.IsKeyDown(_Key) && mOldState.IsKeyUp(_Key)) ;
        }

        private static bool IsKeyPressed(Keys _Key)
        {
            return mKeyboardState.IsKeyDown(_Key);
        }


    }
}
