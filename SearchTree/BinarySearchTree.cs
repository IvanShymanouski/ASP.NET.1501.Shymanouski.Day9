using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTree
{
    public class BinarySearchTree<T> : IEnumerable<T>
    {
        #region fields
        public int Count { get; private set; }

        private class NodeTree
        {
            public T Key { get; set; }

            public NodeTree parent;
            public NodeTree left;
            public NodeTree right;

            public NodeTree(T key)
            {
                Key = key;
            }
        }
        private IComparer<T> comparer;
        private NodeTree root;
        #endregion

        #region ctor
        public BinarySearchTree()
            : this(Comparer<T>.Default)
        {
        }

        public BinarySearchTree(Comparison<T> comparison)
            : this(new ComparerAdapter(comparison))
        {

        }

        public BinarySearchTree(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }
        #endregion

        public bool Search(T item)
        {
            return (SearchNode(item)!=null);
        }

        public void Insert(T key)
        {
            if (Search(key)) throw new ArgumentException("This item alredy exist");

            NodeTree tempNode = root;
            Count++;
            NodeTree node = new NodeTree(key);

           if (InsertSearch(tempNode,node) == null)  root = node;
        }

        public void Delete(T item)
        {
            NodeTree node = SearchNode(item);
            if (node == null) return;
            
            Count--;
            NodeTree takeNode;
            if (node.left == null || node.right == null)
            {
                takeNode = (node.left != null) ? node.left : node.right;
            }
            else
            {
                InsertSearch(node.right, node.left);
                takeNode = node.right;
            }

            if (node == root)
            {
                root = takeNode;
            }
            else
            {
                if (node.parent.left == node) node.parent.left = takeNode;
                else node.parent.right = takeNode;
            }

            if (takeNode != null) takeNode.parent = node.parent;
        }

        /// <exception>InvalidCastException("Tree is null")</exception>
        public T Min()
        {
            if (root == null) throw new InvalidCastException("Tree is null");
            NodeTree tempNode = root;
            while (tempNode.left != null)
            {
                tempNode = tempNode.left;
            }
            return tempNode.Key;
        }
        
        /// <exception>InvalidCastException("Tree is null")</exception>
        public T Max()
        {
            if (root == null) throw new InvalidCastException("Tree is null");
            NodeTree tempNode = root;
            while (tempNode.right != null)
            {
                tempNode = tempNode.right;
            }
            return tempNode.Key;
        }

        #region PreOrderWalk
        public IEnumerable<T> PreOrderWalk()
        {
            foreach (var tempNode in TakeNodePreOrderWalk(root))
            {
             yield return tempNode.Key;
            }
        }

        private IEnumerable<NodeTree> TakeNodePreOrderWalk(NodeTree currentNode)
        {
            if (currentNode != null)
            {

                yield return currentNode;
                foreach (var tempNode in TakeNodePreOrderWalk(currentNode.left))
                {
                    yield return tempNode;
                }
                foreach (var tempNode in TakeNodePreOrderWalk(currentNode.right))
                {
                    yield return tempNode;
                }
            }
        }
        #endregion

        #region InOrderWalk
        public IEnumerable<T> InOrderWalk()
        {
            foreach (var tempNode in TakeNodeInOrderWalk(root))
            {
                yield return tempNode.Key;
            }
        }

        private IEnumerable<NodeTree> TakeNodeInOrderWalk(NodeTree currentNode)
        {
            if (currentNode != null)
            {
                foreach (var tempNode in TakeNodeInOrderWalk(currentNode.left))
                {
                    yield return tempNode;
                }
                yield return currentNode;
                foreach (var tempNode in TakeNodeInOrderWalk(currentNode.right))
                {
                    yield return tempNode;
                }
            }
        }
        #endregion

        #region PostOrderWalk
        public IEnumerable<T> PostOrderWalk()
        {
            foreach (var tempNode in TakeNodePostOrderWalk(root))
            {
                yield return tempNode.Key;
            }
        }

        private IEnumerable<NodeTree> TakeNodePostOrderWalk(NodeTree currentNode)
        {
            if (currentNode != null)
            {
                foreach (var tempNode in TakeNodePostOrderWalk(currentNode.left))
                {
                    yield return tempNode;
                }
                foreach (var tempNode in TakeNodePostOrderWalk(currentNode.right))
                {
                    yield return tempNode;
                }
                yield return currentNode;
            }
        }
        #endregion
      
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderWalk().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private NodeTree SearchNode(T item)
        {
            NodeTree tempNode = root;
            try
            {
                while (tempNode != null && comparer.Compare(tempNode.Key, item) != 0)
                {
                    if (comparer.Compare(tempNode.Key, item) > 0) tempNode = tempNode.left;
                    else tempNode = tempNode.right;
                }
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException("Type not supported default comparer");
            }
            return tempNode;
        }

        //Insert node and return parent of the node
        private NodeTree InsertSearch(NodeTree tempNode, NodeTree node)
        {
            while (tempNode != null)
            {
                if (comparer.Compare(tempNode.Key, node.Key) > 0)
                {
                    if (tempNode.left != null) tempNode = tempNode.left;
                    else
                    {
                        tempNode.left = node;
                        node.parent = tempNode;
                        return tempNode;
                    }
                }
                else
                    if (tempNode.right != null) tempNode = tempNode.right;
                    else
                    {
                        tempNode.right = node;
                        node.parent = tempNode;
                        return tempNode;
                    }
            }

            return tempNode;
        }

        private class ComparerAdapter : IComparer<T>
        {
            Comparison<T> _comparison;

            public ComparerAdapter(Comparison<T> comparison)
            {
                this._comparison = comparison;
            }

            public int Compare(T x, T y)
            {
                return _comparison.Invoke(x, y);
            }
        }
    }

}
