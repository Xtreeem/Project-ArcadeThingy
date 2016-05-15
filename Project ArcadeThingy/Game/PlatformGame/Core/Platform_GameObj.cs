using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    abstract class Platform_GameObj
    {
        public Platform_PhysicsBody Body { get { return mBody; } }
        protected Platform_PhysicsBody mBody;
        protected AnimatedTexture mTexture;

        public virtual void Update(GameTime _GT) { }
        public virtual void Draw(SpriteBatch _SB) { }
        public virtual bool OnCollision(Fixture _Me, Fixture _Other, Contact _C) { return true; }
    }
}
