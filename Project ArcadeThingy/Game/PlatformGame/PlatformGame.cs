using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    public class PlatformGame
    {
        World mWorld = new World(new Vector2(0, 9.8f));

        PF_PlayerController ControllerOne = new PF_PlayerController(1);
        PF_PlayerController ControllerTwo = new PF_PlayerController(2);
        public Rectangle Bounds { get { return mBounds; } }
        protected Rectangle mBounds;
        private List<PF_GameObj> mPlatObjects = new List<PF_GameObj>();

        public PlatformGame(Rectangle _Bounds)
        {
            mBounds = _Bounds;

            mPlatObjects.Add(new PF_Player(mWorld, new Vector2(250, 250), 32, ContentManager.Platformer_Character_Smiley, ControllerOne));
        }

        public void Update(GameTime _GT)
        {
            mWorld.Step((float)_GT.ElapsedGameTime.TotalSeconds);

            for (int i = 0; i < mPlatObjects.Count; i++)
                mPlatObjects[i].Update(_GT);
        }

        public void Draw(SpriteBatch _SB)
        {
            for (int i = 0; i < mPlatObjects.Count; i++)
                mPlatObjects[i].Draw(_SB);
        }
    }
}