using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    enum Platform_Type
    {
        Super,
        Shroom,
    }

    enum Platform_Type_Shroom
    {
        Yellow,
        Red,
        Blue,
        Green,
        GreenYellow,
    }

    enum Platform_Type_Super
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
    }

    abstract class Platform_Platform_Base : Platform_GameObj
    {
        public const int TILE_SIZE = 16;
        protected Rectangle mSrcRec;
        new Texture2D mTexture;

        public Platform_Platform_Base(Vector2 _Position, Vector2 _Size, World _Word, Platform_Type _Type)
        {
            mBody = new Platform_PhysicsBody(_Word, _Position, _Size * TILE_SIZE, 0.0f);
            mTexture = ContentManager.PlatformSheet;
            mSrcRec = new Rectangle(0, 0, TILE_SIZE, TILE_SIZE);
        }

        public override void Draw(SpriteBatch _SB)
        {
            Vector2 tPos = mBody.Position- mBody.Size / 2;
            Rectangle tSourceRec = mSrcRec;

            int maxX = (int)(mBody.Size.X / TILE_SIZE);
            int maxY = (int)(mBody.Size.Y / TILE_SIZE);

            for (int x = 0; x < maxX; x++)
            {
                if (x == 0)
                    tSourceRec.X = mSrcRec.X;
                else if (x == maxX - 1)
                    tSourceRec.X = mSrcRec.X + TILE_SIZE * 2;
                else
                    tSourceRec.X = mSrcRec.X + TILE_SIZE;

                for (int y = 0; y < maxY; y++)
                {
                    if (y == 0)
                        tSourceRec.Y = mSrcRec.Y + 0;
                    else if (y >= maxY - 1)
                        tSourceRec.Y = mSrcRec.Y + TILE_SIZE * 2;
                    else
                        tSourceRec.Y = mSrcRec.Y + TILE_SIZE;

                    if (mBody.Size.Y == TILE_SIZE)
                        tSourceRec.Y = mSrcRec.Y + TILE_SIZE * 3;

                    _SB.Draw(mTexture, tPos + new Vector2(x * TILE_SIZE, y * TILE_SIZE), tSourceRec, Color.White);
                }
            }
        }
    }
}
