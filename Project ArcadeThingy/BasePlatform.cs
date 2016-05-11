using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    class BasePlatform : MovingObj
    {
        private const int TileSize = 16;
        public BasePlatform(Vector2 _Size, Vector2 _Position, ref World _World) : base(_Size, _Position, ref _World)
        {
            mTexture = ContentManager.ShroomPlatform;
        }

        public override void Draw(SpriteBatch _SB)
        {
            Vector2 tPos = mBody.Body.Position.UnitToPixels() - mBody.Size/2;
            Rectangle tSourceRec = new Rectangle(0, 0, TileSize, TileSize);
            _SB.Draw(mTexture, tPos, tSourceRec, Color.White);
            tSourceRec.X = 32;
            _SB.Draw(mTexture, tPos + new Vector2(Size.X - TileSize, 0), tSourceRec, Color.White);
            tSourceRec.X = 16;
            for (int i = 1; i < (Size.X / TileSize) - 1; i++)
            {
                _SB.Draw(mTexture, tPos + new Vector2(i * TileSize, 0), tSourceRec, Color.White);

            }
        }



    }
}
