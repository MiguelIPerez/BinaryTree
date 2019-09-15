using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeDomain.Interfaces
{
    public interface ITreeBuilder
    {
        INode CreateTreeStructure(IList<int> nodes);

        INode InsertNode(INode root, IList<int> dataList);

        INode InsertNode(INode root, int data);

        INode CreateTreeStructure(int data);

        INode GetClosestCommonAncestor(INode tree, int firstData, int secondData);
    }
}
