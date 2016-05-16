using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    public class PF_CoinSpawner : PF_GameObj
    {
        GameTimer mTimer;
        public PF_CoinSpawner(Vector2 _Position, World _World)
        {
            mWorld = _World;
            mBody = new PF_PhysicsBody(_World, _Position, new Vector2(1, 1), 0, true, this);
            mBody.CollisionEnabled = false;
            mTimer = new GameTimer(Utilities.Random.Next(2, 5));
            mTimer.IsLooping = true;
            mTimer.OnFinished = SpawnCoins;
        }

        private void SpawnCoins()
        {
            mTimer.TotalTime = Utilities.Random.Next(5, 10);
            DropCoins(1, 500, 900);
        }

        public override void Update(GameTime _GT)
        {
            mTimer.Update(_GT);
        }
    }
}