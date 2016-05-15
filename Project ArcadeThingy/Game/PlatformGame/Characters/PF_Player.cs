using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    class PF_Player : PF_Character
    {
        public PF_Player(World _World, Vector2 _Position, float _Radius, Texture2D _Texture, PF_Controller _Controller) : base(_Controller, _World)
        {
            //Setup Texture
            mTexture = new AnimatedTexture(_Texture);
            mTexture.AddAnimation(new AnimationDesc(0.0, Vector2.Zero, 32, 32, 1));
            mTexture.Initialize(0);

            //Setup Physical Body
            mBody = new PF_PhysicsBody(_World, _Position, new Vector2(_Radius, _Radius), 1.0f, true, this);
            mBody.BodyType = BodyType.Dynamic;
            mBody.CollidesWith = Category.All;
            mBody.CollisionCategories = Category.Cat4;
        }
    }
}
