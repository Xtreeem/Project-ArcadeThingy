using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Project_ArcadeThingy
{
    class Character : MovingObj
    {
        Controller mController;
        public Character(Vector2 _Size, Vector2 _Position,ref World _World, Controller _Controller) : base(_Size, _Position,ref _World)
        {
            mController = _Controller;
            mController.Set_Pawn(this);

        }

        public virtual void HandleMovementInput(MovementInput _Input, float _intensity)
        {

        }

        public virtual void Jump(float _Intensity)
        {
            mBody.Body.ApplyForce(new Vector2(0, _Intensity));
        }

        public virtual void ApplyMovemventInput(Vector2 _Input)
        {
            AddVelocity(_Input);
        }
    }
}
