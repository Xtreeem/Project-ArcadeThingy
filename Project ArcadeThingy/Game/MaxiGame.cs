using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    class MaxiGame
    {
        protected List<GameObj> mGameObjects = new List<GameObj>();
        public Rectangle Bounds { get { return mBounds; } }
        protected Rectangle mBounds;



        public MaxiGame(Rectangle _Bounds)
        {
            mBounds = _Bounds;
        }

        public void Update(GameTime _GT)
        {
            for (int i = 0; i < mGameObjects.Count; ++i)
                mGameObjects[i].Update(_GT);
        }

        public void Draw(SpriteBatch _SB)
        {
            for (int i = 0; i < mGameObjects.Count; ++i)
                mGameObjects[i].Draw(_SB);
        }
    }
}