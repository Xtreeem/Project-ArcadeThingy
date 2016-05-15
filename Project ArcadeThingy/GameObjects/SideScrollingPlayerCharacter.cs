using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    class SideScrollingPlayerCharacter : SideScrollingCharacter
    {
        public SideScrollingPlayerCharacter(Vector2 _Size, Vector2 _Position,ref World _World, Platform_Controller _Controller) : base(_Size, _Position,ref _World, _Controller)
        {
            mMaxVelocity = new Vector2(150, 150);
            mBody.Set_Tex(ContentManager.BasicCharacter);
            mBody.Body.BodyType = BodyType.Dynamic;
            mBody.Body.FixedRotation = true;
        }
    }
}
