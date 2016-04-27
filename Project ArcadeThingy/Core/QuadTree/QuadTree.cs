using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public class QuadTree<T> where T : IQuadTreeElement
    {
        /// <summary>
        /// The root QuadTreeNode
        /// </summary>
        QuadTreeNode<T> m_root;

        /// <summary>
        /// The bounds of this QuadTree
        /// </summary>
        AABBRectangle m_rectangle;

        /// <summary>
        /// An delegate that performs an action on a QuadTreeNode
        /// </summary>
        /// <param name="obj"></param>
        public delegate void QTAction(QuadTreeNode<T> obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        public QuadTree(AABBRectangle rectangle)
        {
            m_rectangle = rectangle;
            m_root = new QuadTreeNode<T>(m_rectangle, null);
        }

        /// <summary>
        /// Get the count of items in the QuadTree
        /// </summary>
        public int Count { get { return m_root.Count; } }

        /// <summary>
        /// Insert the feature into the QuadTree
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            m_root.Insert(item);
        }

        /// <summary>
        /// Query the QuadTree, returning the items that are in the given area
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public List<T> Query(AABBRectangle _area)
        {
            return m_root.Query(_area);
        }

        /// <summary>
        /// Do the specified action for each item in the quadtree
        /// </summary>
        /// <param name="action"></param>
        public void ForEach(QTAction action)
        {
            m_root.ForEach(action);
        }

        public void DrawQuads(SpriteBatch _SB)
        {
            Random Rand = new Random();
            m_root.Draw(_SB, Rand);

        }
    }
}

