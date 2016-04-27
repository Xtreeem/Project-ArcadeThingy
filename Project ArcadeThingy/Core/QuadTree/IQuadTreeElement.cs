using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public interface IQuadTreeElement
    {
        AABBRectangle Hitbox { get; }
        QuadTreeNode<IQuadTreeElement> Node { get; set; }
    }
}
