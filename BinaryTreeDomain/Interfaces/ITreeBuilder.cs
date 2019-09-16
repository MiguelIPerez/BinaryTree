using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeDomain.Interfaces
{
    public interface ITreeBuilder
    {
        Task<INode> CreateTreeStructureAsync(IList<int> nodes);

        Task<INode> GetClosestCommonAncestorAsync(INode tree, int firstData, int secondData);
    }
}
