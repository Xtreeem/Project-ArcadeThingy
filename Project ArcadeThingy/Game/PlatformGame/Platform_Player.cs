﻿using System;
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
    class Platform_Player : Platform_Character
    {
        public Platform_Player(World _World, Vector2 _Position, float _Radius, Texture2D _Texture)
        {
            //Setup Texture
            mTexture = new AnimatedTexture(_Texture);
            mTexture.AddAnimation(new AnimationDesc(0.0, Vector2.Zero, 32, 32, 1));
            mTexture.Initialize(0);

            //Setup Physical Body
            mBody = new Platform_PhysicsBody(_World, _Position, new Vector2(_Radius, _Radius), 1.0f, true, this);
            mBody.BodyType = BodyType.Dynamic;
        }

        public override void Draw(SpriteBatch _SB)
        {
            mTexture.Draw(_SB, mBody.GetDrawRectangle(), Color.White);
        }




        public override bool OnCollision(Fixture _Me, Fixture _Other, Contact _C)
        {
            throw new NotImplementedException();
        }
    }
}