using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    public class PF_Platform_Super : PF_Platform_Base
    {
        public Platform_Type_Super Type { get; private set; }
        public PF_Platform_Super(Vector2 _Position, Vector2 _Size, World _World, Platform_Type_Super _Type)
            : base(_Position, _Size, _World, Platform_Type.Super)
        {
            Type = _Type;
            switch (_Type)
            {
                case Platform_Type_Super.Two:
                    mSrcRec.X = TILE_SIZE * 4;
                    break;
                case Platform_Type_Super.Three:
                    mSrcRec.X = TILE_SIZE * 4 * 2;
                    break;
                case Platform_Type_Super.Four:
                    mSrcRec.Y = TILE_SIZE * 4;
                    break;
                case Platform_Type_Super.Five:
                    mSrcRec.X = TILE_SIZE * 4;
                    mSrcRec.Y = TILE_SIZE * 4;
                    break;
                case Platform_Type_Super.Six:
                    mSrcRec.X = TILE_SIZE * 4 * 2;
                    mSrcRec.Y = TILE_SIZE * 4;
                    break;
            }
        }
    }
}