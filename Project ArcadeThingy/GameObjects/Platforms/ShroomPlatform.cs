using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    enum ShroomType
    {
        Yellow,
        Red,
        Blue,
        Green,
        GreenYellow,
    }

    class ShroomPlatform : BasePlatform
    {
        public ShroomType Type { get; private set; }
        Rectangle mSrcRec;
        public ShroomPlatform(ShroomType _Type, Vector2 _Size, Vector2 _Position, ref World _World) : base(_Size, _Position, ref _World)
        {
            Type = _Type;
            mTexture = ContentManager.ShroomPlatform;
            mSrcRec = new Rectangle(0, 0, TILE_SIZE, TILE_SIZE);
            mSrcRec.Y = (int)_Type * TILE_SIZE;
        }

        public override void Draw(SpriteBatch _SB)
        {
            Vector2 tPos = mBody.Body.Position.UnitToPixels() - mBody.Size / 2;
            mSrcRec.X = 0;
            _SB.Draw(mTexture, tPos, mSrcRec, Color.White);
            mSrcRec.X = 32;
            _SB.Draw(mTexture, tPos + new Vector2(Size.X - TILE_SIZE, 0), mSrcRec, Color.White);
            mSrcRec.X = 16;
            for (int i = 1; i < (Size.X / TILE_SIZE) - 1; i++)
            {
                _SB.Draw(mTexture, tPos + new Vector2(i * TILE_SIZE, 0), mSrcRec, Color.White);
            }
        }
    }
}