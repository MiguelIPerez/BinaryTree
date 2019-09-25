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
            if (nodes == null && !nodes.Any())
            {
                return treeRoot;
            }

            foreach (var itemData in nodes)
            {
                if (treeRoot == null)
                {
                    treeRoot = this.CreateTreeStructure(itemData);
                }
                else
                {
                    await this.InsertNodeAsync(treeRoot, itemData);
                }
            }

            return treeRoot;
        }

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
 