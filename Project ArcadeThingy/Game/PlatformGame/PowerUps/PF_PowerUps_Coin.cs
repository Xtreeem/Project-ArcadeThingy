using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    class PF_PowerUps_Coin : PF_PowerUps_Base
    {
        const float MAX_LIFE_TIME = 5.0f;
        public double LifeTime { get; private set; } = MAX_LIFE_TIME;

        public PF_PowerUps_Coin(World _World, Vector2 _Position, Vector2 _Size, double _CollisionImmunityTimer, BodyType _BodyType = BodyType.Static) : base(_World, _Position, _Size, _CollisionImmunityTimer, _BodyType)
        {
        }

        public override void Update(GameTime _GT)
        {
            base.Update(_GT);
            if(mCollisionImmunityTimer <= 0.0 && mBody.BodyType == BodyType.Dynamic)
            {
                LifeTime -= _GT.ElapsedGameTime.TotalSeconds;

                if (LifeTime <= 0.0)
                    DeleteThisObject();
            }
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