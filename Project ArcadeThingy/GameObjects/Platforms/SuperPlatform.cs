using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class SuperPlatform : BasePlatform
    {
        public int Type { get; private set; }
        Rectangle mSrcRec;
        public SuperPlatform(int _Type, Vector2 _Size, Vector2 _Position, ref World _World) : base(_Size, _Position, ref _World)
        {
            Type = _Type;
            mTexture = ContentManager.PlatformSheet;
            mSrcRec = new Rectangle(0, 0, TILE_SIZE, TILE_SIZE);
            switch (_Type)
            {
                case 1:
                    mSrcRec.X = TILE_SIZE * 4;
                    break;
                case 2:
                    mSrcRec.X = TILE_SIZE * 4 * 2;
                    break;
                case 3:
                    mSrcRec.Y = TILE_SIZE * 4;
                    break;
                case 4:
                    mSrcRec.X = TILE_SIZE * 4;
                    mSrcRec.Y = TILE_SIZE * 4;
                    break;
                case 5:
                    mSrcRec.X = TILE_SIZE * 4 * 2;
                    mSrcRec.Y = TILE_SIZE * 4;
                    break;
            }
        }

        public override void Draw(SpriteBatch _SB)
        {
            Vector2 tPos = mBody.Body.Position.UnitToPixels() - mBody.Size / 2;
            Rectangle tSourceRec = mSrcRec;
            for (int x = 0; x < (Size.X / TILE_SIZE); x++)
            {
                if (x == 0)
                    tSourceRec.X = mSrcRec.X + 0;
                else if (x == (Size.X / TILE_SIZE) - 1)
                    tSourceRec.X = mSrcRec.X + 16 * 2;
                else
                    tSourceRec.X = mSrcRec.X + 16;

                for (int y = 0; y < (Size.Y / TILE_SIZE); y++)
                {
                    if (y == 0)
                        tSourceRec.Y = mSrcRec.Y + 0;
                    else if (y >= (Size.Y / TILE_SIZE) - 1)
                        tSourceRec.Y = mSrcRec.Y + 32;
                    else
                        tSourceRec.Y = mSrcRec.Y + 16;

                    if (Size.Y == TILE_SIZE)
                        tSourceRec.Y = mSrcRec.Y + 16 * 3;

                    _SB.Draw(mTexture, tPos + new Vector2(x * TILE_SIZE, y * TILE_SIZE), tSourceRec, Color.White);
                }
            }
        }
    }
}
