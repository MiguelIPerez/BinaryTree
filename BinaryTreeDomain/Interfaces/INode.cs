using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeDomain.Interfaces
{
    public interface INode
    {
        Node LeftChild { get; set; }

        Node RightChild { get; set; }

        Task<INode> GetCommonAncestorAsync(int firstData, int secondData);

        int Data { get; }

        Task<bool> InsertNodeAsync(int nodeValue);

        Task<bool> ExistAsync(int data);
    }
}
