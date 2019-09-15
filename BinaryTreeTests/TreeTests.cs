using BinaryTreeDomain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeTests
{
    [TestClass]
    public class TreeTests
    {
        public Tree Tree { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            Tree = new Tree();
        }

        [TestCleanup]
        public void TearDown()
        {
            Tree = null;
        }

        [TestMethod]
        public void CreateTreeStructure_GivenNoArguments_ShouldCreateRootNodeWithDefaultIntData()
        {
            // Act
            var rootNode = Tree.CreateTreeStructure();

            // Assert
            Assert.IsNotNull(rootNode);
            Assert.AreEqual(rootNode.Data, default(int));
        }

        [TestMethod]
        public void CreateTreeStructure_GivenAListOfData_ShouldCreateTreeStructure()
        {
            var dataList = new List<int>();
            var random = new Random();
            foreach (var item in Enumerable.Range(0, 20))
            {
                dataList.Add(random.Next(0, 20));
            }

            var rootNode = Tree.CreateTreeStructure(dataList);
            Assert.IsNotNull(rootNode);
        }

        [TestMethod]
        public void Insert_GivenOneNonExistingDataInTheTree_ShouldReturnTheNodeWithTheDataInserted()
        {
            var rootNode = new Node(21);
            Tree.InsertNode(rootNode, 58);
            Assert.IsNotNull(rootNode.RightChild);
            Assert.AreEqual(rootNode.RightChild.Data, 58);
        }

        [TestMethod]
        public void Insert_GivenOneExistingDataInTheTree_ShouldReturnTheNodeWithoutInsertingTheDuplicatedData()
        {
            var rootNode = new Node(21);
            Tree.InsertNode(rootNode, 58);
            Tree.InsertNode(rootNode, 21);
            Tree.InsertNode(rootNode, 20);

            Assert.AreEqual(rootNode.RightChild.Data, 58);
            Assert.AreEqual(rootNode.LeftChild.Data, 20);

            Assert.IsNull(rootNode.RightChild.RightChild);
            Assert.IsNull(rootNode.RightChild.LeftChild);

            Assert.IsNull(rootNode.LeftChild.RightChild);
            Assert.IsNull(rootNode.LeftChild.LeftChild);
        }

        [TestMethod]
        public void Insert_GivenRootNodeIsNull_ShouldCreateTheNodeWithGivenDataAsARoot()
        {
            var rootNode = Tree.InsertNode(null, 85);
            Assert.IsNotNull(rootNode);
            Assert.AreEqual(rootNode.Data, 85);
        }

        [TestMethod]
        public void Insert_GivenAListOfData_ShouldCreateTreeStructure()
        {
            var rootNode = new Node(25);
            Tree.InsertNode(rootNode, new List<int> { 20, 39, 32, 7, 42, 37, 10 });

            Assert.AreEqual(rootNode.LeftChild.Data, 20);
            Assert.AreEqual(rootNode.RightChild.Data, 39);

            Assert.AreEqual(rootNode.LeftChild.LeftChild.Data, 7);
            Assert.IsNull(rootNode.LeftChild.RightChild);

            Assert.AreEqual(rootNode.LeftChild.LeftChild.RightChild.Data, 10);
            Assert.IsNull(rootNode.LeftChild.LeftChild.LeftChild);

            Assert.AreEqual(rootNode.RightChild.LeftChild.Data, 32);
            Assert.AreEqual(rootNode.RightChild.RightChild.Data, 42);

            Assert.AreEqual(rootNode.RightChild.LeftChild.RightChild.Data, 37);
            Assert.IsNull(rootNode.RightChild.LeftChild.LeftChild);

            Assert.IsNull(rootNode.RightChild.RightChild.RightChild);
            Assert.IsNull(rootNode.RightChild.RightChild.LeftChild);
        }

        [TestMethod]
        public void Insert_GivenNullRootAndAListOfData_ShouldCreateTreeStructureAccordingly()
        {
            var rootNode = Tree.InsertNode(null, new List<int> { 25, 20, 39, 32, 7, 42, 37, 10 });

            Assert.IsNotNull(rootNode);
            Assert.AreEqual(rootNode.Data, 25);

            Assert.AreEqual(rootNode.LeftChild.Data, 20);
            Assert.AreEqual(rootNode.RightChild.Data, 39);

            Assert.AreEqual(rootNode.LeftChild.LeftChild.Data, 7);
            Assert.IsNull(rootNode.LeftChild.RightChild);

            Assert.AreEqual(rootNode.LeftChild.LeftChild.RightChild.Data, 10);
            Assert.IsNull(rootNode.LeftChild.LeftChild.LeftChild);

            Assert.AreEqual(rootNode.RightChild.LeftChild.Data, 32);
            Assert.AreEqual(rootNode.RightChild.RightChild.Data, 42);

            Assert.AreEqual(rootNode.RightChild.LeftChild.RightChild.Data, 37);
            Assert.IsNull(rootNode.RightChild.LeftChild.LeftChild);

            Assert.IsNull(rootNode.RightChild.RightChild.RightChild);
            Assert.IsNull(rootNode.RightChild.RightChild.LeftChild);
        }

        [TestMethod]
        public void Insert_GivenRootNodeNullAndNullList_ShouldReturnNull()
        {
            var rootNode = Tree.InsertNode(null, null);

            Assert.IsNull(rootNode);
        }

        [TestMethod]
        public void GetClosestCommonAncestor_GivenTwoExistingNodes_ShouldReturnAncestor()
        {
            var rootNode = Tree.CreateTreeStructure(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87});
            var ancestor = Tree.GetClosestCommonAncestor(rootNode, 29, 44);
            var ancestor2 = Tree.GetClosestCommonAncestor(rootNode, 44, 85);
            var ancestor3 = Tree.GetClosestCommonAncestor(rootNode, 83, 87);

            Assert.IsNotNull(ancestor);
            Assert.IsNotNull(ancestor2);
            Assert.IsNotNull(ancestor3);

            Assert.AreEqual(ancestor.Data, 39);
            Assert.AreEqual(ancestor2.Data, 67);
            Assert.AreEqual(ancestor3.Data, 85);
        }

        [TestMethod ]
        public void GetClosestCommonAncestor_GivenDataDoesNotExistOnTheTree_ShouldReturnNullAncestor()
        {
            var rootNode = Tree.CreateTreeStructure(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87 });
            var ancestor = Tree.GetClosestCommonAncestor(rootNode, 19, 44);
            var ancestor2 = Tree.GetClosestCommonAncestor(rootNode, 44, 60);
            var ancestor3 = Tree.GetClosestCommonAncestor(rootNode, 19, 60);

            Assert.IsNull(ancestor);
            Assert.IsNull(ancestor2);
            Assert.IsNull(ancestor3);
        }

        [TestMethod]
        public void GetClosestCommonAncestor_GivenNullTree_ShouldReturnNullAncestor()
        {
            var ancestor = Tree.GetClosestCommonAncestor(null, 19, 58);

            Assert.IsNull(ancestor);
        }

        [TestMethod]
        public void GetClosestCommonAncestor_GivenParametersAreTheSame_ShouldReturnAncestor()
        {
            var rootNode = Tree.CreateTreeStructure(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87 });
            var ancestor = Tree.GetClosestCommonAncestor(rootNode, 83, 83);

            Assert.IsNotNull(ancestor);
            Assert.AreEqual(ancestor.Data, 85);
        }

        [TestMethod]
        public void GetClosestCommonAncestor_GivenParameterIsRoot_ShouldReturnRootAsCommonAncestor()
        {
            var rootNode = Tree.CreateTreeStructure(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87 });
            var ancestor = Tree.GetClosestCommonAncestor(rootNode, 83, 67);

            Assert.IsNotNull(ancestor);
            Assert.AreEqual(ancestor.Data, 67);
        }

        [TestMethod]
        public void GetClosestCommonAncestor_GivenOneParameterIsAncestorOfTheOther_ShouldReturnTheParentOfTheAncestorParameterAsCommon()
        {
            var rootNode = Tree.CreateTreeStructure(new List<int> { 67, 39, 76, 28, 44, 29, 74, 85, 83, 87 });
            var ancestor = Tree.GetClosestCommonAncestor(rootNode, 85, 87);

            Assert.IsNotNull(ancestor);
            Assert.AreEqual(ancestor.Data, 76);
        }
    }
}
