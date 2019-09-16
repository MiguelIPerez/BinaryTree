using BinaryTreeDomain.Interfaces;
using System.Threading.Tasks;

namespace BinaryTreeDomain
{
    public class Node : INode
    {
        public int Data { get; private set; }

        public Node LeftChild { get; set; }

        public Node RightChild { get; set; }

        public Node(int data)
        {
            this.Data = data;
        }

        public async Task<INode> GetCommonAncestorAsync(int firstData, int secondData)
        {
            if ((this.IsRoot(firstData, secondData)) ||
                 this.DoesChildHaveDifferentOrientation(firstData, secondData) ||
                 this.IsChildDataEqualToParameters(firstData, secondData))
            {
                return this;
            }
            
            var selectedChild = this.GetChildOrientation(firstData) == ChildOrientation.Left ? this.LeftChild : this.RightChild;
            return await selectedChild.GetCommonAncestorAsync(firstData, secondData);
        }

        internal bool IsChildDataEqualToParameters(int firstData, int secondData)
        {
            return this.RightChild.Data == firstData || this.RightChild.Data == secondData
                   || this.LeftChild.Data == firstData || this.LeftChild.Data == secondData;
        }

        internal bool DoesChildHaveDifferentOrientation(int firstData, int secondData)
        {
            var firstDataOrientation = this.GetChildOrientation(firstData);
            var secondDataOrientation = this.GetChildOrientation(secondData);

            return (firstDataOrientation != secondDataOrientation);
        }

        internal bool IsRoot(int firstData, int secondData)
        {
            return (firstData == this.Data || secondData == this.Data);
        }

        public async Task<bool> InsertNodeAsync(int nodeValue)
        {
            var wasNodeAllocated = false;

            if (nodeValue != this.Data)
            {
                if (nodeValue > this.Data)
                {
                    if (this.RightChild == null)
                    {
                        this.RightChild = new Node(nodeValue);
                        wasNodeAllocated = true;
                    }
                    else
                    {
                        wasNodeAllocated = await this.RightChild.InsertNodeAsync(nodeValue);
                    }
                }
                else
                {
                    if (this.LeftChild == null)
                    {
                        this.LeftChild = new Node(nodeValue);
                        wasNodeAllocated = true;
                    }
                    else
                    {
                        wasNodeAllocated = await this.LeftChild.InsertNodeAsync(nodeValue);
                    }
                }
            }

            return wasNodeAllocated;
        }

        public override string ToString()
        {
            return this.Data.ToString();
        }

        public ChildOrientation GetChildOrientation(int data)
        {
            return data > this.Data ? ChildOrientation.Right : ChildOrientation.Left;
        }

        public async Task<bool> ExistAsync(int data)
        {
            if (this.Data == data)
            {
                return true;
            }
            else if (data > this.Data && this.RightChild != null)
            {
                return await this.RightChild.ExistAsync(data);
            }
            else if (data < this.Data && this.LeftChild != null)
            {
                return await this.LeftChild.ExistAsync(data);
            }
            else
            {
                return false;
            }
        }
    }
}
