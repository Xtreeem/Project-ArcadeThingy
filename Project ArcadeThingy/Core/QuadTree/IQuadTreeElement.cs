using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public interface IQuadTreeElement
    {
        AABBRectangle QuadBox { get; }
        QuadTreeNode<GameObj> Node { get; set; }
    }
}
