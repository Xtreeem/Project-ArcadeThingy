using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project_ArcadeThingy
{
    public delegate void DeleteThis(PF_GameObj _Sender);
    public delegate void CreateObject(PF_GameObj _NewObject);
    public abstract class PF_GameObj
    {
        public event DeleteThis DeleteMe;
        public event CreateObject CreatedObject;
        public PF_PhysicsBody Body { get { return mBody; } }
        protected PF_PhysicsBody mBody;
        protected AnimatedTexture mTexture;
        protected Color mColor = Color.White;
        protected World mWorld;

        public virtual void Update(GameTime _GT)
        {
            if (mTexture != null)
                mTexture.Update(_GT);
        }
        public virtual void Draw(SpriteBatch _SB)
        {
            if (mTexture != null)
                mTexture.Draw(_SB, mBody.GetDrawRectangle(), mBody.IsUserDataNull ? Color.Red : mColor);

        }

        protected void DeleteThisObject()
        {
            if (DeleteMe != null)
                DeleteMe(this);
        }

        protected void CreatedNewObject(PF_GameObj _NewObject)
        {
            if (CreatedObject != null)
                CreatedObject(_NewObject);
        }

        protected void DropCoins(int _Amount, float _Intensity, float _Diviation, double _CollisionImmunityTimer = 0.0, bool _UniformVelocity = false)
        {
            PF_PowerUps_Coin tCoin;
            Vector2 tDirection = Vector2.Zero;
            float tIntensity = Utilities.Random.NextFloat(_Intensity - _Diviation, _Intensity + _Diviation);
            for (int i = 0; i < _Amount; i++)
            {
                tCoin = new PF_PowerUps_Coin(mWorld, mBody.Position, new Vector2(32, 32), _CollisionImmunityTimer, BodyType.Dynamic);
                tCoin.Body.Restitution = 0.8f;
                CreatedObject(tCoin);
                if (i % 2 == 0)
                {
                    tDirection = (new Vector2(Utilities.Random.NextFloat(-1, 1), Utilities.Random.NextFloat(-1, 1)));
                    tCoin.mBody.LinearVelocity = tDirection * tIntensity;
                }
                else
                {
                    tDirection.X *= -1;
                    tCoin.mBody.LinearVelocity = tDirection * tIntensity;
                }
                if (!_UniformVelocity)
                    tIntensity = Utilities.Random.NextFloat(_Intensity - _Diviation, _Intensity + _Diviation);
            }
        }

        public virtual bool OnCollision(Fixture _Me, Fixture _Other, Contact _C) { return true; }
    }
}
