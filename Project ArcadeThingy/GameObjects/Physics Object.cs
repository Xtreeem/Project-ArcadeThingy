using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    public class Physics_Objetct
    {
        public Vector2 Size { get; protected set; }
        protected World mWorld;
        protected Texture2D mTex;
        protected Vector2 mOrigin;
        protected float mDensity;
        protected Body mBody;
        protected GameObj mOwner;
        public virtual float GetMotorSpeedX { get { return mBody.LinearVelocity.X.UnitToPixels(); } }
        public Body Body { get { return mBody; } }

        public Physics_Objetct(ref World _World, GameObj _Owner, Vector2 _Size, Vector2 _Position, Texture2D _Tex = null, BodyType _BodyType = BodyType.Static, float _Density = 1.0f)
        {
            mWorld = _World;
            mTex = _Tex;
            Size = _Size;
            mDensity = _Density;
            mOwner = _Owner;
            SetUpPhysics(_Position);


        }

        public virtual void SetUpPhysics(Vector2 _Position)
        {
            mBody = BodyFactory.CreateRectangle(mWorld, Size.X.PixelToUnit(), Size.Y.PixelToUnit(), mDensity, _Position.PixelsToUnits());
            mBody.Restitution = 0.3f;
            mBody.Friction = 0.5f;
            mBody.UserData = mOwner;
        }

        public void SetUpCircle(Vector2 _Position)
        {
            mBody = BodyFactory.CreateCircle(mWorld, Size.X.PixelToUnit() / 2, mDensity, _Position.PixelsToUnits());
            mBody.Restitution = 0.4f;
            mBody.Friction = 0.25f;
            mBody.UserData = mOwner;
            mBody.BodyType = BodyType.Dynamic;
        }

        public void Set_Tex(Texture2D _Input)
        {
            mTex = _Input;
            mOrigin = new Vector2(mTex.Width / 2, mTex.Height / 2);
        }

        public void Draw(SpriteBatch _SB)
        {
            if (mTex == null) return;
            _SB.Draw(mTex, new Rectangle(Body.Position.UnitToPixels().ToPoint(), Size.ToPoint()), null, Color.White, Body.Rotation, mOrigin, SpriteEffects.None, 1.0f);


            //Vector2 tPos = new Vector2((mBody.Position.X.UnitToPixels()), (mBody.Position.Y.UnitToPixels()));
            //Vector2 tScale = new Vector2(Size.X / (float)ContentManager.BasicPlatform.Width, Size.Y / (float)ContentManager.BasicPlatform.Height);
            //Vector2 tOrigin = new Vector2(ContentManager.BasicPlatform.Width / 2, ContentManager.BasicPlatform.Height / 2);
            //_SB.Draw(ContentManager.BasicPlatform, tPos, null, Color.White, mBody.Rotation, tOrigin, tScale, SpriteEffects.None, 1.0f);
        }

    }
}
