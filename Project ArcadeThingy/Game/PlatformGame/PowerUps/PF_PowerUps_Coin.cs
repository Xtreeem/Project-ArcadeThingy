using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    class PF_PowerUps_Coin : PF_PowerUps_Base
    {
        public PF_PowerUps_Coin(World _World, Vector2 _Position, Vector2 _Size, double _CollisionImmunityTimer, BodyType _BodyType = BodyType.Static) : base(_World, _Position, _Size, _CollisionImmunityTimer, _BodyType)
        {
        }

        public override void PickUpEffect(PF_Character _Claimant)
        {
            mBody.CollisionEnabled = false;
            AudioManager.PlayEffect(SoundEffectName.Pickup_Coin);
            DeleteThisObject();
        }

        protected override void SetUpTexture()
        {
            mTexture = new AnimatedTexture(ContentManager.Platformer_PowerUps_Coin);
            mTexture.AddAnimation(new AnimationDesc(0.07, Vector2.Zero, 32, 32, 8, false, false));
            mTexture.Initialize(0);
        }


    }
}
