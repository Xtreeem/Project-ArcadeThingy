using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public static class CollisionManager
    {
        private static QuadTree<GameObj> mQuadTree;
        private static int mQueryBuffert = 50;


        public static void GiveReferences(ref QuadTree<GameObj> _QuadTree)
        {
            mQuadTree = _QuadTree;
        }

        public static bool CheckMyCollision(ref GameObj _Input)
        {
            AABBRectangle TestingRectangle = new AABBRectangle(
                new Rectangle(
                    (int)_Input.Bounds.UpperLeftCorner().X - mQueryBuffert, 
                    (int)_Input.Bounds.UpperLeftCorner().Y - mQueryBuffert, 
                    _Input.Bounds.Width + (mQueryBuffert * 2), 
                    _Input.Bounds.Height + (mQueryBuffert * 2)
                    )
                ,0.0f);

            List<GameObj> list = mQuadTree.Query(TestingRectangle);

            foreach (GameObj GO in list)
            {
                if (GO == _Input) continue;
                if (!GO.HasCollision) continue;
                if (CollsiionCheck(GO, _Input))
                    return true;
            }
            return false;
        }

        private static bool CollsiionCheck(GameObj _ObjOne, GameObj _ObjTwo)
        {

            throw new NotImplementedException();
        }
    }
}
