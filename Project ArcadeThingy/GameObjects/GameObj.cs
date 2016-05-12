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
    public class GameObj : IQuadTreeElement
    {
        //Quad Tree
        public AABBRectangle QuadBox { get { return mQuadBox; } }
        public QuadTreeNode<GameObj> Node { get; set; }
        protected AABBRectangle mQuadBox;

        //Physics
        public Physics_Objetct Body { get { return mBody; } }
        protected Physics_Objetct mBody;

        //Misc
        protected Color mColor;
        protected Texture2D mTexture;
        protected Vector2 Size;
        protected SpriteEffects mEffect;


        public GameObj(Vector2 _Size, Vector2 _Position,ref World _World)
        {
            Size = _Size;
            mBody = new Physics_Objetct(ref _World, this, _Size, _Position);
            mQuadBox = new AABBRectangle(new Rectangle(mBody.Body.Position.UnitToPixels().ToPoint(), mBody.Size.ToPoint()), mBody.Body.Rotation);
        }

        //public bool mOnCollision(Fixture _FixtureOne, Fixture _FixtureTwo, Contact _Contact)
        //{
        //    return true;
        //}

        public virtual void Draw(SpriteBatch _SB)
        {
            mBody.Draw(_SB);
        }

        public virtual void Update(GameTime _GT)
        {

        }



    }
}
