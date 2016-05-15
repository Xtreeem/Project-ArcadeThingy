using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_ArcadeThingy
{
    public abstract class PF_GameObj
    {
        public PF_PhysicsBody Body { get { return mBody; } }
        protected PF_PhysicsBody mBody;
        protected AnimatedTexture mTexture;

        public virtual void Update(GameTime _GT) { }
        public virtual void Draw(SpriteBatch _SB) { }
        public virtual bool OnCollision(Fixture _Me, Fixture _Other, Contact _C) { return true; }
    }
}
