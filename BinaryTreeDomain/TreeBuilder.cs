using BinaryTreeDomain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeDomain
{
    public class TreeBuilder : ITreeBuilder
    {
        public INode CreateTreeStructure(IList<int> nodes)
        {
            INode treeRoot = null;
            if (nodes!= null && nodes.Any())
            {
                treeRoot = this.CreateTreeStructure(nodes.First());
                nodes.RemoveAt(default(int));
                foreach (var nodeItem in nodes)
                {
                    treeRoot.InsertNode(nodeItem);
                }
            }

            return treeRoot;
        }

        public INode InsertNode(INode root, IList<int> dataList)
        {
            if (root == null)
            {
                return this.CreateTreeStructure(dataList);
            }
            foreach (var data in dataList)
            {
                this.InsertNode(root, data);
            }

            return root;
        }

        public INode InsertNode(INode root, int data)
        {
            if (root == null)
            {
                return new Node(data);
            }

            if (!root.Exist(data))
            {
                root.InsertNode(data);
            }
            
            return root;
        }

        public INode CreateTreeStructure(int data = 0)
        {
            return new Node(data);
        }

        public INode GetClosestCommonAncestor(INode tree, int firstData, int secondData)
        {
            if (tree != null && tree.Exist(firstData) && tree.Exist(secondData))
            {
                return tree.GetCommonAncestor(firstData, secondData);
            }

            return null;
        }
    }
}
