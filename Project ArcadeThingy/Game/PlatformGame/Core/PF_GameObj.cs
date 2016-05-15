using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project_ArcadeThingy
{
    public abstract class PF_GameObj
    {
        public event Action DeleteMe;
        public PF_PhysicsBody Body { get { return mBody; } }
        protected PF_PhysicsBody mBody;
        protected AnimatedTexture mTexture;
        protected Color mColor = Color.White;

        public virtual void Update(GameTime _GT)
        {
            if (mTexture != null)
                mTexture.Update(_GT);
        }
        public virtual void Draw(SpriteBatch _SB)
        {
            if (mTexture != null)
                mTexture.Draw(_SB, mBody.GetDrawRectangle(), mColor);
        }
        public virtual bool OnCollision(Fixture _Me, Fixture _Other, Contact _C) { return true; }
    }
}
