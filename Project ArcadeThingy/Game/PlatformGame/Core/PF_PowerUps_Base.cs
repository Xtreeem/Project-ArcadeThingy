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
    abstract class PF_PowerUps_Base : PF_GameObj
    {
        protected double mCollisionImmunityTimer;

        public PF_PowerUps_Base(World _World, Vector2 _Position, Vector2 _Size, double _CollisionImunityTimer = 0.0f, BodyType _BodyType = BodyType.Static)
        {
            mWorld = _World;
            mCollisionImmunityTimer = _CollisionImunityTimer;
            SetUpPhysics(_World, _Position, _Size, _BodyType);
            SetUpTexture();
        }

        public override void Update(GameTime _GT)
        {
            UpdateTimers(_GT);
            base.Update(_GT);
        }

        public virtual void UpdateTimers(GameTime _GT)
        {
            if (mCollisionImmunityTimer > 0)
                mCollisionImmunityTimer -= _GT.ElapsedGameTime.TotalSeconds;
        }

        protected virtual void SetUpPhysics(World _World, Vector2 _Position, Vector2 _Size, BodyType _BodyType = BodyType.Static)
        {
            mBody = new PF_PhysicsBody(_World, _Position, _Size, 1, true, this);
            mBody.BodyType = _BodyType;
            mBody.CollidesWith = Category.Cat1 | Category.Cat4;
            mBody.CollisionCategories = Category.Cat2;
        }
        protected abstract void SetUpTexture();

        public override bool OnCollision(Fixture _Me, Fixture _Other, Contact _C)
        {
            if (mCollisionImmunityTimer <= 0 && _Other.UserData is PF_Player)
            {
                PickUpEffect(_Other.Body.UserData as PF_Character);
            }
            return !(_Other.UserData is PF_Player);
        }

        public abstract void PickUpEffect(PF_Character _Claimant);

    }
}
