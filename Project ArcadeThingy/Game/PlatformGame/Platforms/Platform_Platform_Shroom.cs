using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    class Platform_Platform_Shroom : Platform_Platform_Base
    {
        public Platform_Platform_Shroom(Vector2 _Position, Vector2 _Size, World _World, Platform_Type_Shroom _Type)
            : base(_Position, _Size, _World, Platform_Type.Shroom)
        {
            mSrcRec.Y = TILE_SIZE * 8 + (int)_Type * TILE_SIZE;
        }
    }
}