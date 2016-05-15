using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    public class PF_Platform_Shroom : PF_Platform_Base
    {
        public Platform_Type_Shroom Type { get; private set; }
        public PF_Platform_Shroom(Vector2 _Position, Vector2 _Size, World _World, Platform_Type_Shroom _Type)
            : base(_Position, _Size, _World, Platform_Type.Shroom)
        {
            Type = _Type;
            mSrcRec.Y = TILE_SIZE * 8 + (int)_Type * TILE_SIZE;
        }
    }
}