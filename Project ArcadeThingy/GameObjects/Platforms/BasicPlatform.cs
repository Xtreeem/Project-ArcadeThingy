using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    class BasicPlatform : BasePlatform
    {
        public BasicPlatform(Vector2 _Size, Vector2 _Position,ref World _World) : base(_Size, _Position,ref _World)
        {
            mBody.Body.BodyType = BodyType.Static;
            mBody.Set_Tex(ContentManager.BasicPlatform);
            mTexture = ContentManager.ShroomPlatform;
        }
    }
}
