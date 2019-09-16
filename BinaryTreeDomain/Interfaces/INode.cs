using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeDomain.Interfaces
{
    public interface INode
    {
        Node LeftChild { get; set; }

        Node RightChild { get; set; }

        INode GetCommonAncestor(int firstData, int secondData);

        int Data { get; }

        bool InsertNode(int nodeValue);

        bool Exist(int data);
    }
}
