using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class GameScene : Scene
    {
        MiniGame TestGame;
        public GameScene()
        {
            TestGame = new MiniGame(new Rectangle(0, 0, SceneManager.Width, SceneManager.Height));
        }

        public override void HandleInput()
        {
        }

        public override void Update(GameTime _GT)
        {
            TestGame.Update(_GT);
        }

        public override void Draw(SpriteBatch _SB)
        {
            TestGame.Draw(_SB);
        }
    }
}
