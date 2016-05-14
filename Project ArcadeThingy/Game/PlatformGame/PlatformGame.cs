using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public class PlatformGame
    {
        //Farseer
        World mWorld = new World(new Vector2(0, 9.8f));
        AnimatedTexture TestingTexture = new AnimatedTexture(ContentManager.MarioSheet);


        PlayerController ControllerOne = new PlayerController(1);
        PlayerController ControllerTwo = new PlayerController(2);
        const int QuadTreeBuffert = 50;
        Vector2 mPositionOffSet;
        public Rectangle Bounds { get { return mBounds; } }
        protected Rectangle mBounds;

        private List<GameObj> mGameObjects = new List<GameObj>();
        private List<Platform_GameObj> mPlatObjects = new List<Platform_GameObj>();

        public PlatformGame(Rectangle _Bounds)
        {
            TestingTexture.AddAnimation(new AnimationDesc(0, new Vector2(0, 0), 16, 16, 1));
            TestingTexture.Initialize(0);

            mBounds = _Bounds;
            mPositionOffSet = new Vector2(mBounds.X, mBounds.Y);

            //mGameObjects.Add(new BasicPlatform(new Vector2(1920, 16), new Vector2(960, 1060), ref mWorld));

            //mGameObjects.Add(new BasicPlatform(new Vector2(64, 16), new Vector2(50, 860), ref mWorld));
            //mGameObjects.Add(new BasicPlatform(new Vector2(64, 16), new Vector2(250, 860), ref mWorld));
            ////mGameObjects.Add(new BasicPlatform(new Vector2(64, 16), new Vector2(50, 1010), ref mWorld));
            ////mGameObjects.Add(new BasicPlatform(new Vector2(64, 16), new Vector2(50, 960), ref mWorld));
            ////mGameObjects.Add(new BasicPlatform(new Vector2(64, 16), new Vector2(100, 955), ref mWorld));
            ////mGameObjects.Add(new BasicPlatform(new Vector2(64, 16), new Vector2(500, 485), ref mWorld));


            mGameObjects.AddRange(FileUtils.GetPlatforms(ref mWorld));
            //mGameObjects.Add(new SideScrollingPlayerCharacter(new Vector2(32, 64), new Vector2(170, 00), ref mWorld, ControllerOne));

            mPlatObjects.Add(new Platform_Player(mWorld, new Vector2(250, 250), 32, ContentManager.Platformer_Character_Smiley));

        }

        public void AddGameObj(GameObj _Input)
        {
            //mGameObjects.Add(_Input);
        }

        public void Update(GameTime _GT)
        {
            //Farseer
            mWorld.Step((float)_GT.ElapsedGameTime.TotalSeconds);
            //ControllerOne.Update(_GT);
            //ControllerTwo.Update(_GT);

            //for (int i = 0; i < mGameObjects.Count; i++)
            //mGameObjects[i].Update(_GT);

            //TestingTexture.Update(_GT);

            //for (int i = 0; i < mGameObjects.Count; i++)
            //    mGameObjects[i].Update(_GT);

            for (int i = 0; i < mPlatObjects.Count; i++)
                mPlatObjects[i].Update(_GT);
        }

        public void Draw(SpriteBatch _SB)
        {
            ////Farseer
            //Vector2 tPos = new Vector2((Test.Position.X * cUnitToPixel), (Test.Position.Y * cUnitToPixel));
            //Vector2 tPos2 = new Vector2((Test2.Position.X * cUnitToPixel), (Test2.Position.Y * cUnitToPixel));
            //Vector2 tScale = new Vector2(50 / (float)ContentManager.BasicPlatform.Width, 50 / (float)ContentManager.BasicPlatform.Height);
            //Vector2 tOrigin = new Vector2(ContentManager.BasicPlatform.Width / 2, ContentManager.BasicPlatform.Height / 2);
            //_SB.Draw(ContentManager.BasicPlatform, tPos, null, Color.White, Test.Rotation, tOrigin, tScale, SpriteEffects.None, 1.0f);
            //_SB.Draw(ContentManager.BasicPlatform, tPos2, null, Color.White, Test2.Rotation, tOrigin, tScale, SpriteEffects.None, 1.0f);
            //Test3.Draw(_SB);
            //Test4.Draw(_SB);


            //TestingTexture.Draw(_SB, new Rectangle(150, 150, 200, 200), Color.White);

            for (int i = 0; i < mGameObjects.Count; i++)
                mGameObjects[i].Draw(_SB);

            for (int i = 0; i < mPlatObjects.Count; i++)
                mPlatObjects[i].Draw(_SB);
        }




    }
}
