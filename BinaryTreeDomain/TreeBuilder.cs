using BinaryTreeDomain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BinaryTreeDomain
{
    public class TreeBuilder : ITreeBuilder
    {
        public async Task<INode> CreateTreeStructureAsync(IList<int> nodes)
        {
            INode treeRoot = null;
            if (nodes!= null && nodes.Any())
            {
                treeRoot = this.CreateTreeStructure(nodes.First());
                nodes.RemoveAt(default(int));
                foreach (var nodeItem in nodes)
                {
                    await this.InsertNodeAsync(treeRoot, nodeItem);
                }
            }

            return treeRoot;
        }

        //public INode InsertNode(INode root, IList<int> dataList)
        //{
        //    if (root == null)
        //    {
        //        return this.CreateTreeStructure(dataList);
        //    }
        //    foreach (var data in dataList)
        //    {
        //        this.InsertNode(root, data);
        //    }

        //    return root;
        //}

        public async Task<INode> InsertNodeAsync(INode root, int data)
        {
            if (root == null)
            {
                return new Node(data);
            }

            if (!(await root.ExistAsync(data)))
            {
                await root.InsertNodeAsync(data);
            }
          
            return root;
        }

        public INode CreateTreeStructure(int data = 0)
        {
            return new Node(data);
        }

        public async Task<INode> GetClosestCommonAncestorAsync(INode tree, int firstData, int secondData)
        {
            if (tree != null && await tree.ExistAsync(firstData) && await tree.ExistAsync(secondData))
            {
                return await tree.GetCommonAncestorAsync(firstData, secondData);
            }

            return null;
        }
    }
}
 