using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
            AudioManager.PlaySong(Songs.First);
            mBounds = _Bounds;

            AddNewObject(new PF_Player(mWorld, new Vector2(250, 250), 32, ContentManager.Platformer_Character_Smiley, ControllerOne));
            AddNewObject(new PF_Platform_Super(new Vector2(952, 1064),  new Vector2((1936/16), 2), mWorld, Platform_Type_Super.One));
            AddNewObject(new PF_Platform_Super(new Vector2(952, 8),  new Vector2((1936/16), 2), mWorld, Platform_Type_Super.One));
            AddNewObject(new PF_Platform_Super(new Vector2(14, 520),  new Vector2((2), 1064/16), mWorld, Platform_Type_Super.One));
            AddNewObject(new PF_Platform_Super(new Vector2(1904, 520),  new Vector2((2), 1064/16), mWorld, Platform_Type_Super.One));
            AddNewObject(new PF_Platform_Super(new Vector2(528, 900),  new Vector2(528/16, 3), mWorld, Platform_Type_Super.Three));
            AddNewObject(new PF_Platform_Super(new Vector2(1392, 900),  new Vector2(528/16, 3), mWorld, Platform_Type_Super.Three));
            AddNewObject(new PF_Platform_Super(new Vector2(952, 500), new Vector2(528 / 16, 3), mWorld, Platform_Type_Super.Three));
            AddNewObject(new PF_Platform_Super(new Vector2(1252, 750), new Vector2(528 / 16, 3), mWorld, Platform_Type_Super.Three));
            AddNewObject(new PF_Platform_Super(new Vector2(652, 750), new Vector2(528 / 16, 3), mWorld, Platform_Type_Super.Three));
            AddNewObject(new PF_Platform_Super(new Vector2(400, 400), new Vector2(384 / 16, 8), mWorld, Platform_Type_Super.Three));
            AddNewObject(new PF_Platform_Super(new Vector2(1520, 400), new Vector2(384 / 16, 8), mWorld, Platform_Type_Super.Three));

            AddNewObject(new PF_PowerUps_Coin(mWorld, new Vector2(500, 500), new Vector2(32, 32), 0, BodyType.Static));
            AddNewObject(new PF_PowerUps_Coin(mWorld, new Vector2(400, 500), new Vector2(32, 32), 0, BodyType.Dynamic));

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

        private void AddNewObject(PF_GameObj _NewObject)
        {
            mPlatObjects.Add(_NewObject);
            _NewObject.DeleteMe += DeleteObject;
            _NewObject.CreatedObject += AddNewObject;
        }

        private void DeleteObject(PF_GameObj _Sender)
        {
            Console.WriteLine("Deleting - " + _Sender.ToString());
            mPlatObjects.Remove(_Sender);
        }
    }
}