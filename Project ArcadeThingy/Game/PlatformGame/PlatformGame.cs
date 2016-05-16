using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    public class PlatformGame
    {
        const int MAX_COINS = 10;

        World mWorld = new World(new Vector2(0, 9.8f));

        PF_PlayerController ControllerOne = new PF_PlayerController(1);
        PF_PlayerController ControllerTwo = new PF_PlayerController(2);
        public Rectangle Bounds { get { return mBounds; } }
        protected Rectangle mBounds;

        public List<PF_GameObj> mObjects = new List<PF_GameObj>();
        public List<PF_GameObj> mSpawners = new List<PF_GameObj>();
        public List<PF_GameObj> mCoins = new List<PF_GameObj>();

        public PlatformGame(Rectangle _Bounds)
        {
            AudioManager.PlaySong(Songs.First);
            mBounds = _Bounds;

            AddRange(FileUtils.GetPlatforms(mWorld));

            AddNewObject(new PF_Player(mWorld, new Vector2(250, 250), 32, ContentManager.GetRandomSmiley(), ControllerOne));
            AddNewObject(new PF_Player(mWorld, new Vector2(550, 250), 32, ContentManager.GetRandomSmiley(), ControllerTwo));
        }

        public void Update(GameTime _GT)
        {
            mWorld.Step((float)_GT.ElapsedGameTime.TotalSeconds);

            for (int i = 0; i < mSpawners.Count; i++)
            {
                if (mCoins.Count < MAX_COINS)
                    mSpawners[i].Update(_GT);
            }

            for (int i = 0; i < mCoins.Count; i++)
                mCoins[i].Update(_GT);

            for (int i = 0; i < mObjects.Count; i++)
                mObjects[i].Update(_GT);
        }

        public void Draw(SpriteBatch _SB)
        {
            for (int i = 0; i < mCoins.Count; i++)
                mCoins[i].Draw(_SB);

            for (int i = 0; i < mObjects.Count; i++)
                mObjects[i].Draw(_SB);
        }

        private void AddRange(List<PF_GameObj> _Objects)
        {
            for (int i = 0; i < _Objects.Count; ++i)
                AddNewObject(_Objects[i]);
        }

        private void AddNewObject(PF_GameObj _NewObject)
        {
            if (_NewObject is PF_CoinSpawner)
                mSpawners.Add(_NewObject);
            else if (_NewObject is PF_PowerUps_Coin)
                mCoins.Add(_NewObject);
            else
                mObjects.Add(_NewObject);

            _NewObject.DeleteMe += DeleteObject;
            _NewObject.CreatedObject += AddNewObject;
        }

        private void DeleteObject(PF_GameObj _Sender)
        {
            Console.WriteLine("Deleting - " + _Sender.ToString());

            if (_Sender is PF_CoinSpawner)
                mSpawners.Remove(_Sender);
            else if (_Sender is PF_PowerUps_Coin)
                mCoins.Remove(_Sender);
            else
                mObjects.Remove(_Sender);
        }
    }
}