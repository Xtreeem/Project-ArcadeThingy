using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class GameScene : Scene
    {
        PlatformGame TestGame;
        MaxiGame FestGame;
        bool mFesting = false;
        public GameScene()
        {
            TestGame = new PlatformGame(new Rectangle(0, 0, SceneManager.Width, SceneManager.Height));
            //FestGame = new MaxiGame(new Rectangle(0, 0, SceneManager.Width, SceneManager.Height));
        }

        public override void Update(GameTime _GT)
        {
            if (mFesting)
                FestGame.Update(_GT);
            else
                TestGame.Update(_GT);
        }

        public override void Draw(SpriteBatch _SB)
        {
            if (mFesting)
                FestGame.Draw(_SB);
            else
                TestGame.Draw(_SB);
        }
    }
}
