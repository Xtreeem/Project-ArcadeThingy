using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;

namespace Project_ArcadeThingy
{
    class TestPower : GameObj
    {
        public TestPower(Vector2 _Size, Vector2 _Pos, ref World _World) : base(_Size, _Pos, ref _World)
        {
            Body.Set_Tex(ContentManager.Dot);
            for (int i = 0; i < mBody.Body.FixtureList.Count; ++i)
                mBody.Body.FixtureList[i].UserData = this;
        }

        public void DoCollision(MovingObj _Obj)
        {
            _Obj.Body.Body.IgnoreGravity = true;
            Console.WriteLine(_Obj.Body.Body.UserData);
        }
    }
}