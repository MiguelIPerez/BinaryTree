using BinaryTreeDomain;
using BinaryTreeDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinaryTreeAPI.Request
{
    public class TreeStructureRequest
    {
        public Node Node { get; set; }

        public IList<int> DataList { get; set; }

        public int FirstDescendant { get; set; }

        public int SecondDescendant { get; set; }
    }
}
